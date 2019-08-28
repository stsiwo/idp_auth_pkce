using GraphQL.DataLoader;
using GraphQL.Types;
using OrderingApi.Domain.CartAgg;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.GQL.Types.CustomType
{
    public class CartProductType : ObjectGraphType<CartProductModel>
    {
        public CartProductType()
        {
            Field<StringGraphType, string>().Name("id");
            Field<ProductNameType, ProductNameModel>().Name("name");
            Field<ProductDescriptionType, ProductDescriptionModel>().Name("description");
            Field<StringGraphType, string>().Name("mainImageUrl");
            Field<PriceType, PriceModel>().Name("price");
            Field<StockType, StockModel>().Name("stock");
            // #DOUBT: set and list
            Field<ListGraphType<CartType>, ISet<CartModel>>().Name("carts");
        }
    }
}
