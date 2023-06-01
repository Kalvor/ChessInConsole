namespace Game.Jobs
{
    public interface IJob
    {
        bool IsPaused { get; }
        CancellationToken CancellationToken { get; }
        Task RunAsync();
        Task StopAsync();
        Task JobMethodAsync();
    }
}
