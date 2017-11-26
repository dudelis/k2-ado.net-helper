using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using K2Field.AdoNetHelper.ServiceBroker.ServiceObjects;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;

namespace K2Field.AdoNetHelper.ServiceBroker.Helpers
{
    public class Helper
    {
        public static Property CreateProperty(string name, string description, SoType type)
        {
            Property property = new Property
            {
                Name = name,
                SoType = type,
                Type = MapHelper.GetTypeBySoType(type),
                MetaData = new MetaData(name, description)
            };
            return property;
        }
        public static Method CreateMethod(string name, string description, MethodType methodType)
        {
            Method m = new Method
            {
                Name = name,
                Type = methodType,
                MetaData = new MetaData(name, description)
            };
            return m;
        }
        public static MethodParameter CreateParameter(string name, SoType soType, bool isRequired)
        {
            MethodParameter methodParam = new MethodParameter
            {
                Name = name,
                IsRequired = isRequired,
                MetaData = new MetaData
                {
                    DisplayName = name
                },
                SoType = soType,
                Type = MapHelper.GetTypeBySoType(soType)
            };
            return methodParam;
        }
        public static List<SoDefinition> GetSoDefinition(ServiceBroker sb)
        {
            string soDefinitionString =
                sb.Service.ServiceConfiguration[Constants.ServiceConfig.ServiceObjectsDefinition].ToString();

            List<SoDefinition> returnList = new JavaScriptSerializer().Deserialize<List<SoDefinition>>(soDefinitionString);

            return returnList;
        }
    }
}
