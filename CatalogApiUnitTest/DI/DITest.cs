using Autofac;
using CatalogApi.Test;
using System;
using Xunit;

namespace CatalogApiUnitTest
{
    public class DITest
    {
        [Fact]
        public void DI_Test()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Dep>().As<IDep>()
                   .InstancePerLifetimeScope();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                var dep1 = scope.Resolve<IDep>();
                var dep2 = scope.Resolve<IDep>();

                Assert.Equal(dep1, dep2);
            }

        }

    }
}
