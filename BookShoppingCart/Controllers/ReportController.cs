using BookShoppingCart.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BookShoppingCart.Controllers
{
    public class ReportController : Controller
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }
        public async Task<IActionResult> TopNSellingBooks(DateTime? startDate, DateTime? endDate, int topN = 5)
        {
            try
            {
                // Use default values if startDate and endDate are not provided
                DateTime sDate = startDate ?? DateTime.UtcNow.AddDays(-7);
                DateTime eDate = endDate ?? DateTime.UtcNow;

                var topSellingBooksVm = await _reportService.GetTopNSellingBooksByDateAsync(sDate, eDate, topN);
               

                return View(topSellingBooksVm);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Something went wrong";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}

