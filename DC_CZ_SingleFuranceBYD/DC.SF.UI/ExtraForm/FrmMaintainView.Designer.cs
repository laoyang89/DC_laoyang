namespace DC.SF.UI
{
    partial class FrmMaintainView
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_MaintainerID = new System.Windows.Forms.TextBox();
            this.btnMaintainLog = new System.Windows.Forms.Button();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnNowLog = new System.Windows.Forms.Button();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "维修人员ID:";
            // 
            // txt_MaintainerID
            // 
            this.txt_MaintainerID.Location = new System.Drawing.Point(131, 36);
            this.txt_MaintainerID.Name = "txt_MaintainerID";
            this.txt_MaintainerID.Size = new System.Drawing.Size(275, 26);
            this.txt_MaintainerID.TabIndex = 1;
            this.txt_MaintainerID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_MaintainerID_KeyDown);
            // 
            // btnMaintainLog
            // 
            this.btnMaintainLog.BackColor = System.Drawing.Color.LimeGreen;
            this.btnMaintainLog.FlatAppearance.BorderSize = 0;
            this.btnMaintainLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaintainLog.ForeColor = System.Drawing.Color.White;
            this.btnMaintainLog.Location = new System.Drawing.Point(588, 32);
            this.btnMaintainLog.Name = "btnMaintainLog";
            this.btnMaintainLog.Size = new System.Drawing.Size(120, 32);
            this.btnMaintainLog.TabIndex = 8;
            this.btnMaintainLog.Text = "历史维修记录";
            this.btnMaintainLog.UseVisualStyleBackColor = false;
            this.btnMaintainLog.Click += new System.EventHandler(this.btnMaintainLog_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnNowLog);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.btnMaintainLog);
            this.panelControl1.Controls.Add(this.txt_MaintainerID);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(850, 100);
            this.panelControl1.TabIndex = 9;
            // 
            // btnNowLog
            // 
            this.btnNowLog.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnNowLog.FlatAppearance.BorderSize = 0;
            this.btnNowLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNowLog.ForeColor = System.Drawing.Color.White;
            this.btnNowLog.Location = new System.Drawing.Point(452, 32);
            this.btnNowLog.Name = "btnNowLog";
            this.btnNowLog.Size = new System.Drawing.Size(120, 32);
            this.btnNowLog.TabIndex = 9;
            this.btnNowLog.Text = "当前维修记录";
            this.btnNowLog.UseVisualStyleBackColor = false;
            this.btnNowLog.Click += new System.EventHandler(this.btnNowLog_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 100);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(850, 305);
            this.gridControl1.TabIndex = 10;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // FrmMaintainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 405);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.MinimizeBox = false;
            this.Name = "FrmMaintainView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "维修界面";
            this.Load += new System.EventHandler(this.FrmMaintainView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_MaintainerID;
        private System.Windows.Forms.Button btnMaintainLog;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Button btnNowLog;
    }
}