using CatalogApi.Infrastructure.DataEntity;
using CatalogApiUnitTest.TestData;
using System;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Collections.Generic;
using Xunit.Abstractions;
using Newtonsoft.Json;
using CatalogApiUnitTest.Extensioins;
using System.Linq;
using Autofac;

namespace CatalogApiUnitTest
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
        }

        private Object myObject = new Object();
        [Fact]
        public void RefKeyWordTest()
        {
            Object HisObject = myObject;
            // should be same
            Assert.Equal(HisObject.GetHashCode(), myObject.GetHashCode());

            CalledMethod(myObject);

            // same
            Assert.Equal(HisObject.GetHashCode(), myObject.GetHashCode());



        }

        private void CalledMethod(Object yourObject)
        {
            // same
            Assert.Equal(myObject.GetHashCode(), yourObject.GetHashCode());

            yourObject = new Object();

            //Assert.Equal(myObject.GetHashCode(), yourObject.GetHashCode());

        }

        [Fact] 
        public void ToInt32_Convertor_ShouldOnlyConvertNumberString()
        {
            Assert.Equal(1, Convert.ToInt32("1"));
        }

        [Fact]
        public async void IAsyncQueryable_HowItWorks()
        {
            IQueryable<TestEntity> tests = new TestAsyncEnumerable<TestEntity>(new List<TestEntity>()
            {
                new TestEntity()
                {
                    Name = "satoshi"
                },
                new TestEntity()
                {
                    Name = "kaoru"
                }
            }.AsQueryable());

            IList<TestEntity> result = await tests.Where(t => t.Name == "satoshi").ToListAsync();

            _output.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));

            Assert.Equal("", JsonConvert.SerializeObject(result, Formatting.Indented));
            

        }

        [Fact]
        public async void IAsyncQuerable_ThroughDbContext()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TestDbContext>()
                .WithParameter("options", new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase("testDB").Options)
                .InstancePerLifetimeScope();

            using (var container = builder.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                TestDbContext context = scope.Resolve<TestDbContext>();
                context.Database.EnsureCreated();

                context.AddRange(new List<TestEntity>()
                {
                    new TestEntity()
                    {
                        Name = "satoshi"
                    },
                    new TestEntity()
                    {
                        Name = "hitomi"
                    },
                });

                context.SaveChanges();

                var tests = await context.TestEntities.Where(t => t.Name == "satoshi").ToListAsync();

                _output.WriteLine(JsonConvert.SerializeObject(tests));

                Assert.True(false);

            }

        }


    }
}
