#region Namespaces

using System.Collections.Generic;

using Eng.Vector.Globalization;

#endregion

namespace Eng.Vector.Exceptions
{
    public class ValidationFailedException : ManagedException
    {
        public ValidationFailedException(string error)
            : this(new[] { error })
        {
        }

        public ValidationFailedException(IList<string> errors)
            : base(Labeling.Factory.Get.ValidationFailed(errors).ToString())
        {
        }
    }
}