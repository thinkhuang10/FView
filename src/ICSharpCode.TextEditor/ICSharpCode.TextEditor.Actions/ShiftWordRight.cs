namespace ICSharpCode.TextEditor.Actions;

public class ShiftWordRight : WordRight
{
    public override void Execute(TextArea textArea)
    {
        TextLocation position = textArea.Caret.Position;
        base.Execute(textArea);
        textArea.AutoClearSelection = false;
        textArea.SelectionManager.ExtendSelection(position, textArea.Caret.Position);
    }
}
