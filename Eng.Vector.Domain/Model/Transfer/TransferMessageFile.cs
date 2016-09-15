using Eng.Aggregation;

namespace Eng.Vector.Domain.Model.Transfer
{
    public class TransferMessageFile : IAggregateRoot
    {
        #region Properties

        public string ID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public byte[] Content { get; set; }

        public bool IsZipped { get; set; }

        public string EisID { get; set; }

        public bool TransferPerformed { get; set; }

        public string Message { get; set; }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return string.Format("FileID: {0}\nName: {1}\nCode: {2}\nIsZipped: {3}\nEisID: {4}", ID, Name, Code, IsZipped, EisID);
        }

        #endregion
    }
}