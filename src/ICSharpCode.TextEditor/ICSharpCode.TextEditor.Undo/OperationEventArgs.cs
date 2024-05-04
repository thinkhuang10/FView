using System;

namespace ICSharpCode.TextEditor.Undo;

public class OperationEventArgs : EventArgs
{
    private readonly IUndoableOperation op;

    public IUndoableOperation Operation => op;

    public OperationEventArgs(IUndoableOperation op)
    {
        this.op = op;
    }
}
