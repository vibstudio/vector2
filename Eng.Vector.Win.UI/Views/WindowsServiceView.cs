namespace Eng.Vector.Win.UI.Views
{
    #region Namespaces

    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using Eng.Vector.Util;

    #endregion

    public partial class WindowsServiceView : ChildViewBase
    {
        #region Ctor(s)

        public WindowsServiceView()
        {
            InitializeComponent();

            windowsServiceDashboard.Bind(Helper.Factory.Of.Vector.AppConfiguration.ServiceName);
        }

        #endregion
        
        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Dock = DockStyle.Fill;
        }

        public override Size ContentSize
        {
            get { return new Size(375, 156); }
        }

        #endregion
    }
}