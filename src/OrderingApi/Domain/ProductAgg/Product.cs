using OrderingApi.Domain.Base;
using OrderingApi.Domain.CartAgg;
using OrderingApi.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.ProductAgg
{
    public class Product : IEntity, IAggregate
    {
        public ProductId ProductId { get; set; }

        public Name Name { get; set; }

        public Description Description { get; set; }

        public MainImageUrl MainImageUrl { get; set; } 

        public Price Price { get; set; }

        public Stock Stock { get; set; }

        public AvailableStock AvailableStock { get; set; } 
        
        public CartId CartId { get; set; } 

        public OrderId OrderId { get; set; }

        // creationDate method can be implemented with ProductId ValueObject
//        public DateTime CreationDate()
//        {
//            return ProductId.CreationDate();
//        }

        public Product(
                
            )
        {

        }
    }
}
