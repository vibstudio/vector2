#region Namespaces

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using Eng.Cache;
using Eng.Diagnostic;
using Eng.IO;
using Eng.Vector.Domain.Abstractions;
using Eng.Vector.Domain.Model;
using Eng.Vector.Domain.Model.Integration;
using Eng.Vector.Exceptions;
using Eng.Vector.IoC;

#endregion

namespace Eng.Vector.Util
{
    public interface IEisConfigurationHelper
    {
        EisConfig EisConfig { get; }
    }

    internal class EisConfigurationHelper : IEisConfigurationHelper
    {
        #region Fields

        private static readonly object eisConfigLocker = new object();

        #endregion

        #region Constants

        private const string EIS_CONFIGURATION_FILE_NAME = "EisConfig.xml";

        #endregion

        #region Properties

        internal static string EisConfigurationFilePath
        {
            get
            {
                string configurationFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, EIS_CONFIGURATION_FILE_NAME);
                if (File.Exists(configurationFilePath))
                {
                    return configurationFilePath;
                }

                throw new ConfigurationFileNotFoundException(ConfigurationScope.Eis);
            }
        }
        
        /// <summary>
        /// Rappresentazione a oggetti del file EisConfig.xml, restituisce un istanza di tipo <see cref="EisConfig"/>
        /// contenente una collezione di oggetti di tipo <see cref="Eis"/>.
        /// </summary>
        public EisConfig EisConfig
        {
            get
            {
                const string EIS_CONFIG_CACHE_KEY = "EisConfig";

                var eisConfig = CacheFactory.Instance.Get(EIS_CONFIG_CACHE_KEY) as EisConfig;
                if (eisConfig == null)
                {
                    lock (eisConfigLocker)
                    {
                        eisConfig = CacheFactory.Instance.Get(EIS_CONFIG_CACHE_KEY) as EisConfig;
                        if (eisConfig == null)
                        {
                            eisConfig = GetEisConfig();
                            CacheFactory.Instance.Add(EIS_CONFIG_CACHE_KEY, eisConfig, CustomCacheItemPolicy.Set.Max);
                        }
                    }
                }
                return eisConfig;
            }
        }

        #endregion

        #region Methods

        private static EisConfig GetEisConfig()
        {
            EisConfig config = new EisConfig();

            string configurationFilePath = EisConfigurationFilePath;

            if (!File.Exists(configurationFilePath))
            {
                throw new ConfigurationFileNotFoundException(ConfigurationScope.Eis);
            }

            XDocument document = XDocument.Load(configurationFilePath);

            IEnumerable<XElement> eisElements = from e in document.Descendants("eis")
                                                select e;

            foreach (XElement eisElement in eisElements)
            {
                try
                {
                    Eis eis = new Eis();

                    eis.ID = eisElement.Attribute("id").Value;

                    eis.NetworkAccessInfo = new TransferNetworkAccessInfo
                    {
                        Credentials = RetrieveNetworkAccessCredentials(eisElement)
                    };

                    eis.Input = new TransferInput
                    {
                        DirectoryInfo = BuildDirectoryInfo(eisElement, TransferDirection.Input),
                        FileInfo = BuildFileInfo(eisElement, configurationFilePath, eis, TransferDirection.Input)
                    };

                    eis.Output = new TransferOutput
                    {
                        DirectoryInfo = BuildDirectoryInfo(eisElement, TransferDirection.Output),
                        FileInfo = BuildFileInfo(eisElement, configurationFilePath, eis, TransferDirection.Output)
                    };

                    config.Add(eis);
                }
                catch (Exception exception)
                {
                    Trace.Write.Error(exception.Message);
                }
            }

            return config;
        }

