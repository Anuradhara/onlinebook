using BookShoppingCart.Models;
using BookShoppingCart.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;
       

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _logger = logger;
            _homeRepository = homeRepository;
        }

        public async Task<IActionResult> Index(string sterm = "", int genreId = 0, int pageNumber = 1, int pageSize = 9)
        {
            var (books, totalBooks) = await _homeRepository.GetBooks(sterm, genreId, pageNumber, pageSize);
            var genres = await _homeRepository.Genres();

            var bookModel = new BookDisplayModel
            {
                Books = books,
                Genres = genres,
                STerm = sterm,
                GenreId = genreId,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalBooks / (double)pageSize)
            };

            if (!string.IsNullOrEmpty(sterm) && books.Count() == 0)
            {
                TempData["searchMessage"] = $"No books found for '{sterm}'.";
            }

            return View(bookModel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
