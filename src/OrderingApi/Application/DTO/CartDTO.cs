using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OrderingApi.Application.DTO
{

    [DataContract]
    public class CartDTO : IDTO
    {
       public Guid CartId { get; set; } 
    }
}