        private static TransferNetworkCredentials RetrieveNetworkAccessCredentials(XElement element)
        {
            TransferNetworkCredentials credentials = new TransferNetworkCredentials();

            XElement fileSystemElement = element.Descendants("fileSystem").FirstOrDefault();
            if (fileSystemElement != null)
            {
                XAttribute domainAttribute = fileSystemElement.Attribute("domain");
                credentials.Domain = domainAttribute != null ? domainAttribute.Value : string.Empty;
                
                XAttribute userNameAttribute = fileSystemElement.Attribute("username");
                credentials.UserName = userNameAttribute != null ? userNameAttribute.Value : string.Empty;

                XAttribute passwordAttribute = fileSystemElement.Attribute("password");
                credentials.Password = passwordAttribute != null ? passwordAttribute.Value : string.Empty;
            }

            return credentials;
        }

        private static TransferDirectoryInfo BuildDirectoryInfo(XElement element, TransferDirection transferDirection)
        {
            TransferDirectoryInfo directoryInfo = new TransferDirectoryInfo();

            string transferDirectionTagName = Enum.GetName(typeof(TransferDirection), transferDirection);
            if (transferDirectionTagName != null)
            {
                string safeTranserDirectionTagName = transferDirectionTagName.ToLower();
                string directoriesXPath = string.Format("/configuration/eis[1]/fileSystem[1]/{0}[1]/directories[1]", safeTranserDirectionTagName);
                
                XElement directoriesRootElement = element.Descendants(safeTranserDirectionTagName).Descendants("directories").FirstOrDefault();

                if (directoriesRootElement == null)
                {
                    throw new EisConfigurationParsingFailedException(directoriesXPath);
                }

                IEnumerable<XElement> directoryElements = directoriesRootElement.Descendants("add");
                foreach (XElement directoryElement in directoryElements)
                {
                    XAttribute directoryIdAttribute = directoryElement.Attribute("id");
                    if (string.Equals(directoryIdAttribute.Value, "root", StringComparison.CurrentCultureIgnoreCase))
                    {
                        directoryInfo.RootPath = directoryElement.Attribute("value").Value;
                    }
                    if (string.Equals(directoryIdAttribute.Value, "dump", StringComparison.CurrentCultureIgnoreCase))
                    {
                        directoryInfo.DumpPath = directoryElement.Attribute("value").Value;
                    }
                    if (string.Equals(directoryIdAttribute.Value, "blackList", StringComparison.CurrentCultureIgnoreCase))
                    {
                        directoryInfo.BlackListPath = directoryElement.Attribute("value").Value;
                    }
                }
            }

            return directoryInfo;
        }

        private static TransferFileInfo BuildFileInfo(XElement element, string configurationFilePath, Eis eis, TransferDirection transferDirection)
        {
            TransferFileInfo fileInfo = new TransferFileInfo();

            string transferDirectionTagName = Enum.GetName(typeof(TransferDirection), transferDirection);
            if (!string.IsNullOrEmpty(transferDirectionTagName))
            {
                string safeTransferDirectionTagName = transferDirectionTagName.ToLower();

                XElement fileElement = (from e in element.Descendants(safeTransferDirectionTagName).Elements("files") select e).FirstOrDefault();

                if (fileElement != null)
                {
                    string containerName = ComposeContainerName(eis.ID, transferDirection, FileSystemLevel.Files);

                    bool zipValue;
                    fileInfo.IsZipped = bool.TryParse(fileElement.Attribute("zip").Value, out zipValue) && zipValue;

                    fileInfo.RulesParser = Container.Build(configurationFilePath, containerName).Resolve<IRulesParsing>();

                    fileInfo.Validators = Container.Build(configurationFilePath, containerName).ResolveAll<IFileValidation>().ToArray();
                }
            }

            return fileInfo;
        }

        private static string ComposeContainerName(string eisID, TransferDirection transferDirection, FileSystemLevel fileSystemLevel)
        {
            return string.Format("{0}-{1}-{2}", eisID, transferDirection, fileSystemLevel).ToLower();
        }

        #endregion
    }
}