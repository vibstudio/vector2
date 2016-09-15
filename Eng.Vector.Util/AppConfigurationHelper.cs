#region Namespaces

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using Eng.Diagnostic;
using Eng.IO;
using Eng.IO.Path;
using Eng.Vector.Domain.Model;
using Eng.Vector.Exceptions;

#endregion

namespace Eng.Vector.Util
{
    /// <summary>
    /// Configurazione applicativa basata sui tipici file di configurazione .NET ".config"
    /// </summary>
    public interface IAppConfigurationHelper
    {
        string ServiceName { get; }

        string GUIName { get; }

        string ConnectionStringName { get; }

        string DefaultConnectionString { get; }

        int InputPollingInterval { get; }

        int OutputPollingInterval { get; }

        int ElaborationRenewalInterval { get; }

        int ThresholdTailing { get; }

        string AppLang { get; }

        AppSkin GUISkin { get; }

        string TraceLevel { get; }

        void SetInputPollingInterval(string value);

        void SetOutputPollingInterval(string value);

        void SetDefaultConnectionString(string value);

        void SetAppLang(string value);

        void SetTraceLevel(string value);

        void MergeConfigs(MergingOptions mergingOptions = MergingOptions.IfDifferent);

        string GetAppSettingValue(string key);
    }

    internal class AppConfigurationHelper : IAppConfigurationHelper
    {
        #region Constants

        private const string SERVICE_NAME = "Vector";

        private static readonly string GUI_NAME = string.Format("{0}GUI", SERVICE_NAME);

        private static readonly string SERVICE_CONFIGURATION_FILE_NAME = string.Format("{0}.exe.config", SERVICE_NAME);

        private static readonly string GUI_CONFIGURATION_FILE_NAME = string.Format("{0}.exe.config", GUI_NAME);

        private const string UNIT_TEST_CONFIGURATION_FILE_NAME = "app.config";

        #region Defaults

        private const AppSkin DEFAULT_APP_SKIN = AppSkin.Sicily;

        private const int DEFAULT_INPUT_POLLING_INTERVAL = 10000;

        private const int DEFAULT_OUTPUT_POLLING_INTERVAL = 10000;

        private const int DEFAULT_ELABORATION_RENEWAL_INTERVAL = 2;

        private const int DEFAULT_THRESHOLD_TAILING = 10000;

        private const string DEFAULT_TRACE_LEVEL = "Error";

        #endregion

        #region Keys

        private const string APP_LANG_KEY = "appLang";

        private const string APP_SKIN_KEY = "appSkin";

        private const string CONNECTIONSTRING_NAME_KEY = "ORADB";

        private const string INPUT_POLLING_INTERVAL_KEY = "inputPollingInterval";

        private const string OUTPUT_POLLING_INTERVAL_KEY = "outputPollingInterval";

        private const string ELABORATION_RENEWAL_INTERVAL_KEY = "elaborationRenewalInterval";

        private const string THRESHOLD_TAILING_KEY = "thresholdTailing";

        #endregion

        #endregion

        #region Public Properties

        public string ServiceName
        {
            get { return SERVICE_NAME; }
        }

        public string GUIName
        {
            get { return GUI_NAME; }
        }

        /// <summary>
        /// Restituisce il nome della stringa di connessione.
        /// </summary>
        public string ConnectionStringName
        {
            get { return GetAppSettingValue(CONNECTIONSTRING_NAME_KEY); }
        }

