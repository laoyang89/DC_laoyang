namespace DC.SF.UI
{
    partial class FrmEnterFalseCellCode
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCellA = new DevExpress.XtraEditors.TextEdit();
            this.txtCellB = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.sbtnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtCarId = new DevExpress.XtraEditors.TextEdit();
            this.lblMsg = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(69, 142);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(114, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "A料假电芯条码：";
            // 
            // txtCellA
            // 
            this.txtCellA.Location = new System.Drawing.Point(189, 139);
            this.txtCellA.Name = "txtCellA";
            this.txtCellA.Size = new System.Drawing.Size(196, 24);
            this.txtCellA.TabIndex = 1;
            // 
            // txtCellB
            // 
            this.txtCellB.Location = new System.Drawing.Point(189, 204);
            this.txtCellB.Name = "txtCellB";
            this.txtCellB.Size = new System.Drawing.Size(196, 24);
            this.txtCellB.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(69, 207);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(114, 18);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "B料假电芯条码：";
            // 
            // sbtnOK
            // 
            this.sbtnOK.Location = new System.Drawing.Point(159, 264);
            this.sbtnOK.Name = "sbtnOK";
            this.sbtnOK.Size = new System.Drawing.Size(112, 33);
            this.sbtnOK.TabIndex = 4;
            this.sbtnOK.Text = "确 认";
            this.sbtnOK.Click += new System.EventHandler(this.sbtnOK_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(123, 84);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 18);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "小车号：";
            // 
            // txtCarId
            // 
            this.txtCarId.Location = new System.Drawing.Point(189, 81);
            this.txtCarId.Name = "txtCarId";
            this.txtCarId.Size = new System.Drawing.Size(196, 24);
            this.txtCarId.TabIndex = 1;
            this.txtCarId.MouseLeave += new System.EventHandler(this.txtCarId_MouseLeave);
            // 
            // lblMsg
            // 
            this.lblMsg.Appearance.Options.UseTextOptions = true;
            this.lblMsg.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblMsg.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblMsg.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMsg.Location = new System.Drawing.Point(12, 12);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(409, 40);
            this.lblMsg.TabIndex = 5;
            // 
            // FrmEnterFalseCellCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 319);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.sbtnOK);
            this.Controls.Add(this.txtCellB);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtCarId);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtCellA);
            this.Controls.Add(this.labelControl1);
            this.MinimizeBox = false;
            this.Name = "FrmEnterFalseCellCode";
            this.Text = "假电芯条码输入";
            this.Load += new System.EventHandler(this.FrmEnterFalseCellCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCellA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarId.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCellA;
        private DevExpress.XtraEditors.TextEdit txtCellB;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton sbtnOK;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtCarId;
        private DevExpress.XtraEditors.LabelControl lblMsg;
    }
}