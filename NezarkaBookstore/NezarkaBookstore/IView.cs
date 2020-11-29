namespace NezarkaBookstore
{
    interface IView
    {
        void PageGenBooks(int customerId, IModel model);
        void PageGenDetail(int customerId, int bookId, IModel model);
        void PageGenShoppingCart(int customerId, IModel model);
        void PageGenError();
        void Finish();
    }
}