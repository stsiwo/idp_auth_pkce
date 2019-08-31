using AutoMapper;
using GraphQL.Resolvers;
using GraphQL.Types;
using OrderingApi.Application.Query;
using OrderingApi.Domain.CartAgg;
using OrderingApi.UI.GQL.Types.CustomType;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.GQL.Types.RootType.QueryType.QueryField.CartQuery
{
    public class GetCartsQueryField : FieldType, IGQLQueryField
    {
        public GetCartsQueryField(ICartQuery cartQuery, IMapper mapper)
        {
            Name = "getCarts";

            Description = "get carts";

            //DefaultValue = new Cart();

            Type = typeof(ListGraphType<CartType>);

            // ?? what is this?? return type of Resolver??
            //ResolvedType = Cart;

            Arguments = new QueryArguments(
                new QueryArgument<ListGraphType<IdGraphType>> { Name = "userIds" }
                );

            Resolver = new AsyncFieldResolver<IList<CartModel>>(async context =>
            {
                IList<Guid> userIds = context.GetArgument<IList<Guid>>("userIds");

                IList<Cart> carts = await cartQuery.GetCartsByIds(userIds);

                return mapper.Map<IList<CartModel>>(carts);

            });

            Metadata = new Dictionary<string, object>()
            {
                { "test-metadata", new Object() }
            };
        }
    }
}
