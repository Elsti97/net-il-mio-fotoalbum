using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                .DefaultIfEmpty()
                .ToArray();

            return View(photos);
        }

        public IActionResult Detail(int id)
        {
            var photo = _context.Photos
                .Include(p => p.Categories)
                .DefaultIfEmpty().SingleOrDefault(p => p.Id == id);

            if (photo is null)
            {
                return NotFound($"Foto numero: {id} non trovata");
            }

            return View(photo);
        }

        public IActionResult Create()
        {
            var photoFormModel = new PhotoFormModel
            {
                Categories = _context.Categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList(),
            };

            return View(photoFormModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoFormModel photoFormModel)
        {
            if (!ModelState.IsValid)
            {
                photoFormModel.Categories = _context.Categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

                return View(photoFormModel);
            }

            photoFormModel.Photo.Categories = photoFormModel.SelectedCategories?.Select(sc => _context.Categories.First(c => c.Id == Convert.ToInt32(sc))).ToList();

            _context.Photos.Add(photoFormModel.Photo);
            _context.SaveChanges();

            photoFormModel.Photo.Url = "https://picsum.photos/id/" + photoFormModel.Photo.Id + "/300/200";
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var photo = _context.Photos.Include(p => p.Categories).DefaultIfEmpty().SingleOrDefault(p => p.Id == id);

            if (photo is null)
            {
                return View($"Foto numero: {id} non trovata");
            }

            var photoFormModel = new PhotoFormModel
            {
                Photo = photo,
                Categories = _context.Categories.ToList().Select(c => new SelectListItem(c.Name, c.Id.ToString(), photo.Categories!.Any(_c => _c.Id == c.Id))).ToList(),
            };

            photoFormModel.SelectedCategories = photoFormModel.Categories.Where(c => c.Selected).Select(c => c.Value).ToList();

            return View(photoFormModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PhotoFormModel photoFormModel)
        {
            if (!ModelState.IsValid)
            {
                photoFormModel.Categories = _context.Categories.ToList().Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

                return View(photoFormModel);
            }

            var photoUpdate = _context.Photos.Include(p => p.Categories).FirstOrDefault(p => p.Id == id);

            if (photoUpdate is null)
            {
                return View($"Foto numero: {id} non trovata");
            }

            photoUpdate.Title = photoFormModel.Photo.Title;
            photoUpdate.Description = photoFormModel.Photo.Description;
            photoUpdate.Url = photoFormModel.Photo.Url;
            photoUpdate.Categories = photoFormModel.SelectedCategories?.Select(sc => _context.Categories.First(c => c.Id == Convert.ToInt32(sc))).ToList();

            _context.Photos.Update(photoUpdate);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var photoDelete = _context.Photos.FirstOrDefault(p => p.Id == id);

            if (photoDelete is null)
            {
                return View("NotFound");
            }

            _context.Photos.Remove(photoDelete);
            _context.SaveChanges();

            return RedirectToAction("Index");
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
