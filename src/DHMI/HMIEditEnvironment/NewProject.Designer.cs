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
            this.Label_ProjectName = new System.Windows.Forms.Label();
            this.Label_ProjectLocation = new System.Windows.Forms.Label();
            this.TextBox_ProjectName = new System.Windows.Forms.TextBox();
            this.TextBox_ProjectLocation = new System.Windows.Forms.TextBox();
            this.RichTextBox_ProjectDescription = new System.Windows.Forms.RichTextBox();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Label_ProjectDescription = new System.Windows.Forms.Label();
            this.Button_OpenFileDialog = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // Label_ProjectName
            // 
            this.Label_ProjectName.AutoSize = true;
            this.Label_ProjectName.Location = new System.Drawing.Point(15, 23);
            this.Label_ProjectName.Name = "Label_ProjectName";
            this.Label_ProjectName.Size = new System.Drawing.Size(41, 12);
            this.Label_ProjectName.TabIndex = 0;
            this.Label_ProjectName.Text = "名称：";
            // 
            // Label_ProjectLocation
            // 
            this.Label_ProjectLocation.AutoSize = true;
            this.Label_ProjectLocation.Location = new System.Drawing.Point(15, 59);
            this.Label_ProjectLocation.Name = "Label_ProjectLocation";
            this.Label_ProjectLocation.Size = new System.Drawing.Size(41, 12);
            this.Label_ProjectLocation.TabIndex = 1;
            this.Label_ProjectLocation.Text = "路径：";
            // 
            // TextBox_ProjectName
            // 
            this.TextBox_ProjectName.Location = new System.Drawing.Point(62, 20);
            this.TextBox_ProjectName.Name = "TextBox_ProjectName";
            this.TextBox_ProjectName.Size = new System.Drawing.Size(433, 21);
            this.TextBox_ProjectName.TabIndex = 2;
            this.TextBox_ProjectName.Text = "新建工程";
            // 
            // TextBox_ProjectLocation
            // 
            this.TextBox_ProjectLocation.Location = new System.Drawing.Point(62, 56);
            this.TextBox_ProjectLocation.Name = "TextBox_ProjectLocation";
            this.TextBox_ProjectLocation.Size = new System.Drawing.Size(352, 21);
            this.TextBox_ProjectLocation.TabIndex = 3;
            // 
            // RichTextBox_ProjectDescription
            // 
            this.RichTextBox_ProjectDescription.Location = new System.Drawing.Point(62, 92);
            this.RichTextBox_ProjectDescription.Name = "RichTextBox_ProjectDescription";
            this.RichTextBox_ProjectDescription.Size = new System.Drawing.Size(433, 69);
            this.RichTextBox_ProjectDescription.TabIndex = 4;
            this.RichTextBox_ProjectDescription.Text = "";
            // 
            // Button_OK
            // 
            this.Button_OK.Location = new System.Drawing.Point(339, 178);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 5;
            this.Button_OK.Text = "确定";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(420, 178);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 6;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Label_ProjectDescription
            // 
            this.Label_ProjectDescription.AutoSize = true;
            this.Label_ProjectDescription.Location = new System.Drawing.Point(15, 95);
            this.Label_ProjectDescription.Name = "Label_ProjectDescription";
            this.Label_ProjectDescription.Size = new System.Drawing.Size(41, 12);
            this.Label_ProjectDescription.TabIndex = 7;
            this.Label_ProjectDescription.Text = "描述：";
            // 
            // Button_OpenFileDialog
            // 
            this.Button_OpenFileDialog.Location = new System.Drawing.Point(420, 56);
            this.Button_OpenFileDialog.Name = "Button_OpenFileDialog";
            this.Button_OpenFileDialog.Size = new System.Drawing.Size(75, 23);
            this.Button_OpenFileDialog.TabIndex = 8;
            this.Button_OpenFileDialog.Text = "浏览";
            this.Button_OpenFileDialog.UseVisualStyleBackColor = true;
            this.Button_OpenFileDialog.Click += new System.EventHandler(this.Button_OpenFileDialog_Click);
            // 
            // NewProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 215);
            this.Controls.Add(this.Button_OpenFileDialog);
            this.Controls.Add(this.Label_ProjectDescription);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.RichTextBox_ProjectDescription);
            this.Controls.Add(this.TextBox_ProjectLocation);
            this.Controls.Add(this.TextBox_ProjectName);
            this.Controls.Add(this.Label_ProjectLocation);
            this.Controls.Add(this.Label_ProjectName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NewProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建工程";
            this.Load += new System.EventHandler(this.NewProject_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_ProjectName;
        private System.Windows.Forms.Label Label_ProjectLocation;
        private System.Windows.Forms.TextBox TextBox_ProjectName;
        private System.Windows.Forms.TextBox TextBox_ProjectLocation;
        private System.Windows.Forms.RichTextBox RichTextBox_ProjectDescription;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Label Label_ProjectDescription;
        private System.Windows.Forms.Button Button_OpenFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}