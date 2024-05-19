namespace XYControl
{
    partial class AddLineForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.XAxisScope = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.YAxisScope = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LineColor = new System.Windows.Forms.Label();
            this.AddButton = new System.Windows.Forms.Button();
            this.ModifyButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.LineGridView = new System.Windows.Forms.DataGridView();
            this.XVar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YVar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OkButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.SelectedYAxisVarButton = new System.Windows.Forms.Button();
            this.SelectedXAxisVarButton = new System.Windows.Forms.Button();
            this.YAxisVar = new System.Windows.Forms.TextBox();
            this.XAxisVar = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LineGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "X范围";
            // 
            // XAxisScope
            // 
            this.XAxisScope.Location = new System.Drawing.Point(69, 13);
            this.XAxisScope.Name = "XAxisScope";
            this.XAxisScope.ReadOnly = true;
            this.XAxisScope.Size = new System.Drawing.Size(333, 21);
            this.XAxisScope.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Y范围";
            // 
            // YAxisScope
            // 
            this.YAxisScope.Location = new System.Drawing.Point(69, 41);
            this.YAxisScope.Name = "YAxisScope";
            this.YAxisScope.ReadOnly = true;
            this.YAxisScope.Size = new System.Drawing.Size(333, 21);
            this.YAxisScope.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "颜色";
            // 
            // LineColor
            // 
            this.LineColor.BackColor = System.Drawing.Color.Gray;
            this.LineColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LineColor.Location = new System.Drawing.Point(69, 69);
            this.LineColor.Name = "LineColor";
            this.LineColor.Size = new System.Drawing.Size(56, 20);
            this.LineColor.TabIndex = 4;
            this.LineColor.Text = "     ";
            this.LineColor.Click += new System.EventHandler(this.LineColor_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(14, 149);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 5;
            this.AddButton.Text = "添加";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ModifyButton
            // 
            this.ModifyButton.Location = new System.Drawing.Point(95, 149);
            this.ModifyButton.Name = "ModifyButton";
            this.ModifyButton.Size = new System.Drawing.Size(75, 23);
            this.ModifyButton.TabIndex = 5;
            this.ModifyButton.Text = "修改";
            this.ModifyButton.UseVisualStyleBackColor = true;
            this.ModifyButton.Click += new System.EventHandler(this.ModifyButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(339, 149);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 5;
            this.DeleteButton.Text = "删除";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // LineGridView
            // 
            this.LineGridView.AllowUserToAddRows = false;
            this.LineGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LineGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.LineGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LineGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.XVar,
            this.YVar});
            this.LineGridView.Location = new System.Drawing.Point(14, 191);
            this.LineGridView.Name = "LineGridView";
            this.LineGridView.RowHeadersVisible = false;
            this.LineGridView.RowHeadersWidth = 62;
            this.LineGridView.RowTemplate.Height = 23;
            this.LineGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.LineGridView.Size = new System.Drawing.Size(400, 225);
            this.LineGridView.TabIndex = 6;
            this.LineGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.LineGridView_CellClick);
            // 
            // XVar
            // 
            this.XVar.HeaderText = "X";
            this.XVar.MinimumWidth = 8;
            this.XVar.Name = "XVar";
            this.XVar.ReadOnly = true;
            this.XVar.Width = 198;
            // 
            // YVar
            // 
            this.YVar.HeaderText = "Y";
            this.YVar.MinimumWidth = 8;
            this.YVar.Name = "YVar";
            this.YVar.ReadOnly = true;
            this.YVar.Width = 198;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(258, 422);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 5;
            this.OkButton.Text = "确定";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(339, 422);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 5;
            this.ExitButton.Text = "取消";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // SelectedYAxisVarButton
            // 
            this.SelectedYAxisVarButton.Location = new System.Drawing.Point(371, 106);
            this.SelectedYAxisVarButton.Name = "SelectedYAxisVarButton";
            this.SelectedYAxisVarButton.Size = new System.Drawing.Size(33, 23);
            this.SelectedYAxisVarButton.TabIndex = 26;
            this.SelectedYAxisVarButton.Text = "...";
            this.SelectedYAxisVarButton.UseVisualStyleBackColor = true;
            this.SelectedYAxisVarButton.Click += new System.EventHandler(this.SelectedYAxisVarButton_Click);
            // 
            // SelectedXAxisVarButton
            // 
            this.SelectedXAxisVarButton.Location = new System.Drawing.Point(176, 106);
            this.SelectedXAxisVarButton.Name = "SelectedXAxisVarButton";
            this.SelectedXAxisVarButton.Size = new System.Drawing.Size(33, 23);
            this.SelectedXAxisVarButton.TabIndex = 25;
            this.SelectedXAxisVarButton.Text = "...";
            this.SelectedXAxisVarButton.UseVisualStyleBackColor = true;
            this.SelectedXAxisVarButton.Click += new System.EventHandler(this.SelectedXAxisVarButton_Click);
            // 
            // YAxisVar
            // 
            this.YAxisVar.Location = new System.Drawing.Point(235, 107);
            this.YAxisVar.Name = "YAxisVar";
            this.YAxisVar.Size = new System.Drawing.Size(130, 21);
            this.YAxisVar.TabIndex = 23;
            // 
            // XAxisVar
            // 
            this.XAxisVar.Location = new System.Drawing.Point(40, 107);
            this.XAxisVar.Name = "XAxisVar";
            this.XAxisVar.Size = new System.Drawing.Size(130, 21);
            this.XAxisVar.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(218, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "X";
            // 
            // AddLineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 450);
            this.Controls.Add(this.SelectedYAxisVarButton);
            this.Controls.Add(this.SelectedXAxisVarButton);
            this.Controls.Add(this.YAxisVar);
            this.Controls.Add(this.XAxisVar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LineGridView);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.ModifyButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.LineColor);
            this.Controls.Add(this.YAxisScope);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.XAxisScope);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddLineForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加曲线";
            this.Load += new System.EventHandler(this.AddLineForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LineGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox XAxisScope;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox YAxisScope;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LineColor;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button ModifyButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.DataGridView LineGridView;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button SelectedYAxisVarButton;
        private System.Windows.Forms.Button SelectedXAxisVarButton;
        private System.Windows.Forms.TextBox YAxisVar;
        private System.Windows.Forms.TextBox XAxisVar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn XVar;
        private System.Windows.Forms.DataGridViewTextBoxColumn YVar;
    }
}