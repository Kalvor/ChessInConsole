using Game;
using Microsoft.Extensions.DependencyInjection;
using Networking;


var services = new ServiceCollection();
services.AddNetworkingLayer();
services.AddGameServices();
IServiceProvider serviceProvider = services.BuildServiceProvider();
//var gameHost = serviceProvider.GetRequiredService<ConsoleGameHost>();
//var inputReader = serviceProvider.GetRequiredService<InputReader>();
//var messageDisplayer = serviceProvider.GetRequiredService<MessageDisplayer>();

//try
//{
//    messageDisplayer.AskForPlayerMetadata();
//    var playerMetadata = inputReader.GetPlayerMetadata();
//    await gameHost.RegisterInNetworkAsync(playerMetadata);

//    _ = gameHost.RunGameThreadsAsync().ConfigureAwait(true);
//    await gameHost.StopGameThreadsAsync();
//}
//catch(Exception)
//{
//    await gameHost.StopGameThreadsAsync();
//}
