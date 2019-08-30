using Autofac;
using OrderingApi.UI.GQL.Types.RootType.QueryType.QueryField;
using OrderingApi.UI.GQL.Types.RootType.QueryType.QueryField.CartQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.GQL.Field.QueryField
{
    public class GetCartsQueryFieldModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GetCartsQueryField>()
                .As<IGQLQueryField>()
                .SingleInstance();
        }
    }
}
