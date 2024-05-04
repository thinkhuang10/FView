namespace HMIEditEnvironment
{
    partial class ProjectProperty
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_ProjectLocation = new System.Windows.Forms.Label();
            this.textBox_ProjectLocation = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label_ProjectLocation
            // 
            this.label_ProjectLocation.AutoSize = true;
            this.label_ProjectLocation.Location = new System.Drawing.Point(10, 15);
            this.label_ProjectLocation.Name = "label_ProjectLocation";
            this.label_ProjectLocation.Size = new System.Drawing.Size(41, 12);
            this.label_ProjectLocation.TabIndex = 1;
            this.label_ProjectLocation.Text = "路径：";
            // 
            // textBox_ProjectLocation
            // 
            this.textBox_ProjectLocation.Location = new System.Drawing.Point(57, 12);
            this.textBox_ProjectLocation.Name = "textBox_ProjectLocation";
            this.textBox_ProjectLocation.ReadOnly = true;
            this.textBox_ProjectLocation.Size = new System.Drawing.Size(433, 21);
            this.textBox_ProjectLocation.TabIndex = 3;
            this.textBox_ProjectLocation.Text = "C:\\FView\\Projects\\";
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(415, 48);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 5;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // ProjectProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 85);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.textBox_ProjectLocation);
            this.Controls.Add(this.label_ProjectLocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ProjectProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工程属性";
            this.Load += new System.EventHandler(this.ProjectProperty_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_ProjectLocation;
        private System.Windows.Forms.TextBox textBox_ProjectLocation;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}