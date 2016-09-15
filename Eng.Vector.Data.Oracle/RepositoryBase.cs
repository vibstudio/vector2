#region Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using Eng.Data;
using Eng.Data.Oracle;
using Eng.Data.Oracle.Types;
using Eng.Diagnostic;
using Eng.Vector.Domain.Repositories;

#endregion

namespace Eng.Vector.Data.Oracle
{
    public abstract class RepositoryBase : ICheckingRepository
    {
        /// <summary>
        /// Determina se il repository è "in salute", cioè se le procedure richiamate in esso esistono e sono fruibili.
        /// </summary>
        public abstract bool IsHealthy { get; }

        #region Fields

        private bool _disposed;

        private OracleDataAccessComponent _dac;

        #endregion

        #region Ctor(s)

        protected RepositoryBase(IDbConnectionParameters connectionParameters)
        {
            _dac = new OracleDataAccessComponent(connectionParameters != null ? connectionParameters.ConnectionString : "");
        }

        protected RepositoryBase(string connectionString = null)
        {
            _dac = new OracleDataAccessComponent(connectionString);
        }

        #endregion

        #region Properties

        public bool DbIsAvailable
        {
            get { return _dac.DbIsAvailable; }
        }

        protected OracleDataAccessComponent Dac
        {
            get { return _dac ?? (_dac = new OracleDataAccessComponent()); }
        }

        #endregion

        #region Disposable implementation

        ~RepositoryBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_dac != null) _dac.Dispose();
            }

            _disposed = true;
        }

        #endregion

        #region Methods

        protected static void ResetInputParameters(DbCommand cmd)
        {
            foreach (DbParameter p in cmd.Parameters)
            {
                if (p.Direction == ParameterDirection.Input || p.Direction == ParameterDirection.InputOutput)
                {
                    p.Value = DBNull.Value;
                }
            }
        }

        protected bool GetHealth(string objectName, string procedureName)
        {
            string sql = string.Format("select * from all_procedures where lower(object_name) = '{0}' and lower(procedure_name) = '{1}'",
                                       objectName.ToLower(),
                                       procedureName.ToLower());

            try
            {
                using (DbCommand command = Dac.CreateCommand(sql))
                {
                    try
                    {
                        List<Procedure> list = Dac.ExecuteReaderCmd(command, ProcedureList);
                        if (list != null && list.Count > 0) return true;
                    }
                    catch (Exception exception)
                    {
                        Trace.Write.Error(exception, typeof(RepositoryBase), "GetHealth");
                        return false;
                    }
                }
            }
            finally
            {
                Dac.Dispose();
            }

            return false;
        }

        private List<Procedure> ProcedureList(DbDataReader reader, DbCommand command)
        {
            var list = new List<Procedure>();

            while (reader.Read())
            {
                var procedure =
                    new Procedure
                    {
                        Aggregate = Dac.GetStringFromReader(reader, "AGGREGATE", command),
                        AuthID = Dac.GetStringFromReader(reader, "AUTHID", command),
                        Deterministic = Dac.GetStringFromReader(reader, "DETERMINISTIC", command),
                        ImplTypeName = Dac.GetStringFromReader(reader, "IMPLTYPENAME", command),
                        ImplTypeOwner = Dac.GetStringFromReader(reader, "IMPLTYPEOWNER", command),
                        Interface = Dac.GetStringFromReader(reader, "INTERFACE", command),
                        ObjectName = Dac.GetStringFromReader(reader, "OBJECT_NAME", command),
                        Owner = Dac.GetStringFromReader(reader, "OWNER", command),
                        Parallel = Dac.GetStringFromReader(reader, "PARALLEL", command),
                        PipeLined = Dac.GetStringFromReader(reader, "PIPELINED", command),
                        ProcedureName = Dac.GetStringFromReader(reader, "PROCEDURE_NAME", command)
                    };

                list.Add(procedure);
            }

            return list;
        }

        #endregion
    }
}