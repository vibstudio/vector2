using Eng.Vector.Globalization;

namespace Eng.Vector.Exceptions
{
    public class DumpFailedException : ManagedException
    {
        public DumpFailedException(string fileName, string message)
            : base(Labeling.Factory.Get.DumpFailed(fileName, message).ToString())
        {
        }
    }
}