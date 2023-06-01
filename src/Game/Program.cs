﻿using Game;
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

try
{
	var processId = args.Length > 0 ?
		args[0] :
		"0";
	var currentProcesses = args.Length > 1 ?
		JsonConvert.DeserializeObject<ConcurrentDictionary<int, string>>(args[1]) :
		new ConcurrentDictionary<int, string>();
    Kernel32_Dll_Import.ShowWindow(Kernel32_Dll_Import.GetConsoleWindow(), 5);

    await ProcessesOrchestrator.StartProcessByInternalIdAsync(processId, currentProcesses, serviceProvider);
}
catch (Exception)
{
	ProcessesOrchestrator.ReturnProcessControl<MainProcess>();
    throw;
}



