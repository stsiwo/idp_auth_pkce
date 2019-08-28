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
    public class CartType : ObjectGraphType<CartModel>
    {
        public CartType(IDataLoaderContextAccessor dataLoaderContextAccessor /*,IPostRepository postRepository*/)
        {
            Field<StringGraphType, Guid>().Name("id");
            Field<UserType, UserModel>().Name("users");
            Field<ListGraphType<CartProductType>, ISet<CartProductModel>>().Name("products");
        }
    }
}
