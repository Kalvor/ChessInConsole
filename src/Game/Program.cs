using Game;
using Game.Processes.Orchestration;
using Microsoft.Extensions.DependencyInjection;
using Networking;

var services = new ServiceCollection();
services.AddNetworkingLayer();
services.AddGameServices();
IServiceProvider serviceProvider = services.BuildServiceProvider();

string processId = args.Length > 0 ?
        args[0] : "2";

nint triggeringProcessWindowHandle = args.Length > 1 ?
    nint.Parse(args[1]) : 0;

int triggeringProcessId = args.Length > 2 ?
    int.Parse(args[2]) : 0;

AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
Console.CancelKeyPress += OnCancelKeyPressed;

try
{
    Kernel32_Dll_Import.ShowWindow(Kernel32_Dll_Import.GetConsoleWindow(), (int)WindowVisibilityEnum.SW_SHOW);

    ProcessesOrchestrator.SuspendProcess(triggeringProcessWindowHandle, triggeringProcessId);

    await ProcessesOrchestrator.StartProcessByInternalIdAsync(processId, serviceProvider, args.Skip(3).ToArray());

    ProcessesOrchestrator.ResumeProcess(triggeringProcessWindowHandle, triggeringProcessId);

}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    Console.Read();
    ProcessesOrchestrator.ResumeProcess(triggeringProcessWindowHandle, triggeringProcessId);
    throw;
}

void OnProcessExit(object? sender, EventArgs e)
{
    ProcessesOrchestrator.ResumeProcess(triggeringProcessWindowHandle, triggeringProcessId);
}
void OnCancelKeyPressed(object? sender, ConsoleCancelEventArgs e)
{
    if(IsMain())
    {
        Environment.Exit(0);
    }
    else
    {
        OnProcessExit(sender, e);
    }
}

bool IsMain()
{
    return triggeringProcessWindowHandle == 0 && triggeringProcessId == 0;
}
