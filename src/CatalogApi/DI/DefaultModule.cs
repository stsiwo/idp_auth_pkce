using Autofac;
using CatalogApi.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.DI
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<Dep>().As<IDep>().InstancePerLifetimeScope();
        }
    }
}
