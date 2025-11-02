using DSA.Shared.Interfaces;
using DSA.Shared.Models;

namespace DSA.Shared.Algorithms.Sorting
{
    public class SelectionSort
    {
        private readonly IVisualizerConsole _console;
        private VisualItem[] _state = new VisualItem[0];

        public SelectionSort(IVisualizerConsole console)
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

                int min_idx = i;

                _state[i].Status = ItemStatus.Reference;
                await Redraw();

                for (int j = i + 1; j < n; j++)
                {

                    _state[j].Status = ItemStatus.Comparing;

                    await Redraw();

                    if (_state[j].Value < _state[min_idx].Value)
                    {

                        if (min_idx != i)
                            _state[min_idx].Status = ItemStatus.Default;

                        min_idx = j;
                    }
                    else
                    {

                        _state[j].Status = ItemStatus.Default;
                    }

                    _state[min_idx].Status = ItemStatus.Reference;
                }

                var temp = _state[min_idx];
                _state[min_idx] = _state[i];
                _state[i] = temp;

                _state[min_idx].Status = ItemStatus.Default;

                _state[i].Status = ItemStatus.Sorted;

                await Redraw();
            }

            _state[n - 1].Status = ItemStatus.Sorted;
            await Redraw();
        }

        private async Task Redraw()
        {
            await _console.Clear();
            await _console.WriteLine("--- Selection Sort ---");
            await _console.DrawState(_state);
            await _console.Sleep(500);
        }
    }
}
