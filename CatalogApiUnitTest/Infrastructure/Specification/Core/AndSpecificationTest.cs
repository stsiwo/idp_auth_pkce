using api = CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.Specification.Core;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace CatalogApiUnitTest.Infrastructure.Specification.Core
{
    public class AndSpecificationTest
    {
        [Fact]
        public void ToExpression_TwoExpression_ShouldBeConnectedWithAnd()
        {
            // arrange
            var leftSpecificationStub = new Mock<ISpecification<api.TestEntity>>();
            leftSpecificationStub.Setup(foo => foo.ToExpression()).Returns(p => p.Price > 0.0m);

            var rightSpecificationStub = new Mock<ISpecification<api.TestEntity>>();
            rightSpecificationStub.Setup(foo => foo.ToExpression()).Returns(p => p.Price < 10.0m);

            var testEntityDummy = new api.TestEntity()
            {
                Name = "satoshi",
                Price = 5.00m,
            };

            Expression<Func<api.TestEntity, bool>> expectedResult = p => p.Price > 0.0m && p.Price < 10.0m;

            // act 
            CompositeSpecification<api.TestEntity> sut = new AndSpecification<api.TestEntity>(leftSpecificationStub.Object, rightSpecificationStub.Object);
            var result = sut.ToExpression();

            Func<api.TestEntity, bool> sutDelegate = result.Compile();

            // assert
            Assert.True(sutDelegate(testEntityDummy));
        }

        [Fact]
        public void ToExpression_TestEntity_ShouldBeExcludedByAndCondition()
        {
            // arrange
            var leftSpecificationStub = new Mock<ISpecification<api.TestEntity>>();
            leftSpecificationStub.Setup(foo => foo.ToExpression()).Returns(p => p.Price > 0.0m);

            var rightSpecificationStub = new Mock<ISpecification<api.TestEntity>>();
            rightSpecificationStub.Setup(foo => foo.ToExpression()).Returns(p => p.Price < 10.0m);

            var testEntityDummy = new api.TestEntity()
            {
                Name = "satoshi",
                Price = 10.00m,
            };

            Expression<Func<api.TestEntity, bool>> expectedResult = p => p.Price > 0.0m && p.Price < 10.0m;

            // act 
            CompositeSpecification<api.TestEntity> sut = new AndSpecification<api.TestEntity>(leftSpecificationStub.Object, rightSpecificationStub.Object);
            var result = sut.ToExpression();

            Func<api.TestEntity, bool> sutDelegate = result.Compile();

            // assert
            Assert.False(sutDelegate(testEntityDummy));

        }
    }
}
