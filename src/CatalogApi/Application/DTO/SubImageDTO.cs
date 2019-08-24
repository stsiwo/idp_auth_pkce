using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Application.DTO
{
    public class SubImageDTO : IDTO
    {
        public String Id { get; set; }
        public String Url { get; set; }
        public string ProductId { get; set; }
    }
}
