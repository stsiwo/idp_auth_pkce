using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.DataEntity
{
    public class Category : IDataEntity
    {
        [Key]
        [Column("id", TypeName = "smallint")]
        public CategoryConstants Id { get; set; }

        [Required]
        [Column("title")]
        public String Title { get; set; }

        [Required]
        [Column("description")]
        public String Description { get; set; }

        [Required]
        [Column("image_url")]
        public String ImageURL { get; set; }

        [JsonIgnore]
        public List<SubCategory> SubCategories { get; set; }

    }
}
