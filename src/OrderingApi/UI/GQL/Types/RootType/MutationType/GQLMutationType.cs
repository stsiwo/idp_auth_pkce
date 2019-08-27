using GraphQL.Types;
using OrderingApi.UI.GQL.Types.RootType.MutationType.MutationField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.GQL.Types.RootType.MutationType
{
    public class GQLMutationType : ObjectGraphType, IGQLMutationType
    {
        public GQLMutationType(IEnumerable<IGQLMutationField> gQLMutationFields)
        {
            foreach (IGQLMutationField gQLField in gQLMutationFields)
            {
                AddField((FieldType)gQLField);
            }
        }
    }
}
