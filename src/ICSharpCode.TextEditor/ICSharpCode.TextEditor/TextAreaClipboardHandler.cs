using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Actions;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Util;

namespace ICSharpCode.TextEditor;

public class TextAreaClipboardHandler
{
    public delegate bool ClipboardContainsTextDelegate();

    private const string LineSelectedType = "MSDEVLineSelect";

    private readonly TextArea textArea;

    public static ClipboardContainsTextDelegate GetClipboardContainsText;

    [ThreadStatic]
    private static int SafeSetClipboardDataVersion;

    public bool EnableCut => textArea.EnableCutOrPaste;

    public bool EnableCopy => true;

    public bool EnablePaste
    {
        get
        {
            if (!textArea.EnableCutOrPaste)
            {
                return false;
            }
            ClipboardContainsTextDelegate getClipboardContainsText = GetClipboardContainsText;
            if (getClipboardContainsText != null)
            {
                return getClipboardContainsText();
            }
            try
            {
                return Clipboard.ContainsText();
            }
            catch (ExternalException)
            {
                return false;
            }
        }
    }

    public bool EnableDelete
    {
        get
        {
            if (textArea.SelectionManager.HasSomethingSelected)
            {
                return !textArea.SelectionManager.SelectionIsReadonly;
            }
            return false;
        }
    }

    public bool EnableSelectAll => true;

    public event CopyTextEventHandler CopyText;

    public TextAreaClipboardHandler(TextArea textArea)
    {
        this.textArea = textArea;
        textArea.SelectionManager.SelectionChanged += DocumentSelectionChanged;
    }

    private void DocumentSelectionChanged(object sender, EventArgs e)
    {
    }

    private bool CopyTextToClipboard(string stringToCopy, bool asLine)
    {
        if (stringToCopy.Length > 0)
        {
            DataObject dataObject = new();
            dataObject.SetData(DataFormats.UnicodeText, autoConvert: true, stringToCopy);
            if (asLine)
            {
                MemoryStream memoryStream = new(1);
                memoryStream.WriteByte(1);
                dataObject.SetData("MSDEVLineSelect", autoConvert: false, memoryStream);
            }
            if (textArea.Document.HighlightingStrategy.Name != "Default")
            {
                dataObject.SetData(DataFormats.Rtf, RtfWriter.GenerateRtf(textArea));
            }
            OnCopyText(new CopyTextEventArgs(stringToCopy));
            SafeSetClipboard(dataObject);
            return true;
        }
        return false;
    }

    private static void SafeSetClipboard(object dataObject)
    {
        int version = ++SafeSetClipboardDataVersion;
        try
        {
            Clipboard.SetDataObject(dataObject, copy: true);
        }
        catch (ExternalException)
        {
            Timer timer = new()
            {
                Interval = 100
            };
            timer.Tick += delegate
            {
                timer.Stop();
                timer.Dispose();
                if (SafeSetClipboardDataVersion == version)
                {
                    try
                    {
                        Clipboard.SetDataObject(dataObject, copy: true, 10, 50);
                    }
                    catch (ExternalException)
                    {
                    }
                }
            };
            timer.Start();
        }
    }

    private bool CopyTextToClipboard(string stringToCopy)
    {
        return CopyTextToClipboard(stringToCopy, asLine: false);
    }

    public void Cut(object sender, EventArgs e)
    {
        if (textArea.SelectionManager.HasSomethingSelected)
        {
            if (CopyTextToClipboard(textArea.SelectionManager.SelectedText) && !textArea.SelectionManager.SelectionIsReadonly)
            {
                textArea.BeginUpdate();
                textArea.Caret.Position = textArea.SelectionManager.SelectionCollection[0].StartPosition;
                textArea.SelectionManager.RemoveSelectedText();
                textArea.EndUpdate();
            }
        }
        else if (textArea.Document.TextEditorProperties.CutCopyWholeLine)
        {
            int lineNumberForOffset = textArea.Document.GetLineNumberForOffset(textArea.Caret.Offset);
            LineSegment lineSegment = textArea.Document.GetLineSegment(lineNumberForOffset);
            string text = textArea.Document.GetText(lineSegment.Offset, lineSegment.TotalLength);
            textArea.SelectionManager.SetSelection(textArea.Document.OffsetToPosition(lineSegment.Offset), textArea.Document.OffsetToPosition(lineSegment.Offset + lineSegment.TotalLength));
            if (CopyTextToClipboard(text, asLine: true) && !textArea.SelectionManager.SelectionIsReadonly)
            {
                textArea.BeginUpdate();
                textArea.Caret.Position = textArea.Document.OffsetToPosition(lineSegment.Offset);
                textArea.SelectionManager.RemoveSelectedText();
                textArea.Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.PositionToEnd, new TextLocation(0, lineNumberForOffset)));
                textArea.EndUpdate();
            }
        }
    }

    public void Copy(object sender, EventArgs e)
    {
        if (!CopyTextToClipboard(textArea.SelectionManager.SelectedText) && textArea.Document.TextEditorProperties.CutCopyWholeLine)
        {
            int lineNumberForOffset = textArea.Document.GetLineNumberForOffset(textArea.Caret.Offset);
            LineSegment lineSegment = textArea.Document.GetLineSegment(lineNumberForOffset);
            string text = textArea.Document.GetText(lineSegment.Offset, lineSegment.TotalLength);
            CopyTextToClipboard(text, asLine: true);
        }
    }

    public void Paste(object sender, EventArgs e)
    {
        if (!textArea.EnableCutOrPaste)
        {
            return;
        }
        int num = 0;
        while (true)
        {
            try
            {
                IDataObject dataObject = Clipboard.GetDataObject();
                if (dataObject == null)
                {
                    break;
                }
                bool dataPresent = dataObject.GetDataPresent("MSDEVLineSelect");
                if (!dataObject.GetDataPresent(DataFormats.UnicodeText))
                {
                    break;
                }
                string text = (string)dataObject.GetData(DataFormats.UnicodeText);
                if (string.IsNullOrEmpty(text))
                {
                    break;
                }
                textArea.Document.UndoStack.StartUndoGroup();
                try
                {
                    if (textArea.SelectionManager.HasSomethingSelected)
                    {
                        textArea.Caret.Position = textArea.SelectionManager.SelectionCollection[0].StartPosition;
                        textArea.SelectionManager.RemoveSelectedText();
                    }
                    if (dataPresent)
                    {
                        int column = textArea.Caret.Column;
                        textArea.Caret.Column = 0;
                        if (!textArea.IsReadOnly(textArea.Caret.Offset))
                        {
                            textArea.InsertString(text);
                        }
                        textArea.Caret.Column = column;
                    }
                    else
                    {
                        textArea.InsertString(text);
                    }
                    break;
                }
                finally
                {
                    textArea.Document.UndoStack.EndUndoGroup();
                }
            }
            catch (ExternalException)
            {
                if (num > 5)
                {
                    throw;
                }
            }
            num++;
        }
    }

    public void Delete(object sender, EventArgs e)
    {
        new Delete().Execute(textArea);
    }

    public void SelectAll(object sender, EventArgs e)
    {
        new SelectWholeDocument().Execute(textArea);
    }

    protected virtual void OnCopyText(CopyTextEventArgs e)
    {
        CopyText?.Invoke(this, e);
    }
}
