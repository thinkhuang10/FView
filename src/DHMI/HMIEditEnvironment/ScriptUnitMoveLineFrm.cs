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
            if (base.Owner != null)
            {
                base.Owner.Select();
            }
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
        this.label1 = new System.Windows.Forms.Label();
        this.lineTxt = new System.Windows.Forms.TextBox();
        this.moveToBtn = new System.Windows.Forms.Button();
        this.cancleBtn = new System.Windows.Forms.Button();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(12, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(59, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "行号(&L)：";
        this.lineTxt.Location = new System.Drawing.Point(12, 29);
        this.lineTxt.Name = "lineTxt";
        this.lineTxt.Size = new System.Drawing.Size(217, 21);
        this.lineTxt.TabIndex = 1;
        this.lineTxt.TextChanged += new System.EventHandler(lineTxt_TextChanged);
        this.lineTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(lineTxt_KeyPress);
        this.moveToBtn.Location = new System.Drawing.Point(78, 68);
        this.moveToBtn.Name = "moveToBtn";
        this.moveToBtn.Size = new System.Drawing.Size(75, 23);
        this.moveToBtn.TabIndex = 2;
        this.moveToBtn.Text = "转到";
        this.moveToBtn.UseVisualStyleBackColor = true;
        this.moveToBtn.Click += new System.EventHandler(moveToBtn_Click);
        this.cancleBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.cancleBtn.Location = new System.Drawing.Point(159, 68);
        this.cancleBtn.Name = "cancleBtn";
        this.cancleBtn.Size = new System.Drawing.Size(75, 23);
        this.cancleBtn.TabIndex = 3;
        this.cancleBtn.Text = "取消";
        this.cancleBtn.UseVisualStyleBackColor = true;
        this.cancleBtn.Click += new System.EventHandler(cancleBtn_Click);
        base.AcceptButton = this.moveToBtn;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.cancleBtn;
        base.ClientSize = new System.Drawing.Size(243, 103);
        base.Controls.Add(this.cancleBtn);
        base.Controls.Add(this.moveToBtn);
        base.Controls.Add(this.lineTxt);
        base.Controls.Add(this.label1);
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "ScriptUnitMoveLineFrm";
        base.ShowIcon = false;
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "转到指定行";
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ScriptUnitMoveLineFrm_FormClosing);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
