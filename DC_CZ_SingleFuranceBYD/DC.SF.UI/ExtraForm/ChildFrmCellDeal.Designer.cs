namespace DC.SF.UI
{
    partial class ChildFrmCellDeal
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
            this.chb_CheckAll = new DevExpress.XtraEditors.CheckEdit();
            this.btn_Delete = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.clb_RepetitiveCells = new DevExpress.XtraEditors.CheckedListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chb_CheckAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clb_RepetitiveCells)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.chb_CheckAll);
            this.panelControl1.Controls.Add(this.btn_Delete);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(700, 61);
            this.panelControl1.TabIndex = 0;
            // 
            // chb_CheckAll
            // 
            this.chb_CheckAll.Location = new System.Drawing.Point(12, 21);
            this.chb_CheckAll.Name = "chb_CheckAll";
            this.chb_CheckAll.Properties.Caption = "选中全部";
            this.chb_CheckAll.Size = new System.Drawing.Size(87, 22);
            this.chb_CheckAll.TabIndex = 1;
            this.chb_CheckAll.CheckedChanged += new System.EventHandler(this.chb_CheckAll_CheckedChanged);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(116, 16);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(122, 31);
            this.btn_Delete.TabIndex = 0;
            this.btn_Delete.Text = "删除选中项";
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.clb_RepetitiveCells);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 61);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(700, 502);
            this.panelControl2.TabIndex = 1;
            // 
            // clb_RepetitiveCells
            // 
            this.clb_RepetitiveCells.Cursor = System.Windows.Forms.Cursors.Default;
            this.clb_RepetitiveCells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clb_RepetitiveCells.Location = new System.Drawing.Point(2, 2);
            this.clb_RepetitiveCells.Name = "clb_RepetitiveCells";
            this.clb_RepetitiveCells.Size = new System.Drawing.Size(696, 498);
            this.clb_RepetitiveCells.TabIndex = 1;
            // 
            // ChildFrmCellDeal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 563);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "ChildFrmCellDeal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "电池处理";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chb_CheckAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clb_RepetitiveCells)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_Delete;
        private DevExpress.XtraEditors.CheckedListBoxControl clb_RepetitiveCells;
        private DevExpress.XtraEditors.CheckEdit chb_CheckAll;
    }
}