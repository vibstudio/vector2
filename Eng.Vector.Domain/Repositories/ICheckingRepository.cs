using System;

namespace Eng.Vector.Domain.Repositories
{
    public interface ICheckingRepository : IDisposable
    {
        bool DbIsAvailable { get; }

        /// <summary>
        /// Determina se il repository è "in salute", cioè se le procedure richiamate in esso esistono e sono fruibili.
        /// </summary>
        bool IsHealthy { get; }
    }
}