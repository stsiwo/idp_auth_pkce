using Bogus;
using OrderingApi.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderingApiUnitTest.TestData.Entity
{
    public static class UserFaker
    {
        public static Faker<User> GetUserFaker()
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.HomeAddress, f => new Address(f.Address.StreetAddress(), f.Address.City(), f.Address.State(), f.Address.Country(), f.Address.ZipCode()))
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Orders, f => OrderFaker.GetRandomOrderList(5))
                .RuleFor(u => u.Cart, f => CartFaker.GetCartList(1).FirstOrDefault())
                .FinishWith((f, u) => 
                {
                    u.Cart.UserId = u.Id;

                    foreach (var order in u.Orders)
                    {
                        order.UserId = u.Id;
                    }
                });

            return userFaker;
        }

        /**
         * maybe it is better to create UserFaker based on the type of User. 
         * for instance, 
         *      1. a user who does not have any Cart and Orders
         *          1.1. a user is guest (does not have information like Name, Phone, Address)
         *          1.2. a user is member (does have informaiton)
         *      2. a user who has a Cart but Orders 
         *          2.1. a user is guest (does not have information like Name, Phone, Address)
         *          2.2. a user is member (does have informaiton)
         *      3. a user who has Orders but Cart 
         **/

        public static IList<User> GetUserList(int amount)
        {

            var faker = GetUserFaker();

            User SeededUser(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            var users = Enumerable.Range(1,amount)
                .Select(SeededUser)
                .ToList();

            return users;  
        }
        public static IList<User> GetRandomUserList(int max)
        {
            var faker = GetUserFaker();

            User SeededUser(int seed)
            {
                return faker.UseSeed(seed).Generate();
            }

            Random rnd = new Random();
            int maxNumberOfInstances = rnd.Next(0, max);

            var users = Enumerable.Range(1, maxNumberOfInstances)
                .Select(SeededUser)
                .ToList();

            return users;  
        }
    }
}
