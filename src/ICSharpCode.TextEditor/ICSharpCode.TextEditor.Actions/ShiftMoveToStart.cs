namespace ICSharpCode.TextEditor.Actions;

public class ShiftMoveToStart : MoveToStart
{
    public override void Execute(TextArea textArea)
    {
        TextLocation position = textArea.Caret.Position;
        base.Execute(textArea);
        textArea.AutoClearSelection = false;
        textArea.SelectionManager.ExtendSelection(position, textArea.Caret.Position);
    }
}
