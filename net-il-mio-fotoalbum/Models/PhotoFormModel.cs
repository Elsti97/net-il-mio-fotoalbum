using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoFormModel
    {
        public Photo Photo { get; set; } = new Photo { Url = "https://picsum.photos/300/200" }; 

        public List<SelectListItem>? Categories { get; set; }

        [Required(ErrorMessage = "Selezionare almeno una categoria")]
        public List<string>? SelectedCategories { get; set; }
    }
}
