using System.Collections.Generic;
using System.Drawing;

namespace ICSharpCode.TextEditor;

public class BrushRegistry
{
    private static readonly Dictionary<Color, Brush> brushes = new();

    private static readonly Dictionary<Color, Pen> pens = new();

    private static readonly Dictionary<Color, Pen> dotPens = new();

    private static readonly float[] dotPattern = new float[4] { 1f, 1f, 1f, 1f };

    public static Brush GetBrush(Color color)
    {
        lock (brushes)
        {
            if (!brushes.TryGetValue(color, out var value))
            {
                value = new SolidBrush(color);
                brushes.Add(color, value);
            }
            return value;
        }
    }

    public static Pen GetPen(Color color)
    {
        lock (pens)
        {
            if (!pens.TryGetValue(color, out var value))
            {
                value = new Pen(color);
                pens.Add(color, value);
            }
            return value;
        }
    }

    public static Pen GetDotPen(Color color)
    {
        lock (dotPens)
        {
            if (!dotPens.TryGetValue(color, out var value))
            {
                value = new Pen(color)
                {
                    DashPattern = dotPattern
                };
                dotPens.Add(color, value);
            }
            return value;
        }
    }
}
