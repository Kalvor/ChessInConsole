using System.Runtime.InteropServices;

namespace Game.Processes.Orchestration
{
    public static class Kernel32_Dll_Import
    {
        [DllImport("kernel32.dll")]
        internal static extern IntPtr OpenThread(ThreadAccessEnum dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        
        [DllImport("kernel32.dll")]
        internal static extern uint SuspendThread(IntPtr hThread);

        [DllImport("kernel32.dll")]
        internal static extern int ResumeThread(IntPtr hThread);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool CloseHandle(IntPtr handle);
    }
}
