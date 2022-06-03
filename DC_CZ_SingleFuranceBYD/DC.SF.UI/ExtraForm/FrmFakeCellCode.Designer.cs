namespace DC.SF.UI.ExtraForm
{
    partial class FrmFakeCellCode
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
            this.txtFakeCellCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "假电芯条码：";
            // 
            // txtFakeCellCode
            // 
            this.txtFakeCellCode.Location = new System.Drawing.Point(149, 79);
            this.txtFakeCellCode.Name = "txtFakeCellCode";
            this.txtFakeCellCode.Size = new System.Drawing.Size(171, 25);
            this.txtFakeCellCode.TabIndex = 1;
            this.txtFakeCellCode.TextChanged += new System.EventHandler(this.txtFakeCellCode_TextChanged);
            // 
            // FrmFakeCellCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 260);
            this.Controls.Add(this.txtFakeCellCode);
            this.Controls.Add(this.label1);
            this.MinimizeBox = false;
            this.Name = "FrmFakeCellCode";
            this.Text = "扫码取样获取假电芯";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFakeCellCode;
    }
}