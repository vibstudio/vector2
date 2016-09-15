using Eng.Vector.Globalization;

namespace Eng.Vector.Exceptions
{
    public class EisConfigurationParsingFailedException : ManagedException
    {
        public EisConfigurationParsingFailedException(string xpath)
            : base(Labeling.Factory.Get.EisConfigurationParsingFailed(xpath).ToString())
        {
        }
    }
}