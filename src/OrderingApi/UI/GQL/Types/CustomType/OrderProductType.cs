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
    public class OrderProductType : ObjectGraphType<OrderProductModel>
    {
        public OrderProductType()
        {
            Field<StringGraphType, string>().Name("id");
            Field<ProductNameType, ProductNameModel>().Name("name");
            Field<PriceType, PriceModel>().Name("price");
            Field<StockType, StockModel>().Name("stock");
            // #DOUBT: set and list
            Field<ListGraphType<OrderType>, ISet<OrderModel>>().Name("orders");
        }
    }
}
