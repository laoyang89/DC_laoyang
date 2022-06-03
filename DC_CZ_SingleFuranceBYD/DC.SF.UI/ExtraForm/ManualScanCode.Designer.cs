namespace DC.SF.UI
{
    partial class ManualScanCode
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClearError = new System.Windows.Forms.Button();
            this.btnStartScan = new System.Windows.Forms.Button();
            this.txt_Position = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_StartLayer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbMsg = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lbScanResult = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txt_Encoding = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_QrCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 158);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1093, 299);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridView1_ShowingEditor);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClearError);
            this.panelControl1.Controls.Add(this.btnStartScan);
            this.panelControl1.Controls.Add(this.txt_Position);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.txt_StartLayer);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.lbMsg);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.button1);
            this.panelControl1.Controls.Add(this.lbScanResult);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.txt_Encoding);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.txt_QrCode);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1093, 158);
            this.panelControl1.TabIndex = 1;
            // 
            // btnClearError
            // 
            this.btnClearError.BackColor = System.Drawing.Color.OrangeRed;
            this.btnClearError.FlatAppearance.BorderSize = 0;
            this.btnClearError.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearError.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnClearError.ForeColor = System.Drawing.Color.White;
            this.btnClearError.Location = new System.Drawing.Point(258, 12);
            this.btnClearError.Name = "btnClearError";
            this.btnClearError.Size = new System.Drawing.Size(110, 32);
            this.btnClearError.TabIndex = 18;
            this.btnClearError.Text = "清除错误";
            this.btnClearError.UseVisualStyleBackColor = false;
            this.btnClearError.Click += new System.EventHandler(this.btnClearError_Click);
            // 
            // btnStartScan
            // 
            this.btnStartScan.BackColor = System.Drawing.Color.Green;
            this.btnStartScan.FlatAppearance.BorderSize = 0;
            this.btnStartScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartScan.ForeColor = System.Drawing.Color.White;
            this.btnStartScan.Location = new System.Drawing.Point(140, 12);
            this.btnStartScan.Name = "btnStartScan";
            this.btnStartScan.Size = new System.Drawing.Size(108, 32);
            this.btnStartScan.TabIndex = 17;
            this.btnStartScan.Text = "开启扫码";
            this.btnStartScan.UseVisualStyleBackColor = false;
            this.btnStartScan.Click += new System.EventHandler(this.btnStartScan_Click);
            // 
            // txt_Position
            // 
            this.txt_Position.Location = new System.Drawing.Point(476, 94);
            this.txt_Position.Name = "txt_Position";
            this.txt_Position.Size = new System.Drawing.Size(71, 26);
            this.txt_Position.TabIndex = 16;
            this.txt_Position.Text = "1";
            this.txt_Position.TextChanged += new System.EventHandler(this.txt_Position_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(352, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 18);
            this.label6.TabIndex = 15;
            this.label6.Text = "起始所在层位置:";
            // 
            // txt_StartLayer
            // 
            this.txt_StartLayer.Location = new System.Drawing.Point(275, 94);
            this.txt_StartLayer.Name = "txt_StartLayer";
            this.txt_StartLayer.Size = new System.Drawing.Size(71, 26);
            this.txt_StartLayer.TabIndex = 14;
            this.txt_StartLayer.Text = "1";
            this.txt_StartLayer.TextChanged += new System.EventHandler(this.txt_StartLayer_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(196, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 18);
            this.label5.TabIndex = 13;
            this.label5.Text = "起始层数:";
            // 
            // lbMsg
            // 
            this.lbMsg.AutoSize = true;
            this.lbMsg.Location = new System.Drawing.Point(92, 129);
            this.lbMsg.Name = "lbMsg";
            this.lbMsg.Size = new System.Drawing.Size(16, 18);
            this.lbMsg.TabIndex = 12;
            this.lbMsg.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "信息:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 32);
            this.button1.TabIndex = 10;
            this.button1.Text = "测";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbScanResult
            // 
            this.lbScanResult.ForeColor = System.Drawing.Color.Black;
            this.lbScanResult.Location = new System.Drawing.Point(570, 56);
            this.lbScanResult.Name = "lbScanResult";
            this.lbScanResult.Size = new System.Drawing.Size(485, 91);
            this.lbScanResult.TabIndex = 9;
            this.lbScanResult.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(461, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "当前扫描结果:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(46, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 32);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txt_Encoding
            // 
            this.txt_Encoding.Location = new System.Drawing.Point(92, 94);
            this.txt_Encoding.Name = "txt_Encoding";
            this.txt_Encoding.Size = new System.Drawing.Size(98, 26);
            this.txt_Encoding.TabIndex = 5;
            this.txt_Encoding.Text = "1";
            this.txt_Encoding.TextChanged += new System.EventHandler(this.txt_Encoding_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "编码:";
            // 
            // txt_QrCode
            // 
            this.txt_QrCode.Location = new System.Drawing.Point(93, 53);
            this.txt_QrCode.Name = "txt_QrCode";
            this.txt_QrCode.Size = new System.Drawing.Size(320, 26);
            this.txt_QrCode.TabIndex = 1;
            this.txt_QrCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_QrCode_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "条码:";
            // 
            // ManualScanCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 457);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.MinimizeBox = false;
            this.Name = "ManualScanCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "手动添加条码";
            this.Load += new System.EventHandler(this.ManualScanCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.TextBox txt_Encoding;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_QrCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lbScanResult;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbMsg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Position;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_StartLayer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnStartScan;
        private System.Windows.Forms.Button btnClearError;
    }
}