using Autofac;
using Newtonsoft.Json;
using OrderingApiUnitTest.Adapter.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace OrderingApiUnitTest.Adapter
{
    public class AutofacAdapterTest
    {
        private readonly ITestOutputHelper _output;
        public AutofacAdapterTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Adapter_Test()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SaveCommand>()
                .As<ICommand>()
                .InstancePerDependency();

            builder.RegisterType<DeleteCommand>()
                .As<ICommand>()
                .InstancePerDependency();

            builder.RegisterAdapter<ICommand, ToolBarButton>(cmd => new ToolBarButton(cmd))
                .InstancePerDependency();

            builder.RegisterType<EditWindow>()
                .InstancePerDependency();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                var window = scope.Resolve<EditWindow>();
                var result = window.Test();

                // DOES NOT WORK!!!!
                _output.WriteLine(JsonConvert.SerializeObject(result));

                Assert.True(false);
            }
        }
    }
}
