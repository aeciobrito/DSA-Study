using DSA.Shared.Interfaces;
using DSA.Shared.Models;

namespace DSA.Console
{
    public class SystemVisualizerConsole : IVisualizerConsole
    {
        public Task Clear()
        {
            System.Console.Clear();
            return Task.CompletedTask;
        }

        public Task WriteLine(string text = "")
        {
            System.Console.WriteLine(text);
            return Task.CompletedTask;
        }

        public Task SetColor(ConsoleColor f, ConsoleColor b)
        {
            System.Console.ForegroundColor = f;
            System.Console.BackgroundColor = b;
            return Task.CompletedTask;
        }

        public Task ResetColor()
        {
            System.Console.ResetColor();
            return Task.CompletedTask;
        }

        public Task Sleep(int milliseconds)
        {
            System.Threading.Thread.Sleep(milliseconds);
            return Task.CompletedTask;
        }

        public async Task DrawState(VisualItem[] state)
        {
            if (state == null || !state.Any()) return;

            int maxVal = state.Max(s => s.Value);
            int minVal = state.Min(s => s.Value);

            if (maxVal < 0) maxVal = 0;
            if (minVal > 0) minVal = 0;

            for (int y = maxVal; y > 0; y--)
            {
                foreach (var item in state)
                {
                    if (item.Value >= y)
                    {
                        await SetColor(GetColor(item.Status), ConsoleColor.Black);
                        System.Console.Write(" █ ");
                    }
                    else
                    {
                        System.Console.Write("   ");
                    }
                }
                await WriteLine();
            }

            foreach (var item in state)
            {
                await SetColor(GetColor(item.Status), ConsoleColor.Black);
                string label = item.Value.ToString();
                System.Console.Write(label.PadLeft(2) + " ");
            }
            await ResetColor();
            await WriteLine();

            for (int y = -1; y >= minVal; y--)
            {
                foreach (var item in state)
                {
                    if (item.Value <= y)
                    {
                        await SetColor(GetColor(item.Status), ConsoleColor.Black);
                        System.Console.Write(" █ ");
                    }
                    else
                    {
                        System.Console.Write("   ");
                    }
                }
                await WriteLine();
            }
        }

        private ConsoleColor GetColor(ItemStatus status) => status switch
        {
            ItemStatus.Reference => ConsoleColor.Red,
            ItemStatus.Comparing => ConsoleColor.Green,
            ItemStatus.Sorted => ConsoleColor.Yellow,
            _ => ConsoleColor.Cyan
        };
    }
}
