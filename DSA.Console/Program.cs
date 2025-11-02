using DSA.Console;
using DSA.Shared.Algorithms.Sorting;
using DSA.Shared.Interfaces;
using DSA.Shared.Utils;

IVisualizerConsole console = new SystemVisualizerConsole();

bool exit = false;
while (!exit)
{
    await console.Clear();
    await console.WriteLine("--- MENU DE ESTUDOS DSA ---");
    await console.WriteLine("\n[ALGORITMOS DE ORDENAÇÃO]");
    await console.WriteLine("  1. Bubble Sort");
    await console.WriteLine("  2. Selection Sort");
    await console.WriteLine("\nS. Sair");
    await console.WriteLine("\nDigite sua opção:");
    await console.Sleep(1); 

    string? option = System.Console.ReadLine();
    int[] arrayToRun;

    switch (option?.ToUpper())
    {
        case "1":
            arrayToRun = ArrayGenerator.Generate();
            BubbleSort bubbleSorter = new (console);
            await bubbleSorter.Run(arrayToRun);
            break;
        case "2":
            arrayToRun = ArrayGenerator.Generate();
            SelectionSort selectionSorter = new (console);
            await selectionSorter.Run(arrayToRun);
            break;
        case "S":
            exit = true;
            break;
        default:
            await console.WriteLine("Opção inválida. Pressione Enter para tentar novamente.");
            System.Console.ReadLine();
            break;
    }

    if (!exit && option != null && "1234".Contains(option))
    {
        await console.WriteLine("\nAlgoritmo Concluído! Pressione Enter para voltar ao menu.");
        System.Console.ReadLine();
    }
}

await console.Clear();
await console.WriteLine("Estudos finalizados.");