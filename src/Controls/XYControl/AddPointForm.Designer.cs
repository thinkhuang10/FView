namespace XYControl
{
    partial class AddPointForm
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
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.PointColor = new System.Windows.Forms.Label();
            this.YAxisVar = new System.Windows.Forms.TextBox();
            this.XAxisVar = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.YAxisScope = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.XAxisScope = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SelectedXAxisVarButton = new System.Windows.Forms.Button();
            this.SelectedYAxisVarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(322, 148);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 16;
            this.CancelButton.Text = "取消";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(241, 148);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 17;
            this.OKButton.Text = "确定";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // PointColor
            // 
            this.PointColor.BackColor = System.Drawing.Color.Gray;
            this.PointColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PointColor.Location = new System.Drawing.Point(70, 68);
            this.PointColor.Name = "PointColor";
            this.PointColor.Size = new System.Drawing.Size(56, 20);
            this.PointColor.TabIndex = 15;
            this.PointColor.Text = "     ";
            this.PointColor.Click += new System.EventHandler(this.PointColor_Click);
            // 
            // YAxisVar
            // 
            this.YAxisVar.Location = new System.Drawing.Point(225, 106);
            this.YAxisVar.Name = "YAxisVar";
            this.YAxisVar.Size = new System.Drawing.Size(130, 21);
            this.YAxisVar.TabIndex = 11;
            // 
            // XAxisVar
            // 
            this.XAxisVar.Location = new System.Drawing.Point(30, 106);
            this.XAxisVar.Name = "XAxisVar";
            this.XAxisVar.Size = new System.Drawing.Size(130, 21);
            this.XAxisVar.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(208, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "Y";
            // 
            // YAxisScope
            // 
            this.YAxisScope.Location = new System.Drawing.Point(70, 40);
            this.YAxisScope.Name = "YAxisScope";
            this.YAxisScope.ReadOnly = true;
            this.YAxisScope.Size = new System.Drawing.Size(333, 21);
            this.YAxisScope.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "颜色";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "Y范围";
            // 
            // XAxisScope
            // 
            this.XAxisScope.Location = new System.Drawing.Point(70, 12);
            this.XAxisScope.Name = "XAxisScope";
            this.XAxisScope.ReadOnly = true;
            this.XAxisScope.Size = new System.Drawing.Size(333, 21);
            this.XAxisScope.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "X范围";
            // 
            // SelectedXAxisVarButton
            // 
            this.SelectedXAxisVarButton.Location = new System.Drawing.Point(166, 105);
            this.SelectedXAxisVarButton.Name = "SelectedXAxisVarButton";
            this.SelectedXAxisVarButton.Size = new System.Drawing.Size(33, 23);
            this.SelectedXAxisVarButton.TabIndex = 19;
            this.SelectedXAxisVarButton.Text = "...";
            this.SelectedXAxisVarButton.UseVisualStyleBackColor = true;
            this.SelectedXAxisVarButton.Click += new System.EventHandler(this.SelectedXAxisVarButton_Click);
            // 
            // SelectedYAxisVarButton
            // 
            this.SelectedYAxisVarButton.Location = new System.Drawing.Point(361, 105);
            this.SelectedYAxisVarButton.Name = "SelectedYAxisVarButton";
            this.SelectedYAxisVarButton.Size = new System.Drawing.Size(33, 23);
            this.SelectedYAxisVarButton.TabIndex = 20;
            this.SelectedYAxisVarButton.Text = "...";
            this.SelectedYAxisVarButton.UseVisualStyleBackColor = true;
            this.SelectedYAxisVarButton.Click += new System.EventHandler(this.SelectedYAxisVarButton_Click);
            // 
            // AddPointForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 189);
            this.Controls.Add(this.SelectedYAxisVarButton);
            this.Controls.Add(this.SelectedXAxisVarButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.PointColor);
            this.Controls.Add(this.YAxisVar);
            this.Controls.Add(this.XAxisVar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.YAxisScope);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.XAxisScope);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddPointForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加点";
            this.Load += new System.EventHandler(this.AddPointForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label PointColor;
        private System.Windows.Forms.TextBox YAxisVar;
        private System.Windows.Forms.TextBox XAxisVar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox YAxisScope;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox XAxisScope;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SelectedXAxisVarButton;
        private System.Windows.Forms.Button SelectedYAxisVarButton;
    }
}