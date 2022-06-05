using ChessMate.Infrastructure.Models;
using ChessMate.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChessMate.Infrastructure.Extensions
{
    public static class DependencyExtension
    {
        public static void InitializeDatabase(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<ChessMateDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        public static void InitializeRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRepository<ColorEntity>, Repository<ColorEntity>>();
            serviceCollection.AddTransient<IRepository<FigureEntity>, Repository<FigureEntity>>();
            serviceCollection.AddTransient<IRepository<PositionEntity>, Repository<PositionEntity>>();
        }
    }
}
