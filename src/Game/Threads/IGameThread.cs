namespace Game.Threads
{
    public interface IGameThread
    {
        bool IsCancaled { get; }
        bool IsPaused { get; }
        Task RunAsync();
        Task StopAsync();
    }
}
