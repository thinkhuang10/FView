﻿namespace FControl.SetFroms
{
    partial class XYSetCurveForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtXScope = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtYScope = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.x = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "X范围";
            // 
            // txtXScope
            // 
            this.txtXScope.Location = new System.Drawing.Point(81, 22);
            this.txtXScope.Name = "txtXScope";
            this.txtXScope.ReadOnly = true;
            this.txtXScope.Size = new System.Drawing.Size(333, 21);
            this.txtXScope.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Y范围";
            // 
            // txtYScope
            // 
            this.txtYScope.Location = new System.Drawing.Point(81, 49);
            this.txtYScope.Name = "txtYScope";
            this.txtYScope.ReadOnly = true;
            this.txtYScope.Size = new System.Drawing.Size(333, 21);
            this.txtYScope.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "颜色";
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.BackColor = System.Drawing.Color.Gray;
            this.lblColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblColor.Location = new System.Drawing.Point(81, 81);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(37, 14);
            this.lblColor.TabIndex = 4;
            this.lblColor.Text = "     ";
            this.lblColor.Click += new System.EventHandler(this.lblColor_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "X";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(29, 108);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(162, 21);
            this.txtX.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(235, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "Y";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(252, 108);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(162, 21);
            this.txtY.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(14, 149);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpd
            // 
            this.btnUpd.Location = new System.Drawing.Point(95, 149);
            this.btnUpd.Name = "btnUpd";
            this.btnUpd.Size = new System.Drawing.Size(75, 23);
            this.btnUpd.TabIndex = 5;
            this.btnUpd.Text = "修改";
            this.btnUpd.UseVisualStyleBackColor = true;
            this.btnUpd.Click += new System.EventHandler(this.btnUpd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(339, 149);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 5;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.x,
            this.y});
            this.dataGridView1.Location = new System.Drawing.Point(14, 191);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(400, 225);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // x
            // 
            this.x.HeaderText = "X";
            this.x.Name = "x";
            this.x.ReadOnly = true;
            this.x.Width = 150;
            // 
            // y
            // 
            this.y.HeaderText = "Y";
            this.y.Name = "y";
            this.y.ReadOnly = true;
            this.y.Width = 150;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(258, 422);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(339, 422);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // XYSetCurveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnUpd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtYScope);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtXScope);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "XYSetCurveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加曲线";
            this.Load += new System.EventHandler(this.XYSetCurveForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtXScope;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtYScope;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn x;
        private System.Windows.Forms.DataGridViewTextBoxColumn y;
    }
}