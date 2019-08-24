using CatalogApi.UI.Validators.ProductQueryString;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogApiUnitTest.DI.TestComponents
{
    class ProductQueryStringValidatorClient
    {
        private readonly IEnumerable<IProductQueryStringValidator> _queryStringValidators;

        public ProductQueryStringValidatorClient(IEnumerable<IProductQueryStringValidator> queryStringValidators)
        {
            _queryStringValidators = queryStringValidators;
        }

        public IList<string> GetImpls()
        {
            IList<string> typeNameList = new List<string>();

            foreach (var impl in _queryStringValidators)
            {
                typeNameList.Add(impl.GetType().ToString());
            }

            return typeNameList;
        }
    }
}
