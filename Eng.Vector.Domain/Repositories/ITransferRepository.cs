using Eng.Vector.Domain.Model.Transfer;

namespace Eng.Vector.Domain.Repositories
{
    public interface ITransferRepository
    {
        OperationResult NotifyTransferState(TransferMessageFile file);
    }
}