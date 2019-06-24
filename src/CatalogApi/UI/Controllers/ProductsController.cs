using CatalogApi.Application.DTO;
using CatalogApi.Application.Service.Products;
using CatalogApi.UI.Utils;
using CatalogApi.UI.Validators.ProductQueryString;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace CatalogApi.UI.Controllers
{
    /**
     * webapi controller caveats
     *   1. don't use "Controller" base class. this is for traditional web page (esp, add View features)
     *   2. can't use coventional ruotes (e.g., UseMVC() then define route in it)
     *   3. for configuration, use AddMvc().ConfigurationApiBehaviorOptions(options => {...}) or services.Configure<ApiBehaviorOptions>(options => {..});</ApiBehaviorOptions>
     *   4. available return types: 
     *          - specific types (e.g., IEnumerable<..>)
                - POCO (DTO object): mvc automatically creates an ObjectResult and serialized to json (default) with OK or NoContent status code
                - IActionResult (this represent HTTP status code so you can return a result with its correspnding status code if useing this)
                    - but need to explicitly state the type of object (such as [ProducesResponseType(typeof(YOUR_Type), StatusCodes.Status201Created]
                - ActionResult 
                    - similar to IActionResult
                    - dont' need to explicitly state the type of object to be returned because you defined the type in type parameter
         5. return type format types
                - JsonResult or ContentResult (plain text): always return its type regardless of accept header from client is different.
                - IActionResult (with Content Negotiation): client's Accept Header (which content type client wants) and its corresponding status code
                    - this enable you to return client-specified content type (like json, xml or plain text) but you need to configure its formatter excepts for json which is default
         6. Content Negotiation: 
                - if Accept Header is included in client request, mvc try to accommodate it content type.
                - need to configure its content type's formatter excepts for json (default)
                - browser's Accept Header is ignored. this is because to provide consistent content type to all browsers, but you can change this setting.
    **/


    [Route("api/[controller]")]
    [ApiController] // automatic HTTP 400 (bad request) response
    public class ProductsController : ControllerBase
    {
        private readonly IUtil _util;
        private readonly ILogger<ProductsController> _logger;
        private readonly IEnumerable<IProductQueryStringValidator> _queryStringValidators;

        // ideally want to use method injection but it looks complicated so defined each App services for each endpoing in this controller.
        private readonly IGetProductsService _getProductsService;


        public ProductsController(
            IUtil util, 
            IGetProductsService getProductsService, 
            ILogger<ProductsController> logger, 
            IEnumerable<IProductQueryStringValidator> queryStringValidators)
        {
            _util = util;
            _getProductsService = getProductsService;
            _logger = logger;
            _queryStringValidators = queryStringValidators;

        }

        // DI for IGetProductsService
        [HttpGet]
        public async Task<ActionResult<ProductDTO>> Get()
        {
            // get qs from request
            string queryString = (HttpContext.Request.QueryString != null) ? HttpContext.Request.QueryString.ToString() : "";

            _logger.LogDebug("Get Endpoint: {@Query}", queryString);

            // validate query string
            foreach (var validator in _queryStringValidators)
            {
                queryString = validator.Validate(queryString);
            }

            // get products from service
            var products = await _getProductsService.GetProducts(HttpUtility.ParseQueryString(queryString));

            // if empty, return 204 
            if (products == null)
            {
                return NoContent();
            }

            // ok with data
            return Ok(products);
        } 

    }
}
