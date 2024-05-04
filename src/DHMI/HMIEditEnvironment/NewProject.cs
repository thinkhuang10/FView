using Model;
using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace HMIEditEnvironment
{
    public partial class NewProject : Form
    {

        public string ProjeceFilePath;

        public NewProject()
        {
            InitializeComponent();
        }

        private void button_OpenFileDialog_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog.ShowDialog();
            if (DialogResult.OK == result)
            {
                textBox_ProjectLocation.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            var directoryPath = Path.Combine(textBox_ProjectLocation.Text.Trim(), textBox_ProjectName.Text.Trim());
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);

                var filePath = Path.Combine(directoryPath, textBox_ProjectName.Text.Trim() + ".fview");
                using (var fileWriter = new StreamWriter(filePath))
                {
                    var projectInfo = new ProjectInfo
                    {
                        Description = richTextBox_ProjectDescription.Text.Trim()
                    };
                    fileWriter.Write(JsonSerializer.Serialize(projectInfo));
                }

                ProjeceFilePath = filePath;

                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("工程名称已存在，请修改！", "FView", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void NewProject_Load(object sender, EventArgs e)
        {

        }
    }
}
