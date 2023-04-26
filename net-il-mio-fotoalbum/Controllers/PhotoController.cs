using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;

namespace net_il_mio_fotoalbum.Controllers
{
    public class PhotoController : Controller
    {
        private readonly ILogger<PhotoController> _logger;
        private readonly AlbumContext _context;

        public PhotoController(ILogger<PhotoController> logger, AlbumContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var photos = _context.Photos
                //.Include(p => p.Category)
                .DefaultIfEmpty()
                .ToArray();

            return View(photos);
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
