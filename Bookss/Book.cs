using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookss
{
    class Book
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual IList<Author> Authors { get; set; }

        public virtual double Rating { get; set; }

        public Book()
        {

        }




    }
    class Author
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Book> Books { get; set; }

    }
}
