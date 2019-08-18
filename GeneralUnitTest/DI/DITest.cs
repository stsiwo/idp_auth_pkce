using Autofac;
using GeneralUnitTest.DI.TestEntity;
using Xunit;
using Xunit.Abstractions;

namespace GeneralUnitTest.DI
{
    public class DITest
    {
        private readonly ITestOutputHelper _output;

        public DITest(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void DI_Test()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<OrderingApiPublisher>()
                .As<IPublisher>()
                .SingleInstance();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                var pub = scope.Resolve<IPublisher>();

                _output.WriteLine(pub.Name);


                Assert.True(false);
            }

        }
    }
}
