using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMate.Application.Extensions
{
    public static class DependencyExtension
    {
        public static void RegisterManagers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IPositionManager, PositionManager>();
        }
    }
}
