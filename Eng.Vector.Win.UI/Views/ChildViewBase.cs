namespace Eng.Vector.Win.UI.Views
{
    #region Namespaces

    using System.Drawing;
    using System.Windows.Forms;

    using Eng.Vector.Win.UI.Common;

    #endregion

    public class ChildViewBase : UserControl, IChildView
    {
        public virtual Size ContentSize
        {
            get { return new Size(920, 580); }
        }
    }
}