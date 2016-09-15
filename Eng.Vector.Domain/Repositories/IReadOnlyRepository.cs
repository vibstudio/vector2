using Eng.Aggregation;
using Eng.Vector.Domain.Model.Transfer;

namespace Eng.Vector.Domain.Repositories
{
    public interface IReadOnlyRepository<out T, in TId> : IAggregateRoot
    {
        T Find(TId id);
    }
}