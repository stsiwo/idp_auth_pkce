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
    public class AddressType : ObjectGraphType<AddressModel>
    {
        public AddressType()
        {
            Field<StringGraphType, string>().Name("street");
            Field<StringGraphType, string>().Name("city");
            Field<StringGraphType, string>().Name("state");
            Field<StringGraphType, string>().Name("country");
            Field<StringGraphType, string>().Name("postalCode");
        }
    }
}
