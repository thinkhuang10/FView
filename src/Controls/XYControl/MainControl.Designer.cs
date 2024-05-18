﻿namespace XYControl
{
    partial class MainControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.xyChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xyChart)).BeginInit();
            this.SuspendLayout();
            // 
            // xyChart
            // 
            this.xyChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xyChart.Location = new System.Drawing.Point(0, 0);
            this.xyChart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.xyChart.Name = "xyChart";
            series1.Name = "Series";
            this.xyChart.Series.Add(series1);
            this.xyChart.Size = new System.Drawing.Size(400, 360);
            this.xyChart.TabIndex = 0;
            this.xyChart.Text = "Chart";
            this.xyChart.DoubleClick += new System.EventHandler(this.Chart_DoubleClick);
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xyChart);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(400, 360);
            this.Load += new System.EventHandler(this.UserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xyChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart xyChart;
        private System.Windows.Forms.Timer timer;
    }
}
