namespace DC.SF.UI
{
    partial class FrmCarStatus
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_StartTime = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveRealData = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnFakeCellChange = new DevExpress.XtraEditors.SimpleButton();
            this.btn_CellDeal = new DevExpress.XtraEditors.SimpleButton();
            this.cbo_Car = new System.Windows.Forms.ComboBox();
            this.btn_ShowTemperatureList = new DevExpress.XtraEditors.SimpleButton();
            this.btn_ShowCellList = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pnl_CarStatus = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_CarStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_StartTime);
            this.panelControl1.Controls.Add(this.btnSaveRealData);
            this.panelControl1.Controls.Add(this.sbtnFakeCellChange);
            this.panelControl1.Controls.Add(this.btn_CellDeal);
            this.panelControl1.Controls.Add(this.cbo_Car);
            this.panelControl1.Controls.Add(this.btn_ShowTemperatureList);
            this.panelControl1.Controls.Add(this.btn_ShowCellList);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1093, 80);
            this.panelControl1.TabIndex = 2;
            // 
            // btn_StartTime
            // 
            this.btn_StartTime.Location = new System.Drawing.Point(263, 17);
            this.btn_StartTime.Name = "btn_StartTime";
            this.btn_StartTime.Size = new System.Drawing.Size(101, 40);
            this.btn_StartTime.TabIndex = 6;
            this.btn_StartTime.Text = "出入炉时间";
            this.btn_StartTime.Click += new System.EventHandler(this.btn_StartTime_Click);
            // 
            // btnSaveRealData
            // 
            this.btnSaveRealData.Location = new System.Drawing.Point(956, 17);
            this.btnSaveRealData.Name = "btnSaveRealData";
            this.btnSaveRealData.Size = new System.Drawing.Size(101, 40);
            this.btnSaveRealData.TabIndex = 5;
            this.btnSaveRealData.Text = "保存实时温度";
            this.btnSaveRealData.Click += new System.EventHandler(this.btnSaveRealData_Click);
            // 
            // sbtnFakeCellChange
            // 
            this.sbtnFakeCellChange.Location = new System.Drawing.Point(817, 17);
            this.sbtnFakeCellChange.Name = "sbtnFakeCellChange";
            this.sbtnFakeCellChange.Size = new System.Drawing.Size(101, 40);
            this.sbtnFakeCellChange.TabIndex = 5;
            this.sbtnFakeCellChange.Text = "假电芯修正";
            this.sbtnFakeCellChange.Click += new System.EventHandler(this.sbtnFakeCellChange_Click);
            // 
            // btn_CellDeal
            // 
            this.btn_CellDeal.Location = new System.Drawing.Point(671, 17);
            this.btn_CellDeal.Name = "btn_CellDeal";
            this.btn_CellDeal.Size = new System.Drawing.Size(101, 40);
            this.btn_CellDeal.TabIndex = 5;
            this.btn_CellDeal.Text = "电池处理";
            this.btn_CellDeal.Click += new System.EventHandler(this.btn_CellDeal_Click);
            // 
            // cbo_Car
            // 
            this.cbo_Car.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Car.FormattingEnabled = true;
            this.cbo_Car.Location = new System.Drawing.Point(104, 25);
            this.cbo_Car.Name = "cbo_Car";
            this.cbo_Car.Size = new System.Drawing.Size(121, 26);
            this.cbo_Car.TabIndex = 4;
            this.cbo_Car.SelectedIndexChanged += new System.EventHandler(this.cbo_Car_SelectedIndexChanged);
            // 
            // btn_ShowTemperatureList
            // 
            this.btn_ShowTemperatureList.Location = new System.Drawing.Point(530, 17);
            this.btn_ShowTemperatureList.Name = "btn_ShowTemperatureList";
            this.btn_ShowTemperatureList.Size = new System.Drawing.Size(101, 40);
            this.btn_ShowTemperatureList.TabIndex = 3;
            this.btn_ShowTemperatureList.Text = "查看温度列表";
            this.btn_ShowTemperatureList.Click += new System.EventHandler(this.btn_ShowTemperatureList_Click);
            // 
            // btn_ShowCellList
            // 
            this.btn_ShowCellList.Location = new System.Drawing.Point(392, 17);
            this.btn_ShowCellList.Name = "btn_ShowCellList";
            this.btn_ShowCellList.Size = new System.Drawing.Size(101, 40);
            this.btn_ShowCellList.TabIndex = 2;
            this.btn_ShowCellList.Text = "查看电池列表";
            this.btn_ShowCellList.Click += new System.EventHandler(this.btn_ShowCellList_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(23, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 18);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "小车编号：";
            // 
            // pnl_CarStatus
            // 
            this.pnl_CarStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_CarStatus.Location = new System.Drawing.Point(0, 80);
            this.pnl_CarStatus.Name = "pnl_CarStatus";
            this.pnl_CarStatus.Size = new System.Drawing.Size(1093, 542);
            this.pnl_CarStatus.TabIndex = 3;
            // 
            // FrmCarStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 622);
            this.Controls.Add(this.pnl_CarStatus);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmCarStatus";
            this.Text = "小车状态";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCarStatus_FormClosed);
            this.Load += new System.EventHandler(this.FrmCarStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_CarStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl pnl_CarStatus;
        private DevExpress.XtraEditors.SimpleButton btn_ShowTemperatureList;
        private DevExpress.XtraEditors.SimpleButton btn_ShowCellList;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox cbo_Car;
        private DevExpress.XtraEditors.SimpleButton btn_CellDeal;
        private DevExpress.XtraEditors.SimpleButton sbtnFakeCellChange;
        private DevExpress.XtraEditors.SimpleButton btnSaveRealData;
        private DevExpress.XtraEditors.SimpleButton btn_StartTime;
    }
}