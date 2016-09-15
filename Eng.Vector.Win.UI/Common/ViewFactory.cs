using Eng.Vector.Win.UI.Views;

namespace Eng.Vector.Win.UI.Common
{
    public class ViewFactory : IViewFactory
    {
        public IChildView Eis
        {
            get { return new EisView(); }
        }

        public IChildView System
        {
            get { return new SystemView(); }
        }

        public IChildView WindowsService
        {
            get { return new WindowsServiceView(); }
        }
    }
}
