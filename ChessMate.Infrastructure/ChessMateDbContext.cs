using ChessMate.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ChessMate.Infrastructure
{
    public class ChessMateDbContext : DbContext
    {
        public DbSet<Color> Colors { get; set; }
        public DbSet<Figure> Figures { get; set; }
        public DbSet<Position> Positions { get; set; }

        public ChessMateDbContext(DbContextOptions<ChessMateDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>().Property(x => x.ID)
                .IsRequired();
            modelBuilder.Entity<Color>().Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Figure>().Property(x=>x.Description)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Figure>().Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(10);


            modelBuilder.Entity<Color>()
                .HasData(
                new Color {ID = 1, Description = "Белый" },
                new Color {ID = 2,  Description = "Черный" });

            modelBuilder.Entity<Figure>().HasData(
                new Figure() { ID = 1, Code = "п", Description = "Пешка" },
                new Figure() { ID = 2, Code = "С", Description = "Слон" },
                new Figure() { ID = 3, Code = "К", Description = "Конь" },
                new Figure() { ID = 4, Code = "Л", Description = "Ладья" },
                new Figure() { ID = 5, Code = "Ф", Description = "Ферзь" },
                new Figure() { ID = 6, Code = "Кр", Description = "Король" }
                );

            base.OnModelCreating(modelBuilder);
        }


    }
        //public ChessMateDbContext() : base("Data Source=EPRUSAMW00DB\\SQLEXPRESS;Initial Catalog=ChessMate;Integrated Security=True")
     /*   public ChessMateDbContext() : base("name=Main")
        {
            Database.SetInitializer(new ChessMateInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.
            base.OnModelCreating(modelBuilder);
        }

       
    }*/
}
