using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CatalogApi.Application.DTO
{
    [DataContract]
    public class ProductDTO : IDTO
    {
        [DataMember]
        public String Id { get; private set; }

        [DataMember]
        public String Name { get; private set; }

        [DataMember]
        public String Description { get; private set; }

        [DataMember]
        public String MainImageURL { get; private set; }

        /** DataContract and collection
         *  DataContract Attribute automatically change complex data structure to array or object in json 
         **/
        [DataMember]
        public List<SubImageDTO> SubImageURLList { get; private set; }

        [DataMember]
        public decimal Price { get; private set; }

        [DataMember]
        public List<ReviewDTO> ReviewList { get; private set; }
    }
}
