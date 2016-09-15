#region Namespaces

using System;
using System.Collections.Generic;
using System.Configuration;

using Eng.Diagnostic;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

#endregion

namespace Eng.Vector.IoC
{
    public class Container
    {
        #region Fields

        private static string _configurationFilePath;

        private static string _targetContainer;

        private static string[] _secondaryContainers;
        
        private static readonly IDictionary<string, IUnityContainer> containers = new Dictionary<string, IUnityContainer>();

        #endregion

        #region Ctor(s)

        private Container(string configurationFilePath, string targetContainer, params string[] secondaryContainers)
        {
            _configurationFilePath = configurationFilePath;
            _targetContainer = targetContainer;
            _secondaryContainers = secondaryContainers;
        }

        #endregion

        public static Container Build(string configurationFilePath, string targetContainer, params string[] secondaryContainers)
        {
            return new Container(configurationFilePath, targetContainer, secondaryContainers);
        }

        public T Resolve<T, TK>() where TK : T, new()
        {
            if (containers.ContainsKey(_targetContainer))
            {
                return containers[_targetContainer].Resolve<T>();
            }

            try
            {
                containers.Add(_targetContainer, BuildContainer());
                return containers[_targetContainer].Resolve<T>();
            }
            catch (Exception exception)
            {
                Trace.Write.Warn(exception.Message, typeof(Container), "Resolve<T, TK>");
                return new TK();
            }
        }

        public T Resolve<T>()
        {
            if (containers.ContainsKey(_targetContainer))
            {
                return containers[_targetContainer].Resolve<T>();
            }

            try
            {
                containers.Add(_targetContainer, BuildContainer());
                return containers[_targetContainer].Resolve<T>();
            }
            catch (Exception exception)
            {
                Trace.Write.Warn(exception.Message, typeof(Container), "Resolve<T>");
                return default(T);
            }
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            if (containers.ContainsKey(_targetContainer))
            {
                return containers[_targetContainer].ResolveAll<T>();
            }

            try
            {
                containers.Add(_targetContainer, BuildContainer());
                return containers[_targetContainer].ResolveAll<T>();
            }
            catch (Exception exception)
            {
                Trace.Write.Warn(exception.Message, typeof(Container), "ResolveAll<T>");
                return new List<T> { default(T) };
            }
        }

        private static IUnityContainer BuildContainer()
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap { ExeConfigFilename = _configurationFilePath };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection("unity");

            IUnityContainer container = new UnityContainer();
            foreach (string containerName in _secondaryContainers)
            {
                section.Configure(container, containerName);
            }
            section.Configure(container, _targetContainer);

            return container;
        }
    }
}