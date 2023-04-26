

namespace net_il_mio_fotoalbum.Models

{
    public class Photo
    {

        public int Id { get; set; }

        //[Required(ErrorMessage = "Il campo Titolo è obbligatorio")]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public bool Visible { get; set; }

        public List<Category>? Categories { get; set; }
    }
}
