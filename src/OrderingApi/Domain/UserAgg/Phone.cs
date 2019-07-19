using OrderingApi.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Domain.UserAgg
{
    public class Phone : IValueObject
    {
        public readonly string Number;

        public Phone(string number)
        {
            Number = number;
        }
    }
}
