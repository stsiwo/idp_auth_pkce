using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CatalogApi.Application.DTO
{
    [DataContract]
    public class ReviewDTO : IDTO
    {
        [DataMember]
        public String Id { get; set; }

        [DataMember]
        public String Author { get; set; }

        [DataMember]
        public String Comment { get; set; }

        [DataMember]
        public int Score { get; set; }

        [DataMember]
        public String CreationDate { get; set; }
    }
}
