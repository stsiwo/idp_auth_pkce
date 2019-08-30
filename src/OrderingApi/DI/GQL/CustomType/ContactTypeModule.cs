﻿using Autofac;
using OrderingApi.UI.GQL.Types.CustomType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.GQL.CustomType
{
    public class ContactTypeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContactType>()
                .SingleInstance();
        }
    }
}
