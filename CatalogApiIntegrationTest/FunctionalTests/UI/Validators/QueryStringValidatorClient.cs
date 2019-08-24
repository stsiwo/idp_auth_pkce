using CatalogApi.UI.Validators.ProductQueryString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApiIntegrationTest.FunctionalTests.UI.Validators
{
    public class QueryStringValidatorClient
    {
        private readonly IEnumerable<IProductQueryStringValidator> _queryStringValidators;

        public QueryStringValidatorClient(IEnumerable<IProductQueryStringValidator> queryStringValidators)
        {
            _queryStringValidators = queryStringValidators;
        }

        public string GetValidatedQueryString(string qs)
        {

            foreach (var validator in _queryStringValidators)
            {
                qs = validator.Validate(qs);
            }

            return qs;
        }
      
    }
}
