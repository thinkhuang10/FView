using System;
using System.Drawing;
using System.Windows.Forms;

namespace ICSharpCode.TextEditor.Gui.CompletionWindow;

public abstract class AbstractCompletionWindow : Form
{
    protected TextEditorControl control;

    protected Size drawingSize;

    private Rectangle workingScreen;

    private readonly Form parentForm;

    private static int shadowStatus;

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams result = base.CreateParams;
            AddShadowToWindow(result);
            return result;
        }
    }

    protected override bool ShowWithoutActivation => true;

    protected AbstractCompletionWindow(Form parentForm, TextEditorControl control)
    {
        workingScreen = Screen.GetWorkingArea(parentForm);
        this.parentForm = parentForm;
        this.control = control;
        SetLocation();
        base.StartPosition = FormStartPosition.Manual;
        base.FormBorderStyle = FormBorderStyle.None;
        base.ShowInTaskbar = false;
        MinimumSize = new Size(1, 1);
        base.Size = new Size(1, 1);
    }

    protected virtual void SetLocation()
    {
        TextArea textArea = control.ActiveTextAreaControl.TextArea;
        TextLocation position = textArea.Caret.Position;
        int drawingXPos = textArea.TextView.GetDrawingXPos(position.Y, position.X);
        int num = (textArea.TextEditorProperties.ShowHorizontalRuler ? textArea.TextView.FontHeight : 0);
        Point p = new(textArea.TextView.DrawingPosition.X + drawingXPos, textArea.TextView.DrawingPosition.Y + textArea.Document.GetVisibleLine(position.Y) * textArea.TextView.FontHeight - textArea.TextView.TextArea.VirtualTop.Y + textArea.TextView.FontHeight + num);
        Point location = control.ActiveTextAreaControl.PointToScreen(p);
        Rectangle rectangle = new(location, drawingSize);
        if (!workingScreen.Contains(rectangle))
        {
            if (rectangle.Right > workingScreen.Right)
            {
                rectangle.X = workingScreen.Right - rectangle.Width;
            }
            if (rectangle.Left < workingScreen.Left)
            {
                rectangle.X = workingScreen.Left;
            }
            if (rectangle.Top < workingScreen.Top)
            {
                rectangle.Y = workingScreen.Top;
            }
            if (rectangle.Bottom > workingScreen.Bottom)
            {
                rectangle.Y = rectangle.Y - rectangle.Height - control.ActiveTextAreaControl.TextArea.TextView.FontHeight;
                if (rectangle.Bottom > workingScreen.Bottom)
                {
                    rectangle.Y = workingScreen.Bottom - rectangle.Height;
                }
            }
        }
        base.Bounds = rectangle;
    }

    public static void AddShadowToWindow(CreateParams createParams)
    {
        if (shadowStatus == 0)
        {
            shadowStatus = -1;
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Version version = Environment.OSVersion.Version;
                if (version.Major > 5 || (version.Major == 5 && version.Minor >= 1))
                {
                    shadowStatus = 1;
                }
            }
        }
        if (shadowStatus == 1)
        {
            createParams.ClassStyle |= 131072;
        }
    }

    protected void ShowCompletionWindow()
    {
        base.Owner = parentForm;
        base.Enabled = true;
        Show();
        control.Focus();
        if (parentForm != null)
        {
            parentForm.LocationChanged += ParentFormLocationChanged;
        }
        control.ActiveTextAreaControl.VScrollBar.ValueChanged += ParentFormLocationChanged;
        control.ActiveTextAreaControl.HScrollBar.ValueChanged += ParentFormLocationChanged;
        control.ActiveTextAreaControl.TextArea.DoProcessDialogKey += ProcessTextAreaKey;
        control.ActiveTextAreaControl.Caret.PositionChanged += CaretOffsetChanged;
        control.ActiveTextAreaControl.TextArea.LostFocus += TextEditorLostFocus;
        control.Resize += ParentFormLocationChanged;
        foreach (Control control in base.Controls)
        {
            control.MouseMove += ControlMouseMove;
        }
    }

    private void ParentFormLocationChanged(object sender, EventArgs e)
    {
        SetLocation();
    }

    public virtual bool ProcessKeyEvent(char ch)
    {
        return false;
    }

    protected virtual bool ProcessTextAreaKey(Keys keyData)
    {
        if (!base.Visible)
        {
            return false;
        }
        if (keyData == Keys.Escape)
        {
            Close();
            return true;
        }
        return false;
    }

    protected virtual void CaretOffsetChanged(object sender, EventArgs e)
    {
    }

    protected void TextEditorLostFocus(object sender, EventArgs e)
    {
        if (!control.ActiveTextAreaControl.TextArea.Focused && !base.ContainsFocus)
        {
            Close();
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        parentForm.LocationChanged -= ParentFormLocationChanged;
        foreach (Control control in base.Controls)
        {
            control.MouseMove -= ControlMouseMove;
        }
        if (control.ActiveTextAreaControl.VScrollBar != null)
        {
            control.ActiveTextAreaControl.VScrollBar.ValueChanged -= ParentFormLocationChanged;
        }
        if (control.ActiveTextAreaControl.HScrollBar != null)
        {
            control.ActiveTextAreaControl.HScrollBar.ValueChanged -= ParentFormLocationChanged;
        }
        control.ActiveTextAreaControl.TextArea.LostFocus -= TextEditorLostFocus;
        control.ActiveTextAreaControl.Caret.PositionChanged -= CaretOffsetChanged;
        control.ActiveTextAreaControl.TextArea.DoProcessDialogKey -= ProcessTextAreaKey;
        control.Resize -= ParentFormLocationChanged;
        Dispose();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        ControlMouseMove(this, e);
    }

    protected void ControlMouseMove(object sender, MouseEventArgs e)
    {
        control.ActiveTextAreaControl.TextArea.ShowHiddenCursor(forceShow: false);
    }
}
