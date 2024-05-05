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
        if (data is FindOptions findOptions)
        {
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
                if (Find(options, dataFile) >= 0 && FindMessageHandler != null)
                {
                    string message = $"查找已完成，共为您找到 {findResults.Count} 个结果。";
                    Invoke(FindMessageHandler, this, new FindMessageEventArgs(message));
                }
            }
            else if (FindMessageHandler != null)
            {
                string message2 = $"不存在名为“{options.PageName}”的页面";
                Invoke(FindMessageHandler, this, new FindMessageEventArgs(message2));
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
        if (FindMessageHandler != null)
        {
            string message3 = $"查找已完成，共为您找到 {findResults.Count} 个结果。";
            Invoke(FindMessageHandler, this, new FindMessageEventArgs(message3));
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
                if (FindResultHandler != null)
                {
                    Invoke(FindResultHandler, this, new FindResultEventArgs(findResult2));
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
        if (st is CString cString)
        {
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
        textBoxContent = new System.Windows.Forms.TextBox();
        buttonFind = new System.Windows.Forms.Button();
        checkBoxMatchCase = new System.Windows.Forms.CheckBox();
        checkBoxWholeWord = new System.Windows.Forms.CheckBox();
        groupBox1 = new System.Windows.Forms.GroupBox();
        groupBox2 = new System.Windows.Forms.GroupBox();
        comboBoxPage = new System.Windows.Forms.ComboBox();
        comboBoxArea = new System.Windows.Forms.ComboBox();
        groupBox3 = new System.Windows.Forms.GroupBox();
        buttonClose = new System.Windows.Forms.Button();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox3.SuspendLayout();
        base.SuspendLayout();
        textBoxContent.Location = new System.Drawing.Point(6, 17);
        textBoxContent.Name = "textBoxContent";
        textBoxContent.Size = new System.Drawing.Size(353, 22);
        textBoxContent.TabIndex = 0;
        buttonFind.Location = new System.Drawing.Point(199, 224);
        buttonFind.Name = "buttonFind";
        buttonFind.Size = new System.Drawing.Size(114, 27);
        buttonFind.TabIndex = 3;
        buttonFind.Text = "查找/下一个(&F)";
        buttonFind.UseVisualStyleBackColor = true;
        buttonFind.Click += new System.EventHandler(buttonFind_Click);
        checkBoxMatchCase.AutoSize = true;
        checkBoxMatchCase.Checked = true;
        checkBoxMatchCase.CheckState = System.Windows.Forms.CheckState.Checked;
        checkBoxMatchCase.Location = new System.Drawing.Point(32, 21);
        checkBoxMatchCase.Name = "checkBoxMatchCase";
        checkBoxMatchCase.Size = new System.Drawing.Size(86, 18);
        checkBoxMatchCase.TabIndex = 0;
        checkBoxMatchCase.Text = "大小写匹配";
        checkBoxMatchCase.UseVisualStyleBackColor = true;
        checkBoxWholeWord.AutoSize = true;
        checkBoxWholeWord.Location = new System.Drawing.Point(233, 21);
        checkBoxWholeWord.Name = "checkBoxWholeWord";
        checkBoxWholeWord.Size = new System.Drawing.Size(74, 18);
        checkBoxWholeWord.TabIndex = 1;
        checkBoxWholeWord.Text = "全字匹配";
        checkBoxWholeWord.UseVisualStyleBackColor = true;
        groupBox1.Controls.Add(textBoxContent);
        groupBox1.Location = new System.Drawing.Point(12, 5);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(375, 49);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "查找内容";
        groupBox2.Controls.Add(comboBoxPage);
        groupBox2.Controls.Add(comboBoxArea);
        groupBox2.Location = new System.Drawing.Point(13, 60);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(374, 88);
        groupBox2.TabIndex = 1;
        groupBox2.TabStop = false;
        groupBox2.Text = "查找范围";
        comboBoxPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBoxPage.FormattingEnabled = true;
        comboBoxPage.Location = new System.Drawing.Point(7, 55);
        comboBoxPage.Name = "comboBoxPage";
        comboBoxPage.Size = new System.Drawing.Size(349, 22);
        comboBoxPage.TabIndex = 1;
        comboBoxArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBoxArea.FormattingEnabled = true;
        comboBoxArea.Items.AddRange(new object[2] { "所有页面", "指定页面" });
        comboBoxArea.Location = new System.Drawing.Point(7, 22);
        comboBoxArea.Name = "comboBoxArea";
        comboBoxArea.Size = new System.Drawing.Size(349, 22);
        comboBoxArea.TabIndex = 0;
        comboBoxArea.SelectedIndexChanged += new System.EventHandler(comboBoxArea_SelectedIndexChanged);
        groupBox3.Controls.Add(checkBoxMatchCase);
        groupBox3.Controls.Add(checkBoxWholeWord);
        groupBox3.Location = new System.Drawing.Point(12, 154);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new System.Drawing.Size(375, 57);
        groupBox3.TabIndex = 2;
        groupBox3.TabStop = false;
        groupBox3.Text = "查找选项";
        buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        buttonClose.Location = new System.Drawing.Point(319, 224);
        buttonClose.Name = "buttonClose";
        buttonClose.Size = new System.Drawing.Size(66, 27);
        buttonClose.TabIndex = 4;
        buttonClose.Text = "关闭(&C)";
        buttonClose.UseVisualStyleBackColor = true;
        buttonClose.Click += new System.EventHandler(buttonClose_Click);
        base.AcceptButton = buttonFind;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = buttonClose;
        base.ClientSize = new System.Drawing.Size(397, 263);
        base.Controls.Add(groupBox3);
        base.Controls.Add(groupBox2);
        base.Controls.Add(groupBox1);
        base.Controls.Add(buttonFind);
        base.Controls.Add(buttonClose);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "TextFindForm";
        base.ShowIcon = false;
        base.ShowInTaskbar = false;
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "文本查找";
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(TextFindForm_FormClosing);
        base.Load += new System.EventHandler(TextFindForm_Load);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        base.ResumeLayout(false);
    }
}
