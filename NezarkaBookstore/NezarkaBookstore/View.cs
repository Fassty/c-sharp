using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;
using Console = System.Console;

namespace NezarkaBookstore
{
    class View:IView
    {
        private readonly TextWriter writer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="writer">Output writer</param>
        public View(TextWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Generates a page for /Books request
        /// </summary>
        /// <param name="customerId">Customer ID from Model</param>
        /// <param name="model">Model</param>
        public void PageGenBooks(int customerId, IModel model)
        {
            GenerateHead();
            GenerateStyle();
            AddHeader(customerId,model);
            int count = 0;
            IList<Book> books = model.GetBooks();

            if (books.Count != 0)
            {
                writer.WriteLine("\tOur books for you:");
                writer.WriteLine("\t<table>");
                writer.WriteLine("\t\t<tr>");

                foreach (var book in books)
                {
                    count++;
                    writer.WriteLine("\t\t\t<td style=\"padding: 10px;\">");
                    writer.WriteLine("\t\t\t\t<a href=\"/Books/Detail/"+ book.Id +"\">"+ book.Title +"</a><br />");
                    writer.WriteLine("\t\t\t\tAuthor: "+ book.Author +"<br />");
                    writer.WriteLine("\t\t\t\tPrice: "+ book.Price +" EUR &lt;<a href=\"/ShoppingCart/Add/"+ book.Id +"\">Buy</a>&gt;");
                    writer.WriteLine("\t\t\t</td>");
                    if (count % 3 == 0 && books.Count != count)
                    {
                        writer.WriteLine("\t\t</tr>");
                        writer.WriteLine("\t\t<tr>");
                    }
                }
                writer.WriteLine("\t\t</tr>");
                writer.WriteLine("\t</table>");
            }
            else
            {
                writer.WriteLine("\tOur books for you:");
                writer.WriteLine("\t<table>");
                writer.WriteLine("\t</table>");
            }
            GenerateEnd();
        }

        /// <summary>
        /// Generates a page for /Books/Detail/_bookId_ request
        /// </summary>
        /// <param name="customerId">Customer ID from Model</param>
        /// <param name="bookId">Book ID from Model</param>
        /// <param name="model">Model</param>
        public void PageGenDetail(int customerId, int bookId, IModel model)
        {
            Book book = model.GetBook(bookId);

            GenerateHead();
            GenerateStyle();
            AddHeader(customerId,model);
            writer.WriteLine("\tBook details:");
            writer.WriteLine("\t<h2>" + book.Title + "</h2>");
            writer.WriteLine("\t<p style=\"margin-left: 20px\">");
            writer.WriteLine("\tAuthor: " + book.Author + "<br />");
            writer.WriteLine("\tPrice: " + book.Price + " EUR<br />");
            writer.WriteLine("\t</p>");
            writer.WriteLine("\t<h3>&lt;<a href=\"/ShoppingCart/Add/" + book.Id + "\">Buy this book</a>&gt;</h3>");
            GenerateEnd();
        }

        /// <summary>
        /// Generates a page for /ShoppingCart request
        /// </summary>
        /// <param name="customerId">Customer ID from Model</param>
        /// <param name="model">Model</param>
        public void PageGenShoppingCart(int customerId, IModel model)
        {
            IList<ShoppingCartItem> books = model.GetCustomer(customerId).ShoppingCart.Items;
            decimal sum = 0;

            GenerateHead();
            GenerateStyle();
            AddHeader(customerId,model);
            if (model.GetCustomer(customerId).ShoppingCart.Items.Count == 0)
            {
                writer.WriteLine("\tYour shopping cart is EMPTY.");
            }
            else
            {
                writer.WriteLine("\tYour shopping cart:");
                writer.WriteLine("\t<table>");
                writer.WriteLine("\t\t<tr>");
                writer.WriteLine("\t\t\t<th>Title</th>");
                writer.WriteLine("\t\t\t<th>Count</th>");
                writer.WriteLine("\t\t\t<th>Price</th>");
                writer.WriteLine("\t\t\t<th>Actions</th>");
                writer.WriteLine("\t\t</tr>");
                foreach (var item in books)
                {
                    Book book = model.GetBook(item.BookId);
                    writer.WriteLine("\t\t<tr>");
                    writer.WriteLine("\t\t\t<td><a href=\"/Books/Detail/" + book.Id + "\">" + book.Title + "</a></td>");
                    writer.WriteLine("\t\t\t<td>" + item.Count + "</td>");
                    writer.Write("\t\t\t<td>");
                    string s = item.Count > 1 ? 
                          item.Count + " * " + book.Price + " = " + item.Count*book.Price : 
                          book.Price.ToString();
                    writer.Write(s);
                    writer.WriteLine(" EUR</td>");
                    writer.WriteLine("\t\t\t<td>&lt;<a href=\"/ShoppingCart/Remove/" + book.Id + "\">Remove</a>&gt;</td>");
                    writer.WriteLine("\t\t</tr>");
                    sum += item.Count * book.Price;
                }
                writer.WriteLine("\t</table>");
                writer.WriteLine("\tTotal price of all items: " + sum + " EUR");
            }
            GenerateEnd();
        }

        /// <summary>
        /// Generates an Invalid Request page if user input was invalid
        /// </summary>
        public void PageGenError()
        {
            GenerateHead();
            writer.WriteLine("<p>Invalid request.</p>");
            GenerateEnd();
        }

        /// <summary>
        /// Generates a common header for all valid pages
        /// </summary>
        /// <param name="customerId">Customer ID from Model</param>
        /// <param name="model">Model</param>
        public void AddHeader(int customerId, IModel model)
        {
            var name = model.GetCustomer(customerId).FirstName;
            var count = model.GetCustomer(customerId).ShoppingCart.Items.Count;
            writer.WriteLine("\t<h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>");
            writer.WriteLine("\t" + name + ", here is your menu:");
            writer.WriteLine("\t<table>");
            writer.WriteLine("\t\t<tr>");
            writer.WriteLine("\t\t\t<td><a href=\"/Books\">Books</a></td>");
            writer.WriteLine("\t\t\t<td><a href=\"/ShoppingCart\">Cart (" + count + ")</a></td>");
            writer.WriteLine("\t\t</tr>");
            writer.WriteLine("\t</table>");
        }

        /// <summary>
        /// Generates a common head HTML code
        /// </summary>
        public void GenerateHead()
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
            writer.WriteLine("<head>");
            writer.WriteLine("\t<meta charset=\"utf-8\" />");
            writer.WriteLine("\t<title>Nezarka.net: Online Shopping for Books</title>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
        }

        /// <summary>
        /// Generates a common style HTML code
        /// </summary>
        public void GenerateStyle()
        {
            writer.WriteLine("\t<style type=\"text/css\">");
            writer.WriteLine("\t\ttable, th, td {");
            writer.WriteLine("\t\t\tborder: 1px solid black;");
            writer.WriteLine("\t\t\tborder-collapse: collapse;");
            writer.WriteLine("\t\t}");
            writer.WriteLine("\t\ttable {");
            writer.WriteLine("\t\t\tmargin-bottom: 10px;");
            writer.WriteLine("\t\t}");
            writer.WriteLine("\t\tpre {");
            writer.WriteLine("\t\t\tline-height: 70%;");
            writer.WriteLine("\t\t}");
            writer.WriteLine("\t</style>");
        }

        /// <summary>
        /// Generates a common end HTML code
        /// </summary>
        public void GenerateEnd()
        {
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
            writer.WriteLine("====");
        }

        /// <summary>
        /// Deprecated, left here for further debugging
        /// </summary>
        public void Finish()
        {
           // writer.Close();
        }
    }
}