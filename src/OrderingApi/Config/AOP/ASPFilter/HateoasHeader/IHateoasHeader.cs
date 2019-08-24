using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Config.AOP.ASPFilter.HateoasHeader
{
    public interface IHateoasHeader
    {
        IList<IDictionary<string, string>> GetLinks();
    }
}
