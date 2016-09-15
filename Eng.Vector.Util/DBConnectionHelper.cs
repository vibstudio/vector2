#region Namespaces

using Eng.Data;
using Eng.Vector.Domain.Repositories;
using Eng.Vector.IoC;

#endregion

namespace Eng.Vector.Util
{
    public interface IDBConnectionHelper
    {
        ICheckingRepository GetCheckingRepository();

        IDbConnectionParameters GetDbConnectionParameters(string eisID);

        ITransferInputRepository GetTransferInputRepository();
        ITransferInputRepository GetTransferInputRepository(string eisID);

        ITransferOutputRepository GeTransferOutputRepository();
        ITransferOutputRepository GeTransferOutputRepository(string eisID);
    }

    internal class DBConnectionHelper : IDBConnectionHelper
    {
        public ICheckingRepository GetCheckingRepository()
        {
            return Container.Build(EisConfigurationHelper.EisConfigurationFilePath, "db")
                            .Resolve<ICheckingRepository>();
        }

        public IDbConnectionParameters GetDbConnectionParameters(string eisID)
        {
            // In caso di mancata parametrizzazione il container restituisce un'istanza della classe DefaultDbConnectionParameters
            return Container.Build(EisConfigurationHelper.EisConfigurationFilePath, eisID.ToLower(), "db")
                            .Resolve<IDbConnectionParameters, DefaultDbConnectionParameters>();
        }

        public ITransferInputRepository GetTransferInputRepository(string eisID)
        {
            return Container.Build(EisConfigurationHelper.EisConfigurationFilePath, eisID.ToLower(), "db")
                            .Resolve<ITransferInputRepository>();
        }

        public ITransferInputRepository GetTransferInputRepository()
        {
            return Container.Build(EisConfigurationHelper.EisConfigurationFilePath, "db")
                            .Resolve<ITransferInputRepository>();
        }

        public ITransferOutputRepository GeTransferOutputRepository(string eisID)
        {
            return Container.Build(EisConfigurationHelper.EisConfigurationFilePath, eisID.ToLower(), "db")
                            .Resolve<ITransferOutputRepository>();
        }

        public ITransferOutputRepository GeTransferOutputRepository()
        {
            return Container.Build(EisConfigurationHelper.EisConfigurationFilePath, "db")
                            .Resolve<ITransferOutputRepository>();
        }
    }
}
