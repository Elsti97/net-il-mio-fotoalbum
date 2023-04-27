using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {

        private readonly AlbumContext _context;

        public PhotosController(AlbumContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetPhotos([FromQuery] string? title)
        {
            var photos = _context.Photos.Where(p => title == null || (p.Title ?? "").Contains(title))
                .Include(p => p.Categories)
                .ToList();

            foreach (var photo in photos)
            {
                foreach (var category in photo.Categories) 
                {
                    category.Photos = null;
                }
            }
            return Ok(photos);
        }

        [HttpGet("{id}")]
        public IActionResult GetPhotoById(int id)
        {
            var photoOne = _context.Photos.FirstOrDefault(p => p.Id == id);
            if (photoOne == null)
            {
                return NotFound();
            }
            return Ok(photoOne);
        }

        [HttpPost]
        public IActionResult CreatePhoto([FromBody] Photo photo)
        {
            _context.Photos.Add(photo);
            _context.SaveChanges();

            return Ok(photo);
        }

        [HttpPut("{id}")]
        public IActionResult photoUpdate(int id, [FromBody] Photo photo)
        {
            var photoUpdate = _context.Photos.FirstOrDefault(p => p.Id == id);

            if (photoUpdate == null)
            {
                return NotFound();
            }

            photoUpdate.Title = photo.Title;
            photoUpdate.Description = photo.Description;
            photoUpdate.Url = photo.Url;
            photoUpdate.Visible = photo.Visible;
            //photoUpdate.Categories = photoFormModel.SelectedCategories?.Select(sc => _context.Categories.First(c => c.Id == Convert.ToInt32(sc))).ToList();

            _context.SaveChanges();

            return Ok(photoUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);

            if (photo == null)
            {
                return NotFound();
            }

            _context.Photos.Remove(photo);
            _context.SaveChanges();

            return Ok();
        }
    }
}
