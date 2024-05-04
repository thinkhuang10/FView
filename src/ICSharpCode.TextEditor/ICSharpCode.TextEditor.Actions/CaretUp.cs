using System.Drawing;

namespace ICSharpCode.TextEditor.Actions;

public class CaretUp : AbstractEditAction
{
    public override void Execute(TextArea textArea)
    {
        TextLocation position = textArea.Caret.Position;
        int y = position.Y;
        int visibleLine = textArea.Document.GetVisibleLine(y);
        if (visibleLine > 0)
        {
            Point mousePosition = new(textArea.TextView.GetDrawingXPos(y, position.X), textArea.TextView.DrawingPosition.Y + (visibleLine - 1) * textArea.TextView.FontHeight - textArea.TextView.TextArea.VirtualTop.Y);
            textArea.Caret.Position = textArea.TextView.GetLogicalPosition(mousePosition);
            textArea.SetCaretToDesiredColumn();
        }
    }
}
