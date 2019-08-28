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
    public class ProductDescriptionType : ObjectGraphType<ProductDescriptionModel>
    {
        public ProductDescriptionType()
        {
            Field<StringGraphType, string>().Name("fullDescription");
        }
    }
}
