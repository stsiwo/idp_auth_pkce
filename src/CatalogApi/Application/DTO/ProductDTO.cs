using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CatalogApi.Application.DTO
{
    [DataContract]
    public class ProductDTO : IDTO
    {
        [DataMember]
        public String Id { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public String Description { get; set; }

        [DataMember]
        public String MainImageURL { get; set; }

        /** DataContract and collection
         *  DataContract Attribute automatically change complex data structure to array or object in json 
         **/
        [DataMember]
        public List<SubImageDTO> SubImageURLList { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public List<ReviewDTO> ReviewList { get; set; }

        [DataMember]
        public SubCategoryDTO SubCategory { get; set; }

        [DataMember]
        public string CreationDate { get; set; }
    }
}
