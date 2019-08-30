using Autofac;
using OrderingApi.UI.GQL.Types.RootType.MutationType.MutationField;
using OrderingApi.UI.GQL.Types.RootType.MutationType.MutationField.CartMutation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.GQL.Field.MutationField
{
    public class CreateCartMutationFieldModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateCartMutationField>()
                .As<IGQLMutationField>()
                .SingleInstance();
        }
    }
}
