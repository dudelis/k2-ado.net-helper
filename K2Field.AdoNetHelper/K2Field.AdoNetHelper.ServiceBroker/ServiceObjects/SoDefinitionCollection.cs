using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2Field.AdoNetHelper.ServiceBroker.ServiceObjects
{
    public class SoDefinitionCollection
    {
        public List<SoDefinition> Items { get; set; }

        public SoDefinitionCollection(List<SoDefinition> soDefinitionCollection)
        {
            Items = soDefinitionCollection;
        }

        public SoDefinition GetSoDefinitionByName(string name)
        {
            var soDefinition = Items.FirstOrDefault(item => item.SoName == name);
            return soDefinition;
        }
    }
}
