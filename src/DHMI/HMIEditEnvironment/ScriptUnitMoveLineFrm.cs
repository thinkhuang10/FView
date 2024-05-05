using ICSharpCode.TextEditor;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class ScriptUnitMoveLineFrm : Form
{
    private TextEditorControl editor;

    private Label label1;

    private TextBox lineTxt;

    private Button moveToBtn;

    private Button cancleBtn;

    private TextEditorControl Editor
    {
        get
        {
            return editor;
        }
        set
        {
            editor = value;
        }
    }

    public ScriptUnitMoveLineFrm()
    {
        InitializeComponent();
    }

    internal void ShowFor(TextEditorControl editor)
    {
        Editor = editor;
        Caret caret = editor.ActiveTextAreaControl.Caret;
        int num = caret.Line + 1;
        lineTxt.Text = num.ToString();
        base.Owner = (Form)editor.TopLevelControl;
        Show();
        lineTxt.SelectAll();
        lineTxt.Focus();
    }

    private void moveToBtn_Click(object sender, EventArgs e)
    {
        int num = int.Parse(lineTxt.Text);
        Caret caret = editor.ActiveTextAreaControl.Caret;
        caret.Line = num - 1;
        caret.Column = 0;
        caret.UpdateCaretPosition();
        Close();
    }

    private void cancleBtn_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void ScriptUnitMoveLineFrm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason != CloseReason.FormOwnerClosing)
        {
            base.Owner?.Select();
            e.Cancel = true;
            Hide();
            editor.Refresh();
        }
    }

    private void lineTxt_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\r' && e.KeyChar != '\b')
        {
            e.Handled = true;
        }
    }

    private void lineTxt_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(lineTxt.Text))
        {
            moveToBtn.Enabled = false;
        }
        else
        {
            moveToBtn.Enabled = true;
        }
    }

    private void InitializeComponent()
    {
        label1 = new System.Windows.Forms.Label();
        lineTxt = new System.Windows.Forms.TextBox();
        moveToBtn = new System.Windows.Forms.Button();
        cancleBtn = new System.Windows.Forms.Button();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(12, 9);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(59, 12);
        label1.TabIndex = 0;
        label1.Text = "行号(&L)：";
        lineTxt.Location = new System.Drawing.Point(12, 29);
        lineTxt.Name = "lineTxt";
        lineTxt.Size = new System.Drawing.Size(217, 21);
        lineTxt.TabIndex = 1;
        lineTxt.TextChanged += new System.EventHandler(lineTxt_TextChanged);
        lineTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(lineTxt_KeyPress);
        moveToBtn.Location = new System.Drawing.Point(78, 68);
        moveToBtn.Name = "moveToBtn";
        moveToBtn.Size = new System.Drawing.Size(75, 23);
        moveToBtn.TabIndex = 2;
        moveToBtn.Text = "转到";
        moveToBtn.UseVisualStyleBackColor = true;
        moveToBtn.Click += new System.EventHandler(moveToBtn_Click);
        cancleBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        cancleBtn.Location = new System.Drawing.Point(159, 68);
        cancleBtn.Name = "cancleBtn";
        cancleBtn.Size = new System.Drawing.Size(75, 23);
        cancleBtn.TabIndex = 3;
        cancleBtn.Text = "取消";
        cancleBtn.UseVisualStyleBackColor = true;
        cancleBtn.Click += new System.EventHandler(cancleBtn_Click);
        base.AcceptButton = moveToBtn;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = cancleBtn;
        base.ClientSize = new System.Drawing.Size(243, 103);
        base.Controls.Add(cancleBtn);
        base.Controls.Add(moveToBtn);
        base.Controls.Add(lineTxt);
        base.Controls.Add(label1);
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "ScriptUnitMoveLineFrm";
        base.ShowIcon = false;
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "转到指定行";
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ScriptUnitMoveLineFrm_FormClosing);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
