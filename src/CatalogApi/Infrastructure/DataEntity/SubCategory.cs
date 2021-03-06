﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Infrastructure.DataEntity
{
    [Table("sub_category")]
    public class SubCategory
    {
        [Key]
        [Column("id", TypeName = "smallint")]
        public SubCategoryConstants Id { get; set; }

        [Required]
        [Column("title")]
        public String Title { get; set; }

        [Required]
        [Column("description")]
        public String Description { get; set; }

        [Required]
        [Column("image_url")]
        public String ImageURL { get; set; }

        [Required]
        [Column("category_id", TypeName = "smallint")]
        public CategoryConstants CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [JsonIgnore]
        public List<Product> Products { get; set; }
    }

}
