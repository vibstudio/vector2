#region Namespaces

using System;

using Eng.Diagnostic;
using Eng.Vector.Domain.Repositories;

#endregion

namespace Eng.Vector.Util
{
    public interface IRepositoryHelper
    {
        bool CanConnect { get; }

        bool IsHealthy { get; }
    }

    internal class RepositoryHelper : IRepositoryHelper
    {
        public bool CanConnect
        {
            get
            {
                try
                {
                    using (ICheckingRepository repository = Helper.Factory.Of.Vector.DBConnection.GetCheckingRepository())
                    {
                        return repository.DbIsAvailable;
                    }
                }
                catch (Exception exception)
                {
                    Trace.Write.Error(exception, typeof(RepositoryHelper), "CanConnect");
                    return false;
                }
            }
        }

        public bool IsHealthy
        {
            get
            {
                try
                {
                    using (ICheckingRepository repository = Helper.Factory.Of.Vector.DBConnection.GetCheckingRepository())
                    {
                        return repository.IsHealthy;
                    }
                }
                catch (Exception exception)
                {
                    Trace.Write.Error(exception, typeof(RepositoryHelper), "IsHealthy");
                    return false;
                }
            }
        }
    }
}
