namespace Eng.Vector.Win.UI
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.menuToolStrip = new System.Windows.Forms.ToolStrip();
            this.eisToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.systemToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.windowsServiceToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.mainTableLayoutPanel.SuspendLayout();
            this.menuToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Controls.Add(this.contentPanel, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.menuToolStrip, 0, 0);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 1;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(1122, 620);
            this.mainTableLayoutPanel.TabIndex = 13;
            // 
            // contentPanel
            // 
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(223, 3);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(896, 614);
            this.contentPanel.TabIndex = 14;
            // 
            // menuToolStrip
            // 
            this.menuToolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menuToolStrip.AutoSize = false;
            this.menuToolStrip.CanOverflow = false;
            this.menuToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.menuToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eisToolStripButton,
            this.systemToolStripButton,
            this.windowsServiceToolStripButton});
            this.menuToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.menuToolStrip.Location = new System.Drawing.Point(0, 0);
            this.menuToolStrip.Name = "menuToolStrip";
            this.menuToolStrip.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.menuToolStrip.ShowItemToolTips = false;
            this.menuToolStrip.Size = new System.Drawing.Size(220, 620);
            this.menuToolStrip.Stretch = true;
            this.menuToolStrip.TabIndex = 13;
            this.menuToolStrip.Text = "menuToolStrip";
            // 
            // eisToolStripButton
            // 
            this.eisToolStripButton.CheckOnClick = true;
            this.eisToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("eisToolStripButton.Image")));
            this.eisToolStripButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.eisToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.eisToolStripButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.eisToolStripButton.Name = "eisToolStripButton";
            this.eisToolStripButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.eisToolStripButton.Size = new System.Drawing.Size(213, 36);
            this.eisToolStripButton.Text = "External Integration Systems";
            this.eisToolStripButton.Click += new System.EventHandler(this.eisToolStripButton_CheckedChanged);
            // 
            // systemToolStripButton
            // 
            this.systemToolStripButton.CheckOnClick = true;
            this.systemToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("systemToolStripButton.Image")));
            this.systemToolStripButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.systemToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.systemToolStripButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.systemToolStripButton.Name = "systemToolStripButton";
            this.systemToolStripButton.Size = new System.Drawing.Size(213, 36);
            this.systemToolStripButton.Text = "System";
            this.systemToolStripButton.ToolTipText = "System";
            this.systemToolStripButton.Click += new System.EventHandler(this.systemToolStripButton_CheckedChanged);
            // 
            // windowsServiceToolStripButton
            // 
            this.windowsServiceToolStripButton.CheckOnClick = true;
            this.windowsServiceToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("windowsServiceToolStripButton.Image")));
            this.windowsServiceToolStripButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.windowsServiceToolStripButton.ImageTransparentColor = System.Drawing.Color.White;
            this.windowsServiceToolStripButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.windowsServiceToolStripButton.Name = "windowsServiceToolStripButton";
            this.windowsServiceToolStripButton.Size = new System.Drawing.Size(213, 36);
            this.windowsServiceToolStripButton.Text = "Windows Service";
            this.windowsServiceToolStripButton.Click += new System.EventHandler(this.windowsServiceToolStripButton_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 620);
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VectorGUI";
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.menuToolStrip.ResumeLayout(false);
            this.menuToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.ToolStrip menuToolStrip;
        private System.Windows.Forms.ToolStripButton eisToolStripButton;
        private System.Windows.Forms.ToolStripButton systemToolStripButton;
        private System.Windows.Forms.ToolStripButton windowsServiceToolStripButton;

    }
}

