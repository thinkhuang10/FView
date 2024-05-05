using System;
using System.Collections.Generic;

namespace ICSharpCode.TextEditor.Undo;

public class UndoStack
{
    private class UndoableSetCaretPosition : IUndoableOperation
    {
        private readonly UndoStack stack;

        private TextLocation pos;

        private TextLocation redoPos;

        public UndoableSetCaretPosition(UndoStack stack, TextLocation pos)
        {
            this.stack = stack;
            this.pos = pos;
        }

        public void Undo()
        {
            redoPos = stack.TextEditorControl.ActiveTextAreaControl.Caret.Position;
            stack.TextEditorControl.ActiveTextAreaControl.Caret.Position = pos;
            stack.TextEditorControl.ActiveTextAreaControl.SelectionManager.ClearSelection();
        }

        public void Redo()
        {
            stack.TextEditorControl.ActiveTextAreaControl.Caret.Position = redoPos;
            stack.TextEditorControl.ActiveTextAreaControl.SelectionManager.ClearSelection();
        }
    }

    private readonly Stack<IUndoableOperation> undostack = new();

    private readonly Stack<IUndoableOperation> redostack = new();

    public TextEditorControlBase TextEditorControl;

    internal bool AcceptChanges = true;

    private int undoGroupDepth;

    private int actionCountInUndoGroup;

    public bool CanUndo => undostack.Count > 0;

    public bool CanRedo => redostack.Count > 0;

    public int UndoItemCount => undostack.Count;

    public int RedoItemCount => redostack.Count;

    public event EventHandler ActionUndone;

    public event EventHandler ActionRedone;

    public event OperationEventHandler OperationPushed;

    public void StartUndoGroup()
    {
        if (undoGroupDepth == 0)
        {
            actionCountInUndoGroup = 0;
        }
        undoGroupDepth++;
    }

    public void EndUndoGroup()
    {
        if (undoGroupDepth == 0)
        {
            throw new InvalidOperationException("There are no open undo groups");
        }
        undoGroupDepth--;
        if (undoGroupDepth == 0 && actionCountInUndoGroup > 1)
        {
            UndoQueue undoQueue = new(undostack, actionCountInUndoGroup);
            undostack.Push(undoQueue);
            OperationPushed?.Invoke(this, new OperationEventArgs(undoQueue));
        }
    }

    public void AssertNoUndoGroupOpen()
    {
        if (undoGroupDepth != 0)
        {
            undoGroupDepth = 0;
            throw new InvalidOperationException("No undo group should be open at this point");
        }
    }

    public void Undo()
    {
        AssertNoUndoGroupOpen();
        if (undostack.Count > 0)
        {
            IUndoableOperation undoableOperation = undostack.Pop();
            redostack.Push(undoableOperation);
            undoableOperation.Undo();
            OnActionUndone();
        }
    }

    public void Redo()
    {
        AssertNoUndoGroupOpen();
        if (redostack.Count > 0)
        {
            IUndoableOperation undoableOperation = redostack.Pop();
            undostack.Push(undoableOperation);
            undoableOperation.Redo();
            OnActionRedone();
        }
    }

    public void Push(IUndoableOperation operation)
    {
        if (operation == null)
        {
            throw new ArgumentNullException("operation");
        }
        if (AcceptChanges)
        {
            StartUndoGroup();
            undostack.Push(operation);
            actionCountInUndoGroup++;
            if (TextEditorControl != null)
            {
                undostack.Push(new UndoableSetCaretPosition(this, TextEditorControl.ActiveTextAreaControl.Caret.Position));
                actionCountInUndoGroup++;
            }
            EndUndoGroup();
            ClearRedoStack();
        }
    }

    public void ClearRedoStack()
    {
        redostack.Clear();
    }

    public void ClearAll()
    {
        AssertNoUndoGroupOpen();
        undostack.Clear();
        redostack.Clear();
        actionCountInUndoGroup = 0;
    }

    protected void OnActionUndone()
    {
        ActionUndone?.Invoke(null, null);
    }

    protected void OnActionRedone()
    {
        ActionRedone?.Invoke(null, null);
    }
}
