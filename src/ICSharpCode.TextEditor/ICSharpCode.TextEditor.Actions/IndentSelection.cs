using ICSharpCode.TextEditor.Document;

namespace ICSharpCode.TextEditor.Actions;

public class IndentSelection : AbstractLineFormatAction
{
    protected override void Convert(IDocument document, int startLine, int endLine)
    {
        document.FormattingStrategy.IndentLines(textArea, startLine, endLine);
    }
}
