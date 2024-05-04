using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;

namespace HMIEditEnvironment.CodeCompletion;

internal class CodeCompletionDataProvider : ICompletionDataProvider
{
    private readonly ScriptUnit form;

    public ImageList ImageList => form.imageList1;

    public string PreSelection => null;

    public int DefaultIndex => -1;

    public CodeCompletionDataProvider(ScriptUnit form)
    {
        this.form = form;
    }

    public CompletionDataProviderKeyResult ProcessKey(char key)
    {
        throw new NotImplementedException();
    }

    public bool InsertAction(ICompletionData data, TextArea textArea, int insertionOffset, char key)
    {
        textArea.Caret.Position = textArea.Document.OffsetToPosition(insertionOffset);
        data.Text += ']';
        return data.InsertAction(textArea, key);
    }

    public ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
    {
        List<ICompletionData> list = new();
        if ('[' == charTyped)
        {
            GenerateVariableCodeCompletionWindowContent(list);
        }
        _ = 46;
        return list.ToArray();
    }

    private void GeneratePageCtrlCodeCompletionWindowContent(List<ICompletionData> dataLst)
    {
        foreach (TreeNode node in form.treeView2.Nodes)
        {
            if (!(node.Text != "工程相关"))
            {
                GetTreeViewPageNode(node, dataLst);
            }
        }
    }

    private void GetTreeViewPageNode(TreeNode nodeItem, List<ICompletionData> dataLst)
    {
    }

    private void GenerateVariableCodeCompletionWindowContent(List<ICompletionData> dataLst)
    {
        foreach (TreeNode node in form.treeView2.Nodes)
        {
            if (!(node.Text != "变量"))
            {
                GetTreeViewVarTableNode(node, dataLst);
            }
        }
    }

    private void GetTreeViewVarTableNode(TreeNode node, List<ICompletionData> dataLst)
    {
        if (node.Nodes.Count > 0)
        {
            foreach (TreeNode node2 in node.Nodes)
            {
                GetTreeViewVarTableNode(node2, dataLst);
            }
            return;
        }
        if (node.Text != "内部变量")
        {
            DefaultCompletionData item = new(node.Text, 2);
            dataLst.Add(item);
        }
    }
}
