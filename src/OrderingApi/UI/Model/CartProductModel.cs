using OrderingApi.Domain.CartAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.Model
{
    public class CartProductModel
    {
        public string Id { get; set; }

        public ProductName Name { get; set; }

        public string Description { get; set; }

        public string MainImageUrl { get; set; } 

        public PriceModel Price { get; set; }

        public StockModel Stock { get; set; }
        public ISet<Cart> Carts { get; set; } 
    }
}
