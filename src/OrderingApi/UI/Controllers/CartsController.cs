using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingApi.Application.DTO;
using OrderingApi.Config.AOP.ASPFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {

        // api/cart/{id}: GET: get a specific cart
        [HttpGet]
        [ServiceFilter(typeof(TestFilter))]
        public ActionResult<string> Get(Guid id)
        {
            HttpContext.Response.Headers.Add("test-tst", "test-test");
            return "hey"; 
        }

        // add the product to the cart
        [HttpPost("{cartId}/products/{productId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult AddProductToCart(Guid cartId, Guid productId)
        {
            // validate client input

            // if failed, return 400 Bad Request

            // if product is not found, return status code 404

            // crete AddProductToCart Command

            // send the command to its command handler

            // the result of the command handler

            // if success, return status code 201
            return Ok();

        }
    }
}
