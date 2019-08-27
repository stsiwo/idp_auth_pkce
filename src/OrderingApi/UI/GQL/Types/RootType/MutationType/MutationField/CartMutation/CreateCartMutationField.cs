using GraphQL.Resolvers;
using GraphQL.Types;
using OrderingApi.Domain.CartAgg;
using OrderingApi.UI.GQL.Types.CustomType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.GQL.Types.RootType.MutationType.MutationField.CartMutation
{
    public class CreateCartMutationField : FieldType, IGQLMutationField
    {
        public CreateCartMutationField(/*ICartRepository personRepository*/)
        {
            Name = "CreateCart";

            Description = "get all person";

            //DefaultValue = new Cart();

            Type = typeof(CartType);

            // ?? what is this?? return type of Resolver??
            //ResolvedType = Cart;

            Arguments = null;

            Resolver = new FuncFieldResolver<Cart>(context => new Cart()
            {
                Id = Guid.NewGuid()
            });

            Metadata = new Dictionary<string, object>()
            {
                { "test-metadata", new Object() }
            };
        }
    }
}
