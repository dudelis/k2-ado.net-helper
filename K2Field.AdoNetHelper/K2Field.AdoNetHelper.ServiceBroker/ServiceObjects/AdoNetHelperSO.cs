using System;
using System.Collections.Generic;
using System.Data;
using K2Field.AdoNetHelper.ServiceBroker.Constants;
using K2Field.AdoNetHelper.ServiceBroker.Helpers;
using SourceCode.Data.SmartObjectsClient;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;

namespace K2Field.AdoNetHelper.ServiceBroker.ServiceObjects
{
    public class AdoNetHelperSo : ServiceObjectBase
    {

        public AdoNetHelperSo(ServiceBroker broker, SoDefinitionCollection soCollection) : base(broker, soCollection)
        {
        }

        public override List<ServiceObject> DescribeServiceObjects()
        {
            var sObjects = new List<ServiceObject>();

            foreach (var item in SoDefCollection.Items)
            {
                var so = new ServiceObject()
                {
                    Name = item.SoName,
                    MetaData = new MetaData(item.SoName, $"Service Object {item.SoName}, created on the basis of the Service Object Definition configuration parameter"),
                    Active = true
                };
                var mExecuteAdoQuery = Helper.CreateMethod(Constants.Methods.ExecuteAdoQuery, "Executes Ado.Net query",
                    MethodType.List);
                mExecuteAdoQuery.MethodParameters.Create(Helper.CreateParameter(Parameters.AdoQuery,
                    SoType.Memo, true));
                so.Methods.Add(mExecuteAdoQuery);
                foreach (var prop in item.Properties)
                {
                    so.Properties.Add(Helper.CreateProperty(prop.Name, prop.Name, prop.SoType));
                    mExecuteAdoQuery.ReturnProperties.Add(prop.Name);
                }
                sObjects.Add(so);
            }

            return sObjects;

        }

        public override void Execute()
        {
            switch (base.ServiceBroker.Service.ServiceObjects[0].Methods[0].Name)
            {
                case Constants.Methods.ExecuteAdoQuery:
                    ExecuteAdoQuery();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void ExecuteAdoQuery()
        {
            var adoQuery = GetStringParameter(Constants.Parameters.AdoQuery);
            SoDefinition soDefinition =
                SoDefCollection.GetSoDefinitionByName(ServiceBroker.Service.ServiceObjects[0].Name);
            ServiceBroker.Service.ServiceObjects[0].Properties.InitResultTable();
            DataTable results = ServiceBroker.ServicePackage.ResultTable;
            DataTable dataTable = new DataTable();

            using (SOConnection soConnection = new SOConnection(ServiceBroker.K2Connection.GetSOConnectionString()))
            using (SOCommand command = new SOCommand(adoQuery, soConnection))
            using (SODataAdapter adapter = new SODataAdapter(command))
            {
                soConnection.Open();
                adapter.Fill(dataTable);
            }
            foreach (DataRow dr in dataTable.Rows)
            {
                DataRow dRow = results.NewRow();
                foreach (var prop in soDefinition.Properties)
                {
                    dRow[prop.Name] = dr[prop.Name];
                }
                results.Rows.Add(dRow);
            }
        }
    }
}
