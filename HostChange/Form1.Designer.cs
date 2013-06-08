namespace HostChange
{
    partial class HostChange
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Update = new System.Windows.Forms.Button();
            this.Reset = new System.Windows.Forms.Button();
            this.Beijing = new System.Windows.Forms.Button();
            this.US = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Update
            // 
            this.Update.Location = new System.Drawing.Point(38, 13);
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(75, 23);
            this.Update.TabIndex = 0;
            this.Update.Text = "Update";
            this.Update.UseVisualStyleBackColor = true;
            this.Update.Click += new System.EventHandler(this.Update_Click);
            // 
            // Reset
            // 
            this.Reset.Location = new System.Drawing.Point(38, 43);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(75, 23);
            this.Reset.TabIndex = 1;
            this.Reset.Text = "Reset";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // Beijing
            // 
            this.Beijing.Location = new System.Drawing.Point(119, 13);
            this.Beijing.Name = "Beijing";
            this.Beijing.Size = new System.Drawing.Size(75, 23);
            this.Beijing.TabIndex = 2;
            this.Beijing.Text = "Beijing";
            this.Beijing.UseVisualStyleBackColor = true;
            this.Beijing.Click += new System.EventHandler(this.Beijing_Click);
            // 
            // US
            // 
            this.US.Location = new System.Drawing.Point(119, 43);
            this.US.Name = "US";
            this.US.Size = new System.Drawing.Size(75, 23);
            this.US.TabIndex = 3;
            this.US.Text = "US";
            this.US.UseVisualStyleBackColor = true;
            this.US.Click += new System.EventHandler(this.US_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(1, 69);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(53, 12);
            this.label.TabIndex = 4;
            this.label.Text = "Welcome!";
            // 
            // HostChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 83);
            this.Controls.Add(this.label);
            this.Controls.Add(this.US);
            this.Controls.Add(this.Beijing);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.Update);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(244, 122);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(244, 122);
            this.Name = "HostChange";
            this.Text = "HostChange";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Update;
        private System.Windows.Forms.Button Reset;
        private System.Windows.Forms.Button Beijing;
        private System.Windows.Forms.Button US;
        private System.Windows.Forms.Label label;
    }
}

