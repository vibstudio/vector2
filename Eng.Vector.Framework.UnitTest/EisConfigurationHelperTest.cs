#region Namespaces

using Eng.Vector.Domain.Model.Integration;
using Eng.Vector.Util;

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace Eng.Vector.Framework.UnitTest
{
    /// <summary>
    ///This is a test class for EisConfigurationHelperTest and is intended
    ///to contain all EisConfigurationHelperTest Unit Tests
    ///</summary>
    [TestClass]
    public class EisConfigurationHelperTest
    {
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for GetEisConfig
        ///</summary>
        [TestMethod]
        public void GetEisConfig()
        {
            EisConfig config = Helper.Factory.Of.Vector.EisConfiguration.EisConfig;

            Assert.IsNotNull(config);
        }
    }
}
