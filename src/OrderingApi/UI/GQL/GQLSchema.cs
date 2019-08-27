using GraphQL;
using GraphQL.Types;
using OrderingApi.UI.GQL.Types.RootType.MutationType;
using OrderingApi.UI.GQL.Types.RootType.QueryType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.GQL
{
    public class GQLSchema : Schema
    {
        public GQLSchema(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            Query = dependencyResolver.Resolve<IGQLQueryType>();
            Mutation = dependencyResolver.Resolve<IGQLMutationType>();
        }
    }
}
