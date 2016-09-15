using Eng.Vector.Globalization;

namespace Eng.Vector.Exceptions
{
    public class DBProcessingFailedException : ManagedException
    {
        private readonly bool _isManaged;

        public DBProcessingFailedException(string message, bool isManaged = true)
            : base(message)
        {
            _isManaged = isManaged;
        }

        public DBProcessingFailedException(string fileName, string message, bool isManaged = true)
            : this(Labeling.Factory.Get.DBProcessingFailed(fileName, message).ToString(), isManaged)
        {
        }

        public override bool IsManaged
        {
            get { return _isManaged; }
        }
    }
}