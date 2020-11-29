using System;
using System.ComponentModel.Design;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace NezarkaBookstore
{
    class Controller : IController
    {
        private IModel model;
        private IView view;
        private TextReader reader;
        private string[] splitCommand;
        private string[] arguments;


        public void Run()
        {
            //reader = File.OpenText("NezarkaTest.in");
            reader = Console.In;
            view = new View(Console.Out);
            model = Model.LoadFrom(reader);
            if (model == null)
            {
                Console.WriteLine("Data Error");
                return;
            }
            ReadCommands();
        }

        public void ReadCommands()
        {
            string command;
            while ((command = reader.ReadLine()) != null)
            {

                if (ParseCommand(command) == false)
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

        public void AddBook(int bookId, int customerId)
        {
            var item = model.GetCustomer(customerId).ShoppingCart.GetById(bookId);
            if (model.GetCustomer(customerId).ShoppingCart.Items.Contains(item))
            {
                model.GetCustomer(customerId).ShoppingCart.Items[bookId].Count++;
            }
            else
            {
                var newItem = new ShoppingCartItem { BookId = bookId, Count = 1 };
                model.GetCustomer(customerId).ShoppingCart.Items.Add(newItem);
            }
            view.PageGenShoppingCart(customerId, model);
        }

        public void RemoveBook(int bookId, int customerId)
        {
            var item = model.GetCustomer(customerId).ShoppingCart.GetById(bookId);
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

        public bool ParseCommand(string command)
        {
            splitCommand = command.Split(' ');
            var bookId = 0;

            if (splitCommand.Length != 3)
            {
                return false;
            }

            if (splitCommand[0] != "GET")
            {
                return false;
            }

            if (int.TryParse(splitCommand[1], out var customerId) == false)
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
            if (arguments.Length == 6)
            {
                if (!int.TryParse(arguments[5], out bookId)) return false;
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

                    var item = model.GetCustomer(customerId).ShoppingCart.GetById(bookId);
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

        public int GetHttpRequest(string request)
        {
            var httpRequest = 0;

            const string server = "http://www.nezarka.net/";

            Match match;
            var matchAdd = new Regex(server + "ShoppingCart/Add/([0-9]+)$");
            var matchRemove = new Regex(server+"ShoppingCart/Remove/([0-9]+)$");
            var matchDetail = new Regex(server+"Books/Detail/([0-9]+)$");


            if (request == server + "Books")
            {
                httpRequest = 1;
            }

            match = matchDetail.Match(request);
            if (match.Success)
            {
                httpRequest = 2;
            }

            if (request == server + "ShoppingCart")
            {
                httpRequest = 3;
            }

            match = matchAdd.Match(request);
            if (match.Success)
            {
                httpRequest = 4;
            }

            match = matchRemove.Match(request);
            if (match.Success)
            {
                httpRequest = 5;
            }

            return httpRequest;
        }
    }
}