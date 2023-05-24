using Game.Threads;
using Game.Tools;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Game
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGameServices(this IServiceCollection services)
        {

            services.AddGameThreads();

            services.AddSingleton<ConsoleGameHost>();
            services.AddSingleton<InputReader>();
            services.AddSingleton<BoardDisplayer>();
            services.AddSingleton<MessageDisplayer>();
            services.AddSingleton((sp) => new CancellationPool(GetGameThreadTypes()));
            return services;
        }

        private static IServiceCollection AddGameThreads(this IServiceCollection services)
        {
            _ =  GetGameThreadTypes()
                .Select(c=> services.AddSingleton(typeof(IGameThread),c))
                .ToList();

            return services;
        }

        private static IEnumerable<Type> GetGameThreadTypes()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(c => c.IsAssignableTo(typeof(IGameThread)) && !c.IsInterface && !c.IsAbstract)
                .ToList();
        }
    }
}
