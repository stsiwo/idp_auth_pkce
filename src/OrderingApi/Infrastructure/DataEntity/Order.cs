using OrderingApi.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Infrastructure.DataEntity
{
    public class Order : IDataEntity
    {
        [Key]
        [Column("id", TypeName = "uuid")]
        public String Id { get; set; }

        [Required]
        [Column("status")]
        public OrderStatusConstants Status { get; set; }

        [Required]
        [Column("user_id")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public IList<Product> Products { get; set; }

        
    }
}
