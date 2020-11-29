using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NezarkaBookstore;

namespace NezarkaBookStoreTest
{
    [TestClass]
    public class ControllerTests
    {
        private Model LoadFromMockup()
        {
            StringReader input = new StringReader(
                "DATA-BEGIN\n" +
                "BOOK;1;Lord of the Rings;J. R. R. Tolkien;59\n" +
                "BOOK;2;Hobbit: There and Back Again;J. R. R. Tolkien;49\n" +
                "BOOK;3;Going Postal;Terry Pratchett;28\n" +
                "BOOK;4;The Colour of Magic;Terry Pratchett;159\n" +
                "BOOK;5;I Shall Wear Midnight;Terry Pratchett;31\n" +
                "CUSTOMER;1;Pavel;Jezek\n" +
                "CUSTOMER;2;Jan;Kofron\n" +
                "CUSTOMER;3;Petr;Hnetynka\n" +
                "CUSTOMER;4;Tomas;Bures\n" +
                "CART-ITEM;2;1;3\n" +
                "CART-ITEM;2;5;1\n" +
                "DATA-END\n");

            var model = Model.LoadFrom(input);
            return model;
        }

        #region ParseCommandTests
        [TestMethod]
        public void ParseCommand_EmptyCommand()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model,view);
            Assert.IsFalse(controller.ParseCommand(""));
        }

        [TestMethod]
        public void ParseCommand_MissingRequestType()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("1 http://www.nezarka.net/Books"));
        }

        [TestMethod]
        public void ParseCommand_MissingCustomerID()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("GET http://www.nezarka.net/Books"));
        }

        [TestMethod]
        public void ParseCommand_MissingURL()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("GET 1"));
        }

        [TestMethod]
        public void ParseCommand_InvalidRequestType()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("PUT 1 http://www.nezarka.net/Books"));
        }

        [TestMethod]
        public void ParseCommand_InvalidCustomerID()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("GET -1 http://www.nezarka.net/Books"));
        }

        [TestMethod]
        public void ParseCommand_CustomerIDOutOfRange()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("GET 5 http://www.nezarka.net/Books"));
        }

        [TestMethod]
        public void ParseCommand_InvalidURL1()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("GET 4 http://www.nezarka.net/"));
        }

        [TestMethod]
        public void ParseCommand_InvalidURL2()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("GET 4 xxxcxschttp://www.nezarka.net/Books"));
        }

        [TestMethod]
        public void ParseCommand_InvalidURL3()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("GET 4 http://www.nezarka.net/Books/3"));
        }

        [TestMethod]
        public void ParseCommand_InvalidURL4()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("GET 4 http://www.nezXarka.net/Books"));
        }

        [TestMethod]
        public void ParseCommand_DetailOutOfRange()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("GET 4 http://www.nezarka.net/Books/Detail/9"));
        }

        [TestMethod]
        public void ParseCommand_DetailInvalidURL()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("GET 4 http://www.nezarka.net/Books/Detail/1/1"));
        }

        [TestMethod]
        public void ParseCommand_ShoppingCartInvalidURL()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("GET 4 http://www.nezarka.net/ShoppingCart/foo"));
        }

        [TestMethod]
        public void ParseCommand_AddNonExistentBook()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("GET 4 http://www.nezarka.net/ShoppingCart/Add/6"));
        }

        [TestMethod]
        public void ParseCommand_RemoveNonExistentBook()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            Assert.IsFalse(controller.ParseCommand("GET 4 http://www.nezarka.net/ShoppingCart/Remove/6"));
        }
        #endregion

        #region AddBookTests
        [TestMethod]
        public void AddBook_AddNewBook()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            controller.AddBook(5,4);
            Assert.AreEqual(model.GetCustomer(4).ShoppingCart.Items.Find(b => b.BookId == 5).Count,1);
        }

        [TestMethod]
        public void AddBook_IncreaseCount()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            controller.AddBook(5,2);
            Assert.AreEqual(model.GetCustomer(2).ShoppingCart.Items.Find(b => b.BookId == 5).Count, 2);
        }
        #endregion

        #region RemoveBookTests
        [TestMethod]
        public void RemoveBook_DecreaseCount()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            controller.RemoveBook(1,2);
            Assert.AreEqual(model.GetCustomer(2).ShoppingCart.Items.Find(b => b.BookId == 1).Count, 2);
        }

        [TestMethod]
        public void RemoveBook_RemoveFromShoppingCart()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            controller.RemoveBook(5, 2);
            Assert.AreEqual(model.GetCustomer(2).ShoppingCart.Items.Find(b => b.BookId == 5), null);
        }
        #endregion

        #region GetHTTPRequestTests
        [TestMethod]
        public void GetHTTPRequest_RequestBooks()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            var result = controller.GetHttpRequest("http://www.nezarka.net/Books");
            Assert.AreEqual(result,1);
        }

        [TestMethod]
        public void GetHTTPRequest_RequestDetail()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            var result = controller.GetHttpRequest("http://www.nezarka.net/Books/Detail/3");
            Assert.AreEqual(result, 2);
        }

        [TestMethod]
        public void GetHTTPRequest_RequestShoppingCart()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            var result = controller.GetHttpRequest("http://www.nezarka.net/ShoppingCart");
            Assert.AreEqual(result, 3);
        }

        [TestMethod]
        public void GetHTTPRequest_RequestAdd()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            var result = controller.GetHttpRequest("http://www.nezarka.net/ShoppingCart/Add/2");
            Assert.AreEqual(result, 4);
        }

        [TestMethod]
        public void GetHTTPRequest_RequestRemove()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            var result = controller.GetHttpRequest("http://www.nezarka.net/ShoppingCart/Remove/5");
            Assert.AreEqual(result, 5);
        }

        [TestMethod]
        public void GetHTTPRequest_InvalidRequest()
        {
            var output = new StringWriter();
            var model = LoadFromMockup();
            var view = new View(output);
            var controller = new Controller(model, view);
            var result = controller.GetHttpRequest("http://www.nezarka.net/");
            Assert.AreEqual(result, 0);
        }

        #endregion
    }
}
