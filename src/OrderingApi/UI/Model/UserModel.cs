using OrderingApi.Domain.OrderAgg;
using OrderingApi.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.Model
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressModel HomeAddress { get; set; }
        public ContactModel ContactInfo { get; set; }
        public CartModel Cart { get; set; }
        public ISet<OrderModel> Orders { get; set; }
    }
}
