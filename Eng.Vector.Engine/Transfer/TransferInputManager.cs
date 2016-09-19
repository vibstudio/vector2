#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

using Eng.Collections;
using Eng.Diagnostic;
using Eng.IO;
using Eng.IO.Path;
using Eng.Vector.Domain.Abstractions;
using Eng.Vector.Domain.Model;
using Eng.Vector.Domain.Model.Integration;
using Eng.Vector.Domain.Model.Transfer;
using Eng.Vector.Domain.Repositories;
using Eng.Vector.Exceptions;
using Eng.Vector.Util;

#endregion

namespace Eng.Vector.Engine.Transfer
{
    public class TransferInputManager : IRunnableTransferInput
    {
        #region Fields

        private readonly Type _classType = typeof(TransferInputManager);

        #endregion

        #region Ctor(s)

        private TransferInputManager()
        {
        }

        #endregion

        #region Factory

        public static TransferInputManager Factory
        {
            get { return new TransferInputManager(); }
        }

        #endregion

        #region IRunnableTransferInput implementation

        public void RunAsync(IEnumerable<TransferFilePath> files)
        {
            const string METHOD_NAME = "RunAsync";

            int debugPosition = 0;

            Trace.Write.Debug("START", _classType, METHOD_NAME, debugPosition++);

            ElaborationKey key = ElaborationKey.CreateKey();
            Trace.Write.Debug(string.Format("key.ID: {0}", key.ID), _classType, METHOD_NAME, debugPosition++);

            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            Trace.Write.Debug(string.Format("User's Windows logon name: {0}", identity.Name), _classType, METHOD_NAME, debugPosition++);
            Trace.Write.Debug(string.Format("{0} is {1}authenticated", identity.Name, identity.IsAuthenticated ? "" : "not "), _classType, METHOD_NAME, debugPosition++);

            TransferConcurrentDispatcher.Factory.AddFiles(key, files);
            Trace.Write.Debug("Add {0} files", _classType, METHOD_NAME, debugPosition);
            
            Task.Factory.StartNew(() => Run(key));
        }

        public OperationResultCollection<TransferFilePath> Run(IEnumerable<TransferFilePath> files)
        {
            OperationResultCollection<TransferFilePath> result = new OperationResultCollection<TransferFilePath>();

            result.AddRange(files.Select(Run));

            return result;
        }

