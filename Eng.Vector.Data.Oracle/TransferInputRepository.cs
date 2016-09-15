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
    public class TransferInputRepository : RepositoryBase, ITransferInputRepository
    {
        #region Fields

        private readonly Type _classType = typeof(TransferInputRepository);

        #endregion
        
        #region Ctor(s)

        public TransferInputRepository(IDbConnectionParameters connectionParameters)
            : base(connectionParameters)
        {
        }

        public TransferInputRepository(string connectionString = null)
            : base(connectionString)
        {
        }

        public TransferInputRepository()
            : this(string.Empty)
        {
        }

        #endregion

        #region ITransferInputRepository implementation

        #region Add

        public OperationResult Add(TransferMessageFile file)
        {
            Guard.ArgumentNotNull(file, "file");

            try
            {
                using (DbCommand command = Dac.CreateStoredProcedureCommand("INF_GESTIONE_TRASF_FILE.TRASFERIMENTIFILEINSERTTRASP",
                                                                            TrasferimentiFileInsertTraspParameters))
                {
                    ResetInputParameters(command);

                    Dac.SetStringParameter(command, "PSIE_COD", file.EisID);
                    Dac.SetStringParameter(command, "PNOMEFILE", file.Name);
                    Dac.SetStringParameter(command, "PCODFILE", file.Code);
                    Dac.SetLobParameter(command, "PCONTENUTO", file.Content);
                    Dac.SetStringParameter(command, "PCOMPRESSO", file.IsZipped ? "S" : "N");

                    Dac.BeginTransaction();

                    Dac.ExecuteNonQueryCmd(command);

                    string outcome = Dac.GetStringParameter(command, "PESITO");
                    string message = Dac.GetStringParameter(command, "PMSGERR");

                    if (String.Equals(outcome, "KO", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Trace.Write.Error(string.Format("PESITO: {0}", outcome), _classType, "Add", 1);
                        Trace.Write.Error(string.Format("PMSGERR: {0}", message), _classType, "Add", 1);

                        Dac.Rollback();

                        throw new DBProcessingFailedException(file.Name, message);
                    }

                    Dac.Commit();
                    
                    return OperationResult.CreateSuccess();
                }
            }
            catch (DBProcessingFailedException)
            {
                throw;
            }
            catch (Exception exception)
            {
                Trace.Write.Error(exception, _classType, "Add");
                throw new DBProcessingFailedException(file.Name, exception.Message, false);
            }
        }

        private List<DbParameter> TrasferimentiFileInsertTraspParameters(string storedName)
        {
            return new List<DbParameter>(7)
                   {
                       // Input
                       Dac.CreateParameter("PSIE_COD", OracleDbType.Varchar2, ParameterDirection.InputOutput),
                       Dac.CreateParameter("PNOMEFILE", OracleDbType.Varchar2, ParameterDirection.InputOutput),
                       Dac.CreateParameter("PCODFILE", OracleDbType.Varchar2, ParameterDirection.InputOutput),
                       Dac.CreateParameter("PCONTENUTO", OracleDbType.Blob, ParameterDirection.InputOutput),
                       Dac.CreateParameter("PCOMPRESSO", OracleDbType.Varchar2, ParameterDirection.InputOutput),
                       // Output
                       Dac.CreateParameter("PESITO", OracleDbType.Varchar2, ParameterDirection.Output, 4000),
                       Dac.CreateParameter("PMSGERR", OracleDbType.Varchar2, ParameterDirection.Output, 4000)
                   };
        }

        #endregion

        #region NotifyTransferState

        public OperationResult NotifyTransferState(TransferMessageFile file)
        {
            Guard.ArgumentNotNull(file, "file");

            try
            {
                using (DbCommand command = Dac.CreateStoredProcedureCommand("INF_GESTIONE_TRASF_FILE.TRASFERIMENTIFILEINBLACKLIST", TrasferimentiFileInBlacklistParameters))
                {
                    ResetInputParameters(command);

                    Dac.SetStringParameter(command, "PSIE_COD", file.EisID);
                    Dac.SetStringParameter(command, "PNOMEFILE", file.Name);
                    Dac.SetStringParameter(command, "PCODFILE", file.Code);
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

        private List<DbParameter> TrasferimentiFileInBlacklistParameters(string storedName)
        {
            return new List<DbParameter>(6)
                   {
                       // Input
                       Dac.CreateParameter("PSIE_COD", OracleDbType.Varchar2, ParameterDirection.InputOutput),
                       Dac.CreateParameter("PNOMEFILE", OracleDbType.Varchar2, ParameterDirection.InputOutput),
                       Dac.CreateParameter("PCODFILE", OracleDbType.Varchar2, ParameterDirection.InputOutput),
                       Dac.CreateParameter("PMSGLOG", OracleDbType.Varchar2, ParameterDirection.InputOutput),
                       // Output
                       Dac.CreateParameter("PESITO", OracleDbType.Varchar2, ParameterDirection.Output, 4000),
                       Dac.CreateParameter("PMSGERR", OracleDbType.Varchar2, ParameterDirection.Output, 4000)
                   };
        }

        #endregion
        
        #region IsHealthy

        public override bool IsHealthy
        {
            get { return GetHealth("INF_GESTIONE_TRASF_FILE", "TRASFERIMENTIFILEINSERTTRASP"); }
        }

        #endregion

        #endregion
    }
}
