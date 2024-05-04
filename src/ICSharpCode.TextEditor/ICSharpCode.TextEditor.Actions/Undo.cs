namespace ICSharpCode.TextEditor.Actions;

public class Undo : AbstractEditAction
{
    public override void Execute(TextArea textArea)
    {
        textArea.MotherTextEditorControl.Undo();
    }
}
