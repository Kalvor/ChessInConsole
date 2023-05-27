using Game.Jobs;
using Game.Processes;
using Game.Tools;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Game
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGameServices(this IServiceCollection services)
        {

            services.RegisterJobs();
            services.RegisterProcesses();

            services.AddSingleton<InputReader>();
            services.AddSingleton<BoardDisplayer>();
            services.AddSingleton<MessageDisplayer>();
            services.AddSingleton((sp) => new CancellationPool(GetContractsImplementationTypes<IJob>()));
            return services;
        }

        private static IEnumerable<Type> GetContractsImplementationTypes<TContract>()
        {
            return Assembly
               .GetExecutingAssembly()
               .GetTypes()
               .Where(c => c.IsAssignableTo(typeof(TContract)) && !c.IsInterface && !c.IsAbstract)
               .ToList();
        }
        private static IServiceCollection RegisterProcesses(this IServiceCollection services)
        {
            _ = GetContractsImplementationTypes<IProcess>()
                .Select(c => services.AddSingleton(c, c))
                .ToList();

            return services;
        }
        private static IServiceCollection RegisterJobs(this IServiceCollection services)
        {
            _ = GetContractsImplementationTypes<IJob>()
                .Select(c => services.AddSingleton(typeof(IJob), c))
                .ToList();

            return services;
        }
    }
}
