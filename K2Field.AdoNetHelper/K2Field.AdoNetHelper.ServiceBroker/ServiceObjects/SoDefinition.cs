using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2Field.AdoNetHelper.ServiceBroker.ServiceObjects
{
    public class SoDefinition
    {
        public string Name { get; set; }
        public List<PropertyDefinition> Properties { get; set; }

        public SoDefinition()
        {
            Properties = new List<PropertyDefinition>();
        }
    }
}
