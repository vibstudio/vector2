using Eng.Vector.Domain.Model.Transfer;

namespace Eng.Vector.Domain.Repositories
{
    public interface ITransferInputRepository : IRepository<OperationResult, TransferMessageFile>, ICheckingRepository, ITransferRepository
    {
    }
}
