using DSA.Shared.Interfaces;
using DSA.Shared.Models;

namespace DSA.Shared.Algorithms.Sorting
{
    public class BubbleSort
    {
        private readonly IVisualizerConsole _console;
        private VisualItem[] _state = [];

        public BubbleSort(IVisualizerConsole console)
        {
            _console = console;
        }

        private void InitializeState(int[] values)
        {
            _state = new VisualItem[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                _state[i] = new VisualItem { Value = values[i], Status = ItemStatus.Default };
            }
        }

        public async Task Run(int[] initialValues)
        {
            InitializeState(initialValues);
            int n = _state.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {

                    _state[j].Status = ItemStatus.Reference;
                    _state[j + 1].Status = ItemStatus.Comparing;
                    await Redraw();

                    if (_state[j].Value > _state[j + 1].Value)
                    {
                        var temp = _state[j];
                        _state[j] = _state[j + 1];
                        _state[j + 1] = temp;

                        await Redraw();
                    }

                    _state[j].Status = ItemStatus.Default;
                    _state[j + 1].Status = ItemStatus.Default;
                }

                _state[n - i - 1].Status = ItemStatus.Sorted;
                await Redraw();
            }
            _state[0].Status = ItemStatus.Sorted;
            await Redraw();
        }

        private async Task Redraw()
        {
            await _console.Clear();
            await _console.WriteLine("--- Bubble Sort ---");
            await _console.DrawState(_state);
            await _console.Sleep(500);
        }
    }
}
