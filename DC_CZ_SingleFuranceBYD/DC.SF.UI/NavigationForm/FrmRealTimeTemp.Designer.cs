namespace DC.SF.UI
{
    partial class FrmRealTimeTemp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRealTimeTemp));
            this.StartCollect = new DevExpress.XtraEditors.SimpleButton();
            this.StopCollect = new DevExpress.XtraEditors.SimpleButton();
            this.ExportData = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSeriese = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbCavity = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtInterval = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.dataGridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.timerColloctTemp = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cmbSeriese.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCavity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterval.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // StartCollect
            // 
            this.StartCollect.Image = ((System.Drawing.Image)(resources.GetObject("StartCollect.Image")));
            this.StartCollect.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.StartCollect.Location = new System.Drawing.Point(34, 13);
            this.StartCollect.Name = "StartCollect";
            this.StartCollect.Size = new System.Drawing.Size(132, 38);
            this.StartCollect.TabIndex = 0;
            this.StartCollect.Text = "开始采集";
            this.StartCollect.Click += new System.EventHandler(this.StartCollect_Click);
            // 
            // StopCollect
            // 
            this.StopCollect.Image = ((System.Drawing.Image)(resources.GetObject("StopCollect.Image")));
            this.StopCollect.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.StopCollect.Location = new System.Drawing.Point(221, 13);
            this.StopCollect.Name = "StopCollect";
            this.StopCollect.Size = new System.Drawing.Size(132, 38);
            this.StopCollect.TabIndex = 0;
            this.StopCollect.Text = "停止采集";
            this.StopCollect.Click += new System.EventHandler(this.StopCollect_Click);
            // 
            // ExportData
            // 
            this.ExportData.Image = ((System.Drawing.Image)(resources.GetObject("ExportData.Image")));
            this.ExportData.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.ExportData.Location = new System.Drawing.Point(1039, 13);
            this.ExportData.Name = "ExportData";
            this.ExportData.Size = new System.Drawing.Size(132, 38);
            this.ExportData.TabIndex = 0;
            this.ExportData.Text = "导出数据";
            this.ExportData.Click += new System.EventHandler(this.ExportData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(819, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择腔体:";
          
            // 
            // cmbSeriese
            // 
            this.cmbSeriese.Location = new System.Drawing.Point(662, 21);
            this.cmbSeriese.Name = "cmbSeriese";
            this.cmbSeriese.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSeriese.Size = new System.Drawing.Size(115, 24);
            this.cmbSeriese.TabIndex = 3;
            this.cmbSeriese.SelectedIndexChanged += new System.EventHandler(this.cmbSeriese_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(584, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "选择层板:";
      
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.button1);
            this.panelControl1.Controls.Add(this.cmbCavity);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtInterval);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.cmbSeriese);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.ExportData);
            this.panelControl1.Controls.Add(this.StopCollect);
            this.panelControl1.Controls.Add(this.StartCollect);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1192, 69);
            this.panelControl1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(515, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbCavity
            // 
            this.cmbCavity.Location = new System.Drawing.Point(898, 21);
            this.cmbCavity.Name = "cmbCavity";
            this.cmbCavity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCavity.Properties.NullText = "";
            this.cmbCavity.Size = new System.Drawing.Size(115, 24);
            this.cmbCavity.TabIndex = 8;
        
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(506, 25);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(15, 18);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "秒";
         
            // 
            // txtInterval
            // 
            this.txtInterval.EditValue = "60";
            this.txtInterval.Location = new System.Drawing.Point(431, 22);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(69, 24);
            this.txtInterval.TabIndex = 6;
           
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(380, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 18);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "频率：";
           
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.dataGridControl1);
            this.panelControl2.Controls.Add(this.chartControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 69);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1192, 670);
            this.panelControl2.TabIndex = 1;
            // 
            // dataGridControl1
            // 
            this.dataGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridControl1.Location = new System.Drawing.Point(2, 2);
            this.dataGridControl1.MainView = this.gridView1;
            this.dataGridControl1.Name = "dataGridControl1";
            this.dataGridControl1.Size = new System.Drawing.Size(1188, 376);
            this.dataGridControl1.TabIndex = 5;
            this.dataGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dataGridControl1;
            this.gridView1.GroupPanelText = " ";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // chartControl1
            // 
            this.chartControl1.DataBindings = null;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Location = new System.Drawing.Point(2, 378);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(1188, 290);
            this.chartControl1.TabIndex = 3;
            // 
            // timerColloctTemp
            // 
            this.timerColloctTemp.Tick += new System.EventHandler(this.timerColloctTemp_Tick);
            // 
            // FrmRealTimeTemp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 739);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MinimizeBox = false;
            this.Name = "FrmRealTimeTemp";
            this.Text = "实时温度分析";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRealTimeTemp_FormClosing);
            this.Load += new System.EventHandler(this.FrmRealTimeTemp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbSeriese.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCavity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterval.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton StartCollect;
        private DevExpress.XtraEditors.SimpleButton StopCollect;
        private DevExpress.XtraEditors.SimpleButton ExportData;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSeriese;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl dataGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtInterval;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Timer timerColloctTemp;
        private DevExpress.XtraEditors.LookUpEdit cmbCavity;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraCharts.ChartControl chartControl1;
    }
}