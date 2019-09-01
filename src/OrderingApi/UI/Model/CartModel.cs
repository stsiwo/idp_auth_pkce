using OrderingApi.Domain.OrderAgg;
using OrderingApi.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.Model
{
    public class CartModel : IModel
    {
        public Guid Id { get; set; }
        public UserModel User { get; set; }
        public ISet<CartProductModel> Products { get; set; }
    }
}
