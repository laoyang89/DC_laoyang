namespace DC.SF.UI
{
    partial class FrmCellAnalyzeBYD
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCellAnalyzeBYD));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ckTimeFlag = new DevExpress.XtraEditors.CheckEdit();
            this.ckIsRepeat = new DevExpress.XtraEditors.CheckEdit();
            this.btnResearch = new DevExpress.XtraEditors.SimpleButton();
            this.dtpEndTime = new DevExpress.XtraEditors.DateEdit();
            this.dtpStartTime = new DevExpress.XtraEditors.DateEdit();
            this.txtCellCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtCarrierNum = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看电芯温度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出全部电芯ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nG标记ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消NG标记ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pagerControl1 = new DC.SF.UI.PagerControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckTimeFlag.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckIsRepeat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrierNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.ckTimeFlag);
            this.panelControl1.Controls.Add(this.ckIsRepeat);
            this.panelControl1.Controls.Add(this.btnResearch);
            this.panelControl1.Controls.Add(this.dtpEndTime);
            this.panelControl1.Controls.Add(this.dtpStartTime);
            this.panelControl1.Controls.Add(this.txtCellCode);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtCarrierNum);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1309, 78);
            this.panelControl1.TabIndex = 3;
            // 
            // ckTimeFlag
            // 
            this.ckTimeFlag.EditValue = true;
            this.ckTimeFlag.Location = new System.Drawing.Point(978, 39);
            this.ckTimeFlag.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckTimeFlag.Name = "ckTimeFlag";
            this.ckTimeFlag.Properties.Caption = "时间筛选";
            this.ckTimeFlag.Size = new System.Drawing.Size(105, 22);
            this.ckTimeFlag.TabIndex = 21;
            // 
            // ckIsRepeat
            // 
            this.ckIsRepeat.Location = new System.Drawing.Point(978, 13);
            this.ckIsRepeat.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckIsRepeat.Name = "ckIsRepeat";
            this.ckIsRepeat.Properties.Caption = "重复条码";
            this.ckIsRepeat.Size = new System.Drawing.Size(105, 22);
            this.ckIsRepeat.TabIndex = 20;
            // 
            // btnResearch
            // 
            this.btnResearch.Appearance.Font = new System.Drawing.Font("黑体", 11.7931F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResearch.Appearance.Options.UseFont = true;
            this.btnResearch.Image = ((System.Drawing.Image)(resources.GetObject("btnResearch.Image")));
            this.btnResearch.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnResearch.Location = new System.Drawing.Point(1123, 15);
            this.btnResearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnResearch.Name = "btnResearch";
            this.btnResearch.Size = new System.Drawing.Size(113, 44);
            this.btnResearch.TabIndex = 19;
            this.btnResearch.Text = "查 询";
            this.btnResearch.Click += new System.EventHandler(this.btnResearch_Click);
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.EditValue = null;
            this.dtpEndTime.Location = new System.Drawing.Point(791, 21);
            this.dtpEndTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.dtpEndTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndTime.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.dtpEndTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpEndTime.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.dtpEndTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpEndTime.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.dtpEndTime.Size = new System.Drawing.Size(165, 24);
            this.dtpEndTime.TabIndex = 18;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.EditValue = null;
            this.dtpStartTime.Location = new System.Drawing.Point(514, 21);
            this.dtpStartTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStartTime.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.dtpStartTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpStartTime.Properties.CalendarTimeProperties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dtpStartTime.Properties.CalendarTimeProperties.TouchUIMaxValue = new System.DateTime(2019, 11, 22, 0, 0, 0, 0);
            this.dtpStartTime.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.dtpStartTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpStartTime.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.dtpStartTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpStartTime.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.dtpStartTime.Size = new System.Drawing.Size(168, 24);
            this.dtpStartTime.TabIndex = 17;
            // 
            // txtCellCode
            // 
            this.txtCellCode.Location = new System.Drawing.Point(255, 21);
            this.txtCellCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCellCode.Name = "txtCellCode";
            this.txtCellCode.Size = new System.Drawing.Size(146, 24);
            this.txtCellCode.TabIndex = 15;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(197, 26);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(45, 18);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "条码：";
            // 
            // txtCarrierNum
            // 
            this.txtCarrierNum.Location = new System.Drawing.Point(106, 21);
            this.txtCarrierNum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCarrierNum.Name = "txtCarrierNum";
            this.txtCarrierNum.Size = new System.Drawing.Size(59, 24);
            this.txtCarrierNum.TabIndex = 16;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 26);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 18);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "小车编号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(701, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 18);
            this.label4.TabIndex = 11;
            this.label4.Text = "截止时间:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(423, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 18);
            this.label3.TabIndex = 12;
            this.label3.Text = "起始时间:";
            // 
            // gridControl1
            // 
            this.gridControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Location = new System.Drawing.Point(0, 78);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1309, 419);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看电芯温度ToolStripMenuItem,
            this.导出全部电芯ToolStripMenuItem,
            this.nG标记ToolStripMenuItem,
            this.取消NG标记ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 100);
            // 
            // 查看电芯温度ToolStripMenuItem
            // 
            this.查看电芯温度ToolStripMenuItem.Name = "查看电芯温度ToolStripMenuItem";
            this.查看电芯温度ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.查看电芯温度ToolStripMenuItem.Text = "查看电芯温度";
            this.查看电芯温度ToolStripMenuItem.Click += new System.EventHandler(this.查看电芯温度ToolStripMenuItem_Click);
            // 
            // 导出全部电芯ToolStripMenuItem
            // 
            this.导出全部电芯ToolStripMenuItem.Name = "导出全部电芯ToolStripMenuItem";
            this.导出全部电芯ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.导出全部电芯ToolStripMenuItem.Text = "导出当前页电芯";
            this.导出全部电芯ToolStripMenuItem.Click += new System.EventHandler(this.导出全部电芯ToolStripMenuItem_Click);
            // 
            // nG标记ToolStripMenuItem
            // 
            this.nG标记ToolStripMenuItem.Name = "nG标记ToolStripMenuItem";
            this.nG标记ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.nG标记ToolStripMenuItem.Text = "NG标记";
            this.nG标记ToolStripMenuItem.Click += new System.EventHandler(this.nG标记ToolStripMenuItem_Click);
            // 
            // 取消NG标记ToolStripMenuItem
            // 
            this.取消NG标记ToolStripMenuItem.Name = "取消NG标记ToolStripMenuItem";
            this.取消NG标记ToolStripMenuItem.Size = new System.Drawing.Size(183, 24);
            this.取消NG标记ToolStripMenuItem.Text = "取消NG标记";
            this.取消NG标记ToolStripMenuItem.Click += new System.EventHandler(this.取消NG标记ToolStripMenuItem_Click);
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
            // 
            // pagerControl1
            // 
            this.pagerControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pagerControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagerControl1.JumpText = "Go";
            this.pagerControl1.Location = new System.Drawing.Point(0, 497);
            this.pagerControl1.Name = "pagerControl1";
            this.pagerControl1.PageIndex = 1;
            this.pagerControl1.PageSize = 30;
            this.pagerControl1.RecordCount = 0;
            this.pagerControl1.Size = new System.Drawing.Size(1309, 82);
            this.pagerControl1.TabIndex = 6;
            // 
            // FrmCellAnalyzeBYD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 579);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.pagerControl1);
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "FrmCellAnalyzeBYD";
            this.Text = "MES 电池分析";
            this.Load += new System.EventHandler(this.FrmCellAnalyzeBYD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckTimeFlag.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckIsRepeat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrierNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit ckIsRepeat;
        private DevExpress.XtraEditors.SimpleButton btnResearch;
        private DevExpress.XtraEditors.DateEdit dtpEndTime;
        private DevExpress.XtraEditors.DateEdit dtpStartTime;
        private DevExpress.XtraEditors.TextEdit txtCellCode;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtCarrierNum;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查看电芯温度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出全部电芯ToolStripMenuItem;
        private PagerControl pagerControl1;
        private DevExpress.XtraEditors.CheckEdit ckTimeFlag;
        private System.Windows.Forms.ToolStripMenuItem nG标记ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消NG标记ToolStripMenuItem;
    }
}