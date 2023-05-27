using Game;
using Game.Processes;
using Game.Processes.Implementations;
using Game.Tools;
using Microsoft.Extensions.DependencyInjection;
using Networking;
using Networking.Services.Implementations;
using System.Diagnostics;
using System.Net.NetworkInformation;

var services = new ServiceCollection();
services.AddNetworkingLayer();
services.AddGameServices();
IServiceProvider serviceProvider = services.BuildServiceProvider();
var processId = args.Length > 0 ? args[0] : "0";
await ProcessesOrchestrator.StartProcessByInternalIdAsync(processId, serviceProvider);



