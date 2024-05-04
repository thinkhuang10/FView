using ICSharpCode.TextEditor.Document;

namespace HMIEditEnvironment;

public class TextRange : AbstractSegment
{
    private readonly IDocument document;

    public TextRange(IDocument document, int offset, int length)
    {
        this.document = document;
        base.offset = offset;
        base.length = length;
    }
}
