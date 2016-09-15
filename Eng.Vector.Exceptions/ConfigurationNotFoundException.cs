using Eng.Vector.Globalization;

namespace Eng.Vector.Exceptions
{
    public class ConfigurationNotFoundException : ManagedException
    {
        public ConfigurationNotFoundException(string configurationName)
            : base(Labeling.Factory.Get.ConfigurationNotFound(configurationName).ToString())
        {
        }
    }
}