using System.Collections.Generic;

namespace NezarkaBookstore
{
    interface IModel
    {
        IList<Book> GetBooks();
        Book GetBook(int id);
        Customer GetCustomer(int id);
    }
}