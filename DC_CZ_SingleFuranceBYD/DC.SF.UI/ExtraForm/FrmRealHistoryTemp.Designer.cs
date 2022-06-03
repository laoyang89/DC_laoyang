namespace DC.SF.UI
{
    partial class FrmRealHistoryTemp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRealHistoryTemp));
            this.panTop = new DevExpress.XtraEditors.PanelControl();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenOK = new System.Windows.Forms.Button();
            this.btnOpenError = new System.Windows.Forms.Button();
            this.cmbSeriese = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panItem = new DevExpress.XtraEditors.PanelControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panTop)).BeginInit();
            this.panTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panItem)).BeginInit();
            this.panItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.btnExport);
            this.panTop.Controls.Add(this.btnOpenOK);
            this.panTop.Controls.Add(this.btnOpenError);
            this.panTop.Controls.Add(this.cmbSeriese);
            this.panTop.Controls.Add(this.label2);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(1246, 62);
            this.panTop.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnExport.Location = new System.Drawing.Point(1083, 15);
            this.btnExport.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 36);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "导 出";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnOpenOK
            // 
            this.btnOpenOK.Location = new System.Drawing.Point(432, 15);
            this.btnOpenOK.Name = "btnOpenOK";
            this.btnOpenOK.Size = new System.Drawing.Size(155, 39);
            this.btnOpenOK.TabIndex = 9;
            this.btnOpenOK.Text = "显示层板全部信息";
            this.btnOpenOK.UseVisualStyleBackColor = true;
            this.btnOpenOK.Click += new System.EventHandler(this.btnOpenOK_Click);
            // 
            // btnOpenError
            // 
            this.btnOpenError.Location = new System.Drawing.Point(278, 15);
            this.btnOpenError.Name = "btnOpenError";
            this.btnOpenError.Size = new System.Drawing.Size(139, 39);
            this.btnOpenError.TabIndex = 8;
            this.btnOpenError.Text = "显示异常信息";
            this.btnOpenError.UseVisualStyleBackColor = true;
            this.btnOpenError.Click += new System.EventHandler(this.btnOpenError_Click);
            // 
            // cmbSeriese
            // 
            this.cmbSeriese.FormattingEnabled = true;
            this.cmbSeriese.Location = new System.Drawing.Point(107, 22);
            this.cmbSeriese.Name = "cmbSeriese";
            this.cmbSeriese.Size = new System.Drawing.Size(143, 26);
            this.cmbSeriese.TabIndex = 7;
            this.cmbSeriese.SelectedIndexChanged += new System.EventHandler(this.cmbSeriese_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "选择曲线:";
            // 
            // panItem
            // 
            this.panItem.Controls.Add(this.chartControl1);
            this.panItem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panItem.Location = new System.Drawing.Point(0, 528);
            this.panItem.Name = "panItem";
            this.panItem.Size = new System.Drawing.Size(1246, 285);
            this.panItem.TabIndex = 1;
            // 
            // chartControl1
            // 
            this.chartControl1.DataBindings = null;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(2, 2);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(1242, 281);
            this.chartControl1.TabIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gridControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 62);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1246, 466);
            this.panelControl1.TabIndex = 2;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1242, 462);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupPanelText = " ";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // FrmRealHistoryTemp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 813);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panItem);
            this.Controls.Add(this.panTop);
            this.MinimizeBox = false;
            this.Name = "FrmRealHistoryTemp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "腔体小车加热历史温度";
            this.Load += new System.EventHandler(this.FrmRealHistoryTemp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panTop)).EndInit();
            this.panTop.ResumeLayout(false);
            this.panTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panItem)).EndInit();
            this.panItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panTop;
        private DevExpress.XtraEditors.PanelControl panItem;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private System.Windows.Forms.ComboBox cmbSeriese;
        private System.Windows.Forms.Button btnOpenError;
        private System.Windows.Forms.Button btnOpenOK;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnExport;
    }
}