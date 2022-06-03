namespace DC.SF.UI
{
    partial class StationControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblStationName = new DevExpress.XtraEditors.LabelControl();
            this.panItem = new DevExpress.XtraEditors.PanelControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMenuBtn_Status = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenu_LookCellCode = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_MenuLookHistoryTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenu_Degree = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenu_LookLayersStatus = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.panItem)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStationName
            // 
            this.lblStationName.Appearance.Font = new System.Drawing.Font("黑体", 12.10619F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStationName.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblStationName.Appearance.Options.UseFont = true;
            this.lblStationName.Appearance.Options.UseForeColor = true;
            this.lblStationName.Appearance.Options.UseTextOptions = true;
            this.lblStationName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblStationName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblStationName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblStationName.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblStationName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStationName.Location = new System.Drawing.Point(0, 0);
            this.lblStationName.Name = "lblStationName";
            this.lblStationName.Size = new System.Drawing.Size(248, 35);
            this.lblStationName.TabIndex = 0;
            this.lblStationName.Text = "工位名";
            // 
            // panItem
            // 
            this.panItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panItem.Location = new System.Drawing.Point(0, 35);
            this.panItem.Name = "panItem";
            this.panItem.Size = new System.Drawing.Size(248, 145);
            this.panItem.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuBtn_Status,
            this.tsMenu_LookCellCode,
            this.ts_MenuLookHistoryTemp,
            this.tsMenu_Degree,
            this.tsMenu_LookLayersStatus});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 124);
            // 
            // tsMenuBtn_Status
            // 
            this.tsMenuBtn_Status.Name = "tsMenuBtn_Status";
            this.tsMenuBtn_Status.Size = new System.Drawing.Size(168, 24);
            this.tsMenuBtn_Status.Text = "小车工艺信息";
            this.tsMenuBtn_Status.Click += new System.EventHandler(this.tsMenuBtn_Status_Click);
            // 
            // tsMenu_LookCellCode
            // 
            this.tsMenu_LookCellCode.Name = "tsMenu_LookCellCode";
            this.tsMenu_LookCellCode.Size = new System.Drawing.Size(168, 24);
            this.tsMenu_LookCellCode.Text = "查看小车电芯";
            this.tsMenu_LookCellCode.Click += new System.EventHandler(this.tsMenu_LookCellCode_Click);
            // 
            // ts_MenuLookHistoryTemp
            // 
            this.ts_MenuLookHistoryTemp.Name = "ts_MenuLookHistoryTemp";
            this.ts_MenuLookHistoryTemp.Size = new System.Drawing.Size(168, 24);
            this.ts_MenuLookHistoryTemp.Text = "查看历史温度";
            this.ts_MenuLookHistoryTemp.Click += new System.EventHandler(this.ts_MenuLookHistoryTemp_Click);
            // 
            // tsMenu_Degree
            // 
            this.tsMenu_Degree.Name = "tsMenu_Degree";
            this.tsMenu_Degree.Size = new System.Drawing.Size(168, 24);
            this.tsMenu_Degree.Text = "查看真空度";
            this.tsMenu_Degree.Click += new System.EventHandler(this.tsMenu_Degree_Click);
            // 
            // tsMenu_LookLayersStatus
            // 
            this.tsMenu_LookLayersStatus.Name = "tsMenu_LookLayersStatus";
            this.tsMenu_LookLayersStatus.Size = new System.Drawing.Size(168, 24);
            this.tsMenu_LookLayersStatus.Text = "查看层板状态";
            this.tsMenu_LookLayersStatus.Click += new System.EventHandler(this.tsMenu_LookLayersStatus_Click);
            // 
            // StationControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.panItem);
            this.Controls.Add(this.lblStationName);
            this.Name = "StationControl";
            this.Size = new System.Drawing.Size(248, 180);
            this.Load += new System.EventHandler(this.MyControls_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panItem)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblStationName;
        private DevExpress.XtraEditors.PanelControl panItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsMenu_LookCellCode;
        private System.Windows.Forms.ToolStripMenuItem tsMenu_LookLayersStatus;
        private System.Windows.Forms.ToolStripMenuItem ts_MenuLookHistoryTemp;
        private System.Windows.Forms.ToolStripMenuItem tsMenuBtn_Status;
        private System.Windows.Forms.ToolStripMenuItem tsMenu_Degree;
    }
}
