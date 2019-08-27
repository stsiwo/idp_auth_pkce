using GraphQL.Types;
using OrderingApi.UI.GQL.Types.RootType.QueryType.QueryField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.GQL.Types.RootType.QueryType
{
    public class GQLQueryType : ObjectGraphType, IGQLQueryType
    {
        public GQLQueryType(IEnumerable<IGQLQueryField> gQLQueryFields)
        {
            foreach (IGQLQueryField gQLField in gQLQueryFields)
            {
                AddField((FieldType)gQLField);
            }
        }
    }
}
