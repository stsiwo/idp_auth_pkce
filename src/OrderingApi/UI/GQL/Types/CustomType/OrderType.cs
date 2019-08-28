using GraphQL.DataLoader;
using GraphQL.Types;
using OrderingApi.Domain.OrderAgg;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.GQL.Types.CustomType
{
    public class OrderType : ObjectGraphType<OrderModel>
    {
        public OrderType(IDataLoaderContextAccessor dataLoaderContextAccessor /*,IPostRepository postRepository*/)
        {
            Field<StringGraphType, Guid>().Name("id");
            Field<StringGraphType, string>().Name("status");
            Field<UserType, UserModel>().Name("user");
            Field<ListGraphType<OrderProductType>, ISet<OrderProductModel>>().Name("products");

        }
    }
}
