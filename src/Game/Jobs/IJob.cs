namespace Game.Jobs
{
    public interface IJob
    {
        bool IsPaused { get; }
        Task RunAsync();
        Task StopAsync();
    }
}
