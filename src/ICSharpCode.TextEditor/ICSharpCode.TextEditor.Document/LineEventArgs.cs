using System;

namespace ICSharpCode.TextEditor.Document;

public class LineEventArgs : EventArgs
{
    private readonly IDocument document;

    private readonly LineSegment lineSegment;

    public IDocument Document => document;

    public LineSegment LineSegment => lineSegment;

    public LineEventArgs(IDocument document, LineSegment lineSegment)
    {
        this.document = document;
        this.lineSegment = lineSegment;
    }

    public override string ToString()
    {
        return $"[LineEventArgs Document={document} LineSegment={lineSegment}]";
    }
}
