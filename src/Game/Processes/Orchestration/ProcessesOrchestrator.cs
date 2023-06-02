using Game.Processes.Implementations;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Game.Processes.Orchestration
{
    public static class ProcessesOrchestrator
    {
        public static ConcurrentDictionary<int, string> _OpenProcesses = new ConcurrentDictionary<int, string>();

        public static void RedirectProcessControl<TCurrentProcess, TTargetProcess>(params object[] targetAdditionalData)
            where TCurrentProcess : IProcess
            where TTargetProcess : IProcess
        {
            StartProcess<TTargetProcess>(targetAdditionalData);
            Kernel32_Dll_Import.ShowWindow(Kernel32_Dll_Import.GetConsoleWindow(), (int)WindowVisibilityEnum.SW_HIDE);
            SuspendProcess<TCurrentProcess>();
            Kernel32_Dll_Import.ShowWindow(Kernel32_Dll_Import.GetConsoleWindow(), (int)WindowVisibilityEnum.SW_SHOW);
        }

        public static void ReturnProcessControl<TTargetProcess>(Type currentProcessType)
            where TTargetProcess : IProcess
        {
            ResumeProcess<TTargetProcess>();
            KillProcess(currentProcessType);
        }

        public static async Task StartProcessByInternalIdAsync(string id, ConcurrentDictionary<int, string> openProcesses, IServiceProvider services, string[] objectJsonData)
        {
            foreach (var oP in openProcesses)
            {
                _OpenProcesses.TryAdd(oP.Key, oP.Value);
            }

            var processType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(c => c.IsAssignableTo(typeof(IProcess)) && !c.IsInterface && !c.IsAbstract)
                .Where(c => (string)c.GetField("InternalId", BindingFlags.Public | BindingFlags.Static)!.GetValue(null)! == id)
                .First();
            var processInstance = (IProcess)services.GetService(processType)!;
            _OpenProcesses.TryAdd(Process.GetCurrentProcess().Id, processType.Name);
            await processInstance.StartAsync(objectJsonData);
        }

        private static void StartProcess<TProcess>(params object[] additionalData ) where TProcess : IProcess
        {
            var processId = (string)typeof(TProcess).GetField("InternalId", BindingFlags.Public | BindingFlags.Static)!.GetValue(null)!;
            ProcessStartInfo p_info = new ProcessStartInfo();
            p_info.UseShellExecute = true;
            p_info.FileName = @"Game.exe";
            p_info.ArgumentList.Add(processId);
            p_info.ArgumentList.Add(JsonConvert.SerializeObject(_OpenProcesses));
            foreach(var obj in additionalData)
            {
                p_info.ArgumentList.Add(JsonConvert.SerializeObject(obj));
            }
            Process.Start(p_info);
        }

        private static void SuspendProcess<TProcess>() where TProcess : IProcess
        {
            var processesIds = GetProcessesIds(typeof(TProcess));
            var processes = Process.GetProcesses().Where(c => processesIds.Contains(c.Id)).ToList();
            foreach (var process in processes)
            {
                foreach (ProcessThread thread in process.Threads)
                {
                    var pOpenThread = Kernel32_Dll_Import.OpenThread(ThreadAccessEnum.SUSPEND_RESUME, false, (uint)thread.Id);
                    if (pOpenThread == IntPtr.Zero)
                        break;

                    Kernel32_Dll_Import.SuspendThread(pOpenThread);
                }
            }
        }

        private static void ResumeProcess<TProcess>() where TProcess : IProcess
        {
            var processesIds = GetProcessesIds(typeof(TProcess));
            var processes = Process.GetProcesses().Where(c => processesIds.Contains(c.Id)).ToList();
            foreach (var process in processes)
            {
                foreach (ProcessThread thread in process.Threads)
                {
                    IntPtr pOpenThread = Kernel32_Dll_Import.OpenThread(ThreadAccessEnum.SUSPEND_RESUME, false, (uint)thread.Id);

                    if (pOpenThread == IntPtr.Zero)
                        continue;

                    int suspendCount = 0;
                    do
                    {
                        suspendCount = Kernel32_Dll_Import.ResumeThread(pOpenThread);
                    } while (suspendCount > 0);

                    Kernel32_Dll_Import.CloseHandle(pOpenThread);
                }
            }
        }

        private static void KillProcess(Type processType)
        {
            var processesIds = GetProcessesIds(processType);
            var processes = Process.GetProcesses().Where(c => processesIds.Contains(c.Id)).ToList();
            foreach (var process in processes)
            {
                _OpenProcesses.Remove(process.Id, out _);
                process.Kill();
            }
        }

        private static ICollection<int> GetProcessesIds(Type processType)
        {
            return _OpenProcesses
                .Where(c => c.Value == processType.Name)
                .Select(c => c.Key).ToList();
        }   
    }
}



