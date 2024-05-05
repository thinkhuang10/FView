using ShapeRuntime.DBAnimation;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HMIEditEnvironment.Animation;

public class DBMultOperation : Form
{
    public List<DBAnimation> DBAnimations = new();

    private Button button1;

    private Button button2;

    private Button button3;

    private CheckedListBox checkedListBox1;

    private Button button4;

    private Button button5;

    private Button button6;

    private Button button7;

    public DBMultOperation()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        DBOperationTypeSelect dBOperationTypeSelect = new();
        if (dBOperationTypeSelect.ShowDialog() != DialogResult.OK)
        {
            return;
        }
        if (dBOperationTypeSelect.OperationType == "查询数据")
        {
            DBSelectForm dBSelectForm = new();
            if (dBSelectForm.ShowDialog() == DialogResult.OK)
            {
                DBSelectAnimation dBSelectAnimation = new()
                {
                    ansync = dBSelectForm.Ansync,
                    Type = "查询数据",
                    enable = true,
                    dbselectSQL = dBSelectForm.ResultSQL,
                    dbselectTO = dBSelectForm.ResultTo,
                    dbselectOtherData = dBSelectForm.OtherData
                };
                checkedListBox1.Items.Add(dBSelectAnimation, isChecked: true);
            }
        }
        else if (dBOperationTypeSelect.OperationType == "修改数据")
        {
            DBUpdateForm dBUpdateForm = new();
            if (dBUpdateForm.ShowDialog() == DialogResult.OK)
            {
                DBUpdateAnimation dBUpdateAnimation = new()
                {
                    ansync = dBUpdateForm.Ansync,
                    Type = "修改数据",
                    enable = true,
                    dbupdateSQL = dBUpdateForm.ResultSQL,
                    dbupdateOtherData = dBUpdateForm.OtherData
                };
                checkedListBox1.Items.Add(dBUpdateAnimation, isChecked: true);
            }
        }
        else if (dBOperationTypeSelect.OperationType == "添加数据")
        {
            DBInsertForm dBInsertForm = new();
            if (dBInsertForm.ShowDialog() == DialogResult.OK)
            {
                DBInsertAnimation dBInsertAnimation = new()
                {
                    ansync = dBInsertForm.Ansync,
                    Type = "添加数据",
                    enable = true,
                    dbinsertSQL = dBInsertForm.ResultSQL,
                    dbinsertOtherData = dBInsertForm.OtherData
                };
                checkedListBox1.Items.Add(dBInsertAnimation, isChecked: true);
            }
        }
        else if (dBOperationTypeSelect.OperationType == "删除数据")
        {
            DBDeleteForm dBDeleteForm = new();
            if (dBDeleteForm.ShowDialog() == DialogResult.OK)
            {
                DBDeleteAnimation dBDeleteAnimation = new()
                {
                    ansync = dBDeleteForm.Ansync,
                    Type = "删除数据",
                    enable = true,
                    dbdeleteSQL = dBDeleteForm.ResultSQL,
                    dbdeleteOtherData = dBDeleteForm.OtherData
                };
                checkedListBox1.Items.Add(dBDeleteAnimation, isChecked: true);
            }
        }
    }

    private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        ((DBAnimation)checkedListBox1.Items[e.Index]).enable = e.NewValue == CheckState.Checked;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (checkedListBox1.SelectedItem == null)
        {
            return;
        }
        DBAnimation dBAnimation = (DBAnimation)checkedListBox1.SelectedItem;
        if (dBAnimation.Type == "插入数据")
        {
            dBAnimation.Type = "添加数据";
        }
        switch (dBAnimation.Type)
        {
            case "查询数据":
                {
                    DBSelectForm dBSelectForm = new()
                    {
                        ResultSQL = ((DBSelectAnimation)dBAnimation).dbselectSQL,
                        ResultTo = ((DBSelectAnimation)dBAnimation).dbselectTO,
                        Ansync = ((DBSelectAnimation)dBAnimation).ansync,
                        OtherData = ((DBSelectAnimation)dBAnimation).dbselectOtherData
                    };
                    if (dBSelectForm.ShowDialog() == DialogResult.OK)
                    {
                        ((DBSelectAnimation)dBAnimation).dbselectSQL = dBSelectForm.ResultSQL;
                        ((DBSelectAnimation)dBAnimation).dbselectTO = dBSelectForm.ResultTo;
                        ((DBSelectAnimation)dBAnimation).ansync = dBSelectForm.Ansync;
                        ((DBSelectAnimation)dBAnimation).dbselectOtherData = dBSelectForm.OtherData;
                    }
                    break;
                }
            case "修改数据":
                {
                    DBUpdateForm dBUpdateForm = new()
                    {
                        ResultSQL = ((DBUpdateAnimation)dBAnimation).dbupdateSQL,
                        Ansync = ((DBUpdateAnimation)dBAnimation).ansync,
                        OtherData = ((DBUpdateAnimation)dBAnimation).dbupdateOtherData
                    };
                    if (dBUpdateForm.ShowDialog() == DialogResult.OK)
                    {
                        ((DBUpdateAnimation)dBAnimation).dbupdateSQL = dBUpdateForm.ResultSQL;
                        ((DBUpdateAnimation)dBAnimation).ansync = dBUpdateForm.Ansync;
                        ((DBUpdateAnimation)dBAnimation).dbupdateOtherData = dBUpdateForm.OtherData;
                    }
                    break;
                }
            case "添加数据":
                {
                    DBInsertForm dBInsertForm = new()
                    {
                        ResultSQL = ((DBInsertAnimation)dBAnimation).dbinsertSQL,
                        Ansync = ((DBInsertAnimation)dBAnimation).ansync,
                        OtherData = ((DBInsertAnimation)dBAnimation).dbinsertOtherData
                    };
                    if (dBInsertForm.ShowDialog() == DialogResult.OK)
                    {
                        ((DBInsertAnimation)dBAnimation).dbinsertSQL = dBInsertForm.ResultSQL;
                        ((DBInsertAnimation)dBAnimation).ansync = dBInsertForm.Ansync;
                        ((DBInsertAnimation)dBAnimation).dbinsertOtherData = dBInsertForm.OtherData;
                    }
                    break;
                }
            case "删除数据":
                {
                    DBDeleteForm dBDeleteForm = new()
                    {
                        ResultSQL = ((DBDeleteAnimation)dBAnimation).dbdeleteSQL,
                        Ansync = ((DBDeleteAnimation)dBAnimation).ansync,
                        OtherData = ((DBDeleteAnimation)dBAnimation).dbdeleteOtherData
                    };
                    if (dBDeleteForm.ShowDialog() == DialogResult.OK)
                    {
                        ((DBDeleteAnimation)dBAnimation).dbdeleteSQL = dBDeleteForm.ResultSQL;
                        ((DBDeleteAnimation)dBAnimation).ansync = dBDeleteForm.Ansync;
                        ((DBDeleteAnimation)dBAnimation).dbdeleteOtherData = dBDeleteForm.OtherData;
                    }
                    break;
                }
        }
    }

    private void button3_Click(object sender, EventArgs e)
    {
        checkedListBox1.Items.Remove(checkedListBox1.SelectedItem);
    }

    private void button6_Click(object sender, EventArgs e)
    {
        Dictionary<int, object> dictionary = new();
        foreach (int selectedIndex in checkedListBox1.SelectedIndices)
        {
            dictionary.Add(selectedIndex, checkedListBox1.Items[selectedIndex]);
        }
        checkedListBox1.SelectedItems.Clear();
        foreach (int key in dictionary.Keys)
        {
            if (key != 0)
            {
                checkedListBox1.Items.Remove(dictionary[key]);
                checkedListBox1.Items.Insert(key - 1, dictionary[key]);
                checkedListBox1.SelectedItems.Add(dictionary[key]);
                continue;
            }
            break;
        }
    }

    private void button7_Click(object sender, EventArgs e)
    {
        Dictionary<int, object> dictionary = new();
        foreach (int selectedIndex in checkedListBox1.SelectedIndices)
        {
            dictionary.Add(selectedIndex, checkedListBox1.Items[selectedIndex]);
        }
        checkedListBox1.SelectedItems.Clear();
        foreach (int key in dictionary.Keys)
        {
            if (key != checkedListBox1.Items.Count - 1)
            {
                checkedListBox1.Items.Remove(dictionary[key]);
                checkedListBox1.Items.Insert(key + 1, dictionary[key]);
                checkedListBox1.SelectedItems.Add(dictionary[key]);
                continue;
            }
            break;
        }
    }

    private void button4_Click(object sender, EventArgs e)
    {
        DBAnimations = new List<DBAnimation>();
        foreach (object item in checkedListBox1.Items)
        {
            if (item is DBAnimation)
            {
                DBAnimations.Add((DBAnimation)item);
            }
        }
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void DBMultOperation_Load(object sender, EventArgs e)
    {
        if (DBAnimations == null)
        {
            return;
        }
        foreach (DBAnimation dBAnimation in DBAnimations)
        {
            checkedListBox1.Items.Add(dBAnimation, dBAnimation.enable);
        }
    }

    private void button5_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void InitializeComponent()
    {
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        checkedListBox1 = new System.Windows.Forms.CheckedListBox();
        button4 = new System.Windows.Forms.Button();
        button5 = new System.Windows.Forms.Button();
        button6 = new System.Windows.Forms.Button();
        button7 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(12, 12);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 0;
        button1.Text = "添加";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.Location = new System.Drawing.Point(93, 12);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 1;
        button2.Text = "修改";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        button3.Location = new System.Drawing.Point(174, 12);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(75, 23);
        button3.TabIndex = 2;
        button3.Text = "删除";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        checkedListBox1.FormattingEnabled = true;
        checkedListBox1.Location = new System.Drawing.Point(12, 41);
        checkedListBox1.Name = "checkedListBox1";
        checkedListBox1.ScrollAlwaysVisible = true;
        checkedListBox1.Size = new System.Drawing.Size(206, 356);
        checkedListBox1.TabIndex = 3;
        checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(checkedListBox1_ItemCheck);
        button4.Location = new System.Drawing.Point(29, 409);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(75, 23);
        button4.TabIndex = 6;
        button4.Text = "确定";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(button4_Click);
        button5.Location = new System.Drawing.Point(157, 409);
        button5.Name = "button5";
        button5.Size = new System.Drawing.Size(75, 23);
        button5.TabIndex = 7;
        button5.Text = "取消";
        button5.UseVisualStyleBackColor = true;
        button5.Click += new System.EventHandler(button5_Click);
        button6.Location = new System.Drawing.Point(224, 41);
        button6.Name = "button6";
        button6.Size = new System.Drawing.Size(25, 23);
        button6.TabIndex = 4;
        button6.Text = "↑";
        button6.UseVisualStyleBackColor = true;
        button6.Click += new System.EventHandler(button6_Click);
        button7.Location = new System.Drawing.Point(224, 86);
        button7.Name = "button7";
        button7.Size = new System.Drawing.Size(25, 23);
        button7.TabIndex = 5;
        button7.Text = "↓";
        button7.UseVisualStyleBackColor = true;
        button7.Click += new System.EventHandler(button7_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(261, 444);
        base.Controls.Add(button7);
        base.Controls.Add(button6);
        base.Controls.Add(checkedListBox1);
        base.Controls.Add(button3);
        base.Controls.Add(button2);
        base.Controls.Add(button5);
        base.Controls.Add(button4);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Name = "DBMultOperation";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "数据库操作";
        base.Load += new System.EventHandler(DBMultOperation_Load);
        base.ResumeLayout(false);
    }
}
