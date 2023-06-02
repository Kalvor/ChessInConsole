using Game;
using Game.Processes;
using Game.Processes.Implementations;
using Game.Processes.Orchestration;
using Microsoft.Extensions.DependencyInjection;
using Networking;
using Newtonsoft.Json;
using System.Collections.Concurrent;

var services = new ServiceCollection();
services.AddNetworkingLayer();
services.AddGameServices();
IServiceProvider serviceProvider = services.BuildServiceProvider();

string processId = args.Length > 0 ?
        args[0] : "0";

nint triggeringProcessWindowHandle = args.Length > 1 ?
    nint.Parse(args[1]) : 0;

int triggeringProcessId = args.Length > 2 ?
    int.Parse(args[2]) : 0;

try
{
    Kernel32_Dll_Import.ShowWindow(Kernel32_Dll_Import.GetConsoleWindow(), (int)WindowVisibilityEnum.SW_SHOW);

    ProcessesOrchestrator.SuspendProcessTriggeringProcess(triggeringProcessWindowHandle, triggeringProcessId);

	await ProcessesOrchestrator.StartProcessByInternalIdAsync(processId, serviceProvider, args.Skip(3).ToArray());

    ProcessesOrchestrator.ResumeProcessTriggeringProcess(triggeringProcessWindowHandle, triggeringProcessId);

}
catch (Exception e )
{
	Console.WriteLine(e.Message);
	Console.Read();
    ProcessesOrchestrator.ResumeProcessTriggeringProcess(triggeringProcessWindowHandle, triggeringProcessId);
    throw;
}



