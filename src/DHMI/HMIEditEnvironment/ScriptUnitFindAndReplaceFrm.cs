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
            base.Owner?.Select();
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
        label1 = new System.Windows.Forms.Label();
        findContentTxtBox = new System.Windows.Forms.TextBox();
        findNextBtn = new System.Windows.Forms.Button();
        cancleBtn = new System.Windows.Forms.Button();
        caseChkBox = new System.Windows.Forms.CheckBox();
        wholeWordChkBox = new System.Windows.Forms.CheckBox();
        groupBox1 = new System.Windows.Forms.GroupBox();
        replaceBtn = new System.Windows.Forms.Button();
        replaceAllBtn = new System.Windows.Forms.Button();
        findPreBtn = new System.Windows.Forms.Button();
        replaceContentTxtBox = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        groupBox1.SuspendLayout();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(12, 15);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(83, 12);
        label1.TabIndex = 0;
        label1.Text = "查找内容(&N)：";
        findContentTxtBox.Location = new System.Drawing.Point(101, 12);
        findContentTxtBox.Name = "findContentTxtBox";
        findContentTxtBox.Size = new System.Drawing.Size(258, 21);
        findContentTxtBox.TabIndex = 1;
        findNextBtn.Location = new System.Drawing.Point(249, 151);
        findNextBtn.Name = "findNextBtn";
        findNextBtn.Size = new System.Drawing.Size(110, 23);
        findNextBtn.TabIndex = 2;
        findNextBtn.Text = "查找下一个(&F)";
        findNextBtn.UseVisualStyleBackColor = true;
        findNextBtn.Click += new System.EventHandler(findNextBtn_Click);
        cancleBtn.Location = new System.Drawing.Point(249, 180);
        cancleBtn.Name = "cancleBtn";
        cancleBtn.Size = new System.Drawing.Size(110, 23);
        cancleBtn.TabIndex = 4;
        cancleBtn.Text = "取消";
        cancleBtn.UseVisualStyleBackColor = true;
        cancleBtn.Click += new System.EventHandler(cancleBtn_Click);
        caseChkBox.AutoSize = true;
        caseChkBox.Location = new System.Drawing.Point(195, 36);
        caseChkBox.Name = "caseChkBox";
        caseChkBox.Size = new System.Drawing.Size(108, 16);
        caseChkBox.TabIndex = 5;
        caseChkBox.Text = " 区分大小写(&C)";
        caseChkBox.UseVisualStyleBackColor = true;
        wholeWordChkBox.AutoSize = true;
        wholeWordChkBox.Location = new System.Drawing.Point(40, 36);
        wholeWordChkBox.Name = "wholeWordChkBox";
        wholeWordChkBox.Size = new System.Drawing.Size(90, 16);
        wholeWordChkBox.TabIndex = 6;
        wholeWordChkBox.Text = "全字匹配(&W)";
        wholeWordChkBox.UseVisualStyleBackColor = true;
        groupBox1.Controls.Add(wholeWordChkBox);
        groupBox1.Controls.Add(caseChkBox);
        groupBox1.Location = new System.Drawing.Point(14, 78);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(345, 67);
        groupBox1.TabIndex = 7;
        groupBox1.TabStop = false;
        groupBox1.Text = "查找选项";
        replaceBtn.Location = new System.Drawing.Point(17, 180);
        replaceBtn.Name = "replaceBtn";
        replaceBtn.Size = new System.Drawing.Size(110, 23);
        replaceBtn.TabIndex = 8;
        replaceBtn.Text = "替换(&R)";
        replaceBtn.UseVisualStyleBackColor = true;
        replaceBtn.Click += new System.EventHandler(replaceBtn_Click);
        replaceAllBtn.Location = new System.Drawing.Point(133, 180);
        replaceAllBtn.Name = "replaceAllBtn";
        replaceAllBtn.Size = new System.Drawing.Size(110, 23);
        replaceAllBtn.TabIndex = 9;
        replaceAllBtn.Text = "全部替换(&A)";
        replaceAllBtn.UseVisualStyleBackColor = true;
        replaceAllBtn.Click += new System.EventHandler(replaceAllBtn_Click);
        findPreBtn.Location = new System.Drawing.Point(133, 151);
        findPreBtn.Name = "findPreBtn";
        findPreBtn.Size = new System.Drawing.Size(110, 23);
        findPreBtn.TabIndex = 10;
        findPreBtn.Text = "查找上一个(&P)";
        findPreBtn.UseVisualStyleBackColor = true;
        findPreBtn.Click += new System.EventHandler(findPreBtn_Click);
        replaceContentTxtBox.Location = new System.Drawing.Point(101, 42);
        replaceContentTxtBox.Name = "replaceContentTxtBox";
        replaceContentTxtBox.Size = new System.Drawing.Size(258, 21);
        replaceContentTxtBox.TabIndex = 12;
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(12, 45);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(71, 12);
        label2.TabIndex = 11;
        label2.Text = "替换为(&P)：";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(371, 214);
        base.Controls.Add(replaceContentTxtBox);
        base.Controls.Add(label2);
        base.Controls.Add(findPreBtn);
        base.Controls.Add(replaceAllBtn);
        base.Controls.Add(replaceBtn);
        base.Controls.Add(groupBox1);
        base.Controls.Add(cancleBtn);
        base.Controls.Add(findNextBtn);
        base.Controls.Add(findContentTxtBox);
        base.Controls.Add(label1);
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "ScriptUnitFindAndReplaceFrm";
        base.ShowIcon = false;
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "查找";
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ScriptUnitFindFrm_FormClosing);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
