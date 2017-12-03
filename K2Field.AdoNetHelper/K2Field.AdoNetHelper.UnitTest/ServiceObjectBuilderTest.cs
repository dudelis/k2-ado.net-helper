using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using K2Field.AdoNetHelper.ServiceBroker.Helpers;

namespace K2Field.AdoNetHelper.UnitTest
{
    [TestClass]
    public class ServiceObjectBuilderTest
    {
        [TestMethod]
        public void ServiceObjectBase_Create()
        {
            string name = "So_1";

            var soBuilder = new ServiceObjectBuilder();
        }
    }
}
