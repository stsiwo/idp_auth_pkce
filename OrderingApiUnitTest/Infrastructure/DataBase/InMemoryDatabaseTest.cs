using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Proxy;
using NHibernate.Proxy.DynamicProxy;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OrderingApiUnitTest.Infrastructure.DataBase
{
    public class InMemoryDatabaseTest : IDisposable
    {
        private static Configuration Configuration;
        private static ISessionFactory SessionFactory;
        protected ISession session;

        public InMemoryDatabaseTest(Assembly assemblyContainingMapping)
        {
            if (Configuration == null)
            {
                Configuration = new Configuration()
                    .SetProperty(NHibernate.Cfg.Environment.ReleaseConnections, "on_close")
                    .SetProperty(NHibernate.Cfg.Environment.Dialect, typeof(SQLiteDialect).AssemblyQualifiedName)
                    .SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, typeof(SQLite20Driver).AssemblyQualifiedName)
                    .SetProperty(NHibernate.Cfg.Environment.ConnectionString, "data source=:memory:")
                    //.SetProperty(NHibernate.Cfg.Environment.ProxyFactoryFactoryClass, typeof(StaticProxyFactory).AssemblyQualifiedName)
                    .AddAssembly(assemblyContainingMapping);

                SessionFactory = Configuration.BuildSessionFactory();
            }

            session = SessionFactory.OpenSession();

            new SchemaExport(Configuration).Execute(true, true, false, session.Connection, Console.Out);
        }

        public void Dispose()
        {
            session.Dispose();
        }
    }
}
