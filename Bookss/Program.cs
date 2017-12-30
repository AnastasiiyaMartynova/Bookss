using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using FluentNHibernate.Mapping;
using NHibernate.Tool.hbm2ddl;

namespace Bookss
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            List<Book> books = new List<Book>();
            var book1 = new Book() { Title = "Voina i mir", Rating = 7.8 };
            var book2 = new Book() { Title = "Anna Karenina", Rating = 7.7 };
            var author1 = new Author { Name = "L.N. Tolstoy", Books = new[] { book1, book2 } };
            var book3 = new Book() { Title = "Onegin", Rating = 9 };
            var author3 = new Author { Name = "A.S.Pushkin", Books = new[] { book3 } };

            var persistense = GetPersistense<Author>();
            persistense.Add(author1);
            persistense.Add(author3);
            //var all = persistense.GetAll();
            Console.WriteLine(Util.GetBestBook(persistense, "L.N. Tolstoy").Title);
            /*var persistense_b = GetPersistense<Book>();
            persistense_b.Add(book1);
            persistense_b.Add(book3);
            persistense_b.Add(book2);
            var bestBooks = Util.GetTopTwoBooks(persistense_b);*/


            //...

        }

        private static IPersistense<T> GetPersistense<T>() where T : class
        {
            var sessionFactory = Util.CreateSessionFactory();
            return new Persistense<T>(sessionFactory);
        }

        /*private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.UsingFile("C:\\books\\books.db"))
                    .Mappings(t => t.FluentMappings.AddFromAssemblyOf<Book>()).ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, true))
                    .BuildSessionFactory();
        }*/
    }
}
