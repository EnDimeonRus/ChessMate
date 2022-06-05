using ChessMate.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ChessMate.Infrastructure
{
    public class ChessMateDbContext : DbContext
    {
        public virtual DbSet<ColorEntity> Colors { get; set; }
        public virtual DbSet<FigureEntity> Figures { get; set; }
        public virtual DbSet<PositionEntity> Positions { get; set; }



        public ChessMateDbContext(DbContextOptions<ChessMateDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ColorEntity>(entity => entity.ToTable("Colors"));
            modelBuilder.Entity<FigureEntity>(entity => entity.ToTable("Figures"));
            modelBuilder.Entity<PositionEntity>(entity => entity.ToTable("Positions"));


            modelBuilder.Entity<ColorEntity>().Property(x => x.ID)
                .IsRequired();
            modelBuilder.Entity<ColorEntity>().Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<FigureEntity>().Property(x => x.ID)
                .IsRequired();
            modelBuilder.Entity<FigureEntity>().Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<FigureEntity>().Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(10);


            modelBuilder.Entity<PositionEntity>()
                .HasOne(x => x.Color)
                .WithMany()
                .IsRequired();
            modelBuilder.Entity<PositionEntity>()
                .HasOne(x => x.Figure)
                .WithMany()
                .IsRequired();
            modelBuilder.Entity<PositionEntity>().Property(x => x.ID)
                .IsRequired();
            modelBuilder.Entity<PositionEntity>().Property(x => x.PreviousPosition)
                .IsRequired()
                .HasMaxLength(2);
            modelBuilder.Entity<PositionEntity>().Property(x => x.CurrentPosition)
                .IsRequired()
                .HasMaxLength(2);

            modelBuilder.Entity<ColorEntity>()
                .HasData(
                new ColorEntity { ID = 1, Description = "Белый" },
                new ColorEntity { ID = 2, Description = "Черный" });

            modelBuilder.Entity<FigureEntity>().HasData(
                new FigureEntity() { ID = 1, Code = "п", Description = "Пешка" },
                new FigureEntity() { ID = 2, Code = "С", Description = "Слон" },
                new FigureEntity() { ID = 3, Code = "К", Description = "Конь" },
                new FigureEntity() { ID = 4, Code = "Л", Description = "Ладья" },
                new FigureEntity() { ID = 5, Code = "Ф", Description = "Ферзь" },
                new FigureEntity() { ID = 6, Code = "Кр", Description = "Король" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
