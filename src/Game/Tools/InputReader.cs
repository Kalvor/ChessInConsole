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
            do
            {
                var x = Console.ReadLine();
                if (!int.TryParse(x, out int result))
                {
                    continue;
                }
                return result;
            }
            while (true);
        }

        public ConsoleKey ReadKeyPressed()
        {
            return Console.ReadKey().Key;
        }
    }
}
