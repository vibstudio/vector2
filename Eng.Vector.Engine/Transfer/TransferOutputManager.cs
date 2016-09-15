#region Namespaces

using System;
using System.IO;

using Eng.Diagnostic;
using Eng.IO;
using Eng.IO.Compression;
using Eng.IO.Path;
using Eng.Vector.Domain;
using Eng.Vector.Domain.Abstractions;
using Eng.Vector.Domain.Model;
using Eng.Vector.Domain.Model.Integration;
using Eng.Vector.Domain.Model.Transfer;
using Eng.Vector.Domain.Repositories;
using Eng.Vector.Util;

#endregion

namespace Eng.Vector.Engine.Transfer
{
    public class TransferOutputManager : IRunnableTransferOutput
    {
        #region Fields

        private readonly Type _classType = typeof(TransferOutputManager);

        #endregion

        #region Ctor(s)

        private TransferOutputManager()
        {
        }

        #endregion

        #region Factory

        public static TransferOutputManager Factory
        {
            get { return new TransferOutputManager(); }
        }

        #endregion

        #region IRunnableTransferOutput implementation

        public TransferOutputResultCollection Run(Eis eis)
        {
            TransferOutputResultCollection resultCollection = new TransferOutputResultCollection();

            const string METHOD_NAME = "Run`1";
            int tracePosition = 0;

            try
            {
                using (ITransferOutputRepository repository = Helper.Factory.Of.Vector.DBConnection.GeTransferOutputRepository(eis.ID))
                {
                    tracePosition = 1;

                    TransferOutputMessage message = repository.Find(eis.ID);

                    tracePosition = 2;

                    Trace.Write.DebugFormat("EisID: {0}", eis.ID);

                    if (message.IsPerformed)
                    {
                        tracePosition = 3;

                        foreach (TransferMessageFile file in message.Files)
                        {
                            FilePath filePath = new FilePath(Path.Combine(eis.Output.DirectoryInfo.RootPath, file.Name));

                            TransferFileInfo fileInfo = eis.Output.FileInfo;

                            TransferNetworkCredentials networkCredentials = eis.NetworkAccessInfo.Credentials;
                            if (networkCredentials.ExecuteImpersonation)
                            {
                                bool authenticated = NetworkShareAccessProvider.ImpersonateUser(networkCredentials.UserName,
                                                                                                networkCredentials.Domain,
                                                                                                networkCredentials.Password);
                                if (authenticated)
                                {
                                    WriteFile(file, fileInfo, filePath, repository);
                                    NetworkShareAccessProvider.UndoImpersonation();
                                }
                                else
                                {
                                    Trace.Write.Error(NetworkShareAccessProvider.GetLastError(), _classType, METHOD_NAME);
                                }
                            }
                            else
                            {
                                WriteFile(file, fileInfo, filePath, repository);
                            }

                            resultCollection.Add(OperationResult<string>.CreateSuccess(eis.ID));
                        }

                        tracePosition = 4;
                    }
                }
            }
            catch (Exception exception)
            {
                Trace.Write.Error(exception, _classType, "Run", tracePosition);
                resultCollection.Add(OperationResult<string>.CreateFailure(eis.ID, exception.Message));
            }

            return resultCollection;
        }

        public TransferOutputResultCollection Run()
        {
            TransferOutputResultCollection resultCollection = new TransferOutputResultCollection();

            EisConfig eisConfig = Helper.Factory.Of.Vector.EisConfiguration.EisConfig;

            foreach (Eis eis in eisConfig)
            {
                resultCollection.AddRange(Run(eis));
            }

            return resultCollection;
        }

        #endregion

        #region Methods

        private void WriteFile(TransferMessageFile file, TransferFileInfo fileInfo, FilePath filePath, ITransferOutputRepository repository)
        {
            try
            {
                // Se Zip-DB = true e Zip-Config = true => trasferire direttamente il file nella cartella di output
                if (file.IsZipped && fileInfo.IsZipped)
                {
                    file.Content.Write(filePath);
                }

                // Se Zip-DB = true e Zip-Config = false => decomprimere il file prima di trasferirlo nella cartella di output
                if (file.IsZipped && !fileInfo.IsZipped)
                {
                    file.Content.Write(filePath, CompressionMode.Decompress);
                }

                // Se Zip-DB = false e Zip-Config = true => comprimere il file prima di trasferirlo nella cartella di output
                if (!file.IsZipped && fileInfo.IsZipped)
                {
                    file.Content.Write(filePath, CompressionMode.Compress, file.Name);
                }

                // Se Zip-DB = false e Zip-Config = false => trasferire direttamente il file nella cartella di output
                if (!file.IsZipped && !fileInfo.IsZipped)
                {
                    file.Content.Write(filePath);
                }

                // In un futuro quando VECTOR sarà usato anche in altri processi (fuori da MDM) questo potrebbe essere un limite
                // TODO: Studiare il modo di passare una collezione al DB per far eseguire un aggiornamento massivo post-trasferimento
                repository.NotifyTransferState(new TransferMessageFile { TransferPerformed = true, ID = file.ID });
            }
            catch (Exception exception)
            {
                int position = 1;

                Trace.Write.Error(exception, _classType, "WriteFile", position);

                try
                {
                    repository.NotifyTransferState(new TransferMessageFile { TransferPerformed = false, Message = exception.Message, ID = file.ID });
                }
                catch (Exception exception2)
                {
                    position = 2;

                    Trace.Write.Error(exception2, _classType, "WriteFile", position);
                }
            }
        }

        #endregion
    }
}