        public OperationResult<TransferFilePath> Run(TransferFilePath filePath)
        {
            const string METHOD_NAME = "Run(TransferFilePath filePath)";

            int tracePosition = 0;
            int debugPosition = 1;
            string fileCode = string.Empty;

            try
            {
                Trace.Write.Debug(string.Format("File name: {0}", filePath.FileName), _classType, METHOD_NAME, debugPosition++);

                string eisID = filePath.Specialization.EisID;
                Trace.Write.Debug(filePath.FileName, _classType, METHOD_NAME, debugPosition++);

                ITransferInputRepository repository = Helper.Factory.Of.Vector.DBConnection.GetTransferInputRepository(eisID);
                Trace.Write.Debug("ITransferInputRepository initialized", _classType, METHOD_NAME, debugPosition++);

                EisConfig eisConfig = Helper.Factory.Of.Vector.EisConfiguration.EisConfig;
                Trace.Write.Debug("EisConfig retrieved", _classType, METHOD_NAME, debugPosition++);

                TransferNetworkCredentials networkCredentials = eisConfig.GetNetworkAccessInfo(eisID).Credentials;
                Trace.Write.Debug(string.Format("EisID: {0}, {1}", eisID, networkCredentials));

                TransferDirectoryInfo directoryInfo = eisConfig.GetDirectoryInfo(eisID, TransferDirection.Input);
                Trace.Write.Debug("TransferDirectoryInfo retrieved", _classType, METHOD_NAME, debugPosition++);

                TransferFileInfo fileInfo = eisConfig.GetFileInfo(eisID, TransferDirection.Input);
                Trace.Write.Debug("TransferFileInfo retrieved", _classType, METHOD_NAME, debugPosition++);

                try
                {
                    /*---------------------------------*/
                    /* 1. FILE CODE RECOVERY
                    /*---------------------------------*/
                    tracePosition = 1;

                    try
                    {
                        fileCode = fileInfo.RulesParser.ExtractRule(filePath.FileName);
                        Trace.Write.Debug(string.Format("File code: {0}", fileCode), _classType, METHOD_NAME, debugPosition++);
                    }
                    catch (NotRecognizedRulesException exception)
                    {
                        Trace.Write.Error(exception, _classType, METHOD_NAME, tracePosition);
                        throw;
                    }

                    /*---------------------------------*/
                    /* 2. VALIDATION
                    /*---------------------------------*/
                    tracePosition = 2;

                    Trace.Write.Debug("Validation start", _classType, METHOD_NAME, debugPosition++);

                    foreach (IFileValidation validator in fileInfo.Validators)
                    {
                        validator.Validate(filePath);
                    }

                    Trace.Write.Debug("Validation stop", _classType, METHOD_NAME, debugPosition++);
                    
                    /*---------------------------------*/
                    /* 3. PERSISTENCE
                    /*---------------------------------*/
                    tracePosition = 3;

                    TransferMessageFile file = new TransferMessageFile
                                               {
                                                   EisID = eisID,
                                                   Code = fileCode,
                                                   Content = filePath.ToBytes(fileInfo.IsZipped),
                                                   Name = filePath.FileName(fileInfo.IsZipped),
                                                   IsZipped = fileInfo.IsZipped
                                               };

                    Trace.Write.Debug(file.ToString(), _classType, METHOD_NAME, debugPosition);
                
                    repository.Add(file);

                    /*---------------------------------*/
                    /* 4. DUMPING
                    /*---------------------------------*/
                    tracePosition = 4;

                    Helper.Factory.Of.Vector.FileMoving.Dump(networkCredentials, directoryInfo, filePath);

                    /*---------------------------------*/
                    /* 5. RETURN
                    /*---------------------------------*/
                    tracePosition = 5;

                    return OperationResult<TransferFilePath>.CreateSuccess(filePath);
                }
                catch (ManagedException exception)
                {
                    Trace.Write.Error(exception, _classType, METHOD_NAME, tracePosition);

                    if (exception.IsManaged)
                    {
                        try
                        {
                            tracePosition = 6;

                            TransferMessageFile rejectedFile = GetRejectedFile(filePath, eisID, fileCode, exception);
                            Helper.Factory.Of.Vector.FileMoving.Reject(networkCredentials,
                                                                       directoryInfo,
                                                                       filePath,
                                                                       () => repository.NotifyTransferState(rejectedFile));
                        }
                        catch (Exception exception2)
                        {
                            Trace.Write.Error(exception2, _classType, METHOD_NAME, tracePosition);

                            Helper.Factory.Of.Vector.FileMoving.Reject(networkCredentials, directoryInfo, filePath);
                        }
                    }

                    return OperationResult<TransferFilePath>.CreateFailure(exception.Message, filePath);
                }
                catch (Exception exception)
                {
                    Trace.Write.Error(exception, _classType, METHOD_NAME, tracePosition);

                    try
                    {
                        tracePosition = 7;

                        TransferMessageFile rejectedFile = GetRejectedFile(filePath, eisID, fileCode, exception);
                        Helper.Factory.Of.Vector.FileMoving.Reject(networkCredentials,
                                                                   directoryInfo,
                                                                   filePath,
                                                                   () => repository.NotifyTransferState(rejectedFile));
                    }
                    catch (Exception exception2)
                    {
                        Trace.Write.Error(exception2, _classType, METHOD_NAME, tracePosition);

                        Helper.Factory.Of.Vector.FileMoving.Reject(networkCredentials, directoryInfo, filePath);
                    }

                    return OperationResult<TransferFilePath>.CreateFailure(exception.Message, filePath);
                }
                finally
                {
                    tracePosition = 8;
                    repository.Dispose();
                }
            }
            catch (Exception exception)
            {
                Trace.Write.Error(exception, _classType, METHOD_NAME, tracePosition);

                return OperationResult<TransferFilePath>.CreateFailure(exception.Message, filePath);
            }
        }

        #endregion

        #region Methods

        private void Run(ElaborationKey key)
        {
            while (true)
            {
                try
                {
                    Run(TransferConcurrentDispatcher.Factory.GetFile(key));
                }
                catch
                {
                    break;
                }
            }
        }

        private static TransferMessageFile GetRejectedFile(TransferFilePath filePath, string eisID, string fileCode, Exception exception)
        {
            return new TransferMessageFile
                   {
                       EisID = eisID,
                       Name = filePath.FileName,
                       Code = fileCode,
                       Message = exception.Message
                   };
        }

        #endregion
    }
}
