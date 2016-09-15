using Eng.Vector.Globalization;

namespace Eng.Vector.Exceptions
{
    public class TextualValidationFailedException : ManagedException
    {
        public TextualValidationFailedException()
            : base(Labeling.Factory.Get.TextualValidationFailed.ToString())
        {
        }
    }
}