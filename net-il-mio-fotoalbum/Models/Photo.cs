namespace net_il_mio_fotoalbum.Models
{
    public class Photo
    {

        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public bool Visible { get; set; }

        //public int? CategoryId { get; set; }
        //public Category? Category { get; set; }
    }
}
