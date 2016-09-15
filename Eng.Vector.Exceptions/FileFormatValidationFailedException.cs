using Eng.Vector.Globalization;

namespace Eng.Vector.Exceptions
{
    public class FileFormatValidationFailedException : ManagedException
    {
        public FileFormatValidationFailedException(string fileExtension)
            : base(Labeling.Factory.Get.FileFormatValidationFailed(fileExtension).ToString())
        {
        }

        public FileFormatValidationFailedException(string[] extensions)
            : base(Labeling.Factory.Get.FileFormatValidationFailed(extensions).ToString())
        {
        }
    }
}