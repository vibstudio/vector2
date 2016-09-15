namespace Eng.Vector.Service
{
    #region Namespaces

    using System.ServiceProcess;

    #endregion

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            ServiceBase.Run(new Vector());
        }
    }
}