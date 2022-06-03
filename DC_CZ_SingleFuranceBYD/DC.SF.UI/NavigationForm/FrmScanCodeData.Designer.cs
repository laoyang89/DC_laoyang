namespace DC.SF.UI
{
    partial class FrmScanCodeData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmScanCodeData));
            this.dtpEndTime = new DevExpress.XtraEditors.DateEdit();
            this.dtpStartTime = new DevExpress.XtraEditors.DateEdit();
            this.txtCarrierNum = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCellCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtCellPlcCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.cmbMESType = new System.Windows.Forms.ComboBox();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.cmbScanCodeType = new System.Windows.Forms.ComboBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnResearch = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pagerControl1 = new DC.SF.UI.PagerControl();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrierNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellPlcCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.EditValue = null;
            this.dtpEndTime.Location = new System.Drawing.Point(645, 57);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 7.646018F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndTime.Properties.Appearance.Options.UseFont = true;
            this.dtpEndTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndTime.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.dtpEndTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpEndTime.Size = new System.Drawing.Size(167, 22);
            this.dtpEndTime.TabIndex = 25;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.EditValue = null;
            this.dtpStartTime.Location = new System.Drawing.Point(645, 24);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 7.646018F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartTime.Properties.Appearance.Options.UseFont = true;
            this.dtpStartTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStartTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.dtpStartTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStartTime.Properties.CalendarTimeProperties.EditFormat.FormatString = "yyyy/MM/dd HH:mm:ss";
            this.dtpStartTime.Properties.CalendarTimeProperties.TouchUIMaxValue = new System.DateTime(2019, 11, 22, 0, 0, 0, 0);
            this.dtpStartTime.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.dtpStartTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpStartTime.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.dtpStartTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpStartTime.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.dtpStartTime.Size = new System.Drawing.Size(167, 22);
            this.dtpStartTime.TabIndex = 24;
            // 
            // txtCarrierNum
            // 
            this.txtCarrierNum.Location = new System.Drawing.Point(90, 21);
            this.txtCarrierNum.Name = "txtCarrierNum";
            this.txtCarrierNum.Size = new System.Drawing.Size(90, 24);
            this.txtCarrierNum.TabIndex = 23;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(564, 60);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(75, 18);
            this.labelControl3.TabIndex = 21;
            this.labelControl3.Text = "截止时间：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(564, 27);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(75, 18);
            this.labelControl2.TabIndex = 22;
            this.labelControl2.Text = "起始时间：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 18);
            this.labelControl1.TabIndex = 20;
            this.labelControl1.Text = "载体编号：";
            // 
            // txtCellCode
            // 
            this.txtCellCode.Location = new System.Drawing.Point(267, 22);
            this.txtCellCode.Name = "txtCellCode";
            this.txtCellCode.Size = new System.Drawing.Size(267, 24);
            this.txtCellCode.TabIndex = 27;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(186, 25);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(75, 18);
            this.labelControl4.TabIndex = 26;
            this.labelControl4.Text = "电芯条码：";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtCellPlcCode);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.cmbMESType);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.cmbScanCodeType);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.btnExport);
            this.panelControl1.Controls.Add(this.btnResearch);
            this.panelControl1.Controls.Add(this.txtCarrierNum);
            this.panelControl1.Controls.Add(this.txtCellCode);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.dtpEndTime);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.dtpStartTime);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1464, 104);
            this.panelControl1.TabIndex = 28;
            // 
            // txtCellPlcCode
            // 
            this.txtCellPlcCode.Location = new System.Drawing.Point(444, 56);
            this.txtCellPlcCode.Name = "txtCellPlcCode";
            this.txtCellPlcCode.Size = new System.Drawing.Size(90, 24);
            this.txtCellPlcCode.TabIndex = 35;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(363, 59);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(75, 18);
            this.labelControl7.TabIndex = 34;
            this.labelControl7.Text = "电芯编码：";
            // 
            // cmbMESType
            // 
            this.cmbMESType.FormattingEnabled = true;
            this.cmbMESType.Items.AddRange(new object[] {
            " ",
            "True",
            "False"});
            this.cmbMESType.Location = new System.Drawing.Point(267, 55);
            this.cmbMESType.Name = "cmbMESType";
            this.cmbMESType.Size = new System.Drawing.Size(90, 26);
            this.cmbMESType.TabIndex = 33;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(186, 59);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(73, 18);
            this.labelControl6.TabIndex = 32;
            this.labelControl6.Text = "MES状态：";
            // 
            // cmbScanCodeType
            // 
            this.cmbScanCodeType.FormattingEnabled = true;
            this.cmbScanCodeType.Items.AddRange(new object[] {
            " ",
            "True",
            "False"});
            this.cmbScanCodeType.Location = new System.Drawing.Point(90, 55);
            this.cmbScanCodeType.Name = "cmbScanCodeType";
            this.cmbScanCodeType.Size = new System.Drawing.Size(90, 26);
            this.cmbScanCodeType.TabIndex = 31;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(9, 59);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(75, 18);
            this.labelControl5.TabIndex = 30;
            this.labelControl5.Text = "扫码状态：";
            // 
            // btnExport
            // 
            this.btnExport.Appearance.Font = new System.Drawing.Font("黑体", 11.7931F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Appearance.Options.UseFont = true;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnExport.Location = new System.Drawing.Point(856, 53);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(114, 36);
            this.btnExport.TabIndex = 29;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnResearch
            // 
            this.btnResearch.Appearance.Font = new System.Drawing.Font("黑体", 11.7931F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResearch.Appearance.Options.UseFont = true;
            this.btnResearch.Image = ((System.Drawing.Image)(resources.GetObject("btnResearch.Image")));
            this.btnResearch.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnResearch.Location = new System.Drawing.Point(856, 11);
            this.btnResearch.Name = "btnResearch";
            this.btnResearch.Size = new System.Drawing.Size(114, 36);
            this.btnResearch.TabIndex = 28;
            this.btnResearch.Text = "查 询";
            this.btnResearch.Click += new System.EventHandler(this.btnResearch_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 104);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1464, 610);
            this.gridControl1.TabIndex = 29;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupPanelText = " ";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // pagerControl1
            // 
            this.pagerControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagerControl1.JumpText = "Go";
            this.pagerControl1.Location = new System.Drawing.Point(0, 659);
            this.pagerControl1.Name = "pagerControl1";
            this.pagerControl1.PageIndex = 1;
            this.pagerControl1.PageSize = 30;
            this.pagerControl1.RecordCount = 0;
            this.pagerControl1.Size = new System.Drawing.Size(1464, 55);
            this.pagerControl1.TabIndex = 30;
            // 
            // FrmScanCodeData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1464, 714);
            this.Controls.Add(this.pagerControl1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.MinimizeBox = false;
            this.Name = "FrmScanCodeData";
            this.Text = "扫码记录";
            this.Load += new System.EventHandler(this.FrmScanCodeData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrierNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellPlcCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dtpEndTime;
        private DevExpress.XtraEditors.DateEdit dtpStartTime;
        private DevExpress.XtraEditors.TextEdit txtCarrierNum;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCellCode;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnResearch;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private PagerControl pagerControl1;
        private System.Windows.Forms.ComboBox cmbScanCodeType;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.ComboBox cmbMESType;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtCellPlcCode;
        private DevExpress.XtraEditors.LabelControl labelControl7;
    }
}