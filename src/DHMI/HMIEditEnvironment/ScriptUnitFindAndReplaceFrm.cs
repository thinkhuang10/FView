using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class ScriptUnitFindAndReplaceFrm : Form
{
    private readonly TextEditorSearcher searcher;

    private TextEditorControl editor;

    public bool lastSearchWasBackward;

    public bool lastSearchLoopedAround;

    private Label label1;

    private TextBox findContentTxtBox;

    private Button findNextBtn;

    private Button cancleBtn;

    private CheckBox caseChkBox;

    private CheckBox wholeWordChkBox;

    private GroupBox groupBox1;

    private Button replaceBtn;

    private Button replaceAllBtn;

    private Button findPreBtn;

    private TextBox replaceContentTxtBox;

    private Label label2;

    private TextEditorControl Editor
    {
        get
        {
            return editor;
        }
        set
        {
            editor = value;
            searcher.Document = editor.Document;
            UpdateTitleBar();
        }
    }

    public bool ReplaceMode
    {
        get
        {
            return replaceContentTxtBox.Visible;
        }
        set
        {
            Button button = replaceBtn;
            bool visible = (replaceAllBtn.Visible = value);
            button.Visible = visible;
            Label label = label2;
            bool visible2 = (replaceContentTxtBox.Visible = value);
            label.Visible = visible2;
            base.AcceptButton = (value ? replaceBtn : findNextBtn);
            UpdateTitleBar();
        }
    }

    public ScriptUnitFindAndReplaceFrm()
    {
        InitializeComponent();
        searcher = new TextEditorSearcher();
    }

    private void UpdateTitleBar()
    {
        string text = (ReplaceMode ? "查找和替换" : "查找");
        Text = text;
    }

    public void ShowFor(TextEditorControl editor, bool replaceMode)
    {
        Editor = editor;
        searcher.ClearScanRegion();
        SelectionManager selectionManager = editor.ActiveTextAreaControl.SelectionManager;
        if (selectionManager.HasSomethingSelected && selectionManager.SelectionCollection.Count == 1)
        {
            ISelection selection = selectionManager.SelectionCollection[0];
            if (selection.StartPosition.Y == selection.EndPosition.Y)
            {
                findContentTxtBox.Text = selectionManager.SelectedText;
            }
        }
        else
        {
            Caret caret = editor.ActiveTextAreaControl.Caret;
            int num = TextUtilities.FindWordStart(editor.Document, caret.Offset);
            int num2 = TextUtilities.FindWordEnd(editor.Document, caret.Offset);
            findContentTxtBox.Text = editor.Document.GetText(num, num2 - num);
        }
        ReplaceMode = replaceMode;
        base.Owner = (Form)editor.TopLevelControl;
        Show();
        findContentTxtBox.SelectAll();
        findContentTxtBox.Focus();
    }

    private void findPreBtn_Click(object sender, EventArgs e)
    {
        FindNext(searchBackward: true, "未找到匹配内容！");
    }

    private void findNextBtn_Click(object sender, EventArgs e)
    {
        FindNext(searchBackward: false, "未找到匹配内容！");
    }

    public TextRange FindNext(bool searchBackward, string msg)
    {
        if (string.IsNullOrEmpty(findContentTxtBox.Text))
        {
            MessageBox.Show("请指定要查找的内容！", "提示");
            return null;
        }
        lastSearchWasBackward = searchBackward;
        searcher.LookWord = findContentTxtBox.Text;
        searcher.MatchCase = caseChkBox.Checked;
        searcher.MatchWholeWord = wholeWordChkBox.Checked;
        Caret caret = editor.ActiveTextAreaControl.Caret;
        int offset = caret.Offset;
        TextRange textRange = searcher.FindNext(offset, searchBackward, out lastSearchLoopedAround);
        if (textRange != null)
        {
            SelectResult(textRange);
        }
        else if (msg != null)
        {
            MessageBox.Show(msg, "提示");
        }
        return textRange;
    }

    private void SelectResult(TextRange range)
    {
        TextLocation startPosition = editor.Document.OffsetToPosition(range.Offset);
        TextLocation endPosition = editor.Document.OffsetToPosition(range.Offset + range.Length);
        editor.ActiveTextAreaControl.SelectionManager.SetSelection(startPosition, endPosition);
        if (lastSearchWasBackward)
        {
            editor.ActiveTextAreaControl.Caret.Position = editor.Document.OffsetToPosition(range.Offset);
        }
        else
        {
            editor.ActiveTextAreaControl.Caret.Position = editor.Document.OffsetToPosition(range.Offset + range.Length);
        }
    }

    private void ScriptUnitFindFrm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (e.CloseReason != CloseReason.FormOwnerClosing)
        {
            if (base.Owner != null)
            {
                base.Owner.Select();
            }
            e.Cancel = true;
            Hide();
            searcher.ClearScanRegion();
            editor.Refresh();
        }
    }

    private void InsertText(string text)
    {
        TextArea textArea = editor.ActiveTextAreaControl.TextArea;
        lock (textArea.Document.UndoStack)
        {
            if (textArea.SelectionManager.HasSomethingSelected)
            {
                textArea.Caret.Position = textArea.SelectionManager.SelectionCollection[0].StartPosition;
                textArea.SelectionManager.RemoveSelectedText();
            }
            textArea.InsertString(text);
        }
    }

    private void replaceBtn_Click(object sender, EventArgs e)
    {
        SelectionManager selectionManager = editor.ActiveTextAreaControl.SelectionManager;
        if (string.Equals(selectionManager.SelectedText, findContentTxtBox.Text, StringComparison.OrdinalIgnoreCase))
        {
            InsertText(replaceContentTxtBox.Text);
        }
        FindNext(lastSearchWasBackward, "内容没有找到！");
    }

    private void replaceAllBtn_Click(object sender, EventArgs e)
    {
        int num = 0;
        editor.ActiveTextAreaControl.Caret.Position = editor.Document.OffsetToPosition(searcher.BeginOffset);
        lock (editor.Document.UndoStack)
        {
            while (FindNext(searchBackward: false, null) != null && !lastSearchLoopedAround)
            {
                num++;
                InsertText(replaceContentTxtBox.Text);
            }
        }
        if (num == 0)
        {
            MessageBox.Show($"没有找到目标内容: {findContentTxtBox.Text}！", "提示");
            return;
        }
        MessageBox.Show($"替换了 {num} 处内容！", "提示");
        Close();
    }

    private void cancleBtn_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.findContentTxtBox = new System.Windows.Forms.TextBox();
        this.findNextBtn = new System.Windows.Forms.Button();
        this.cancleBtn = new System.Windows.Forms.Button();
        this.caseChkBox = new System.Windows.Forms.CheckBox();
        this.wholeWordChkBox = new System.Windows.Forms.CheckBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.replaceBtn = new System.Windows.Forms.Button();
        this.replaceAllBtn = new System.Windows.Forms.Button();
        this.findPreBtn = new System.Windows.Forms.Button();
        this.replaceContentTxtBox = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(12, 15);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(83, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "查找内容(&N)：";
        this.findContentTxtBox.Location = new System.Drawing.Point(101, 12);
        this.findContentTxtBox.Name = "findContentTxtBox";
        this.findContentTxtBox.Size = new System.Drawing.Size(258, 21);
        this.findContentTxtBox.TabIndex = 1;
        this.findNextBtn.Location = new System.Drawing.Point(249, 151);
        this.findNextBtn.Name = "findNextBtn";
        this.findNextBtn.Size = new System.Drawing.Size(110, 23);
        this.findNextBtn.TabIndex = 2;
        this.findNextBtn.Text = "查找下一个(&F)";
        this.findNextBtn.UseVisualStyleBackColor = true;
        this.findNextBtn.Click += new System.EventHandler(findNextBtn_Click);
        this.cancleBtn.Location = new System.Drawing.Point(249, 180);
        this.cancleBtn.Name = "cancleBtn";
        this.cancleBtn.Size = new System.Drawing.Size(110, 23);
        this.cancleBtn.TabIndex = 4;
        this.cancleBtn.Text = "取消";
        this.cancleBtn.UseVisualStyleBackColor = true;
        this.cancleBtn.Click += new System.EventHandler(cancleBtn_Click);
        this.caseChkBox.AutoSize = true;
        this.caseChkBox.Location = new System.Drawing.Point(195, 36);
        this.caseChkBox.Name = "caseChkBox";
        this.caseChkBox.Size = new System.Drawing.Size(108, 16);
        this.caseChkBox.TabIndex = 5;
        this.caseChkBox.Text = " 区分大小写(&C)";
        this.caseChkBox.UseVisualStyleBackColor = true;
        this.wholeWordChkBox.AutoSize = true;
        this.wholeWordChkBox.Location = new System.Drawing.Point(40, 36);
        this.wholeWordChkBox.Name = "wholeWordChkBox";
        this.wholeWordChkBox.Size = new System.Drawing.Size(90, 16);
        this.wholeWordChkBox.TabIndex = 6;
        this.wholeWordChkBox.Text = "全字匹配(&W)";
        this.wholeWordChkBox.UseVisualStyleBackColor = true;
        this.groupBox1.Controls.Add(this.wholeWordChkBox);
        this.groupBox1.Controls.Add(this.caseChkBox);
        this.groupBox1.Location = new System.Drawing.Point(14, 78);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(345, 67);
        this.groupBox1.TabIndex = 7;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "查找选项";
        this.replaceBtn.Location = new System.Drawing.Point(17, 180);
        this.replaceBtn.Name = "replaceBtn";
        this.replaceBtn.Size = new System.Drawing.Size(110, 23);
        this.replaceBtn.TabIndex = 8;
        this.replaceBtn.Text = "替换(&R)";
        this.replaceBtn.UseVisualStyleBackColor = true;
        this.replaceBtn.Click += new System.EventHandler(replaceBtn_Click);
        this.replaceAllBtn.Location = new System.Drawing.Point(133, 180);
        this.replaceAllBtn.Name = "replaceAllBtn";
        this.replaceAllBtn.Size = new System.Drawing.Size(110, 23);
        this.replaceAllBtn.TabIndex = 9;
        this.replaceAllBtn.Text = "全部替换(&A)";
        this.replaceAllBtn.UseVisualStyleBackColor = true;
        this.replaceAllBtn.Click += new System.EventHandler(replaceAllBtn_Click);
        this.findPreBtn.Location = new System.Drawing.Point(133, 151);
        this.findPreBtn.Name = "findPreBtn";
        this.findPreBtn.Size = new System.Drawing.Size(110, 23);
        this.findPreBtn.TabIndex = 10;
        this.findPreBtn.Text = "查找上一个(&P)";
        this.findPreBtn.UseVisualStyleBackColor = true;
        this.findPreBtn.Click += new System.EventHandler(findPreBtn_Click);
        this.replaceContentTxtBox.Location = new System.Drawing.Point(101, 42);
        this.replaceContentTxtBox.Name = "replaceContentTxtBox";
        this.replaceContentTxtBox.Size = new System.Drawing.Size(258, 21);
        this.replaceContentTxtBox.TabIndex = 12;
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(12, 45);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(71, 12);
        this.label2.TabIndex = 11;
        this.label2.Text = "替换为(&P)：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(371, 214);
        base.Controls.Add(this.replaceContentTxtBox);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.findPreBtn);
        base.Controls.Add(this.replaceAllBtn);
        base.Controls.Add(this.replaceBtn);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.cancleBtn);
        base.Controls.Add(this.findNextBtn);
        base.Controls.Add(this.findContentTxtBox);
        base.Controls.Add(this.label1);
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "ScriptUnitFindAndReplaceFrm";
        base.ShowIcon = false;
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "查找";
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ScriptUnitFindFrm_FormClosing);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
