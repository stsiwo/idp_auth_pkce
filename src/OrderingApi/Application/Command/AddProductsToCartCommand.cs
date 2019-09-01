﻿using MediatR;
using OrderingApi.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.Application.Command
{
    public class AddProductsToCartCommand : IRequest<CartModel>
    {
        public Guid UserId { get; set; }
        public IList<Guid> ProductIds { get; set; }

        public AddProductsToCartCommand()
        {

        }
    }
}
