using GraphQL.DataLoader;
using GraphQL.Types;
using OrderingApi.Domain.UserAgg;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.GQL.Types.CustomType
{
    public class UserType : ObjectGraphType<UserModel>
    {
        public UserType(IDataLoaderContextAccessor dataLoaderContextAccessor /*,IPostRepository postRepository*/)
        {
            Field<StringGraphType, string>().Name("id");
            Field<NameType, NameModel>().Name("name");
            Field<AddressType, AddressModel>().Name("address");
            Field<ContactType, ContactModel>().Name("contactInfo");
            Field<CartType, CartModel>().Name("cart");
            Field<ListGraphType<OrderType>, ISet<OrderModel>>().Name("orders");
        }
    }
}
