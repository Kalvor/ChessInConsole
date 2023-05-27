using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace Game.Processes
{
    public static class ProcessesOrchestrator
    {
        private static ConcurrentDictionary<int, Type> _OpenProcesses = new ConcurrentDictionary<int, Type>();

        public static void StartProcess<TProcess>() where TProcess : IProcess
        {
            var processId = (string)typeof(TProcess).GetField("InternalId", BindingFlags.Public | BindingFlags.Static)!.GetValue(null)!;
            ProcessStartInfo p_info = new ProcessStartInfo();
            p_info.UseShellExecute = true;
            p_info.CreateNoWindow = false;
            p_info.WindowStyle = ProcessWindowStyle.Normal;
            p_info.FileName = @"Game.exe";
            p_info.ArgumentList.Add(processId);
            
            var process = Process.Start(p_info);
            _OpenProcesses.TryAdd(process.Id,typeof(TProcess));
        }

        public static void KillProcess<TProcess>() where TProcess : IProcess
        {
            var processes = Process.GetProcesses().Where(c=> GetProcessesIds<TProcess>().Contains(c.Id)).ToList();
            foreach(var process in processes)
            {
                process.Kill();
                _OpenProcesses.Remove(process.Id,out _);
            }
        }

        public static void KillAllProcesses()
        {
            var processes = Process.GetProcesses().Where(c => _OpenProcesses.Select(c=>c.Key).Contains(c.Id)).ToList();
            foreach (var process in processes)
            {
                _OpenProcesses.Remove(process.Id, out _);
                process.Kill();
            }
        }

        public static async Task StartProcessByInternalIdAsync(string id,IServiceProvider services)
        {
            var processType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(c => c.IsAssignableTo(typeof(IProcess)) && !c.IsInterface && !c.IsAbstract)
                .Where(c => (string)c.GetField("InternalId",BindingFlags.Public|BindingFlags.Static)!.GetValue(null)! == id)
                .First();
            _OpenProcesses.TryAdd(Process.GetCurrentProcess().Id, processType);
            var processInstance = (IProcess)services.GetService(processType)!;
            await processInstance.StartAsync();
        }

        private static ICollection<int> GetProcessesIds<TProcess>() where TProcess : IProcess
        {
            return _OpenProcesses
                .Where(c => c.Value.IsAssignableFrom(typeof(TProcess)))
                .Select(c => c.Key).ToList();
        }
    }
}
