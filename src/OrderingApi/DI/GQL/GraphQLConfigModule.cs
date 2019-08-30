using Autofac;
using GraphQL;
using GraphQL.DataLoader;
using OrderingApi.UI.GQL;
using OrderingApi.UI.GQL.Types.CustomType;
using OrderingApi.UI.GQL.Types.RootType.MutationType;
using OrderingApi.UI.GQL.Types.RootType.MutationType.MutationField;
using OrderingApi.UI.GQL.Types.RootType.MutationType.MutationField.CartMutation;
using OrderingApi.UI.GQL.Types.RootType.QueryType;
using OrderingApi.UI.GQL.Types.RootType.QueryType.QueryField;
using OrderingApi.UI.GQL.Types.RootType.QueryType.QueryField.CartQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.GQL
{
    public class GraphQLConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // document executor
            builder.RegisterType<DocumentExecuter>()
                .As<IDocumentExecuter>()
                // should be per thread/request??
                // #DOUBT
                .InstancePerLifetimeScope();

            // schema 
            builder.RegisterType<GQLSchema>()
                .SingleInstance();

            // query type
            builder.RegisterType<GQLQueryType>()
                .As<IGQLQueryType>()
                .SingleInstance();

            // mutation type
            builder.RegisterType<GQLMutationType>()
                .As<IGQLMutationType>()
                .SingleInstance();

            // dataloader (to avoid N+1 problem query)
            builder.RegisterType<DataLoaderContextAccessor>()
                .As<IDataLoaderContextAccessor>()
                .SingleInstance();

            builder.RegisterType<DataLoaderDocumentListener>()
                .SingleInstance();
        }
    }
}
