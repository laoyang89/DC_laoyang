namespace DC.SF.UI
{
    partial class FrmFactoryDebug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFactoryDebug));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnClearRecord = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearDB = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearCellList = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearCar = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnClearPLCACode = new DevExpress.XtraEditors.SimpleButton();
            this.btnResumeSend = new DevExpress.XtraEditors.SimpleButton();
            this.txtCarNum = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.txt_ClearCarId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtTime = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnClearTempAndVacuum = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ClearCarId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTime.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.toggleSwitch1);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1092, 94);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "开启模拟";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.EditValue = true;
            this.toggleSwitch1.Location = new System.Drawing.Point(180, 47);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.Properties.OffText = "关闭";
            this.toggleSwitch1.Properties.OnText = "开启";
            this.toggleSwitch1.Size = new System.Drawing.Size(125, 29);
            this.toggleSwitch1.TabIndex = 2;
            this.toggleSwitch1.EditValueChanged += new System.EventHandler(this.toggleSwitch1_Toggled);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 52);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(150, 18);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "非调试开启模拟扫码：";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btnClearRecord);
            this.groupControl2.Controls.Add(this.btnClearDB);
            this.groupControl2.Controls.Add(this.btnClearCellList);
            this.groupControl2.Controls.Add(this.btnClearCar);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 94);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1092, 100);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "数据清空";
            // 
            // btnClearRecord
            // 
            this.btnClearRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnClearRecord.Image")));
            this.btnClearRecord.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClearRecord.Location = new System.Drawing.Point(713, 40);
            this.btnClearRecord.Name = "btnClearRecord";
            this.btnClearRecord.Size = new System.Drawing.Size(149, 43);
            this.btnClearRecord.TabIndex = 0;
            this.btnClearRecord.Text = "清空计时";
            this.btnClearRecord.Click += new System.EventHandler(this.btnClearRecord_Click);
            // 
            // btnClearDB
            // 
            this.btnClearDB.Image = ((System.Drawing.Image)(resources.GetObject("btnClearDB.Image")));
            this.btnClearDB.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClearDB.Location = new System.Drawing.Point(494, 40);
            this.btnClearDB.Name = "btnClearDB";
            this.btnClearDB.Size = new System.Drawing.Size(149, 43);
            this.btnClearDB.TabIndex = 0;
            this.btnClearDB.Text = "清空数据库";
            this.btnClearDB.Click += new System.EventHandler(this.btnClearDB_Click);
            // 
            // btnClearCellList
            // 
            this.btnClearCellList.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCellList.Image")));
            this.btnClearCellList.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClearCellList.Location = new System.Drawing.Point(266, 40);
            this.btnClearCellList.Name = "btnClearCellList";
            this.btnClearCellList.Size = new System.Drawing.Size(149, 43);
            this.btnClearCellList.TabIndex = 0;
            this.btnClearCellList.Text = "清空条码列表";
            this.btnClearCellList.Click += new System.EventHandler(this.btnClearCellList_Click);
            // 
            // btnClearCar
            // 
            this.btnClearCar.Image = ((System.Drawing.Image)(resources.GetObject("btnClearCar.Image")));
            this.btnClearCar.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClearCar.Location = new System.Drawing.Point(47, 40);
            this.btnClearCar.Name = "btnClearCar";
            this.btnClearCar.Size = new System.Drawing.Size(149, 43);
            this.btnClearCar.TabIndex = 0;
            this.btnClearCar.Text = "清空小车信息";
            this.btnClearCar.Click += new System.EventHandler(this.btnClearCar_Click);
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.btnClearPLCACode);
            this.groupControl3.Controls.Add(this.btnResumeSend);
            this.groupControl3.Controls.Add(this.txtCarNum);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl3.Location = new System.Drawing.Point(0, 194);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(1092, 94);
            this.groupControl3.TabIndex = 2;
            this.groupControl3.Text = "状态修改";
            // 
            // btnClearPLCACode
            // 
            this.btnClearPLCACode.Location = new System.Drawing.Point(472, 45);
            this.btnClearPLCACode.Name = "btnClearPLCACode";
            this.btnClearPLCACode.Size = new System.Drawing.Size(171, 30);
            this.btnClearPLCACode.TabIndex = 2;
            this.btnClearPLCACode.Text = "尝试清空PLC A料条码";
            this.btnClearPLCACode.Click += new System.EventHandler(this.btnClearPLCACode_Click);
            // 
            // btnResumeSend
            // 
            this.btnResumeSend.Location = new System.Drawing.Point(244, 45);
            this.btnResumeSend.Name = "btnResumeSend";
            this.btnResumeSend.Size = new System.Drawing.Size(171, 30);
            this.btnResumeSend.TabIndex = 2;
            this.btnResumeSend.Text = "恢复下料发送状态";
            this.btnResumeSend.Click += new System.EventHandler(this.btnResumeSend_Click);
            // 
            // txtCarNum
            // 
            this.txtCarNum.Location = new System.Drawing.Point(113, 48);
            this.txtCarNum.Name = "txtCarNum";
            this.txtCarNum.Size = new System.Drawing.Size(93, 24);
            this.txtCarNum.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(47, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 18);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "小车号：";
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.btnClearTempAndVacuum);
            this.groupControl4.Controls.Add(this.txtTime);
            this.groupControl4.Controls.Add(this.labelControl4);
            this.groupControl4.Controls.Add(this.txt_ClearCarId);
            this.groupControl4.Controls.Add(this.labelControl3);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl4.Location = new System.Drawing.Point(0, 288);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(1092, 109);
            this.groupControl4.TabIndex = 3;
            this.groupControl4.Text = "清空小车某一时间点之前温度和真空度";
            // 
            // txt_ClearCarId
            // 
            this.txt_ClearCarId.Location = new System.Drawing.Point(113, 53);
            this.txt_ClearCarId.Name = "txt_ClearCarId";
            this.txt_ClearCarId.Size = new System.Drawing.Size(93, 24);
            this.txt_ClearCarId.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(47, 56);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 18);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "小车号：";
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(322, 53);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(231, 24);
            this.txtTime.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(256, 56);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(45, 18);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "时间：";
            // 
            // btnClearTempAndVacuum
            // 
            this.btnClearTempAndVacuum.Location = new System.Drawing.Point(584, 48);
            this.btnClearTempAndVacuum.Name = "btnClearTempAndVacuum";
            this.btnClearTempAndVacuum.Size = new System.Drawing.Size(166, 33);
            this.btnClearTempAndVacuum.TabIndex = 6;
            this.btnClearTempAndVacuum.Text = "清空";
            this.btnClearTempAndVacuum.Click += new System.EventHandler(this.btnClearTempAndVacuum_Click);
            // 
            // FrmFactoryDebug
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1092, 670);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmFactoryDebug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "厂家调试";
            this.Load += new System.EventHandler(this.FrmFactoryDebug_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            this.groupControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ClearCarId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTime.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnClearCar;
        private DevExpress.XtraEditors.SimpleButton btnClearCellList;
        private DevExpress.XtraEditors.SimpleButton btnClearDB;
        private DevExpress.XtraEditors.SimpleButton btnClearRecord;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtCarNum;
        private DevExpress.XtraEditors.SimpleButton btnResumeSend;
        private DevExpress.XtraEditors.SimpleButton btnClearPLCACode;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btnClearTempAndVacuum;
        private DevExpress.XtraEditors.TextEdit txtTime;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txt_ClearCarId;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}