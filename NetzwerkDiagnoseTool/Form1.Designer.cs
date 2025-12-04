namespace NetzwerkDiagnoseTool
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.btnPing = new System.Windows.Forms.Button();
            this.btnSysInfo = new System.Windows.Forms.Button();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Location = new System.Drawing.Point(301, 12);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(219, 49);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.lblTitle.Location = new System.Drawing.Point(12, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(188, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "NETWORK TOOL";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(353, 97);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(122, 20);
            this.txtHost.TabIndex = 1;
            // 
            // btnPing
            // 
            this.btnPing.Location = new System.Drawing.Point(445, 144);
            this.btnPing.Name = "btnPing";
            this.btnPing.Size = new System.Drawing.Size(75, 23);
            this.btnPing.TabIndex = 2;
            this.btnPing.Text = "PING";
            this.btnPing.UseVisualStyleBackColor = true;
            this.btnPing.Click += new System.EventHandler(this.btnPing_Click);
            // 
            // btnSysInfo
            // 
            this.btnSysInfo.Location = new System.Drawing.Point(301, 144);
            this.btnSysInfo.Name = "btnSysInfo";
            this.btnSysInfo.Size = new System.Drawing.Size(75, 23);
            this.btnSysInfo.TabIndex = 3;
            this.btnSysInfo.Text = "SYS INFO";
            this.btnSysInfo.UseVisualStyleBackColor = true;
            this.btnSysInfo.Click += new System.EventHandler(this.btnSysInfo_Click);
            // 
            // rtbOutput
            // 
            this.rtbOutput.Location = new System.Drawing.Point(169, 216);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(554, 96);
            this.rtbOutput.TabIndex = 4;
            this.rtbOutput.Text = "";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(813, 341);
            this.Controls.Add(this.rtbOutput);
            this.Controls.Add(this.btnSysInfo);
            this.Controls.Add(this.btnPing);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.pnlHeader);
            this.Name = "Form1";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Button btnPing;
        private System.Windows.Forms.Button btnSysInfo;
        private System.Windows.Forms.RichTextBox rtbOutput;
    }
}

