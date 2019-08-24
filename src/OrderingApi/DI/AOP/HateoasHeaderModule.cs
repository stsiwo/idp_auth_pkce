using Autofac;
using OrderingApi.Config.AOP.ASPFilter.HateoasHeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.AOP
{
    public class HateoasHeaderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AddCartToUserHateoasHeader>()
                .As<IHateoasHeader>()
                .SingleInstance();
        }
    }
}
