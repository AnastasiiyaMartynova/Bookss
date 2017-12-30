using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookss
{
    interface IPersistense<T> where T : class
    {
        T GetById(object id);

        void Update(T item);

        void Delete(T item);

        void Add(T item);

        IEnumerable<T> GetAll();
    }

    class Persistense<T> : IPersistense<T> where T : class
    {
        private ISessionFactory _sessionFactory;

        public Persistense(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void Add(T item)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(item);
                    transaction.Commit();
                }
            }
        }

        public void Delete(T item)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<T> GetAll()
        {
            var session = _sessionFactory.OpenSession();
            return session.QueryOver<T>().Future();
            /*using (var session = _sessionFactory.OpenSession())
            {
                return session.QueryOver<T>().Future();
            }*/
        }

        public T GetById(object id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var n = session.Get<T>(id);
                return n;
            }
        }

        public void Update(T item)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(item);
                    transaction.Commit();
                }
            }
        }


    }



}
