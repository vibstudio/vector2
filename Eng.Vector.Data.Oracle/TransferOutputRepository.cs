#region Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using Eng.Data;
using Eng.Data.Oracle;
using Eng.Diagnostic;
using Eng.Vector.Domain.Model.Transfer;
using Eng.Vector.Domain.Repositories;
using Eng.Vector.Exceptions;

using Microsoft.Practices.Unity.Utility;

#endregion

namespace Eng.Vector.Data.Oracle
{
    public class TransferOutputRepository : RepositoryBase, ITransferOutputRepository
    {
        #region Fields

        private readonly Type _classType = typeof(TransferOutputRepository);

        #endregion

        #region Ctor(s)

        public TransferOutputRepository(IDbConnectionParameters connectionParameters)
            : base(connectionParameters)
        {
        }

        public TransferOutputRepository(string connectionString = null)
            : base(connectionString)
        {
        }

        public TransferOutputRepository()
            : this(string.Empty)
        {
        }

        #endregion

        #region ITransferOutputRepository implementation

        #region Find

        public TransferOutputMessage Find(string eisID)
        {
            Guard.ArgumentNotNull(eisID, "eisID");

            try
            {
                using (DbCommand command = Dac.CreateStoredProcedureCommand("INF_GESTIONE_TRASF_FILE.TRASFERIMENTIFILEGETOUT", FindParameters))
                {
                    ResetInputParameters(command);

                    // Input
                    Dac.SetStringParameter(command, "PSIE_COD", eisID);

                    // Output
                    List<TransferMessageFile> files = Dac.ExecuteReaderCmd(command, ReadFilesFromReader);
                    string outcome = Dac.GetStringParameter(command, "PESITO");
                    string message = Dac.GetStringParameter(command, "PMSGERR");

                    if (string.Equals(outcome, "KO", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Trace.Write.Error(string.Format("PESITO: {0}", outcome), _classType, "Find", 1);
                        Trace.Write.Error(string.Format("PMSGERR: {0}", message), _classType, "Find", 1);

                        throw new DBProcessingFailedException(message);
                    }

                    return new TransferOutputMessage(files.ToArray());
                }
            }
            catch (DBProcessingFailedException)
            {
                throw;
            }
            catch (Exception exception)
            {
                Trace.Write.Error(exception, _classType, "Find");
                throw new DBProcessingFailedException(exception.Message, false);
            }
        }

        private List<DbParameter> FindParameters(string storedName)
        {
            return new List<DbParameter>(4)
                   {
                       // Input
                       Dac.CreateParameter("PSIE_COD", OracleDbType.Varchar2, ParameterDirection.InputOutput),
                       // Output
                       Dac.CreateParameter("PREFCURSORELENCOFILE", OracleDbType.RefCursor, ParameterDirection.Output, 0),
                       Dac.CreateParameter("PESITO", OracleDbType.Varchar2, ParameterDirection.Output, 4000),
                       Dac.CreateParameter("PMSGERR", OracleDbType.Varchar2, ParameterDirection.Output, 4000)
                   };
        }

        private List<TransferMessageFile> ReadFilesFromReader(DbDataReader reader, DbCommand command)
        {
            List<TransferMessageFile> list = new List<TransferMessageFile>();

            while (reader.Read())
            {
                TransferMessageFile file = new TransferMessageFile
                                           {
                                               ID = Dac.GetStringFromReader(reader, "ITRF_TRASFERIMENTO_FILE_ID", command),
                                               Name = Dac.GetStringFromReader(reader, "ITRF_NOMEFILE", command),
                                               Content = Dac.GetBytesFromReader(reader, "ITRF_CONTENUTO", command),
                                               IsZipped = string.Equals(Dac.GetStringFromReader(reader, "ITRF_COMPRESSO", command), "S", StringComparison.CurrentCultureIgnoreCase)
                                           };

                list.Add(file);
            }

            return list;
        }

        #endregion

        #region NotifyTransferState

        public OperationResult NotifyTransferState(TransferMessageFile file)
        {
            Guard.ArgumentNotNull(file, "file");
        
            try
            {
                using (DbCommand command = Dac.CreateStoredProcedureCommand("INF_GESTIONE_TRASF_FILE.TRASFERIMENTIFILEOUTUPDSTATO", NotifyTransferStateParameters))
                {
                    ResetInputParameters(command);
        
                    Dac.SetDecimalParameter(command, "PTRASFERIMENTO_FILE_ID", Convert.ToDecimal(file.ID));
                    Dac.SetStringParameter(command, "PESITO", file.TransferPerformed ? "OK" : "KO");
                    Dac.SetStringParameter(command, "PMSGLOG", file.Message);
        
                    Dac.ExecuteNonQueryCmd(command);
        
                    string failureMessage = Dac.GetStringParameter(command, "PMSGERR");
        
                    if (string.IsNullOrEmpty(failureMessage)) return OperationResult.CreateSuccess();
        
                    Trace.Write.Error(failureMessage, _classType, "NotifyTransferState");
                    return OperationResult.CreateFailure(failureMessage);
                }
            }
            catch (Exception exception)
            {
                Trace.Write.Error(exception, _classType, "NotifyTransferState");
                return OperationResult.CreateFailure(exception.Message);
            }
        }
        
        private List<DbParameter> NotifyTransferStateParameters(string storedName)
        {
            return new List<DbParameter>(4)
                   {
                       // Input
                       Dac.CreateParameter("PTRASFERIMENTO_FILE_ID", OracleDbType.Decimal, ParameterDirection.InputOutput),
                       Dac.CreateParameter("PESITO", OracleDbType.Varchar2, ParameterDirection.InputOutput),
                       Dac.CreateParameter("PMSGLOG", OracleDbType.Varchar2, ParameterDirection.InputOutput),
                       // Output
                       Dac.CreateParameter("PMSGERR", OracleDbType.Varchar2, ParameterDirection.Output, 4000)
                   };
        }

        #endregion

        #region IsHealthy

        public override bool IsHealthy
        {
            get { return GetHealth("INF_GESTIONE_TRASF_FILE", "TRASFERIMENTIFILEGETOUT"); }
        }

        #endregion

        #endregion
    }
}
