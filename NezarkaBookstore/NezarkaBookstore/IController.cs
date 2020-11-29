namespace NezarkaBookstore
{
    interface IController
    {
        void Run();
        void ReadCommands();
        void AddBook(int bookId, int customerId);
        void RemoveBook(int bookId, int customerId);
        void ExecuteCommand();
        bool ParseCommand(string command);
        int GetHttpRequest(string request);

    }
}