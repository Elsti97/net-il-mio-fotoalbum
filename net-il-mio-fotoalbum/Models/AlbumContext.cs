using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Models
{
    public class AlbumContext : DbContext
    {

        public AlbumContext(DbContextOptions<AlbumContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-fotografo;Integrated Security=True; TrustServerCertificate=True; encrypt = False");
        }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Category> Categories { get; set; }


        public void Seed()
        {
            if (!Categories.Any())
            {
                var seedCategories = new Category[]
                {
            new Category { Name = "Ritratto" },
            new Category { Name = "Evento" },
            new Category { Name = "Moda" },
            new Category { Name = "Arte" },
            new Category { Name = "Videogioco" },
            new Category { Name = "Architettura" },
            new Category { Name = "Viaggio" },
            new Category { Name = "Prodotto" },
            new Category { Name = "Pubblicitario" },
            new Category { Name = "Giornalismo" },
            new Category { Name = "Sportivo" },
            new Category { Name = "Animali" },
            new Category { Name = "Scientifica" },
            new Category { Name = "Aerea" },
                };

                Categories.AddRange(seedCategories);

                SaveChanges();
            }

            if (!Photos.Any())
            {
                var seed = new Photo[]
                {
                new Photo
                {
                    Title = "Tramonto",
                    Description  = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean rutrum magna quis lorem pellentesque, ut mattis odio interdum. Suspendisse vel bibendum eros, non ullamcorper odio",
                    Url  = "https://picsum.photos/id/20/300/200",
                    Visible = true
                },
                new Photo
                {
                    Title = "Monumento",
                    Description = "Morbi dapibus, purus vel consequat pretium, orci ante aliquet urna, id pretium dolor orci et nunc. Nam imperdiet mi ligula, in lobortis magna iaculis ac",
                    Url = "https://picsum.photos/id/3/300/200",
                    Visible = true
                }
                };

                Photos.AddRange(seed);
                SaveChanges();
            }
        }
    }
}
