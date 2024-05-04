using System;

namespace ICSharpCode.TextEditor;

public class CopyTextEventArgs : EventArgs
{
    private readonly string text;

    public string Text => text;

    public CopyTextEventArgs(string text)
    {
        this.text = text;
    }
}
