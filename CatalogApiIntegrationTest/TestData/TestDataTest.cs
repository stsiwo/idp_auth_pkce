using CatalogApi.Infrastructure.DataEntity;
using CatalogApiIntegrationTest.TestData.Entity;
using Newtonsoft.Json;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace CatalogApiIntegrationTest.TestData
{
    public class TestDataTest
    {
        private readonly ITestOutputHelper _output;

        public TestDataTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void GetCategoryFaker_AmountBeyondMaxCategory_ShouldBeFallBackToMaxCategoryAmount() 
        {
            var categories = CategoryFaker.GetCategoryList(12);

            _output.WriteLine(JsonConvert.SerializeObject(categories, Formatting.Indented));

            Assert.Equal(10, categories.Count);
        }

        [Fact]
        public void GetCategoryFaker_AmountLessThanMaxCategoryAmount_ShouldKeepItsAmount() 
        {
            var categories = CategoryFaker.GetCategoryList(3);

            _output.WriteLine(JsonConvert.SerializeObject(categories, Formatting.Indented));

            Assert.Equal(3, categories.Count);
        }

        [Fact]
        public void GetSubCategoryFaker_AmountBeyondMaxSubCategory_ShouldBeFallBackToMaxSubCategoryAmount() 
        {
            var subCategories = SubCategoryFaker.GetSubCategoryList(100);

            _output.WriteLine(JsonConvert.SerializeObject(subCategories, Formatting.Indented));

            Assert.Equal(55, subCategories.Count);
        }

        [Fact]
        public void GetSubCategoryFaker_AmountLessThanMaxSubCategoryAmount_ShouldKeepItsAmount() 
        {
            var subCategories = SubCategoryFaker.GetSubCategoryList(4);

            _output.WriteLine(JsonConvert.SerializeObject(subCategories, Formatting.Indented));

            Assert.Equal(4, subCategories.Count);
        }

        [Fact]
        public void GetSubCategoryFaker_AllCategoryId_ShouldBeIncluded() 
        {
            int totalSubCategories = Enum.GetNames(typeof(SubCategoryConstants)).Length;
            var subCategories = SubCategoryFaker.GetSubCategoryList(totalSubCategories);

            _output.WriteLine(JsonConvert.SerializeObject(subCategories, Formatting.Indented));

            var result = subCategories.GroupBy(s => s.CategoryId).OrderBy(g => g.Key).Select(g => (int)g.Key);
            var totalCategoryNumbers = Enum.GetNames(typeof(CategoryConstants)).Length;
            var categoryIdList = Enumerable.Range(0, totalCategoryNumbers);

            Assert.Equal(JsonConvert.SerializeObject(result), JsonConvert.SerializeObject(categoryIdList));
        }

        [Fact]
        public void GetSubCategoryFaker_CategoryId_ShouldMatchWithCategoryFakerId() 
        {
            var subCategories = SubCategoryFaker.GetSubCategoryList(100);

            _output.WriteLine(JsonConvert.SerializeObject(subCategories, Formatting.Indented));

            var isCategoryIdEqual = subCategories.All(s => s.CategoryId == s.Category.Id);

            Assert.True(isCategoryIdEqual);
        }

        [Fact]
        public void GetSubCategoryFaker_CategoryId_ShouldBelessThanMaxCategoryNumbers() 
        {
            var subCategories = SubCategoryFaker.GetSubCategoryList(100);

            _output.WriteLine(JsonConvert.SerializeObject(subCategories, Formatting.Indented));

            var result = subCategories.All(s => (int)s.CategoryId <= 10);

            Assert.True(result);
        }

        [Fact]
        public void GetProductFaker_CategoryId_ShouldBelessThanMaxCategoryNumbers() 
        {
            var products = ProductFaker.GetProductList(100);

            _output.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

            var result = products.All(p => p.SubCategory.Id == p.SubCategoryId);

            Assert.True(result);
        }

        [Fact]
        public void GetProductFaker_AllSubCategoryId_ShouldBeIncluded() 
        {
            var products = ProductFaker.GetProductList(100); 

            var result = products.GroupBy(p => p.SubCategoryId).OrderBy(g => g.Key).Select(g => (int)g.Key);
            var totalSubCategoryNumbers = Enum.GetNames(typeof(SubCategoryConstants)).Length;
            var subCategoryIdList = Enumerable.Range(0, totalSubCategoryNumbers);

            _output.WriteLine(JsonConvert.SerializeObject(subCategoryIdList));

            Assert.Equal(JsonConvert.SerializeObject(result), JsonConvert.SerializeObject(subCategoryIdList));
        }
    }
}
