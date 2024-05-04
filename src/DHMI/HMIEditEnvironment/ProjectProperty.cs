using System;
using System.Windows.Forms;

namespace HMIEditEnvironment
{
    public partial class ProjectProperty : Form
    {
        public string ProjecePath;

        public ProjectProperty(string ProjectPath)
        {
            InitializeComponent();

            this.ProjecePath = ProjectPath; 
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ProjectProperty_Load(object sender, EventArgs e)
        {
            textBox_ProjectLocation.Text = ProjecePath;
        }
    }
}
