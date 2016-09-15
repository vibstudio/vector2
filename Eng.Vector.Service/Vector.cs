#region Namespaces

using System;
using System.ServiceProcess;
using System.Timers;

using Eng.Diagnostic;
using Eng.Vector.Engine.Transfer;
using Eng.Vector.Util;

#endregion

namespace Eng.Vector.Service
{
    public partial class Vector : ServiceBase
    {
        #region Fields

        // Timer con cui fare polling sui files messi a disposizione dai SIE verso i corrispettivi moduli di gestione dati (DB)
        private Timer _inputPollingTimer;

        // Timer con cui fare polling sui files elaborati dai moduli di gestione dati (DB) verso i SIE
        private Timer _outputPollingTimer;

        #endregion

        #region Ctor(s)

        public Vector()
        {
            InitializeComponent();
        }

        #endregion

        #region Overrides

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            Trace.Write.Debug("OnPowerEvent START");

            switch (powerStatus)
            {
                case PowerBroadcastStatus.PowerStatusChange:
                    break;
                case PowerBroadcastStatus.BatteryLow:
                    break;
            }

            return base.OnPowerEvent(powerStatus);
        }

        protected override void OnShutdown()
        {
            Trace.Write.Debug("OnShutdown START");

            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            Trace.Write.Debug("OnStart START");

            // SIE -> DB
            // Impostazione dell'intervallo con cui fare polling sui files messi a disposizione dai SIE verso i corrispettivi moduli di gestione dati (DB)
            int inputPollingInterval = Helper.Factory.Of.Vector.AppConfiguration.InputPollingInterval;
            _inputPollingTimer = new Timer(inputPollingInterval);
            _inputPollingTimer.Elapsed += InputPollingTimerElapsed;
            _inputPollingTimer.Enabled = true;
            Trace.Write.DebugFormat("inputPollingInterval: {0}", inputPollingInterval);

            // DB -> SIE
            // Impostazione dell'intervallo con cui fare polling sui files elaborati dai moduli di gestione dati (DB) verso i SIE
            int outputPollingInterval = Helper.Factory.Of.Vector.AppConfiguration.OutputPollingInterval;
            _outputPollingTimer = new Timer(outputPollingInterval);
            _outputPollingTimer.Elapsed += OutputPollingTimerElapsed;
            _outputPollingTimer.Enabled = true;
            Trace.Write.DebugFormat("outputPollingInterval: {0}", outputPollingInterval);
        }

        protected override void OnStop()
        {
            Trace.Write.Debug("OnStop START");

            _inputPollingTimer.Enabled = false;

            _outputPollingTimer.Enabled = false;
        }

        #endregion

        #region Event handlers

        private void InputPollingTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Trace.Write.Debug("InputPollingTimer elapsed");

            PerformInputPolling();
        }

        private void OutputPollingTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Trace.Write.Debug("OutputPollingTimer elapsed");

            PerformOutputPolling();
        }

        #endregion

        #region Methods

        private void PerformInputPolling()
        {
            Trace.Write.Debug("PerformInputPolling START");

            try
            {
                TransferInputManager.Factory.RunAsync(Helper.Factory.Of.Vector.EisConfiguration.EisConfig.GetFilePaths());
            }
            catch (Exception exception)
            {
                Trace.Write.Error(exception, GetType(), "PerformInputPolling");
            }

            Trace.Write.Debug("PerformInputPolling END");
        }

        private void PerformOutputPolling()
        {
            Trace.Write.Debug("PerformOutputPolling START");

            try
            {
                TransferOutputManager.Factory.Run();
            }
            catch (Exception exception)
            {
                Trace.Write.Error(exception, GetType(), "PerformOutputPolling");
            }

            Trace.Write.Debug("PerformOutputPolling END");
        }

        #endregion

    }
}