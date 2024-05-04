using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;

namespace HMIEditEnvironment.CodeCompletion;

internal class CodeCompletionKeyHandler
{
    private readonly ScriptUnit form;

    private readonly TextEditorControl editor;

    private CodeCompletionWindow codeCompletionWin;

    private CodeCompletionKeyHandler(ScriptUnit form, TextEditorControl editor)
    {
        this.form = form;
        this.editor = editor;
    }

    internal static CodeCompletionKeyHandler Attach(ScriptUnit form, TextEditorControl editor)
    {
        CodeCompletionKeyHandler codeCompletionKeyHandler = new(form, editor);
        editor.ActiveTextAreaControl.TextArea.KeyEventHandler += codeCompletionKeyHandler.TextArea_KeyEventHandler;
        return codeCompletionKeyHandler;
    }

    private bool TextArea_KeyEventHandler(char key)
    {
        _ = codeCompletionWin;
        ICompletionDataProvider completionDataProvider = new CodeCompletionDataProvider(form);
        codeCompletionWin = CodeCompletionWindow.ShowCompletionWindow(form, editor, null, completionDataProvider, key);
        return false;
    }
}
