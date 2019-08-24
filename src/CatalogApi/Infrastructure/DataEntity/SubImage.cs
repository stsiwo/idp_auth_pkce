using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.DataEntity
{
    [Table("sub_image")]
    public class SubImage : IDataEntity
    {
        [Key]
        [Column("id", TypeName = "uuid")]
        public String Id { get; set; }

        [Required]
        [Column("url")]
        public String Url { get; set; }

        [Required]
        [Column("product_id", TypeName = "uuid")]
        public string ProductId { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
