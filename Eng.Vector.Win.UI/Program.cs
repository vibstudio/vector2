#region Namespaces

using System;
using System.Windows.Forms;

using Eng.Vector.Util;

#endregion

namespace Eng.Vector.Win.UI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Helper.Factory.Of.Vector.AppConfiguration.MergeConfigs();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}