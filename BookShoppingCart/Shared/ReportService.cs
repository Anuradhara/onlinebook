using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System;
using static BookShoppingCart.Models.DTOs.TopNSoldBookModel;
using static BookShoppingCart.Models.DTOs.TopNSoldBooksVm;



namespace BookShoppingCart.Shared
{
    public class ReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TopNSoldBooksVm> GetTopNSellingBooksByDateAsync(DateTime startDate, DateTime endDate, int topN = 5)
        {
            try
            {
                var query = from od in _context.OrderDetails
                            join o in _context.Orders on od.OrderId equals o.Id
                            join b in _context.Books on od.BookId equals b.Id
                            where o.IsPaid && !o.IsDeleted && o.CreateDate >= startDate && o.CreateDate <= endDate
                            group od by new { b.BookName, b.AuthorName, b.Image } into g
                            select new
                            {
                                g.Key.BookName,
                                g.Key.AuthorName,
                                TotalUnitSold = g.Sum(od => od.Quantity),
                                g.Key.Image
                            };

                var topSellingBooks = query
                    .AsEnumerable()  // Switch to client-side evaluation
                    .Select(g => new TopNSoldBookModel(
                        g.BookName,
                        g.AuthorName,
                        g.TotalUnitSold,
                        g.Image
                    ))
                    .OrderByDescending(b => b.TotalUnitSold)
                    .Take(topN)
                    .ToList();

                return new TopNSoldBooksVm(startDate, endDate, topSellingBooks);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                throw new ApplicationException("An error occurred while generating the report.", ex);
            }
        }
    }
}

