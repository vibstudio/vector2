namespace Eng.Vector.Win.UI.Controls
{
    partial class FilePathTextField
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
            components = new System.ComponentModel.Container();

            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.filePathTestFlag = new Eng.Windows.Forms.EngTestFlag();
            this.filePathCheckBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel.Controls.Add(this.filePathTextBox, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.filePathTestFlag, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.filePathCheckBox, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(773, 26);
            this.tableLayoutPanel.TabIndex = 0;
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filePathTextBox.Location = new System.Drawing.Point(23, 3);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(721, 20);
            this.filePathTextBox.TabIndex = 0;
            // 
            // filePathTestFlag
            // 
            this.filePathTestFlag.BackColor = System.Drawing.Color.Red;
            this.filePathTestFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filePathTestFlag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filePathTestFlag.Location = new System.Drawing.Point(750, 3);
            this.filePathTestFlag.Name = "filePathTestFlag";
            this.filePathTestFlag.Size = new System.Drawing.Size(20, 20);
            this.filePathTestFlag.TabIndex = 1;
            this.filePathTestFlag.TestIsPassed = false;
            // 
            // filePathCheckBox
            // 
            this.filePathCheckBox.AutoSize = true;
            this.filePathCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filePathCheckBox.Location = new System.Drawing.Point(3, 3);
            this.filePathCheckBox.Name = "filePathCheckBox";
            this.filePathCheckBox.Size = new System.Drawing.Size(14, 20);
            this.filePathCheckBox.TabIndex = 2;
            this.filePathCheckBox.Text = "filePathCheckBox";
            this.filePathCheckBox.UseVisualStyleBackColor = true;
            // 
            // FilePathTextField
            // 
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "FilePathTextField";
            this.Size = new System.Drawing.Size(773, 26);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TextBox filePathTextBox;
        private Eng.Windows.Forms.EngTestFlag filePathTestFlag;
        private System.Windows.Forms.CheckBox filePathCheckBox;
    }
}
