using OrderingApi.Domain.CartAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.Model
{
    public class CartProductModel
    {
        public Guid Id { get; set; }

        public ProductNameModel Name { get; set; }

        public ProductDescriptionModel Description { get; set; }

        public string MainImageUrl { get; set; } 

        public PriceModel Price { get; set; }

        public StockModel Stock { get; set; }
        public ISet<CartModel> Carts { get; set; } 
    }
}
