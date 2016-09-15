using Eng.Aggregation;

namespace Eng.Vector.Domain.Model.Transfer
{
    public class TransferOutputMessage : OperationResult, IAggregateRoot
    {
        public TransferOutputMessage(TransferMessageFile[] files)
            : this(true, "", files)
        {
        }

        public TransferOutputMessage(bool isPerformed, string message = "", TransferMessageFile[] files = null)
            : base(isPerformed, message)
        {
            Files = files;
        }

        public TransferMessageFile[] Files { get; set; }
    }
}
