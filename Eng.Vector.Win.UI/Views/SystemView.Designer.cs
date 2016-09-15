namespace Eng.Vector.Win.UI.Views
{
    partial class SystemView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.saveButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.pollingIntervalsTabPage = new System.Windows.Forms.TabPage();
            this.timeConverterGroupBox = new System.Windows.Forms.GroupBox();
            this.timeConverter = new Eng.Windows.Forms.EngTimeConverter(this.components);
            this.outputPollingIntervalTextBox = new System.Windows.Forms.TextBox();
            this.outputPollingIntervalLabel = new System.Windows.Forms.Label();
            this.inputPollingIntervalTextBox = new System.Windows.Forms.TextBox();
            this.inputPollingIntervalLabel = new System.Windows.Forms.Label();
            this.dbAccessTabPage = new System.Windows.Forms.TabPage();
            this.dbHealthTestLabel = new System.Windows.Forms.Label();
            this.dbConnectTestLabel = new System.Windows.Forms.Label();
            this.defaultConnectionStringWrapCheckBox = new System.Windows.Forms.CheckBox();
            this.defaultConnectionStringLabel = new System.Windows.Forms.Label();
            this.defaultConnectionStringTextBox = new System.Windows.Forms.TextBox();
            this.dbHealthTestFlag = new Eng.Windows.Forms.EngTestFlag();
            this.dbConnectTestFlag = new Eng.Windows.Forms.EngTestFlag();
            this.systemTabPage = new System.Windows.Forms.TabPage();
            this.traceLevelsComboBox = new System.Windows.Forms.ComboBox();
            this.traceLevelLabel = new System.Windows.Forms.Label();
            this.langsComboBox = new System.Windows.Forms.ComboBox();
            this.languageLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.pollingIntervalsTabPage.SuspendLayout();
            this.timeConverterGroupBox.SuspendLayout();
            this.dbAccessTabPage.SuspendLayout();
            this.systemTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.saveButton, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.tabControl, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(634, 432);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(539, 401);
            this.saveButton.Margin = new System.Windows.Forms.Padding(3, 5, 5, 5);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(90, 26);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.pollingIntervalsTabPage);
            this.tabControl.Controls.Add(this.dbAccessTabPage);
            this.tabControl.Controls.Add(this.systemTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(9, 8);
            this.tabControl.Margin = new System.Windows.Forms.Padding(9, 8, 5, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(620, 388);
            this.tabControl.TabIndex = 1;
            // 
            // pollingIntervalsTabPage
            // 
            this.pollingIntervalsTabPage.Controls.Add(this.timeConverterGroupBox);
            this.pollingIntervalsTabPage.Controls.Add(this.outputPollingIntervalTextBox);
            this.pollingIntervalsTabPage.Controls.Add(this.outputPollingIntervalLabel);
            this.pollingIntervalsTabPage.Controls.Add(this.inputPollingIntervalTextBox);
            this.pollingIntervalsTabPage.Controls.Add(this.inputPollingIntervalLabel);
            this.pollingIntervalsTabPage.Location = new System.Drawing.Point(4, 22);
            this.pollingIntervalsTabPage.Name = "pollingIntervalsTabPage";
            this.pollingIntervalsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.pollingIntervalsTabPage.Size = new System.Drawing.Size(612, 362);
            this.pollingIntervalsTabPage.TabIndex = 0;
            this.pollingIntervalsTabPage.Text = "Polling intervals";
            this.pollingIntervalsTabPage.UseVisualStyleBackColor = true;
            // 
            // timeConverterGroupBox
            // 
            this.timeConverterGroupBox.Controls.Add(this.timeConverter);
            this.timeConverterGroupBox.Location = new System.Drawing.Point(12, 106);
            this.timeConverterGroupBox.Name = "timeConverterGroupBox";
            this.timeConverterGroupBox.Size = new System.Drawing.Size(224, 185);
            this.timeConverterGroupBox.TabIndex = 4;
            this.timeConverterGroupBox.TabStop = false;
            this.timeConverterGroupBox.Text = "Time converter";
            // 
            // timeConverter
            // 
            this.timeConverter.Location = new System.Drawing.Point(5, 13);
            this.timeConverter.Name = "timeConverter";
            this.timeConverter.Size = new System.Drawing.Size(214, 166);
            this.timeConverter.TabIndex = 0;
            this.timeConverter.Text = "1";
            // 
            // outputPollingIntervalTextBox
            // 
            this.outputPollingIntervalTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputPollingIntervalTextBox.Location = new System.Drawing.Point(12, 70);
            this.outputPollingIntervalTextBox.Name = "outputPollingIntervalTextBox";
            this.outputPollingIntervalTextBox.Size = new System.Drawing.Size(587, 20);
            this.outputPollingIntervalTextBox.TabIndex = 3;
            // 
            // outputPollingIntervalLabel
            // 
            this.outputPollingIntervalLabel.AutoSize = true;
            this.outputPollingIntervalLabel.Location = new System.Drawing.Point(12, 54);
            this.outputPollingIntervalLabel.Name = "outputPollingIntervalLabel";
            this.outputPollingIntervalLabel.Size = new System.Drawing.Size(109, 13);
            this.outputPollingIntervalLabel.TabIndex = 2;
            this.outputPollingIntervalLabel.Text = "Output polling interval";
            // 
            // inputPollingIntervalTextBox
            // 
            this.inputPollingIntervalTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputPollingIntervalTextBox.Location = new System.Drawing.Point(12, 26);
            this.inputPollingIntervalTextBox.Name = "inputPollingIntervalTextBox";
            this.inputPollingIntervalTextBox.Size = new System.Drawing.Size(587, 20);
            this.inputPollingIntervalTextBox.TabIndex = 1;
            // 
            // inputPollingIntervalLabel
            // 
            this.inputPollingIntervalLabel.AutoSize = true;
            this.inputPollingIntervalLabel.Location = new System.Drawing.Point(12, 10);
            this.inputPollingIntervalLabel.Name = "inputPollingIntervalLabel";
            this.inputPollingIntervalLabel.Size = new System.Drawing.Size(101, 13);
            this.inputPollingIntervalLabel.TabIndex = 0;
            this.inputPollingIntervalLabel.Text = "Input polling interval";
            // 
            // dbAccessTabPage
            // 
            this.dbAccessTabPage.Controls.Add(this.dbHealthTestLabel);
            this.dbAccessTabPage.Controls.Add(this.dbConnectTestLabel);
            this.dbAccessTabPage.Controls.Add(this.defaultConnectionStringWrapCheckBox);
            this.dbAccessTabPage.Controls.Add(this.defaultConnectionStringLabel);
            this.dbAccessTabPage.Controls.Add(this.defaultConnectionStringTextBox);
            this.dbAccessTabPage.Controls.Add(this.dbHealthTestFlag);
            this.dbAccessTabPage.Controls.Add(this.dbConnectTestFlag);
            this.dbAccessTabPage.Location = new System.Drawing.Point(4, 22);
            this.dbAccessTabPage.Name = "dbAccessTabPage";
            this.dbAccessTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.dbAccessTabPage.Size = new System.Drawing.Size(612, 362);
            this.dbAccessTabPage.TabIndex = 1;
            this.dbAccessTabPage.Text = "DB access";
            this.dbAccessTabPage.UseVisualStyleBackColor = true;
            // 
            // dbHealthTestLabel
            // 
            this.dbHealthTestLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dbHealthTestLabel.Location = new System.Drawing.Point(581, 10);
            this.dbHealthTestLabel.Name = "dbHealthTestLabel";
            this.dbHealthTestLabel.Size = new System.Drawing.Size(18, 13);
            this.dbHealthTestLabel.TabIndex = 6;
            this.dbHealthTestLabel.Text = "H";
            this.dbHealthTestLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dbConnectTestLabel
            // 
            this.dbConnectTestLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dbConnectTestLabel.Location = new System.Drawing.Point(555, 10);
            this.dbConnectTestLabel.Name = "dbConnectTestLabel";
            this.dbConnectTestLabel.Size = new System.Drawing.Size(18, 13);
            this.dbConnectTestLabel.TabIndex = 5;
            this.dbConnectTestLabel.Text = "C";
            this.dbConnectTestLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // defaultConnectionStringWrapCheckBox
            // 
            this.defaultConnectionStringWrapCheckBox.AutoSize = true;
            this.defaultConnectionStringWrapCheckBox.Checked = true;
            this.defaultConnectionStringWrapCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.defaultConnectionStringWrapCheckBox.Location = new System.Drawing.Point(15, 116);
            this.defaultConnectionStringWrapCheckBox.Name = "defaultConnectionStringWrapCheckBox";
            this.defaultConnectionStringWrapCheckBox.Size = new System.Drawing.Size(78, 17);
            this.defaultConnectionStringWrapCheckBox.TabIndex = 2;
            this.defaultConnectionStringWrapCheckBox.Text = "Word wrap";
            this.defaultConnectionStringWrapCheckBox.UseVisualStyleBackColor = true;
            this.defaultConnectionStringWrapCheckBox.CheckedChanged += new System.EventHandler(this.defaultConnectionStringWrapCheckBox_CheckedChanged);
            // 
            // defaultConnectionStringLabel
            // 
            this.defaultConnectionStringLabel.AutoSize = true;
            this.defaultConnectionStringLabel.Location = new System.Drawing.Point(12, 10);
            this.defaultConnectionStringLabel.Name = "defaultConnectionStringLabel";
            this.defaultConnectionStringLabel.Size = new System.Drawing.Size(125, 13);
            this.defaultConnectionStringLabel.TabIndex = 1;
            this.defaultConnectionStringLabel.Text = "Default connection string";
            // 
            // defaultConnectionStringTextBox
            // 
            this.defaultConnectionStringTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultConnectionStringTextBox.Location = new System.Drawing.Point(12, 26);
            this.defaultConnectionStringTextBox.Multiline = true;
            this.defaultConnectionStringTextBox.Name = "defaultConnectionStringTextBox";
            this.defaultConnectionStringTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.defaultConnectionStringTextBox.Size = new System.Drawing.Size(533, 83);
            this.defaultConnectionStringTextBox.TabIndex = 0;
            // 
            // dbHealthTestFlag
            // 
            this.dbHealthTestFlag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dbHealthTestFlag.BackColor = System.Drawing.Color.Red;
            this.dbHealthTestFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbHealthTestFlag.Location = new System.Drawing.Point(581, 26);
            this.dbHealthTestFlag.Name = "dbHealthTestFlag";
            this.dbHealthTestFlag.Size = new System.Drawing.Size(18, 18);
            this.dbHealthTestFlag.TabIndex = 4;
            this.dbHealthTestFlag.TestIsPassed = false;
            // 
            // dbConnectTestFlag
            // 
            this.dbConnectTestFlag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dbConnectTestFlag.BackColor = System.Drawing.Color.Red;
            this.dbConnectTestFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbConnectTestFlag.Location = new System.Drawing.Point(555, 26);
            this.dbConnectTestFlag.Name = "dbConnectTestFlag";
            this.dbConnectTestFlag.Size = new System.Drawing.Size(18, 18);
            this.dbConnectTestFlag.TabIndex = 3;
            this.dbConnectTestFlag.TestIsPassed = false;
            // 
            // systemTabPage
            // 
            this.systemTabPage.Controls.Add(this.traceLevelsComboBox);
            this.systemTabPage.Controls.Add(this.traceLevelLabel);
            this.systemTabPage.Controls.Add(this.langsComboBox);
            this.systemTabPage.Controls.Add(this.languageLabel);
            this.systemTabPage.Location = new System.Drawing.Point(4, 22);
            this.systemTabPage.Name = "systemTabPage";
            this.systemTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.systemTabPage.Size = new System.Drawing.Size(612, 362);
            this.systemTabPage.TabIndex = 2;
            this.systemTabPage.Text = "System";
            this.systemTabPage.UseVisualStyleBackColor = true;
            // 
            // traceLevelsComboBox
            // 
            this.traceLevelsComboBox.FormattingEnabled = true;
            this.traceLevelsComboBox.Location = new System.Drawing.Point(12, 70);
            this.traceLevelsComboBox.Name = "traceLevelsComboBox";
            this.traceLevelsComboBox.Size = new System.Drawing.Size(144, 21);
            this.traceLevelsComboBox.TabIndex = 3;
            // 
            // traceLevelLabel
            // 
            this.traceLevelLabel.AutoSize = true;
            this.traceLevelLabel.Location = new System.Drawing.Point(12, 54);
            this.traceLevelLabel.Name = "traceLevelLabel";
            this.traceLevelLabel.Size = new System.Drawing.Size(60, 13);
            this.traceLevelLabel.TabIndex = 2;
            this.traceLevelLabel.Text = "Trace level";
            // 
            // langsComboBox
            // 
            this.langsComboBox.FormattingEnabled = true;
            this.langsComboBox.Location = new System.Drawing.Point(12, 26);
            this.langsComboBox.Name = "langsComboBox";
            this.langsComboBox.Size = new System.Drawing.Size(144, 21);
            this.langsComboBox.TabIndex = 1;
            // 
            // languageLabel
            // 
            this.languageLabel.AutoSize = true;
            this.languageLabel.Location = new System.Drawing.Point(12, 10);
            this.languageLabel.Name = "languageLabel";
            this.languageLabel.Size = new System.Drawing.Size(55, 13);
            this.languageLabel.TabIndex = 0;
            this.languageLabel.Text = "Language";
            // 
            // SystemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "SystemView";
            this.Size = new System.Drawing.Size(634, 432);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.pollingIntervalsTabPage.ResumeLayout(false);
            this.pollingIntervalsTabPage.PerformLayout();
            this.timeConverterGroupBox.ResumeLayout(false);
            this.dbAccessTabPage.ResumeLayout(false);
            this.dbAccessTabPage.PerformLayout();
            this.systemTabPage.ResumeLayout(false);
            this.systemTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage pollingIntervalsTabPage;
        private System.Windows.Forms.TabPage dbAccessTabPage;
        private System.Windows.Forms.TabPage systemTabPage;
        private System.Windows.Forms.GroupBox timeConverterGroupBox;
        private Eng.Windows.Forms.EngTimeConverter timeConverter;
        private System.Windows.Forms.TextBox outputPollingIntervalTextBox;
        private System.Windows.Forms.Label outputPollingIntervalLabel;
        private System.Windows.Forms.TextBox inputPollingIntervalTextBox;
        private System.Windows.Forms.Label inputPollingIntervalLabel;
        private System.Windows.Forms.CheckBox defaultConnectionStringWrapCheckBox;
        private System.Windows.Forms.Label defaultConnectionStringLabel;
        private System.Windows.Forms.TextBox defaultConnectionStringTextBox;
        private Eng.Windows.Forms.EngTestFlag dbHealthTestFlag;
        private Eng.Windows.Forms.EngTestFlag dbConnectTestFlag;
        private System.Windows.Forms.Label dbHealthTestLabel;
        private System.Windows.Forms.Label dbConnectTestLabel;
        private System.Windows.Forms.ComboBox traceLevelsComboBox;
        private System.Windows.Forms.Label traceLevelLabel;
        private System.Windows.Forms.ComboBox langsComboBox;
        private System.Windows.Forms.Label languageLabel;

    }
}
