using Microsoft.AspNetCore.Components;
using System.Text;
using System.Web;
using DSA.Shared.Interfaces;
using DSA.Shared.Models;

namespace DSA.Website;

public class BlazorConsole : IVisualizerConsole
{

    private readonly StringBuilder _frameBuilder = new();

    public MarkupString CurrentState { get; private set; } = new(string.Empty);

    public Action? TriggerRefresh { get; set; }

    private ConsoleColor _foregroundColor = ConsoleColor.White;
    private ConsoleColor _backgroundColor = ConsoleColor.Black;

    public Task Clear()
    {
        _frameBuilder.Clear();
        return Task.CompletedTask;
    }

    public Task WriteLine(string text = "")
    {
        if (!string.IsNullOrEmpty(text))
        {
            AppendHtmlEncoded(text);
        }
        _frameBuilder.Append("<br />");
        return Task.CompletedTask;
    }

    public Task SetColor(ConsoleColor foreground, ConsoleColor background)
    {
        _foregroundColor = foreground;
        _backgroundColor = background;
        return Task.CompletedTask;
    }

    public Task ResetColor()
    {
        _foregroundColor = ConsoleColor.White;
        _backgroundColor = ConsoleColor.Black;
        return Task.CompletedTask;
    }

    public async Task Sleep(int milliseconds)
    {

        CurrentState = new MarkupString(_frameBuilder.ToString());

        TriggerRefresh?.Invoke();

        await Task.Delay(milliseconds);
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
                    await Write(" █ ");
                }
                else
                {
                    await Write("   ");
                }
            }
            await WriteLine();
        }

        foreach (var item in state)
        {
            await SetColor(GetColor(item.Status), ConsoleColor.Black);
            string label = item.Value.ToString();
            await Write(label.PadLeft(2) + " ");
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
                    await Write(" █ ");
                }
                else
                {
                    await Write("   ");
                }
            }
            await WriteLine();
        }
    }

    private Task Write(string text)
    {
        AppendHtmlEncoded(text);
        return Task.CompletedTask;
    }

    private void AppendHtmlEncoded(string text)
    {
        string htmlText = HttpUtility.HtmlEncode(text);

        bool hasBgColor = _backgroundColor != ConsoleColor.Black;
        bool hasFgColor = _foregroundColor != ConsoleColor.White;

        if (hasBgColor)
        {
            _frameBuilder.Append($@"<span style=""background-color:{HtmlEncode(_backgroundColor)}"">");
        }
        if (hasFgColor)
        {
            _frameBuilder.Append($@"<span style=""color:{HtmlEncode(_foregroundColor)}"">");
        }

        _frameBuilder.Append(htmlText);

        if (hasFgColor)
        {
            _frameBuilder.Append("</span>");
        }
        if (hasBgColor)
        {
            _frameBuilder.Append("</span>");
        }
    }

    private ConsoleColor GetColor(ItemStatus status) => status switch
    {
        ItemStatus.Reference => ConsoleColor.Red,
        ItemStatus.Comparing => ConsoleColor.Green,
        ItemStatus.Sorted => ConsoleColor.Yellow,
        _ => ConsoleColor.Cyan
    };

    public static string HtmlEncode(ConsoleColor color)
    {
        return color switch
        {
            ConsoleColor.Black => "#000000",
            ConsoleColor.White => "#ffffff",
            ConsoleColor.Blue => "#0000ff",
            ConsoleColor.Red => "#ff0000",
            ConsoleColor.Green => "#00ff00",
            ConsoleColor.Yellow => "#ffff00",
            ConsoleColor.Cyan => "#00ffff",
            ConsoleColor.Magenta => "#ff00ff",
            ConsoleColor.Gray => "#808080",
            ConsoleColor.DarkBlue => "#00008b",
            ConsoleColor.DarkRed => "#8b0000",
            ConsoleColor.DarkGreen => "#006400",
            ConsoleColor.DarkYellow => "#8b8000",
            ConsoleColor.DarkCyan => "#008b8b",
            ConsoleColor.DarkMagenta => "#8b008b",
            ConsoleColor.DarkGray => "#a9a9a9",
            _ => throw new NotImplementedException(),
        };
    }
}