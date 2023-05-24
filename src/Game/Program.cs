using Game;
using Game.Tools;
using Microsoft.Extensions.DependencyInjection;
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
foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
{
    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
    {
        if (!ip.IsDnsEligible)
        {
            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                // All IP Address in the LAN
            }
        }
    }
}
System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
System.Net.NetworkInformation.PingReply rep = p.Send("192.168.1.1");
if (rep.Status == System.Net.NetworkInformation.IPStatus.Success)
{
    //host is active
}