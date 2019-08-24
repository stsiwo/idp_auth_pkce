using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Config.AOP.ASPFilter.HateoasHeader
{
    public class AddCartToUserHateoasHeader : IHateoasHeader
    {
        public IList<IDictionary<string, string>> GetLinks()
        {
            return new List<IDictionary<string, string>>()
            {
                new Dictionary<string, string>()
                {
                    { "url", "/carts/test1" },
                    { "rel", "test1" },
                    { "type", "json" },
                },
                new Dictionary<string, string>()
                {
                    { "url", "/carts/test2" },
                    { "rel", "test2" },
                    { "type", "json" },
                },
                new Dictionary<string, string>()
                {
                    { "url", "/carts/test3" },
                    { "rel", "test3" },
                    { "type", "json" },
                },
            };
        }
    }
}
