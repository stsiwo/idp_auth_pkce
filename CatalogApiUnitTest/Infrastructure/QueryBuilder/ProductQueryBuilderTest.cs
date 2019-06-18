using AutoMapper;
using CatalogApi.Application.DTO;
using CatalogApi.Application.Repository;
using CatalogApi.Infrastructure;
using CatalogApi.Infrastructure.DataEntity;
using CatalogApi.Infrastructure.QueryBuilder;
using CatalogApi.Infrastructure.Repository;
using CatalogApiUnitTest.Configs;
using CatalogApiUnitTest.TestData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CatalogApiUnitTest.Infrastructure.Repository
{
    /**
     * things you need to test:
     *   1. test OrderClause (Where Clause is tested by another class)
     *      1.1. sort based on CreationDate (ASC)
     *      1.2. sort based on CreationDate (DESC) 
     *      1.3. sort based on Price (ASC: cheaper first)
     *      1.4. sort based on Price (DESC: more expensive first) 
     *      1.5. sort based on Name (ASC)
     *      1.6. sort based on Name (DESC) 
     *      1.7. sort based on # of Reviews (ASC)
     *      1.8. sort based on # of Reviews (DESC) 
     *      1.9. sort based on Average Score of total Reviews (ASC)
     *      1.10. sort based on Average Score of total Reviews (DESC) 
     *      1.11. sort based on default (1.1) if the query string include nothing or anthing other than specified one 
     **/
     
    public class ProductQueryBuilderTest
    {

        //   1.1. sort based on CreationDate (ASC)
        [Fact]
        public async void Build_IQueryable_ShouldConstructedWithoutWhereAndOrderByClause() 
        {
            // arrange 
            // 0. DbContextOptionsBuilder (use in-memory)
            var optionsBuilder = new DbContextOptionsBuilder<CatalogApiDbContext>().UseInMemoryDatabase("testDB");
            // 1. CatalogApiDbContext (use real implementation)  
            CatalogApiDbContext contextStub = new CatalogApiDbContext(optionsBuilder.Options);




            // act 

            // assert

        }
    }
}
