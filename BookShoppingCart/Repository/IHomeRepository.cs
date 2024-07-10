namespace BookShoppingCart
{
    public interface IHomeRepository
    {
      
        Task<IEnumerable<Genre>> Genres();
        Task<(IEnumerable<Book> Books, int TotalCount)> GetBooks(string sTerm = "", int genreId = 0, int pageNumber = 1, int pageSize = 10);


    }
}