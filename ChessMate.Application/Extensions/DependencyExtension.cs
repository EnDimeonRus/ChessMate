using ChessMate.Application.Managers;
using ChessMate.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace ChessMate.Application.Extensions
{
    public static class DependencyExtension
    {
        public static void RegisterManagers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IPositionManager, PositionManager>();
        }

        public static void RegisterValidators(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IPositionValidator, PositionValidator>();
        }
    }
}
