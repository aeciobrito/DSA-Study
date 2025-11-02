using DSA.Shared.Models;

namespace DSA.Shared.Interfaces
{

    public interface IVisualizerConsole
    {
        Task Clear();
        Task WriteLine(string text = "");
        Task SetColor(ConsoleColor foreground, ConsoleColor background);
        Task ResetColor();
        Task Sleep(int milliseconds);

        Task DrawState(VisualItem[] state); 
    }
}
