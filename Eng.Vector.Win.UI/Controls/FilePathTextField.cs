namespace Eng.Vector.Win.UI.Controls
{
    #region Namespaces

    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Forms;

    #endregion

    public partial class FilePathTextField : Panel
    {
        #region Fields

        private string _filePath;

        private bool _isChecked;

        #endregion

        #region Ctor(s)

        public FilePathTextField()
        {
            InitializeComponent();
        }

        public FilePathTextField(IContainer container)
            : this()
        {
            container.Add(this);
        }

        #endregion

        #region Properties

        public bool Checked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                filePathCheckBox.Checked = value;
            }
        }

        #endregion

        #region Events

        public event EventHandler FilePathSelectionChanged
        {
            add { filePathCheckBox.Click += value; }
            remove { filePathCheckBox.Click += value; }
        }

        #endregion

        #region Overrides

        public override string Text
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                filePathTextBox.Text = value;
                filePathTestFlag.TestIsPassed = Directory.Exists(value);
            }
        }

        #endregion
    }
}