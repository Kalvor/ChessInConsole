namespace Game.Tools
{
    public sealed class InputReader
    {
        public Task ListenForKey(ConsoleKey key)
        {
            while(Console.ReadKey().Key != key)
            {

            }
            return Task.CompletedTask;
        }

        public string ReadString()
        {
            return Console.ReadLine()!;
        }

        public int ReadInt()
        {
            return int.Parse(Console.ReadLine()!);
        }

        public ConsoleKey ReadKeyPressed()
        {
            return Console.ReadKey().Key;
        }
    }
}
