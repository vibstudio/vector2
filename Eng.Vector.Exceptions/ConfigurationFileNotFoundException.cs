#region Namespaces

using Eng.Vector.Domain.Model;
using Eng.Vector.Globalization;

#endregion

namespace Eng.Vector.Exceptions
{
    public class ConfigurationFileNotFoundException : ManagedException
    {
        public ConfigurationFileNotFoundException(ConfigurationScope scope)
            : base(Labeling.Factory.Get.ConfigurationFileNotFound(scope).ToString())
        {
        }
    }
}