using Eng.Aggregation;

namespace Eng.Vector.Domain.Repositories
{
    public interface IRepository<out T, in TId> : IAggregateRoot
    {
        /// <summary>
        /// Aggiunge un oggetto di un deterinato tipo.
        /// </summary>
        /// <param name="item">L'oggetto da inserire.</param>
        /// <returns>Il risultato dell'operazone.</returns>
        T Add(TId item);
    }
}
