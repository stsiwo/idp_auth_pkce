using Autofac;
using AutoMapper;
using CatalogApi.Configs.AutoMapper;

namespace CatalogApi.DI
{
    public class SingletonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //   1. AutoMapper (AutoMapper is only sharable by singleton; can't use as like "InstancePerDependency"
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile<DefaultProfile>();
            });
            var mapper = mapperConfig.CreateMapper(); 
            builder.RegisterInstance(mapper).As<IMapper>().SingleInstance();
        }
    }
}
