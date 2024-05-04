using System;

namespace ICSharpCode.TextEditor.Actions;

public class MovePageUp : AbstractEditAction
{
    public override void Execute(TextArea textArea)
    {
        int line = textArea.Caret.Line;
        int num = Math.Max(textArea.Document.GetNextVisibleLineBelow(line, textArea.TextView.VisibleLineCount), 0);
        if (line != num)
        {
            textArea.Caret.Position = new TextLocation(0, num);
            textArea.SetCaretToDesiredColumn();
        }
    }
}
