#region Namespaces

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Eng.Diagnostic;
using Eng.Vector.Util;

#endregion

namespace Eng.Vector.Win.UI.Views
{
    public partial class SystemView : ChildViewBase
    {
        #region Ctor(s)

        public SystemView()
        {
            InitializeComponent();

            InitializeLabels();

            FillControls();

            RunDBTests();
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
            get { return new Size(600, 480); }
        }

        #endregion

        #region Event handlers

        private void saveButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += SaveDoWork;
            worker.RunWorkerCompleted += SaveRunWorkerCompleted;
            worker.RunWorkerAsync(this);

        }

        void SaveDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = SaveConfiguration();
        }

        void SaveRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                OperationResult result = (OperationResult)e.Result;

                if (result.IsPerformed)
                {
                    MessageBox.Show(Globalization.Labeling.Factory.Get.OperationPerformed.ToString(),
                                    Globalization.Labeling.Factory.Get.InfoCaption.ToString(),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Globalization.Labeling.Factory.Get.OperationFailed(result.Message).ToString(),
                                    Globalization.Labeling.Factory.Get.ErrorCaption.ToString(),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(Globalization.Labeling.Factory.Get.OperationFailed(exception.Message).ToString(),
                                Globalization.Labeling.Factory.Get.ErrorCaption.ToString(),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            Cursor = Cursors.Default;
        }

        private void defaultConnectionStringWrapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            defaultConnectionStringTextBox.WordWrap = defaultConnectionStringWrapCheckBox.Checked;
        }

        #endregion

        #region Methods

        private void InitializeLabels()
        {
            pollingIntervalsTabPage.Text = Globalization.Labeling.Factory.Get.PollingIntervals.ToString();
            dbAccessTabPage.Text = Globalization.Labeling.Factory.Get.DBAccess.ToString();
            systemTabPage.Text = Globalization.Labeling.Factory.Get.System.ToString();

            inputPollingIntervalLabel.Text = Globalization.Labeling.Factory.Get.InputPollingInterval.ToString();
            outputPollingIntervalLabel.Text = Globalization.Labeling.Factory.Get.OutputPollingInterval.ToString();
            timeConverterGroupBox.Text = Globalization.Labeling.Factory.Get.TimeConverter.ToString();

            defaultConnectionStringLabel.Text = Globalization.Labeling.Factory.Get.DefaultConnectionString.ToString();
            defaultConnectionStringWrapCheckBox.Text = Globalization.Labeling.Factory.Get.WordWrap.ToString();

            languageLabel.Text = Globalization.Labeling.Factory.Get.Language.ToString();
            traceLevelLabel.Text = Globalization.Labeling.Factory.Get.TraceLevel.ToString();
            saveButton.Text = Globalization.Labeling.Factory.Get.Save.ToString();
        }

        private void FillControls()
        {
            // Polling intervals
            inputPollingIntervalTextBox.Text = Convert.ToString(Helper.Factory.Of.Vector.AppConfiguration.InputPollingInterval);
            outputPollingIntervalTextBox.Text = Convert.ToString(Helper.Factory.Of.Vector.AppConfiguration.OutputPollingInterval);
            
            // DB access
            defaultConnectionStringTextBox.Text = Helper.Factory.Of.Vector.AppConfiguration.DefaultConnectionString;


            // System configurations
            FillListControl(langsComboBox,
                            Enum.GetValues(typeof(AppLang))
                                .Cast<AppLang>()
                                .Select(x => Tuple.Create(x.ToString().ToUpper(), x.ToString()))
                                .ToList(),
                            "Item1",
                            "Item2");
            SelectValue(langsComboBox, Helper.Factory.Of.Vector.AppConfiguration.AppLang);
            FillListControl(traceLevelsComboBox,
                            Enum.GetValues(typeof(TraceLevel))
                                .Cast<TraceLevel>()
                                .Select(x => Tuple.Create(x.ToString().ToUpper(), x.ToString()))
                                .ToList(),
                            "Item1",
                            "Item2");
            SelectValue(traceLevelsComboBox, Helper.Factory.Of.Vector.AppConfiguration.TraceLevel);
        }

        private void FillListControl(ListControl control, IEnumerable dataSource, string displayMember, string valueMember)
        {
            control.DataSource = dataSource;
            control.DisplayMember = displayMember;
            control.ValueMember = valueMember;
        }

        private void SelectValue(ListControl control, string displayMember)
        {
            var dataSource = control.DataSource as IList<Tuple<string, string>>;

            if (dataSource != null)
            {
                control.SelectedIndex = 0;
                foreach (var tuple in dataSource)
                {
                    if (displayMember.ToLower().Equals(tuple.Item1.ToLower()))
                    {
                        control.SelectedValue = tuple.Item2;
                        break;
                    }
                }
            }
        }

        private void RunDBTests()
        {
            dbConnectTestFlag.TestIsPassed = Helper.Factory.Of.Vector.Repository.CanConnect;
            dbHealthTestFlag.TestIsPassed = Helper.Factory.Of.Vector.Repository.IsHealthy;
        }

        private OperationResult SaveConfiguration()
        {
            try
            {
                Helper.Factory.Of.Vector.AppConfiguration.SetInputPollingInterval(inputPollingIntervalTextBox.Text);
                Helper.Factory.Of.Vector.AppConfiguration.SetOutputPollingInterval(outputPollingIntervalTextBox.Text);
                Helper.Factory.Of.Vector.AppConfiguration.SetDefaultConnectionString(defaultConnectionStringTextBox.Text);
                Helper.Factory.Of.Vector.AppConfiguration.SetAppLang(langsComboBox.Text);
                Helper.Factory.Of.Vector.AppConfiguration.SetTraceLevel(traceLevelsComboBox.Text);

                Helper.Factory.Of.Vector.AppConfiguration.MergeConfigs();

                FillControls();

                RunDBTests();

                return OperationResult.CreateSuccess();
            }
            catch (Exception exception)
            {
                return OperationResult.CreateFailure(exception.Message);
            }
        }

        #endregion

    }
}