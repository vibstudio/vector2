namespace Eng.Vector.Util
{
    #region System helper factories

    public interface ISystemHelperFactory
    {
        IVectorHelperFactory Vector { get; }

        IVectorDOCHelperFactory VectorDOC { get; }
    }

    internal class SystemHelperFactory : ISystemHelperFactory
    {
        public IVectorHelperFactory Vector
        {
            get { return new VectorHelperFactory(); }
        }

        public IVectorDOCHelperFactory VectorDOC
        {
            get { return new VectorDOCHelperFactory(); }
        }
    }

    #region Vector factory

    public interface IVectorHelperFactory
    {
        IDBConnectionHelper DBConnection { get; }

        IAppConfigurationHelper AppConfiguration { get; }

        IEisConfigurationHelper EisConfiguration { get; }

        IFileMovingHelper FileMoving { get; }

        IRepositoryHelper Repository { get; }
    }

    internal class VectorHelperFactory : IVectorHelperFactory
    {
        public IDBConnectionHelper DBConnection
        {
            get { return new DBConnectionHelper(); }
        }

        public IAppConfigurationHelper AppConfiguration
        {
            get { return new AppConfigurationHelper(); }
        }

        public IEisConfigurationHelper EisConfiguration
        {
            get { return new EisConfigurationHelper(); }
        }

        public IFileMovingHelper FileMoving
        {
            get { return new FileMovingHelper(); }
        }

        public IRepositoryHelper Repository
        {
            get { return new RepositoryHelper(); }
        }
    }

    #endregion

    #region VectorDOC factory

    public interface IVectorDOCHelperFactory
    {
    }

    internal class VectorDOCHelperFactory : IVectorDOCHelperFactory
    {
    }

    #endregion

    #endregion

    public sealed class Helper
    {
        public static Helper Factory
        {
            get { return new Helper(); }
        }

        private Helper()
        {
        }

        public ISystemHelperFactory Of
        {
            get { return new SystemHelperFactory(); }
        }
    }
}