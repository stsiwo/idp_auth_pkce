using MediatR;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.Command
{
    public class AddProductsToCartCommand : IRequest<CartModel>
    {
        public string UserId { get; set; }
        public IList<string> ProductIds { get; set; }

        public AddProductsToCartCommand()
        {

        }
    }
}
