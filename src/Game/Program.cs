using Game;
using Game.Processes;
using Game.Processes.Implementations;
using Game.Tools;
using Microsoft.Extensions.DependencyInjection;
using Networking;
using Networking.Services.Implementations;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.NetworkInformation;

var services = new ServiceCollection();
services.AddNetworkingLayer();
services.AddGameServices();
IServiceProvider serviceProvider = services.BuildServiceProvider();
var processId = args.Length > 0 ? args[0] : "0";
var currentProcesses = args.Length > 1 ? JsonConvert.DeserializeObject<ConcurrentDictionary<int, string>>(args[1]) : new ConcurrentDictionary<int, string>();
Console.WriteLine(currentProcesses.Count);
if(currentProcesses.Count> 0)
{
	foreach (var item in currentProcesses)
	{
		Console.WriteLine(item.Key+" | " + item.Value);
	}
}
await ProcessesOrchestrator.StartProcessByInternalIdAsync(processId, currentProcesses, serviceProvider);



