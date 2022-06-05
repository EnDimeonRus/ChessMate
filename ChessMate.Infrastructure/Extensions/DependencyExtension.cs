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
    }
}
