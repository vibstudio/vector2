using Eng.Vector.Domain.Model.Transfer;

namespace Eng.Vector.Domain.Repositories
{
    public interface ITransferOutputRepository : IReadOnlyRepository<TransferOutputMessage, string>, ICheckingRepository, ITransferRepository
    {
    }
}