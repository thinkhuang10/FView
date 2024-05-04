namespace HMIEditEnvironment
{
    partial class NewProject
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
            this.label_ProjectName = new System.Windows.Forms.Label();
            this.label_ProjectLocation = new System.Windows.Forms.Label();
            this.textBox_ProjectName = new System.Windows.Forms.TextBox();
            this.textBox_ProjectLocation = new System.Windows.Forms.TextBox();
            this.richTextBox_ProjectDescription = new System.Windows.Forms.RichTextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label_ProjectDescription = new System.Windows.Forms.Label();
            this.button_OpenFileDialog = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label_ProjectName
            // 
            this.label_ProjectName.AutoSize = true;
            this.label_ProjectName.Location = new System.Drawing.Point(15, 23);
            this.label_ProjectName.Name = "label_ProjectName";
            this.label_ProjectName.Size = new System.Drawing.Size(41, 12);
            this.label_ProjectName.TabIndex = 0;
            this.label_ProjectName.Text = "名称：";
            // 
            // label_ProjectLocation
            // 
            this.label_ProjectLocation.AutoSize = true;
            this.label_ProjectLocation.Location = new System.Drawing.Point(15, 59);
            this.label_ProjectLocation.Name = "label_ProjectLocation";
            this.label_ProjectLocation.Size = new System.Drawing.Size(41, 12);
            this.label_ProjectLocation.TabIndex = 1;
            this.label_ProjectLocation.Text = "路径：";
            // 
            // textBox_ProjectName
            // 
            this.textBox_ProjectName.Location = new System.Drawing.Point(62, 20);
            this.textBox_ProjectName.Name = "textBox_ProjectName";
            this.textBox_ProjectName.Size = new System.Drawing.Size(433, 21);
            this.textBox_ProjectName.TabIndex = 2;
            this.textBox_ProjectName.Text = "新建工程";
            // 
            // textBox_ProjectLocation
            // 
            this.textBox_ProjectLocation.Location = new System.Drawing.Point(62, 56);
            this.textBox_ProjectLocation.Name = "textBox_ProjectLocation";
            this.textBox_ProjectLocation.Size = new System.Drawing.Size(352, 21);
            this.textBox_ProjectLocation.TabIndex = 3;
            this.textBox_ProjectLocation.Text = "C:\\FView\\Projects\\";
            // 
            // richTextBox_ProjectDescription
            // 
            this.richTextBox_ProjectDescription.Location = new System.Drawing.Point(62, 92);
            this.richTextBox_ProjectDescription.Name = "richTextBox_ProjectDescription";
            this.richTextBox_ProjectDescription.Size = new System.Drawing.Size(433, 69);
            this.richTextBox_ProjectDescription.TabIndex = 4;
            this.richTextBox_ProjectDescription.Text = "";
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(339, 178);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 5;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(420, 178);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 6;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // label_ProjectDescription
            // 
            this.label_ProjectDescription.AutoSize = true;
            this.label_ProjectDescription.Location = new System.Drawing.Point(15, 95);
            this.label_ProjectDescription.Name = "label_ProjectDescription";
            this.label_ProjectDescription.Size = new System.Drawing.Size(41, 12);
            this.label_ProjectDescription.TabIndex = 7;
            this.label_ProjectDescription.Text = "描述：";
            // 
            // button_OpenFileDialog
            // 
            this.button_OpenFileDialog.Location = new System.Drawing.Point(420, 56);
            this.button_OpenFileDialog.Name = "button_OpenFileDialog";
            this.button_OpenFileDialog.Size = new System.Drawing.Size(75, 23);
            this.button_OpenFileDialog.TabIndex = 8;
            this.button_OpenFileDialog.Text = "浏览";
            this.button_OpenFileDialog.UseVisualStyleBackColor = true;
            this.button_OpenFileDialog.Click += new System.EventHandler(this.button_OpenFileDialog_Click);
            // 
            // NewProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 215);
            this.Controls.Add(this.button_OpenFileDialog);
            this.Controls.Add(this.label_ProjectDescription);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.richTextBox_ProjectDescription);
            this.Controls.Add(this.textBox_ProjectLocation);
            this.Controls.Add(this.textBox_ProjectName);
            this.Controls.Add(this.label_ProjectLocation);
            this.Controls.Add(this.label_ProjectName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NewProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建工程";
            this.Load += new System.EventHandler(this.NewProject_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_ProjectName;
        private System.Windows.Forms.Label label_ProjectLocation;
        private System.Windows.Forms.TextBox textBox_ProjectName;
        private System.Windows.Forms.TextBox textBox_ProjectLocation;
        private System.Windows.Forms.RichTextBox richTextBox_ProjectDescription;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label_ProjectDescription;
        private System.Windows.Forms.Button button_OpenFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}