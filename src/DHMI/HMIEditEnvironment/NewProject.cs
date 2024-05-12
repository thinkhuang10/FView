using LogHelper;
using Model;
using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using Util;

namespace HMIEditEnvironment
{
    public partial class NewProject : Form
    {
        public string ProjeceFilePath;

        public NewProject()
        {
            InitializeComponent();
        }

        private void NewProject_Load(object sender, EventArgs e)
        {
            TextBox_ProjectLocation.Text = PathHelper.GetDefaultProjectPath();
        }

        private void Button_OpenFileDialog_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog.ShowDialog();
            if (DialogResult.OK == result)
            {
                TextBox_ProjectLocation.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            var projectName = TextBox_ProjectName.Text.Trim();
            var projectPath = TextBox_ProjectLocation.Text.Trim();
            if (string.IsNullOrEmpty(projectName))
            {
                MessageBox.Show("请输入工程名称！", ConstantHelper.SoftwareName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(projectPath))
            {
                MessageBox.Show("请输入工程路径！", ConstantHelper.SoftwareName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var directoryPath = Path.Combine(projectPath, projectName);
            if (Directory.Exists(directoryPath))
            {
                MessageBox.Show("工程名称已存在，请修改！", ConstantHelper.SoftwareName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;            
            }

            try
            {
                Directory.CreateDirectory(directoryPath);

                var filePath = Path.Combine(directoryPath, projectName + ConstantHelper.ProjectSuffixName);
                using (var fileWriter = new StreamWriter(filePath))
                {
                    var projectInfo = new ProjectInfo
                    {
                        Description = RichTextBox_ProjectDescription.Text.Trim()
                    };
                    fileWriter.Write(JsonSerializer.Serialize(projectInfo));
                }

                ProjeceFilePath = filePath;

                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                MessageBox.Show("新建工程失败:" + ex.Message, ConstantHelper.SoftwareName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogUtil.Error("新建工程失败:" + ex);
            }
        }
    }
}
