using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SlothBookStore.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SlothBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookDbContext _dbContext;

        public BookController(BookDbContext dbContext, ILogger<HomeController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [Authorize]
        [Route("Book/{id}")]
        public IActionResult Index(string Id)
        {
            var books = _dbContext.BooksSet.ToList();
            var book = _dbContext.BooksSet.FirstOrDefault(t => t.Id == Id);

            return View(book);
        }
    }
}
