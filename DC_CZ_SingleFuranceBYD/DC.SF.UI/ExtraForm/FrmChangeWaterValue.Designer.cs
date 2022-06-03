namespace DC.SF.UI
{
    partial class FrmChangeWaterValue
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
            this.sbtnOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtCellBValue = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtCellAValue = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellBValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellAValue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sbtnOK
            // 
            this.sbtnOK.Location = new System.Drawing.Point(167, 185);
            this.sbtnOK.Name = "sbtnOK";
            this.sbtnOK.Size = new System.Drawing.Size(112, 33);
            this.sbtnOK.TabIndex = 9;
            this.sbtnOK.Text = "确 认";
            this.sbtnOK.Click += new System.EventHandler(this.sbtnOK_Click);
            // 
            // txtCellBValue
            // 
            this.txtCellBValue.Location = new System.Drawing.Point(193, 120);
            this.txtCellBValue.Name = "txtCellBValue";
            this.txtCellBValue.Size = new System.Drawing.Size(196, 24);
            this.txtCellBValue.TabIndex = 8;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(42, 123);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(144, 18);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "B料假电芯水含量值：";
            // 
            // txtCellAValue
            // 
            this.txtCellAValue.Location = new System.Drawing.Point(193, 55);
            this.txtCellAValue.Name = "txtCellAValue";
            this.txtCellAValue.Size = new System.Drawing.Size(196, 24);
            this.txtCellAValue.TabIndex = 6;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(42, 58);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(144, 18);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "A料假电芯水含量值：";
            // 
            // FrmChangeWaterValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 258);
            this.Controls.Add(this.sbtnOK);
            this.Controls.Add(this.txtCellBValue);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtCellAValue);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChangeWaterValue";
            this.Text = " 假电芯水含量值修改";
            this.Load += new System.EventHandler(this.FrmChangeWaterValue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCellBValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellAValue.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbtnOK;
        private DevExpress.XtraEditors.TextEdit txtCellBValue;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtCellAValue;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}