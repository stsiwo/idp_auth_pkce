using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.AutoMapper
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var myAssembly = typeof(Program).Assembly;

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                // assembly scanning; don't need to register each profile
                cfg.AddMaps(myAssembly);

            }).CreateMapper())
                .As<IMapper>()
                .SingleInstance();

        }
    }
}
