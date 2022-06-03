namespace DC.SF.UI
{
    partial class FrmEquipmentBYD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEquipmentBYD));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpEndTime = new DevExpress.XtraEditors.DateEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpStartTime = new DevExpress.XtraEditors.DateEdit();
            this.btnResearch = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pagerControl1 = new DC.SF.UI.PagerControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.dtpEndTime);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.dtpStartTime);
            this.panelControl1.Controls.Add(this.btnResearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1309, 78);
            this.panelControl1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 18);
            this.label3.TabIndex = 22;
            this.label3.Text = "起始时间:";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.EditValue = null;
            this.dtpEndTime.Location = new System.Drawing.Point(380, 24);
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
            this.dtpEndTime.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(290, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 18);
            this.label4.TabIndex = 23;
            this.label4.Text = "截止时间:";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.EditValue = null;
            this.dtpStartTime.Location = new System.Drawing.Point(103, 24);
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
            this.dtpStartTime.TabIndex = 21;
            // 
            // btnResearch
            // 
            this.btnResearch.Appearance.Font = new System.Drawing.Font("黑体", 11.7931F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResearch.Appearance.Options.UseFont = true;
            this.btnResearch.Image = ((System.Drawing.Image)(resources.GetObject("btnResearch.Image")));
            this.btnResearch.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnResearch.Location = new System.Drawing.Point(571, 13);
            this.btnResearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnResearch.Name = "btnResearch";
            this.btnResearch.Size = new System.Drawing.Size(113, 44);
            this.btnResearch.TabIndex = 20;
            this.btnResearch.Text = "查 询";
            this.btnResearch.Click += new System.EventHandler(this.btnResearch_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Location = new System.Drawing.Point(0, 78);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1309, 419);
            this.gridControl1.TabIndex = 7;
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
            this.pagerControl1.TabIndex = 8;
            // 
            // FrmEquipmentBYD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 579);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.pagerControl1);
            this.Controls.Add(this.panelControl1);
            this.MinimizeBox = false;
            this.Name = "FrmEquipmentBYD";
            this.Text = "系统操作查询";
            this.Load += new System.EventHandler(this.FrmEquipmentBYD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private PagerControl pagerControl1;
        private DevExpress.XtraEditors.SimpleButton btnResearch;
        private DevExpress.XtraEditors.DateEdit dtpEndTime;
        private DevExpress.XtraEditors.DateEdit dtpStartTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}