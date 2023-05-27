using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Game.Processes
{
    public static class ProcessesOrchestrator
    {
        public static ConcurrentDictionary<int, string> _OpenProcesses = new ConcurrentDictionary<int, string>();

        public static void StartProcess<TProcess>() where TProcess : IProcess
        {
            var processId = (string)typeof(TProcess).GetField("InternalId", BindingFlags.Public | BindingFlags.Static)!.GetValue(null)!;
            ProcessStartInfo p_info = new ProcessStartInfo();
            p_info.UseShellExecute = true;
            p_info.CreateNoWindow = false;
            p_info.WindowStyle = ProcessWindowStyle.Normal;
            p_info.FileName = @"Game.exe";
            p_info.ArgumentList.Add(processId);
            p_info.ArgumentList.Add(JsonConvert.SerializeObject(_OpenProcesses));
            
            var process = Process.Start(p_info);
            _OpenProcesses.TryAdd(process.Id,typeof(TProcess).Name);
        }

        public static void SuspendProcess<TProcess>() where TProcess : IProcess
        {
            var processes = Process.GetProcesses().Where(c => GetProcessesIds(typeof(TProcess)).Contains(c.Id)).ToList();
            foreach (var process in processes)
            {
                foreach (ProcessThread thread in process.Threads)
                {
                    var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                    if (pOpenThread == IntPtr.Zero)
                    {
                        break;
                    }
                    SuspendThread(pOpenThread);
                }
            }
        }

        public static void ResumeProcess<TProcess>() where TProcess : IProcess
        {
            var processes = Process.GetProcesses().Where(c => GetProcessesIds(typeof(TProcess)).Contains(c.Id)).ToList();
            Console.WriteLine(processes.Count);
            foreach (var process in processes)
            {
                foreach (ProcessThread thread in process.Threads)
                {
                    IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);

                    if (pOpenThread == IntPtr.Zero)
                    {
                        continue;
                    }

                    var suspendCount = 0;
                    do
                    {
                        suspendCount = ResumeThread(pOpenThread);
                    } while (suspendCount > 0);

                    CloseHandle(pOpenThread);
                }
            }
        }

        public static void KillProcess(Type processType)
        {
            var processes = Process.GetProcesses().Where(c => GetProcessesIds(processType).Contains(c.Id)).ToList();
            foreach (var process in processes)
            {
                _OpenProcesses.Remove(process.Id, out _);
                process.Kill();
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

        public static async Task StartProcessByInternalIdAsync(string id, ConcurrentDictionary<int, string> openProcesses, IServiceProvider services)
        {
            foreach(var oP in openProcesses)
            {
                _OpenProcesses.TryAdd(oP.Key, oP.Value);
            }
            
            var processType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(c => c.IsAssignableTo(typeof(IProcess)) && !c.IsInterface && !c.IsAbstract)
                .Where(c => (string)c.GetField("InternalId",BindingFlags.Public|BindingFlags.Static)!.GetValue(null)! == id)
                .First();
            _OpenProcesses.TryAdd(Process.GetCurrentProcess().Id, processType.Name);
            var processInstance = (IProcess)services.GetService(processType)!;
            await processInstance.StartAsync();
        }

        private static ICollection<int> GetProcessesIds(Type processType) 
        {
            return _OpenProcesses
                .Where(c => c.Value == processType.Name)
                .Select(c => c.Key).ToList();
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);

        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }
    }
}



