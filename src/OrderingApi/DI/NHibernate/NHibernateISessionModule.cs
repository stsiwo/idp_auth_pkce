using Autofac;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.DI.NHibernate
{
    public class NHibernateISessionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new Configuration().Configure().BuildSessionFactory())
                .As<ISessionFactory>()
                .SingleInstance();

            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession())
                .As<ISession>()
                .InstancePerLifetimeScope();
        }
    }
}
