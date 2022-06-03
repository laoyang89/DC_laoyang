namespace DC.SF.UI
{
    partial class FrmOperateRecord
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
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tb_ID = new System.Windows.Forms.TextBox();
            this.cbb_OPText = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbb_UserName = new System.Windows.Forms.ComboBox();
            this.btn_Query = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_End = new System.Windows.Forms.DateTimePicker();
            this.dtp_Start = new System.Windows.Forms.DateTimePicker();
            this.dgv_OperateRecord = new System.Windows.Forms.DataGridView();
            this.pagerControl1 = new DC.SF.UI.PagerControl();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_OperateRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("楷体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1387, 71);
            this.label2.TabIndex = 7;
            this.label2.Text = "操 作 记 录";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tb_ID);
            this.panel2.Controls.Add(this.cbb_OPText);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cbb_UserName);
            this.panel2.Controls.Add(this.btn_Query);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dtp_End);
            this.panel2.Controls.Add(this.dtp_Start);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1389, 136);
            this.panel2.TabIndex = 14;
            // 
            // tb_ID
            // 
            this.tb_ID.Location = new System.Drawing.Point(917, 90);
            this.tb_ID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tb_ID.Name = "tb_ID";
            this.tb_ID.Size = new System.Drawing.Size(148, 26);
            this.tb_ID.TabIndex = 19;
            // 
            // cbb_OPText
            // 
            this.cbb_OPText.FormattingEnabled = true;
            this.cbb_OPText.Location = new System.Drawing.Point(1143, 89);
            this.cbb_OPText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbb_OPText.Name = "cbb_OPText";
            this.cbb_OPText.Size = new System.Drawing.Size(138, 26);
            this.cbb_OPText.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1072, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 18);
            this.label6.TabIndex = 17;
            this.label6.Text = "操作内容:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(870, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 18);
            this.label5.TabIndex = 15;
            this.label5.Text = "卡号:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(675, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 18);
            this.label4.TabIndex = 14;
            this.label4.Text = "用户名:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // cbb_UserName
            // 
            this.cbb_UserName.FormattingEnabled = true;
            this.cbb_UserName.Location = new System.Drawing.Point(729, 89);
            this.cbb_UserName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbb_UserName.Name = "cbb_UserName";
            this.cbb_UserName.Size = new System.Drawing.Size(138, 26);
            this.cbb_UserName.TabIndex = 13;
            // 
            // btn_Query
            // 
            this.btn_Query.Location = new System.Drawing.Point(1296, 89);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(87, 28);
            this.btn_Query.TabIndex = 12;
            this.btn_Query.Text = "查 询";
            this.btn_Query.UseVisualStyleBackColor = true;
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(353, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "结束时间：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "开始时间：";
            // 
            // dtp_End
            // 
            this.dtp_End.CustomFormat = "yyyy-MM-dd";
            this.dtp_End.Location = new System.Drawing.Point(437, 89);
            this.dtp_End.Name = "dtp_End";
            this.dtp_End.Size = new System.Drawing.Size(233, 26);
            this.dtp_End.TabIndex = 9;
            // 
            // dtp_Start
            // 
            this.dtp_Start.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtp_Start.Location = new System.Drawing.Point(107, 89);
            this.dtp_Start.Name = "dtp_Start";
            this.dtp_Start.Size = new System.Drawing.Size(233, 26);
            this.dtp_Start.TabIndex = 8;
            // 
            // dgv_OperateRecord
            // 
            this.dgv_OperateRecord.AllowUserToAddRows = false;
            this.dgv_OperateRecord.AllowUserToDeleteRows = false;
            this.dgv_OperateRecord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_OperateRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_OperateRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_OperateRecord.Location = new System.Drawing.Point(0, 136);
            this.dgv_OperateRecord.MultiSelect = false;
            this.dgv_OperateRecord.Name = "dgv_OperateRecord";
            this.dgv_OperateRecord.ReadOnly = true;
            this.dgv_OperateRecord.RowTemplate.Height = 27;
            this.dgv_OperateRecord.Size = new System.Drawing.Size(1389, 606);
            this.dgv_OperateRecord.TabIndex = 16;
            // 
            // pagerControl1
            // 
            this.pagerControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagerControl1.JumpText = "Go";
            this.pagerControl1.Location = new System.Drawing.Point(0, 742);
            this.pagerControl1.Name = "pagerControl1";
            this.pagerControl1.PageIndex = 1;
            this.pagerControl1.PageSize = 30;
            this.pagerControl1.RecordCount = 0;
            this.pagerControl1.Size = new System.Drawing.Size(1389, 45);
            this.pagerControl1.TabIndex = 15;
            // 
            // FrmOperateRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1389, 787);
            this.Controls.Add(this.dgv_OperateRecord);
            this.Controls.Add(this.pagerControl1);
            this.Controls.Add(this.panel2);
            this.MinimizeBox = false;
            this.Name = "FrmOperateRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "操作日志";
            this.Load += new System.EventHandler(this.FrmOperateRecord_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_OperateRecord)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Query;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_End;
        private System.Windows.Forms.DateTimePicker dtp_Start;
        private PagerControl pagerControl1;
        private System.Windows.Forms.DataGridView dgv_OperateRecord;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbb_UserName;
        private System.Windows.Forms.TextBox tb_ID;
        private System.Windows.Forms.ComboBox cbb_OPText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}