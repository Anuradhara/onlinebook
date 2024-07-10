namespace BookShoppingCart.Models.DTOs
{
    public record TopNSoldBookModel(string BookName, string AuthorName, int TotalUnitSold, string Image);
}