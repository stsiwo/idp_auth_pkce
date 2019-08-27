using GraphQL.Resolvers;
using GraphQL.Types;
using OrderingApi.Domain.CartAgg;
using OrderingApi.UI.GQL.Types.CustomType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.GQL.Types.RootType.QueryType.QueryField.CartQuery
{
    public class GetCartsQueryField : FieldType, IGQLQueryField
    {
        public GetCartsQueryField(/*ICartRepository personRepository*/)
        {
            Name = "getCarts";

            Description = "get carts";

            //DefaultValue = new Cart();

            Type = typeof(ListGraphType<CartType>);

            // ?? what is this?? return type of Resolver??
            //ResolvedType = Cart;

            Arguments = new QueryArguments(
                new QueryArgument<StringGraphType> { Name = "userId" });

            Resolver = new FuncFieldResolver<IList<Cart>>(context =>
            {
                return new List<Cart>()
                {
                    new Cart()
                    {
                        Id = Guid.NewGuid(),
                    },
                    new Cart()
                    {
                        Id = Guid.NewGuid(),
                    },
                };
            });

            Metadata = new Dictionary<string, object>()
            {
                { "test-metadata", new Object() }
            };
        }
    }
}
