namespace Eng.Vector.Domain.Model
{
    using Eng.IO.Path;

    public class TransferFilePath : SpecializedFilePath<TransferID>
    {
        public TransferFilePath(string path)
            : base(path)
        {
        }
    }
}
