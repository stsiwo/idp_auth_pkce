using Autofac;
using OrderingApi.Domain.TestAgg;
using OrderingApi.Domain.Validator;
using OrderingApi.Domain.Validator.FieldValidationRule.TestAgg;
using OrderingApi.Domain.Validator.Notification;
using OrderingApi.Domain.Validator.Specification.Builder;
using OrderingApi.Domain.Validator.Specification.Builder.TestAgg;
using OrderingApi.Domain.Validator.Specification.TestAgg.Password;
using OrderingApi.Domain.Validator.Specification.TestAgg.Price;
using OrderingApi.Domain.Validator.TestAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OrderingApiUnitTest.Domain.Validator.TestAgg
{
    public class TestEntityValidatorTest
    {
        [Fact]
        public void Validate_TestFields_ShouldBeSatisfied()
        {
            var builder = new ContainerBuilder();

            // target dependency registration
            builder.RegisterType<HasNumericSpecification>().As<ITestPasswordSpecification>().InstancePerLifetimeScope();
            builder.RegisterType<HasSpecialCharSpecification>().As<ITestPasswordSpecification>().InstancePerLifetimeScope();

            builder.RegisterType<MoreThan1000Specification>().As<ITestPriceSpecification>().InstancePerLifetimeScope();
            builder.RegisterType<LessThan5000Specification>().As<ITestPriceSpecification>().InstancePerLifetimeScope();

            builder.RegisterType<TestPasswordSpecificationBuilder>().As<ISpecificationBuilder<Test, ITestPasswordSpecification>>().InstancePerLifetimeScope();
            builder.RegisterType<TestPriceSpecificationBuilder>().As<ISpecificationBuilder<Test, ITestPriceSpecification>>().InstancePerLifetimeScope();

            builder.RegisterType<DefaultValidationNotification>().As<IValidationNotification>().InstancePerLifetimeScope();

            builder.RegisterType<PasswordFieldValidationRule>()
                .As<ITestFieldValidationRule>()
                .WithParameter("message", "password must include at least one numeric value and special character")
                .WithParameter("field", "Password");

            builder.RegisterType<PriceFieldValidationRule>()
                .As<ITestFieldValidationRule>()
                .WithParameter("message", "price must be more than 1,000 and less than 5,000")
                .WithParameter("field", "Price");

            builder.RegisterType<TestEntityValidator>()
                .As<IEntityValidator<Test, ITestFieldValidationRule>>()
                .InstancePerLifetimeScope();

            // client
            builder.RegisterType<TestEntityValidatorTestClient>().InstancePerLifetimeScope();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // dummy Test Aggregate
                Test testDummy = new Test()
                {
                    Password = "a#1",
                    Price = 1500
                };

                var client = scope.Resolve<TestEntityValidatorTestClient>();

                IValidationNotification notification = client.TestValidate(testDummy);

                Assert.False(notification.HasValidationError());
            }
        }

        [Fact]
        public void Validate_ValidationNotification_ShouldContainOneErrorMessageBecausePasswordDoesNotContainSpecialChar()
        {
            var builder = new ContainerBuilder();

            // target dependency registration
            builder.RegisterType<HasNumericSpecification>().As<ITestPasswordSpecification>().InstancePerLifetimeScope();
            builder.RegisterType<HasSpecialCharSpecification>().As<ITestPasswordSpecification>().InstancePerLifetimeScope();

            builder.RegisterType<MoreThan1000Specification>().As<ITestPriceSpecification>().InstancePerLifetimeScope();
            builder.RegisterType<LessThan5000Specification>().As<ITestPriceSpecification>().InstancePerLifetimeScope();

            builder.RegisterType<TestPasswordSpecificationBuilder>().As<ISpecificationBuilder<Test, ITestPasswordSpecification>>().InstancePerLifetimeScope();
            builder.RegisterType<TestPriceSpecificationBuilder>().As<ISpecificationBuilder<Test, ITestPriceSpecification>>().InstancePerLifetimeScope();

            builder.RegisterType<DefaultValidationNotification>().As<IValidationNotification>().InstancePerLifetimeScope();

            builder.RegisterType<PasswordFieldValidationRule>()
                .As<ITestFieldValidationRule>()
                .WithParameter("message", "password must include at least one numeric value and special character")
                .WithParameter("field", "Password");

            builder.RegisterType<PriceFieldValidationRule>()
                .As<ITestFieldValidationRule>()
                .WithParameter("message", "price must be more than 1,000 and less than 5,000")
                .WithParameter("field", "Price");

            builder.RegisterType<TestEntityValidator>()
                .As<IEntityValidator<Test, ITestFieldValidationRule>>()
                .InstancePerLifetimeScope();

            // client
            builder.RegisterType<TestEntityValidatorTestClient>().InstancePerLifetimeScope();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // dummy Test Aggregate
                Test testDummy = new Test()
                {
                    Password = "a1",
                    Price = 1500
                };

                var client = scope.Resolve<TestEntityValidatorTestClient>();

                IValidationNotification notification = client.TestValidate(testDummy);

                Assert.True(notification.HasValidationError());

                IEnumerable<ValidationError> errors = notification.GetErrors();

                Assert.Single(errors);

                Assert.Equal("Password", errors.FirstOrDefault().Field);
            }
        }

        [Fact]
        public void Validate_ValidationNotification_ShouldContainOneErrorMessageBecausePriceIsLessThan1000()
        {
            var builder = new ContainerBuilder();

            // target dependency registration
            builder.RegisterType<HasNumericSpecification>().As<ITestPasswordSpecification>().InstancePerLifetimeScope();
            builder.RegisterType<HasSpecialCharSpecification>().As<ITestPasswordSpecification>().InstancePerLifetimeScope();

            builder.RegisterType<MoreThan1000Specification>().As<ITestPriceSpecification>().InstancePerLifetimeScope();
            builder.RegisterType<LessThan5000Specification>().As<ITestPriceSpecification>().InstancePerLifetimeScope();

            builder.RegisterType<TestPasswordSpecificationBuilder>().As<ISpecificationBuilder<Test, ITestPasswordSpecification>>().InstancePerLifetimeScope();
            builder.RegisterType<TestPriceSpecificationBuilder>().As<ISpecificationBuilder<Test, ITestPriceSpecification>>().InstancePerLifetimeScope();

            builder.RegisterType<DefaultValidationNotification>().As<IValidationNotification>().InstancePerLifetimeScope();

            builder.RegisterType<PasswordFieldValidationRule>()
                .As<ITestFieldValidationRule>()
                .WithParameter("message", "password must include at least one numeric value and special character")
                .WithParameter("field", "Password");

            builder.RegisterType<PriceFieldValidationRule>()
                .As<ITestFieldValidationRule>()
                .WithParameter("message", "price must be more than 1,000 and less than 5,000")
                .WithParameter("field", "Price");

            builder.RegisterType<TestEntityValidator>()
                .As<IEntityValidator<Test, ITestFieldValidationRule>>()
                .InstancePerLifetimeScope();

            // client
            builder.RegisterType<TestEntityValidatorTestClient>().InstancePerLifetimeScope();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // dummy Test Aggregate
                Test testDummy = new Test()
                {
                    Password = "a1#",
                    Price = 500
                };

                var client = scope.Resolve<TestEntityValidatorTestClient>();

                IValidationNotification notification = client.TestValidate(testDummy);

                Assert.True(notification.HasValidationError());

                IEnumerable<ValidationError> errors = notification.GetErrors();

                Assert.Single(errors);

                Assert.Equal("Price", errors.FirstOrDefault().Field);
            }
        }

        [Fact]
        public void Validate_ValidationNotification_ShouldContainOneErrorMessageBecausePriceIsMoreThan5000AndDoesNotContainNumericValue()
        {
            var builder = new ContainerBuilder();

            // target dependency registration
            builder.RegisterType<HasNumericSpecification>().As<ITestPasswordSpecification>().InstancePerLifetimeScope();
            builder.RegisterType<HasSpecialCharSpecification>().As<ITestPasswordSpecification>().InstancePerLifetimeScope();

            builder.RegisterType<MoreThan1000Specification>().As<ITestPriceSpecification>().InstancePerLifetimeScope();
            builder.RegisterType<LessThan5000Specification>().As<ITestPriceSpecification>().InstancePerLifetimeScope();

            builder.RegisterType<TestPasswordSpecificationBuilder>().As<ISpecificationBuilder<Test, ITestPasswordSpecification>>().InstancePerLifetimeScope();
            builder.RegisterType<TestPriceSpecificationBuilder>().As<ISpecificationBuilder<Test, ITestPriceSpecification>>().InstancePerLifetimeScope();

            builder.RegisterType<DefaultValidationNotification>().As<IValidationNotification>().InstancePerLifetimeScope();

            builder.RegisterType<PasswordFieldValidationRule>()
                .As<ITestFieldValidationRule>()
                .WithParameter("message", "password must include at least one numeric value and special character")
                .WithParameter("field", "Password");

            builder.RegisterType<PriceFieldValidationRule>()
                .As<ITestFieldValidationRule>()
                .WithParameter("message", "price must be more than 1,000 and less than 5,000")
                .WithParameter("field", "Price");

            builder.RegisterType<TestEntityValidator>()
                .As<IEntityValidator<Test, ITestFieldValidationRule>>()
                .InstancePerLifetimeScope();

            // client
            builder.RegisterType<TestEntityValidatorTestClient>().InstancePerLifetimeScope();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                // dummy Test Aggregate
                Test testDummy = new Test()
                {
                    Password = "a#",
                    Price = 6000
                };

                var client = scope.Resolve<TestEntityValidatorTestClient>();

                IValidationNotification notification = client.TestValidate(testDummy);

                Assert.True(notification.HasValidationError());

                IEnumerable<ValidationError> errors = notification.GetErrors();

                Assert.Equal(2, errors.Count());
            }
        }

    }
}
