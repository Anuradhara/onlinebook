namespace BookShoppingCart.Models.DTOs
{
    public class BookDisplayModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string STerm { get; set; } = "";
        public int GenreId { get; set; } = 0;
        public int? SelectedGenreId { get; set; } 
        public int CurrentPage { get; set; } 
        public int TotalPages { get; set; }
    }
}
