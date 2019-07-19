using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.ProductAgg
{
    public class MainImageUrl : IValueObject
    {
        public readonly string Url;

        public MainImageUrl(string url)
        {
            Url = url;
        }
    }
}
