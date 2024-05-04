using System;

namespace ICSharpCode.TextEditor.Document;

public class LineCountChangeEventArgs : EventArgs
{
    private readonly IDocument document;

    private readonly int start;

    private readonly int moved;

    public IDocument Document => document;

    public int LineStart => start;

    public int LinesMoved => moved;

    public LineCountChangeEventArgs(IDocument document, int lineStart, int linesMoved)
    {
        this.document = document;
        start = lineStart;
        moved = linesMoved;
    }
}
