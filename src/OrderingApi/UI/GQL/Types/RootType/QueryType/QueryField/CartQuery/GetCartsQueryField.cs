using GraphQL.Resolvers;
using GraphQL.Types;
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

            Resolver = new FuncFieldResolver<IList<CartModel>>(context =>
            {
                return new List<CartModel>()
                {
                    new CartModel()
                    {
                        Id = Guid.NewGuid().ToString(),
                        User = new UserModel()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = new NameModel()
                            {
                                FirstName = "satoshi",
                                LastName = "iwao"
                            },
                            HomeAddress = new AddressModel()
                            {
                                Street = "Street",
                                City = "City",
                                State = "State",
                                Country = "Country",
                                PostalCode = "PostalCode",
                            },
                            ContactInfo = new ContactModel()
                            {
                                HomeNumber = "2342342342342",
                            },
                            Orders = new HashSet<OrderModel>()
                            {
                                new OrderModel()
                                {
                                    Id = "order-id",
                                    Status = "pending",
                                    User = null,
                                    Products = new HashSet<OrderProductModel>()
                                    {
                                        new OrderProductModel()
                                        {
                                            Id = "order-product",
                                            Name = new ProductNameModel()
                                            {
                                                FullName = "full-name",
                                            },
                                            Price = new PriceModel()
                                            {
                                                StandardPrice = 2342342.00m,
                                            },
                                            Stock = new StockModel()
                                            {
                                                CurrentStock = 5,
                                                AvailableStock = 1,
                                            },
                                            Orders = null
                                        }
                                    }
                                }
                            }
                        },
                        Products = new HashSet<CartProductModel>()
                        {
                            new CartProductModel()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = new ProductNameModel()
                                {
                                    FullName = "cart product name"
                                },
                                Description = new ProductDescriptionModel()
                                {
                                    FullDescription = "full desc"
                                },
                                MainImageUrl = "main image url",
                                Price = new PriceModel()
                                {
                                    StandardPrice = 43243242.00m
                                },
                                Stock = new StockModel()
                                {
                                    CurrentStock = 3,
                                    AvailableStock = 1
                                },
                                Carts = new HashSet<CartModel>()
                                {
                                    new CartModel()
                                    {
                                        Id = "cart id"
                                    }
                                }
                            }
                        }
                    },
                    new CartModel()
                    {
                        Id = Guid.NewGuid().ToString(),
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
