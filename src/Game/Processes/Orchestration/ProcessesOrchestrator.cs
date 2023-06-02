using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;

namespace Game.Processes.Orchestration
{
    public static class ProcessesOrchestrator
    {
        public static void SuspendProcess(nint processWindowHandle, int processId) 
        { 
            if(processWindowHandle != 0) 
               Kernel32_Dll_Import.ShowWindow(processWindowHandle, (int)WindowVisibilityEnum.SW_HIDE);

            if(processId != 0)
                SuspendProcess(processId);
        }

        public static void ResumeProcess(nint processWindowHandle, int processId) 
        {
            if (processWindowHandle != 0)
                Kernel32_Dll_Import.ShowWindow(processWindowHandle, (int)WindowVisibilityEnum.SW_SHOW);

            if(processId != 0)
                ResumeProcess(processId);
        }

        public static async Task StartProcessByInternalIdAsync(string id, IServiceProvider services, string[] objectJsonData)
        {
            var processType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(c => c.IsAssignableTo(typeof(IProcess)) && !c.IsInterface && !c.IsAbstract)
                .Where(c => (string)c.GetField("InternalId", BindingFlags.Public | BindingFlags.Static)!.GetValue(null)! == id)
                .First();
            var processInstance = (IProcess)services.GetService(processType)!;
            await processInstance.StartAsync(objectJsonData);
        }

        public static void RedirectProcessControl<TTargetProcess>(params object[] targetAdditionalData)
            where TTargetProcess : IProcess
        {
            nint currentProcessWindowHandle = Kernel32_Dll_Import.GetConsoleWindow();
            int currentProcessId = Process.GetCurrentProcess().Id;
            var process = StartProcess<TTargetProcess>(currentProcessWindowHandle, currentProcessId, targetAdditionalData);
            while (!process!.HasExited) //Softlock preventing triggering process to close itself and further execution (possible due to time needed in another instance for suspension of trigerring process threads) 
            {

            }
        }

        private static Process StartProcess<TTargetProcess>(nint currentProcessWindowHandle, int currentProcessId, params object[] additionalData)
            where TTargetProcess : IProcess
        {
            var targetProcessInternalId = (string)typeof(TTargetProcess).GetField("InternalId", BindingFlags.Public | BindingFlags.Static)!.GetValue(null)!;
            
            ProcessStartInfo p_info = new ProcessStartInfo();        
            p_info.UseShellExecute= true;
            p_info.FileName = @"Game.exe";
            p_info.ArgumentList.Add(targetProcessInternalId);
            p_info.ArgumentList.Add(currentProcessWindowHandle.ToString());
            p_info.ArgumentList.Add(currentProcessId.ToString());
            foreach(var obj in additionalData)
            {
                p_info.ArgumentList.Add(JsonConvert.SerializeObject(obj));
            }
            return Process.Start(p_info)!;
        }

        private static void SuspendProcess(int processId)
        {
            var process = Process.GetProcessById(processId);
            foreach (ProcessThread thread in process.Threads)
            {
                IntPtr pOpenThread = Kernel32_Dll_Import.OpenThread(ThreadAccessEnum.SUSPEND_RESUME, false, (uint)thread.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                Kernel32_Dll_Import.SuspendThread(pOpenThread);

                Kernel32_Dll_Import.CloseHandle(pOpenThread);
            }
        }

        private static void ResumeProcess(int processId)
        {
            var process = Process.GetProcessById(processId);
            foreach (ProcessThread thread in process.Threads)
            {
                IntPtr pOpenThread = Kernel32_Dll_Import.OpenThread(ThreadAccessEnum.SUSPEND_RESUME, false, (uint)thread.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                int suspendCount;
                do
                {
                    suspendCount = Kernel32_Dll_Import.ResumeThread(pOpenThread);
                } while (suspendCount > 0);

                Kernel32_Dll_Import.CloseHandle(pOpenThread);
            }
        }
    }
}



