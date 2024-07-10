namespace BookShoppingCart.Repository
{
    public interface IBookRepository
    {
        Task AddBook(Book book);
        Task DeleteBook(Book book);
        Task<Book?> GetBookById(int id);
     
        Task UpdateBook(Book book);

        Task<(IEnumerable<Book> Books, int TotalCount)> GetBooks(string sTerm = "", int genreId = 0, int pageNumber = 1, int pageSize = 9);


    }
}
