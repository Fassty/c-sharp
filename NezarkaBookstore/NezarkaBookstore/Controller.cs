using System;
using System.ComponentModel.Design;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace NezarkaBookstore
{
    /// <summary>
    /// Processes user input and sends requests to view class
    /// </summary>
    class Controller : IController
    {
        private Model model;
        private IView view;
        private TextReader reader; // Input reader
        private string[] splitCommand; // Command split into 3 parts: request_type customerId request
        private string[] arguments; // Split request URL for bookId extraction

        /// <summary>
        /// Default constructor
        /// </summary>
        public Controller()
        {

        }

        /// <summary>
        /// Implicit constructor for testing purposes
        /// </summary>
        /// <param name="model"></param>
        /// <param name="view"></param>
        public Controller(Model model, IView view)
        {
            this.model = model;
            this.view = view;
        }

        /// <summary>
        /// Loads data from input
        /// </summary>
        public void Run()
        {
            reader = Console.In;
            view = new View(new StreamWriter("output.html"));
            model = Model.LoadFrom(reader);
            if (model == null)
            {
                Console.WriteLine("Data error.");
                return;
            }
            ReadCommands();
        }

        /// <summary>
        /// Main command processing loop
        /// </summary>
        public void ReadCommands()
        {
            string command;

            while ((command = reader.ReadLine()) != null)
            {

                if (!ParseCommand(command))
                {
                    view.PageGenError();
                }
                else
                {
                    ExecuteCommand();
                }
            }

            reader.Close();
            view.Finish();
        }

        /// <summary>
        /// Adds a book to customer's shopping cart or increases count if a book is already present
        /// </summary>
        /// <param name="bookId">Book ID from Model</param>
        /// <param name="customerId">Customer ID from Model</param>
        public void AddBook(int bookId, int customerId)
        {
            Book book = model.GetBook(bookId);
            var item = model.GetCustomer(customerId).ShoppingCart.GetById(book.Id);
            if (model.GetCustomer(customerId).ShoppingCart.Items.Contains(item))
            {
                item.Count++;
            }
            else
            {
                var newItem = new ShoppingCartItem { BookId = book.Id, Count = 1 };
                model.GetCustomer(customerId).ShoppingCart.Items.Add(newItem);
            }
            view.PageGenShoppingCart(customerId, model);
        }

        /// <summary>
        /// Removes a book from customer's shopping cart or decreases its count by 1
        /// </summary>
        /// <param name="bookId">Book ID from Model</param>
        /// <param name="customerId">Customer ID from Model</param>
        public void RemoveBook(int bookId, int customerId)
        {
            Book book = model.GetBook(bookId);
            var item = model.GetCustomer(customerId).ShoppingCart.GetById(book.Id);
            if (item != null)
            {
                if (item.Count == 1)
                {
                    model.GetCustomer(customerId).ShoppingCart.Items.Remove(item);
                }
                else
                {
                    item.Count--;
                }
            }
            view.PageGenShoppingCart(customerId, model);
        }

        /// <summary>
        /// If a command was valid it gets executed
        /// </summary>
        public void ExecuteCommand()
        {
            var customerId = int.Parse(splitCommand[1]);
            var requestId = GetHttpRequest(splitCommand[2]);
            var bookId = 0;

            if (arguments.Length == 6)
            {
                bookId = int.Parse(arguments[5]);
            }

            switch (requestId)
            {
                case (1):
                    view.PageGenBooks(customerId,model);
                    break;
                case (2):
                    view.PageGenDetail(customerId,bookId, model);
                    break;
                case (3):
                    view.PageGenShoppingCart(customerId, model);
                    break;
                case (4):
                    AddBook(bookId,customerId);
                    break;
                case (5):
                    RemoveBook(bookId,customerId);
                    break;
            }

        }

        /// <summary>
        /// Validates user input
        /// </summary>
        /// <param name="command">Command as string</param>
        /// <returns>True if a command is valid false otherwise</returns>
        public bool ParseCommand(string command)
        {
            splitCommand = command.Split(new[] {' '},StringSplitOptions.RemoveEmptyEntries);
            var bookId = 0;

            if (splitCommand.Length != 3)
            {
                return false;
            }

            if (splitCommand[0] != "GET")
            {
                return false;
            }

            if (!int.TryParse(splitCommand[1], out var customerId))
            {
                return false;
            }

            if (model.GetCustomer(customerId) == null)
            {
                return false;
            }

            var request = splitCommand[2];
            var httpRequest = GetHttpRequest(request);
            arguments = splitCommand[2].Split('/');
            if (arguments.Length == 6 && !int.TryParse(arguments[5], out bookId))
            {
                return false;
            }

            if (arguments.Length != 4 && arguments.Length != 6)
            {
                return false;
            }

            switch (httpRequest)
            {
                case (1):
                    break;
                case (2):
                    if (model.GetBook(bookId) == null)
                    {
                        return false;
                    }
                    break;
                case (3):
                    break;
                case (4):
                    if (model.GetBook(bookId) == null)
                    {
                        return false;
                    }
                    break;
                case (5):
                    if (model.GetBook(bookId) == null)
                    {
                        return false;
                    }

                    Book book = model.GetBook(bookId);
                    var item = model.GetCustomer(customerId).ShoppingCart.GetById(book.Id);
                    if (item == null || !model.GetCustomer(customerId).ShoppingCart.Items.Contains(item))
                    {
                        return false;
                    }
                    break;
                case (0):
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if GET request is valid
        /// </summary>
        /// <param name="request">URL from user input</param>
        /// <returns>Request id if a request is valid, 0 otherwise</returns>
        public int GetHttpRequest(string request)
        {
            var httpRequest = 0;

            const string server = "http://www.nezarka.net/";

            Match match;
            var matchAdd = new Regex("^" + server + "ShoppingCart/Add/([0-9]+)$");
            var matchRemove = new Regex("^" + server + "ShoppingCart/Remove/([0-9]+)$");
            var matchDetail = new Regex("^" + server + "Books/Detail/([0-9]+)$");
            if (request == server + "Books")
            {
                httpRequest = 1;
                return httpRequest;
            }

            match = matchDetail.Match(request);
            if (match.Success)
            {
                httpRequest = 2;
                return httpRequest;
            }

            if (request == server + "ShoppingCart")
            {
                httpRequest = 3;
                return httpRequest;
            }

            match = matchAdd.Match(request);
            if (match.Success)
            {
                httpRequest = 4;
                return httpRequest;
            }

            match = matchRemove.Match(request);
            if (match.Success)
            {
                httpRequest = 5;
                return httpRequest;
            }

            return httpRequest;
        }
    }
}