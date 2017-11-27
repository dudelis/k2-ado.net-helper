﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K2Field.AdoNetHelper.ServiceBroker.Helpers;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;

namespace K2Field.AdoNetHelper.ServiceBroker.ServiceObjects
{
    public class PropertyDefinition
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public SoType SoType => MapHelper.GetSoTypeByName(Type);

    }
}
