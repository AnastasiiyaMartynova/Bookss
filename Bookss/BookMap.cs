using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Linq;

namespace Bookss
{
    class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Id(t => t.Id);
            Map(t => t.Title);
            HasManyToMany(t => t.Authors);
            Map(t => t.Rating);
        }

    }

    class AuthorMap : ClassMap<Author>
    {
        public AuthorMap()
        {
            Id(t => t.Id);
            Map(t => t.Name);
            HasManyToMany(t => t.Books).Cascade.All();
        }
    }

    static class Util
    {

        public static IList<Book> GetTopTwoBooks(IPersistense<Book> books)
        {
            return books.GetAll().OrderByDescending(b => b.Rating).Take(2).ToList();
        }

        public static Book GetBestBook(this IPersistense<Author> authors, string name)
        {
            return authors.GetAll().ToList<Author>().First(a => a.Name.Contains(name)).Books.OrderByDescending(b => b.Rating).ElementAt(1);
        }

        public static ISessionFactory CreateSessionFactory()
        {
            return
                Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.UsingFile("C:\\libr\\books.db"))
                    .Mappings(t => t.FluentMappings.AddFromAssemblyOf<Book>())
                    .ExposeConfiguration(cfg =>
                    new SchemaExport(cfg).Create(false, true))
                    .BuildSessionFactory();
        }
    }
}
