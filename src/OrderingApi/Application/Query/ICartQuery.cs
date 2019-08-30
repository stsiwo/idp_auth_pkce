using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.Query
{
    public interface ICartQuery
    {
        Task<IList<CartModel>> GetCartsByIds(IList<string> ids);
    }
}
