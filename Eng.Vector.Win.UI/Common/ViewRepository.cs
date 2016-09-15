namespace Eng.Vector.Win.UI.Common
{
    public class ViewRepository
    {
        private static readonly IViewFactory get;

        public static IViewFactory Get
        {
            get { return get; }
        }

        static ViewRepository()
        {
            get = GetFactory();
        }

        private static IViewFactory GetFactory()
        {
            return new ViewFactory();
        }
    }
}
