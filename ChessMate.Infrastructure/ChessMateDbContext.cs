using ChessMate.Infrastructure.Models;
using System.Data.Entity;

namespace ChessMate.Infrastructure
{
    public class ChessMateDbContext : DbContext
    {
        public ChessMateDbContext() : base("Data Source=EPRUSAMW00DB\\SQLEXPRESS;Initial Catalog=ChessMate;Integrated Security=True")
        {
            Database.SetInitializer(new ChessMateInitializer());
        }
        
        public DbSet<Color> Colors { get; set; }
        public DbSet<Figure> Figures { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}
