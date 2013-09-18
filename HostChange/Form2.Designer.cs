namespace HostChange
{
    partial class Form2
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
            this.showlable = new System.Windows.Forms.Label();
            this.Smartlink = new System.Windows.Forms.LinkLabel();
            this.thanksto = new System.Windows.Forms.Label();
            this.imoutolink1 = new System.Windows.Forms.LinkLabel();
            this.ninelink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // showlable
            // 
            this.showlable.AutoSize = true;
            this.showlable.Location = new System.Drawing.Point(12, 11);
            this.showlable.Name = "showlable";
            this.showlable.Size = new System.Drawing.Size(0, 12);
            this.showlable.TabIndex = 1;
            // 
            // Smartlink
            // 
            this.Smartlink.AutoSize = true;
            this.Smartlink.Location = new System.Drawing.Point(12, 100);
            this.Smartlink.Name = "Smartlink";
            this.Smartlink.Size = new System.Drawing.Size(59, 12);
            this.Smartlink.TabIndex = 2;
            this.Smartlink.TabStop = true;
            this.Smartlink.Text = "SmartHost";
            this.Smartlink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Smartlink_LinkClicked);
            // 
            // thanksto
            // 
            this.thanksto.AutoSize = true;
            this.thanksto.Location = new System.Drawing.Point(12, 84);
            this.thanksto.Name = "thanksto";
            this.thanksto.Size = new System.Drawing.Size(65, 12);
            this.thanksto.TabIndex = 3;
            this.thanksto.Text = "Thanks to:";
            // 
            // imoutolink1
            // 
            this.imoutolink1.AutoSize = true;
            this.imoutolink1.Location = new System.Drawing.Point(77, 100);
            this.imoutolink1.Name = "imoutolink1";
            this.imoutolink1.Size = new System.Drawing.Size(71, 12);
            this.imoutolink1.TabIndex = 4;
            this.imoutolink1.TabStop = true;
            this.imoutolink1.Text = "imouto.host";
            this.imoutolink1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.imoutolink1_LinkClicked);
            // 
            // ninelink
            // 
            this.ninelink.AutoSize = true;
            this.ninelink.Location = new System.Drawing.Point(154, 100);
            this.ninelink.Name = "ninelink";
            this.ninelink.Size = new System.Drawing.Size(35, 12);
            this.ninelink.TabIndex = 5;
            this.ninelink.TabStop = true;
            this.ninelink.Text = "9host";
            this.ninelink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ninelink_LinkClicked);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 121);
            this.Controls.Add(this.ninelink);
            this.Controls.Add(this.imoutolink1);
            this.Controls.Add(this.thanksto);
            this.Controls.Add(this.Smartlink);
            this.Controls.Add(this.showlable);
            this.Name = "Form2";
            this.Text = "Version";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label showlable;
        private System.Windows.Forms.LinkLabel Smartlink;
        private System.Windows.Forms.Label thanksto;
        private System.Windows.Forms.LinkLabel imoutolink1;
        private System.Windows.Forms.LinkLabel ninelink;
    }
}