#region Namespaces

using System;
using System.Drawing;
using System.Windows.Forms;

using Eng.Environment;
using Eng.Vector.Globalization;
using Eng.Vector.Util;
using Eng.Vector.Win.UI.Common;
using Eng.Windows.Forms;

#endregion

namespace Eng.Vector.Win.UI
{
    public partial class MainForm : Form
    {
        #region Constants

        private const int OLD_OS_RESIZING_FACTOR = 11;

        #endregion

        #region Ctor(s)

        public MainForm()
        {
            InitializeComponent();

            InitializeLabels();

            SetChildView(eisToolStripButton);
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            menuToolStrip.Renderer = new ToolStripEngRender(Helper.Factory.Of.Vector.AppConfiguration.GUISkin);
        }

        #region Event handlers

        private void eisToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            SetChildView(eisToolStripButton);
        }

        private void systemToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            SetChildView(systemToolStripButton);
        }

        private void windowsServiceToolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            SetChildView(windowsServiceToolStripButton);
        }

        #endregion

        #region Methods

        private void InitializeLabels()
        {
            eisToolStripButton.Text = Labeling.Factory.Get.Eis(true).ToString();
            systemToolStripButton.Text = Labeling.Factory.Get.System.ToString();
            windowsServiceToolStripButton.Text = Labeling.Factory.Get.WindowsService.ToString();
        }

        private void SetChildView(ToolStripButton button)
        {
            foreach (ToolStripButton item in button.GetCurrentParent().Items)
            {
                if (item == button)
                {
                    item.Checked = true;
                }
                if ((item != null) && (item != button))
                {
                    item.Checked = false;
                }
            }

            if (eisToolStripButton.Checked)
            {
                this.InvokeAction(() =>
                {
                    Cursor = Cursors.WaitCursor;
                    FillChildView(ViewRepository.Get.Eis);
                    Cursor = Cursors.Default;
                });
            }
            else if (systemToolStripButton.Checked)
            {
                this.InvokeAction(() =>
                {
                    Cursor = Cursors.WaitCursor;
                    FillChildView(ViewRepository.Get.System);
                    Cursor = Cursors.Default;
                });

            }
            else if (windowsServiceToolStripButton.Checked)
            {
                this.InvokeAction(() =>
                {
                    Cursor = Cursors.WaitCursor;
                    FillChildView(ViewRepository.Get.WindowsService);
                    Cursor = Cursors.Default;
                });
            }

            CenterWindow();
        }

        private void CenterWindow()
        {
            int boundWidth = Screen.PrimaryScreen.Bounds.Width;
            int boundHeight = Screen.PrimaryScreen.Bounds.Height;
            int x = boundWidth - Width;
            int y = boundHeight - Height;
            Location = new Point(x / 2, y / 2);
        }

        private void FillChildView(IChildView childView)
        {
            contentPanel.Controls.Clear();
            contentPanel.Controls.Add((Control)childView);

            int width = childView.ContentSize.Width + menuToolStrip.Width;
            int height = OS.PlatformVersion.IsOlderThan(OSPlatformVersion.WindowsVista)
                             ? childView.ContentSize.Height - OLD_OS_RESIZING_FACTOR
                             : childView.ContentSize.Height;

            Size = new Size(width, height);
        }

        #endregion
    }
}