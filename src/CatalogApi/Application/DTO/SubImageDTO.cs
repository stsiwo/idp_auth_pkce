using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Application.DTO
{
    public class SubImageDTO : IDTO
    {
        public String Id { get; private set; }
        public String Url { get; private set; }
        public int ProductId { get; private set; }
    }
}
