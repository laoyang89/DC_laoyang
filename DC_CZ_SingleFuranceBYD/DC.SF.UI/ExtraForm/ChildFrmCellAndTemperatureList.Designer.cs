namespace DC.SF.UI
{
    partial class ChildFrmCellAndTemperatureList
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChildFrmCellAndTemperatureList));
            this.panel2 = new System.Windows.Forms.Panel();
            this.pagerControl1 = new DC.SF.UI.PagerControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_Show = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panTemp = new DevExpress.XtraEditors.PanelControl();
            this.cmbLayers = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panCell = new DevExpress.XtraEditors.PanelControl();
            this.txtCellOrRank = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Show)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panTemp)).BeginInit();
            this.panTemp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLayers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panCell)).BeginInit();
            this.panCell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellOrRank.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pagerControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 571);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1031, 53);
            this.panel2.TabIndex = 3;
            // 
            // pagerControl1
            // 
            this.pagerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagerControl1.JumpText = "Go";
            this.pagerControl1.Location = new System.Drawing.Point(0, 0);
            this.pagerControl1.Name = "pagerControl1";
            this.pagerControl1.PageIndex = 1;
            this.pagerControl1.PageSize = 30;
            this.pagerControl1.RecordCount = 0;
            this.pagerControl1.Size = new System.Drawing.Size(1031, 53);
            this.pagerControl1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_Show);
            this.panel1.Controls.Add(this.panelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1031, 571);
            this.panel1.TabIndex = 4;
            // 
            // dgv_Show
            // 
            this.dgv_Show.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dgv_Show.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dgv_Show.Location = new System.Drawing.Point(0, 62);
            this.dgv_Show.MainView = this.gridView1;
            this.dgv_Show.Name = "dgv_Show";
            this.dgv_Show.Size = new System.Drawing.Size(1031, 509);
            this.dgv_Show.TabIndex = 4;
            this.dgv_Show.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.dgv_Show;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.panTemp);
            this.panelControl1.Controls.Add(this.panCell);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1031, 62);
            this.panelControl1.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.btnSearch.Location = new System.Drawing.Point(681, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(111, 39);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查 询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panTemp
            // 
            this.panTemp.Controls.Add(this.cmbLayers);
            this.panTemp.Controls.Add(this.labelControl2);
            this.panTemp.Dock = System.Windows.Forms.DockStyle.Left;
            this.panTemp.Location = new System.Drawing.Point(327, 2);
            this.panTemp.Name = "panTemp";
            this.panTemp.Size = new System.Drawing.Size(329, 58);
            this.panTemp.TabIndex = 1;
            // 
            // cmbLayers
            // 
            this.cmbLayers.Location = new System.Drawing.Point(87, 19);
            this.cmbLayers.Name = "cmbLayers";
            this.cmbLayers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLayers.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbLayers.Size = new System.Drawing.Size(100, 24);
            this.cmbLayers.TabIndex = 1;
            this.cmbLayers.SelectedIndexChanged += new System.EventHandler(this.cmbLayers_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(21, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 18);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "层板号：";
            // 
            // panCell
            // 
            this.panCell.Controls.Add(this.txtCellOrRank);
            this.panCell.Controls.Add(this.labelControl1);
            this.panCell.Dock = System.Windows.Forms.DockStyle.Left;
            this.panCell.Location = new System.Drawing.Point(2, 2);
            this.panCell.Name = "panCell";
            this.panCell.Size = new System.Drawing.Size(325, 58);
            this.panCell.TabIndex = 0;
            // 
            // txtCellOrRank
            // 
            this.txtCellOrRank.Location = new System.Drawing.Point(100, 19);
            this.txtCellOrRank.Name = "txtCellOrRank";
            this.txtCellOrRank.Size = new System.Drawing.Size(183, 24);
            this.txtCellOrRank.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "条码编码：";
            // 
            // ChildFrmCellAndTemperatureList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 624);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "ChildFrmCellAndTemperatureList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChildFrmCellAndTemperatureList";
            this.Load += new System.EventHandler(this.ChildFrmCellAndTemperatureList_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Show)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panTemp)).EndInit();
            this.panTemp.ResumeLayout(false);
            this.panTemp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLayers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panCell)).EndInit();
            this.panCell.ResumeLayout(false);
            this.panCell.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCellOrRank.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private PagerControl pagerControl1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl dgv_Show;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panCell;
        private DevExpress.XtraEditors.PanelControl panTemp;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.TextEdit txtCellOrRank;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbLayers;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}