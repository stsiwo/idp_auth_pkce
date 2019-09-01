using GraphQL.Resolvers;
using GraphQL.Types;
using MediatR;
using OrderingApi.Domain.CartAgg;
using OrderingApi.Application.Command;
using OrderingApi.UI.GQL.Types.CustomType;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.GQL.Types.RootType.MutationType.MutationField.CartMutation
{
    public class AddProductsToCartMutationField : FieldType, IGQLMutationField
    {
        public AddProductsToCartMutationField(/*ICartRepository personRepository*/ IMediator mediator)
        {
            Name = "AddProductsToCart";

            Description = "add products to cart";

            //DefaultValue = new Cart();

            Type = typeof(CartType);

            // ?? what is this?? return type of Resolver??
            //ResolvedType = Cart;

            Arguments = new QueryArguments
                (
                    new QueryArgument<IdGraphType> { Name = "userId" }
                    , new QueryArgument<ListGraphType<IdGraphType>> { Name = "productIds" }
                );

            Resolver = new AsyncFieldResolver<CartModel>(async context => 
            {
                Guid userId = context.GetArgument<Guid>("userId");
                IList<Guid> productIds = context.GetArgument<IList<Guid>>("productIds");

                // need to map argument to command here

                var result = await mediator.Send(new AddProductsToCartCommand()
                {
                    UserId = userId,
                    ProductIds = productIds
                });

                // MediatR Pipeline does validation of inputs and logging as AOP or use GQL validation

                return result;

            });

            Metadata = new Dictionary<string, object>()
            {
                { "test-metadata", new Object() }
            };
        }
    }
}
