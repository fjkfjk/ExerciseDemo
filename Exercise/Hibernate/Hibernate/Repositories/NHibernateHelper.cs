using Hibernate.Domain;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Hibernate.Repositories
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly(typeof(Patient).Assembly);
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
        public static void SetupContext()
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(Patient).Assembly);
            new SchemaExport(configuration).Execute(false, true, false);
        }
    }
}