        /// <summary>
        /// Restituisce il valore della connectionString.
        /// </summary>
        public string DefaultConnectionString
        {
            get
            {
                const string CONFIGURATION_NAME = "connectionStrings";
                const string ATTRIBUTE_NAME = "connectionString";

                try
                {
                    if (string.IsNullOrEmpty(ConnectionStringName))
                    {
                        throw new ConfigurationNotFoundException(ConnectionStringName);
                    }

                    XDocument document = XDocument.Load(ResolveConfigurationFilePath());

                    XElement element = document.Descendants(CONFIGURATION_NAME)
                                               .Elements("add")
                                               .FirstOrDefault(x => x.Attribute("name").Value == ConnectionStringName);

                    if (element == null) throw new ConfigurationNotFoundException(CONFIGURATION_NAME);

                    return element.Attribute(ATTRIBUTE_NAME).Value;
                }
                catch (Exception exception)
                {
                    Trace.Write.Error(exception, typeof(AppConfigurationHelper), "DefaultConnectionString");

                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Restituisce l'intervallo in millisecondi con cui fare polling sui files messi a disposizione dai SIE.
        /// </summary>
        public int InputPollingInterval
        {
            get
            {
                int pollingInterval;
                return int.TryParse(GetAppSettingValue(INPUT_POLLING_INTERVAL_KEY), out pollingInterval)
                           ? pollingInterval
                           : DEFAULT_INPUT_POLLING_INTERVAL;
            }
        }

        /// <summary>
        /// Restuisce l'intervallo in millisecondi con cui fare polling sui files elaborati dalla Distribuzione da inviare ai SIE.
        /// </summary>
        public int OutputPollingInterval
        {
            get
            {
                int pollingInterval;
                return int.TryParse(GetAppSettingValue(OUTPUT_POLLING_INTERVAL_KEY), out pollingInterval)
                           ? pollingInterval
                           : DEFAULT_OUTPUT_POLLING_INTERVAL;
            }
        }

        /// <summary>
        /// Restituisce l'intervallo in minuti entro il quale rinnovare il processo di elaborazione.
        /// </summary>
        public int ElaborationRenewalInterval
        {
            get
            {
                int elaborationRenewalInterval;
                return int.TryParse(GetAppSettingValue(ELABORATION_RENEWAL_INTERVAL_KEY), out elaborationRenewalInterval)
                           ? elaborationRenewalInterval
                           : DEFAULT_ELABORATION_RENEWAL_INTERVAL;
            }
        }

        /// <summary>
        /// Restituisce la soglia di scodamento dei tracciati già trattati.
        /// </summary>
        public int ThresholdTailing
        {
            get
            {
                int thresholdTailing;
                return int.TryParse(GetAppSettingValue(THRESHOLD_TAILING_KEY), out thresholdTailing)
                           ? thresholdTailing
                           : DEFAULT_THRESHOLD_TAILING;
            }
        }

        /// <summary>
        /// Restituisce la lingua usata dall'applicazione.
        /// </summary>
        public string AppLang
        {
            get { return GetAppSettingValue(APP_LANG_KEY); }
        }

        /// <summary>
        /// Restituisce la "pelle" (lo stile di presentazione) della GUI.
        /// </summary>
        public AppSkin GUISkin
        {
            get
            {
                string appSkinValue = GetAppSettingValue(APP_SKIN_KEY);
                if (string.IsNullOrEmpty(appSkinValue))
                {
                    return DEFAULT_APP_SKIN;
                }

                AppSkin appSkin;
                return Enum.TryParse(appSkinValue, true, out appSkin) ? appSkin : DEFAULT_APP_SKIN;
            }
        }

        /// <summary>
        /// Restituisce il livello di trace.
        /// </summary>
        public string TraceLevel
        {
            get
            {
                const string ATTRIBUTE_NAME = "value";

                try
                {
                    XDocument document = XDocument.Load(ResolveConfigurationFilePath());

                    XElement element =
                        document
                            .Descendants("log4net")
                            .Descendants("root")
                            .Elements("level")
                            .FirstOrDefault();

                    if (element == null) throw new ConfigurationNotFoundException("log4net");

                    return element.Attribute(ATTRIBUTE_NAME).Value;
                }
                catch (Exception exception)
                {
                    Trace.Write.Error(exception, typeof(AppConfigurationHelper), "TraceLevel");

                    return DEFAULT_TRACE_LEVEL;
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        public void SetInputPollingInterval(string value)
        {
            SetAppSetting(INPUT_POLLING_INTERVAL_KEY, value);
        }

        public void SetOutputPollingInterval(string value)
        {
            SetAppSetting(OUTPUT_POLLING_INTERVAL_KEY, value);
        }
        
        public void SetDefaultConnectionString(string value)
        {
            string configPath = ResolveConfigurationFilePath();

            XDocument document = XDocument.Load(configPath);

            XElement element =
                document.Descendants("connectionStrings")
                        .Elements("add")
                        .FirstOrDefault(x => x.Attribute("name").Value == ConnectionStringName);

            if (element != null)
            {
                element.Attribute("connectionString").SetValue(value);
                document.Save(configPath);
            }
        }

        public void SetAppLang(string value)
        {
            SetAppSetting(APP_LANG_KEY, value.ToUpper());
        }

        public void SetTraceLevel(string value)
        {
            string configPath = ResolveConfigurationFilePath();

            XDocument document = XDocument.Load(configPath);

            XElement element = document.Descendants("log4net")
                                       .Descendants("root")
                                       .Elements("level")
                                       .FirstOrDefault();

            if (element != null)
            {
                XAttribute attribute = element.Attribute("value");
                if (attribute != null)
                {
                    attribute.SetValue(value.ToUpper());
                    document.Save(configPath);
                }
            }
        }

        public void MergeConfigs(MergingOptions mergingOptions = MergingOptions.IfDifferent)
        {
            string serviceAppConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SERVICE_CONFIGURATION_FILE_NAME);
            string guiAppConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GUI_CONFIGURATION_FILE_NAME);

            if (mergingOptions == MergingOptions.IfDifferent)
            {
                try
                {
                    if (!FilePath.Create(serviceAppConfigFilePath).AreEqual(guiAppConfigFilePath))
                    {
                        MergeFiles(serviceAppConfigFilePath, guiAppConfigFilePath);
                    }
                }
                catch (Exception exception)
                {
                    Trace.Write.Warn(exception.Message, typeof(AppConfigurationHelper), "MergeConfigs");
                }
            }
            else
            {
                MergeFiles(serviceAppConfigFilePath, guiAppConfigFilePath);
            }
        }

        /// <summary>
        /// Restituisce il valore di una chiave all'interno della sezione appSettings del file di configurazione.
        /// L'algoritmo valuterà, nell'ordine, l'app.config del Windows Service, della GUI application e del
        /// progetto di unit test.
        /// </summary>
        /// <param name="key">
        /// Il nome della chiave da cui recuperare il valore.
        /// </param>
        /// <returns>
        /// Il valore di una chiave all'interno della sezione appSettings del file di configurazione.
        /// </returns>
        public string GetAppSettingValue(string key)
        {
            const string SECTION_NAME = "appSettings";

            string configurationFilePath = ResolveConfigurationFilePath();

            XDocument document = XDocument.Load(configurationFilePath);

            XElement element = document.Descendants(SECTION_NAME)
                                       .Elements("add")
                                       .FirstOrDefault(e => e.Attribute("key").Value == key);

            return element != null ? element.Attribute("value").Value : string.Empty;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Imposta il valore di una chiave all'interno della sezione appSettings del file di configurazione.
        /// L'algoritmo valuterà, nell'ordine, l'app.config del Windows Service, della GUI application e del
        /// progetto di unit test.
        /// </summary>
        /// <param name="key">
        /// Il nome della chiave per cui impostare il valore.
        /// </param>
        /// <param name="value">
        /// Il nuovo valore da impostare.
        /// </param>
        private static void SetAppSetting(string key, string value)
        {
            const string SECTION_NAME = "appSettings";

            string configurationFilePath = ResolveConfigurationFilePath();

            if (File.Exists(configurationFilePath))
            {
                XDocument document = XDocument.Load(configurationFilePath);

                XElement element = document.Descendants(SECTION_NAME)
                                           .Elements("add")
                                           .FirstOrDefault(e => e.Attribute("key").Value == key);

                if (element != null)
                {
                    element.Attribute("value").SetValue(value);
                    document.Save(configurationFilePath);
                }
            }
        }

        private static string ResolveConfigurationFilePath()
        {
            string configurationFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SERVICE_CONFIGURATION_FILE_NAME);
            if (File.Exists(configurationFilePath)) return configurationFilePath;

            configurationFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GUI_CONFIGURATION_FILE_NAME);
            if (File.Exists(configurationFilePath)) return configurationFilePath;
          
            configurationFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UNIT_TEST_CONFIGURATION_FILE_NAME);
            if (File.Exists(configurationFilePath)) return configurationFilePath;

            throw new ConfigurationFileNotFoundException(ConfigurationScope.Application);
        }

        private static void MergeFiles(string filePath1, string filePath2)
        {
            if (File.Exists(filePath1) && File.Exists(filePath2))
            {
                using (var reader = new StreamReader(filePath1))
                {
                    string content = reader.ReadToEnd();

                    using (var writer = new StreamWriter(filePath2, false))
                    {
                        writer.Write(content);
                        writer.Flush();
                    }
                }
            }
        }

        #endregion
    }
}
