using Autofac;
using OrderingApi.Domain.TestAgg;
using OrderingApi.Domain.Validator.Specification.Builder;
using OrderingApi.Domain.Validator.Specification.Builder.TestAgg;
using OrderingApi.Domain.Validator.Specification.TestAgg.Password;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OrderingApiUnitTest.Domain.Validator.Specification.Builder.TestAgg
{
    public class TestPasswordSpecificationBuilderTest
    {
        [Fact]
        public void TestValidate_TestPasswordSpecifications_ShouldBeSatisfiedCondition()
        {
            var builder = new ContainerBuilder();

            // target dependency registration
            builder.RegisterType<HasNumericSpecification>().As<ITestPasswordSpecification>().InstancePerLifetimeScope();
            builder.RegisterType<HasSpecialCharSpecification>().As<ITestPasswordSpecification>().InstancePerLifetimeScope();

            builder.RegisterType<TestPasswordSpecificationBuilder>().As<ISpecificationBuilder<Test, ITestPasswordSpecification>>().InstancePerLifetimeScope();

            // client
            builder.RegisterType<TestPasswordSpecificationBuilderTestClient>().InstancePerLifetimeScope();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // dummy Test Aggregate
                Test testDummy = new Test()
                {
                    Password = "a1@"
                };

                var client = scope.Resolve<TestPasswordSpecificationBuilderTestClient>();


                Assert.True(client.TestValidate(testDummy));
            }

        }

        [Fact]
        public void TestValidate_TestPasswordSpecifications_ShouldNotBeSatisfiedBecauseOfLackOfSpecialCharacter()
        {
            var builder = new ContainerBuilder();

            // target dependency registration
            builder.RegisterType<HasNumericSpecification>().As<ITestPasswordSpecification>().InstancePerLifetimeScope();
            builder.RegisterType<HasSpecialCharSpecification>().As<ITestPasswordSpecification>().InstancePerLifetimeScope();

            builder.RegisterType<TestPasswordSpecificationBuilder>().As<ISpecificationBuilder<Test, ITestPasswordSpecification>>().InstancePerLifetimeScope();

            // client
            builder.RegisterType<TestPasswordSpecificationBuilderTestClient>().InstancePerLifetimeScope();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // dummy Test Aggregate
                Test testDummy = new Test()
                {
                    Password = "a1"
                };

                var client = scope.Resolve<TestPasswordSpecificationBuilderTestClient>();


                Assert.False(client.TestValidate(testDummy));
            }
        }

        [Fact]
        public void TestValidate_TestPasswordSpecifications_ShouldNotBeSatisfiedBecauseOfLackOfNumericValue()
        {
            var builder = new ContainerBuilder();

            // target dependency registration
            builder.RegisterType<HasNumericSpecification>().As<ITestPasswordSpecification>().InstancePerLifetimeScope();
            builder.RegisterType<HasSpecialCharSpecification>().As<ITestPasswordSpecification>().InstancePerLifetimeScope();

            builder.RegisterType<TestPasswordSpecificationBuilder>().As<ISpecificationBuilder<Test, ITestPasswordSpecification>>().InstancePerLifetimeScope();

            // client
            builder.RegisterType<TestPasswordSpecificationBuilderTestClient>().InstancePerLifetimeScope();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // dummy Test Aggregate
                Test testDummy = new Test()
                {
                    Password = "a#"
                };

                var client = scope.Resolve<TestPasswordSpecificationBuilderTestClient>();


                Assert.False(client.TestValidate(testDummy));
            }
        }
    }
}
