namespace ICSharpCode.TextEditor.Document;

public class LineLengthChangeEventArgs : LineEventArgs
{
    private readonly int lengthDelta;

    public int LengthDelta => lengthDelta;

    public LineLengthChangeEventArgs(IDocument document, LineSegment lineSegment, int moved)
        : base(document, lineSegment)
    {
        lengthDelta = moved;
    }

    public override string ToString()
    {
        return $"[LineLengthEventArgs Document={base.Document} LineSegment={base.LineSegment} LengthDelta={lengthDelta}]";
    }
}
