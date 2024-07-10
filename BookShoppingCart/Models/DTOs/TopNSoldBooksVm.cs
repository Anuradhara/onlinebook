namespace BookShoppingCart.Models.DTOs
{
    public record TopNSoldBooksVm(DateTime StartDate, DateTime EndDate, IEnumerable<TopNSoldBookModel> TopNSoldBooks);
}
