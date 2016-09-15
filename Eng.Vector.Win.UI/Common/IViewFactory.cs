namespace Eng.Vector.Win.UI.Common
{
    public interface IViewFactory
    {
        IChildView Eis { get; }

        IChildView System { get; }

        IChildView WindowsService { get; }
    }
}
