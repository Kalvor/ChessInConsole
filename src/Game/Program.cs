using Game;
using Game.Tools;
using Microsoft.Extensions.DependencyInjection;
using Networking.Services.Implementations;
using System.Net.NetworkInformation;


//var services = new ServiceCollection();
//services.AddGameServices();
//IServiceProvider serviceProvider = services.BuildServiceProvider();
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
var networkAccessor = new NetworkAccessor();
//var hosts = networkAccessor.GetAvaliableHosts().ToList();

await networkAccessor.SendDataToHostAsync(new Networking.Data.Host
{
    Address = System.Net.IPAddress.Parse("192.168.1.6")
}, "DUPA");
Console.WriteLine();