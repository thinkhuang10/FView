using DevExpress.XtraEditors;
using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class TextFindForm : XtraForm
{
    private static Thread Static_FindThread;

    private static FindOptions Static_FindOptions;

    private readonly AutoResetEvent eventFindNext = new(initialState: false);

    private readonly AutoResetEvent eventAbortFind = new(initialState: false);

    private readonly List<FindResult> findResults = new();

    private TextBox textBoxContent;

    private Button buttonFind;

    private CheckBox checkBoxMatchCase;

    private CheckBox checkBoxWholeWord;

    private GroupBox groupBox1;

    private GroupBox groupBox2;

    private System.Windows.Forms.ComboBox comboBoxPage;

    private System.Windows.Forms.ComboBox comboBoxArea;

    private GroupBox groupBox3;

    private Button buttonClose;

    public event EventHandler<FindResultEventArgs> FindResultHandler;

    private event EventHandler<FindMessageEventArgs> FindMessageHandler;

    public TextFindForm()
    {
        InitializeComponent();
    }

    private void TextFindForm_Load(object sender, EventArgs e)
    {
        FindMessageHandler += delegate (object s1, FindMessageEventArgs e1)
        {
            MessageBox.Show(e1.Message, "查找提示");
        };
        comboBoxPage.Enabled = false;
        comboBoxArea.Text = comboBoxArea.Items[0].ToString();
        if (Static_FindOptions != null)
        {
            checkBoxMatchCase.Checked = Static_FindOptions.MatchCase;
            checkBoxWholeWord.Checked = Static_FindOptions.WholeWord;
        }
        textBoxContent.Focus();
    }

    private void TextFindForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (Static_FindThread != null && Static_FindThread.IsAlive)
        {
            eventAbortFind.Set();
            if (!Static_FindThread.Join(500))
            {
                Static_FindThread.Abort();
            }
            Static_FindThread = null;
        }
        findResults.Clear();
        FindOptions findOptions = new()
        {
            MatchCase = checkBoxMatchCase.Checked,
            WholeWord = checkBoxWholeWord.Checked
        };
        Static_FindOptions = findOptions;
    }

    private void comboBoxArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBoxArea.SelectedIndex == 1)
        {
            comboBoxPage.Items.Clear();
            foreach (DataFile df in CEditEnvironmentGlobal.dfs)
            {
                comboBoxPage.Items.Add(df.pageName);
            }
            comboBoxPage.SelectedIndex = 0;
            comboBoxPage.Enabled = true;
        }
        else
        {
            comboBoxPage.Items.Clear();
            comboBoxPage.Items.Add(string.Empty);
            comboBoxPage.SelectedIndex = 0;
            comboBoxPage.Enabled = false;
        }
    }

    private void buttonClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void buttonFind_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(textBoxContent.Text))
        {
            textBoxContent.Focus();
            return;
        }
        FindOptions findOptions = new()
        {
            Content = textBoxContent.Text,
            PageName = comboBoxPage.SelectedItem as string,
            MatchCase = checkBoxMatchCase.Checked,
            WholeWord = checkBoxWholeWord.Checked
        };
        FindOptions findOptions2 = findOptions;
        if (Static_FindThread != null && Static_FindThread.IsAlive)
        {
            if (findOptions2.Equals(Static_FindOptions))
            {
                eventFindNext.Set();
            }
            else
            {
                eventAbortFind.Set();
                if (!Static_FindThread.Join(500))
                {
                    Static_FindThread.Abort();
                }
                Static_FindThread = null;
            }
        }
        if (Static_FindThread == null || !Static_FindThread.IsAlive)
        {
            findResults.Clear();
            eventAbortFind.Reset();
            eventFindNext.Reset();
            Static_FindThread = new Thread(DoFind);
            Static_FindThread.Start(findOptions2);
            Static_FindOptions = findOptions2;
        }
    }

    private void DoFind(object data)
    {
        if (data is FindOptions)
        {
            FindOptions findOptions = (FindOptions)data;
            if (!string.IsNullOrEmpty(findOptions.Content))
            {
                Find(findOptions);
            }
        }
    }

    private void Find(FindOptions options)
    {
        if (!string.IsNullOrEmpty(options.PageName))
        {
            DataFile dataFile = CEditEnvironmentGlobal.dfs.Find((DataFile df) => df.pageName.Equals(options.PageName));
            if (dataFile != null)
            {
                if (Find(options, dataFile) >= 0 && this.FindMessageHandler != null)
                {
                    string message = $"查找已完成，共为您找到 {findResults.Count} 个结果。";
                    Invoke(this.FindMessageHandler, this, new FindMessageEventArgs(message));
                }
            }
            else if (this.FindMessageHandler != null)
            {
                string message2 = $"不存在名为“{options.PageName}”的页面";
                Invoke(this.FindMessageHandler, this, new FindMessageEventArgs(message2));
            }
            return;
        }
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            if (Find(options, df) < 0)
            {
                return;
            }
        }
        if (this.FindMessageHandler != null)
        {
            string message3 = $"查找已完成，共为您找到 {findResults.Count} 个结果。";
            Invoke(this.FindMessageHandler, this, new FindMessageEventArgs(message3));
        }
    }

    private int Find(FindOptions options, DataFile df)
    {
        WaitHandle[] waitHandles = new WaitHandle[2] { eventFindNext, eventAbortFind };
        foreach (CShape item in df.ListAllShowCShape)
        {
            if (IsMatch(options, item))
            {
                FindResult findResult = new()
                {
                    PageName = df.name,
                    PageDisplayName = df.pageName,
                    ShapeName = item.Name
                };
                FindResult findResult2 = findResult;
                findResults.Add(findResult2);
                if (this.FindResultHandler != null)
                {
                    Invoke(this.FindResultHandler, this, new FindResultEventArgs(findResult2));
                }
                int num = WaitHandle.WaitAny(waitHandles);
                if (num != 0 && num == 1)
                {
                    return -1;
                }
            }
            else if (eventAbortFind.WaitOne(100))
            {
                return -2;
            }
        }
        return 0;
    }

    private bool IsMatch(FindOptions options, CShape st)
    {
        bool result = false;
        if (st is CString)
        {
            CString cString = (CString)st;
            if (options.WholeWord)
            {
                if (options.MatchCase)
                {
                    if (options.Content.Equals(st.Name, StringComparison.CurrentCulture) || options.Content.Equals(cString.DisplayStr, StringComparison.CurrentCulture))
                    {
                        result = true;
                    }
                }
                else if (options.Content.Equals(st.Name, StringComparison.CurrentCultureIgnoreCase) || options.Content.Equals(cString.DisplayStr, StringComparison.CurrentCultureIgnoreCase))
                {
                    result = true;
                }
            }
            else if (options.MatchCase)
            {
                if (st.Name.Contains(options.Content) || cString.DisplayStr.Contains(options.Content))
                {
                    result = true;
                }
            }
            else if (st.Name.ToUpper().Contains(options.Content.ToUpper()) || cString.DisplayStr.ToUpper().Contains(options.Content.ToUpper()))
            {
                result = true;
            }
        }
        else if (options.WholeWord)
        {
            if (options.MatchCase)
            {
                if (options.Content.Equals(st.Name, StringComparison.CurrentCulture))
                {
                    result = true;
                }
            }
            else if (options.Content.Equals(st.Name, StringComparison.CurrentCultureIgnoreCase))
            {
                result = true;
            }
        }
        else if (options.MatchCase)
        {
            if (st.Name.Contains(options.Content))
            {
                result = true;
            }
        }
        else if (st.Name.ToUpper().Contains(options.Content.ToUpper()))
        {
            result = true;
        }
        return result;
    }

    private void InitializeComponent()
    {
        this.textBoxContent = new System.Windows.Forms.TextBox();
        this.buttonFind = new System.Windows.Forms.Button();
        this.checkBoxMatchCase = new System.Windows.Forms.CheckBox();
        this.checkBoxWholeWord = new System.Windows.Forms.CheckBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.comboBoxPage = new System.Windows.Forms.ComboBox();
        this.comboBoxArea = new System.Windows.Forms.ComboBox();
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        this.buttonClose = new System.Windows.Forms.Button();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.groupBox3.SuspendLayout();
        base.SuspendLayout();
        this.textBoxContent.Location = new System.Drawing.Point(6, 17);
        this.textBoxContent.Name = "textBoxContent";
        this.textBoxContent.Size = new System.Drawing.Size(353, 22);
        this.textBoxContent.TabIndex = 0;
        this.buttonFind.Location = new System.Drawing.Point(199, 224);
        this.buttonFind.Name = "buttonFind";
        this.buttonFind.Size = new System.Drawing.Size(114, 27);
        this.buttonFind.TabIndex = 3;
        this.buttonFind.Text = "查找/下一个(&F)";
        this.buttonFind.UseVisualStyleBackColor = true;
        this.buttonFind.Click += new System.EventHandler(buttonFind_Click);
        this.checkBoxMatchCase.AutoSize = true;
        this.checkBoxMatchCase.Checked = true;
        this.checkBoxMatchCase.CheckState = System.Windows.Forms.CheckState.Checked;
        this.checkBoxMatchCase.Location = new System.Drawing.Point(32, 21);
        this.checkBoxMatchCase.Name = "checkBoxMatchCase";
        this.checkBoxMatchCase.Size = new System.Drawing.Size(86, 18);
        this.checkBoxMatchCase.TabIndex = 0;
        this.checkBoxMatchCase.Text = "大小写匹配";
        this.checkBoxMatchCase.UseVisualStyleBackColor = true;
        this.checkBoxWholeWord.AutoSize = true;
        this.checkBoxWholeWord.Location = new System.Drawing.Point(233, 21);
        this.checkBoxWholeWord.Name = "checkBoxWholeWord";
        this.checkBoxWholeWord.Size = new System.Drawing.Size(74, 18);
        this.checkBoxWholeWord.TabIndex = 1;
        this.checkBoxWholeWord.Text = "全字匹配";
        this.checkBoxWholeWord.UseVisualStyleBackColor = true;
        this.groupBox1.Controls.Add(this.textBoxContent);
        this.groupBox1.Location = new System.Drawing.Point(12, 5);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(375, 49);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "查找内容";
        this.groupBox2.Controls.Add(this.comboBoxPage);
        this.groupBox2.Controls.Add(this.comboBoxArea);
        this.groupBox2.Location = new System.Drawing.Point(13, 60);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(374, 88);
        this.groupBox2.TabIndex = 1;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "查找范围";
        this.comboBoxPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBoxPage.FormattingEnabled = true;
        this.comboBoxPage.Location = new System.Drawing.Point(7, 55);
        this.comboBoxPage.Name = "comboBoxPage";
        this.comboBoxPage.Size = new System.Drawing.Size(349, 22);
        this.comboBoxPage.TabIndex = 1;
        this.comboBoxArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBoxArea.FormattingEnabled = true;
        this.comboBoxArea.Items.AddRange(new object[2] { "所有页面", "指定页面" });
        this.comboBoxArea.Location = new System.Drawing.Point(7, 22);
        this.comboBoxArea.Name = "comboBoxArea";
        this.comboBoxArea.Size = new System.Drawing.Size(349, 22);
        this.comboBoxArea.TabIndex = 0;
        this.comboBoxArea.SelectedIndexChanged += new System.EventHandler(comboBoxArea_SelectedIndexChanged);
        this.groupBox3.Controls.Add(this.checkBoxMatchCase);
        this.groupBox3.Controls.Add(this.checkBoxWholeWord);
        this.groupBox3.Location = new System.Drawing.Point(12, 154);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new System.Drawing.Size(375, 57);
        this.groupBox3.TabIndex = 2;
        this.groupBox3.TabStop = false;
        this.groupBox3.Text = "查找选项";
        this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.buttonClose.Location = new System.Drawing.Point(319, 224);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(66, 27);
        this.buttonClose.TabIndex = 4;
        this.buttonClose.Text = "关闭(&C)";
        this.buttonClose.UseVisualStyleBackColor = true;
        this.buttonClose.Click += new System.EventHandler(buttonClose_Click);
        base.AcceptButton = this.buttonFind;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.buttonClose;
        base.ClientSize = new System.Drawing.Size(397, 263);
        base.Controls.Add(this.groupBox3);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.buttonFind);
        base.Controls.Add(this.buttonClose);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "TextFindForm";
        base.ShowIcon = false;
        base.ShowInTaskbar = false;
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "文本查找";
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(TextFindForm_FormClosing);
        base.Load += new System.EventHandler(TextFindForm_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox3.ResumeLayout(false);
        this.groupBox3.PerformLayout();
        base.ResumeLayout(false);
    }
}
