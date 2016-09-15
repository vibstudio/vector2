namespace Eng.Vector.Win.UI.Views
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using Eng.Vector.Domain.Model;
    using Eng.Vector.Util;
    using Eng.Vector.Win.UI.Controls;

    #endregion

    public partial class EisView : ChildViewBase
    {
        #region Ctor(s)

        public EisView()
        {
            InitializeComponent();
            InitializeTabControl();
        }

        #endregion

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Dock = DockStyle.Fill;
        }

        #endregion

        #region Event handlers

        private void TabControlSelectedIndexChanged(object sender, EventArgs e)
        {
            ResetDirectorySelection();
        }

        private void TabPageSelectionChanged(object sender, EisDirectorySelectionChangeEventArgs e)
        {
            FillDetail(e.EisID, e.DirectoryPath, e.Direction, e.HierarchicalScope);
        }

        #endregion

        #region Methods

        private void ResetDirectorySelection()
        {
            EisDirectoryTabPage page = tabControl.TabPages[tabControl.SelectedIndex] as EisDirectoryTabPage;
            if (page != null)
            {
                page.ResetSelection();
            }
        }

        private void InitializeTabControl()
        {
            tabControl.TabPages.Clear();

            IEnumerable<string> eisIdList = Helper.Factory.Of.Vector.EisConfiguration.EisConfig.List.Select(x => x.ID);
            foreach (string eisID in eisIdList)
            {
                EisDirectoryTabPage page = new EisDirectoryTabPage(eisID);
                page.SelectionChanged += TabPageSelectionChanged;
                tabControl.TabPages.Add(page);
            }

            tabControl.SelectedIndexChanged += TabControlSelectedIndexChanged;

            ResetDirectorySelection();
        }

        private void FillDetail(string eisID, string directoryPath, TransferDirection transferDirection, DirectoryHierarchicalScope hierarchicalScope)
        {
            tableLayoutPanel.Controls.Remove(tableLayoutPanel.GetControlFromPosition(1, 0));
            tableLayoutPanel.Controls.Add(new EisDetailPanel(eisID, directoryPath, transferDirection, hierarchicalScope), 1, 0);
        }

        #endregion

    }
}