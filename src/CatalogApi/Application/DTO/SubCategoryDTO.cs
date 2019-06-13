using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogApi.Application.DTO
{
    public class SubCategoryDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public String Title { get; set; }

        [DataMember]
        public String Description { get; set; }

        [DataMember]
        public String ImageURL { get; set; }

        [DataMember]
        public int CategoryId { get; set; }

        [DataMember]
        public CategoryDTO Category { get; set; }
    }
}
