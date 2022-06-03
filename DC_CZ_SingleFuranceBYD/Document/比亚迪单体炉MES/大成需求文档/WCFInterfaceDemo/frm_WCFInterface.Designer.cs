namespace WCFInterfaceClient
{
    partial class frm_WCFInterface
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.grp_Send = new System.Windows.Forms.GroupBox();
            this.txtInterface = new DevExpress.XtraEditors.LookUpEdit();
            this.btn_SendMsg = new System.Windows.Forms.Button();
            this.txtFuncParam = new DevExpress.XtraEditors.MemoEdit();
            this.txtFuncName = new DevExpress.XtraEditors.LookUpEdit();
            this.grp_Return = new System.Windows.Forms.GroupBox();
            this.txtReturn = new DevExpress.XtraEditors.MemoEdit();
            this.grp_Send.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterface.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFuncParam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFuncName.Properties)).BeginInit();
            this.grp_Return.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturn.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_Send
            // 
            this.grp_Send.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grp_Send.Controls.Add(this.txtInterface);
            this.grp_Send.Controls.Add(this.btn_SendMsg);
            this.grp_Send.Controls.Add(this.txtFuncParam);
            this.grp_Send.Controls.Add(this.txtFuncName);
            this.grp_Send.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_Send.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grp_Send.Location = new System.Drawing.Point(0, 0);
            this.grp_Send.Name = "grp_Send";
            this.grp_Send.Size = new System.Drawing.Size(893, 225);
            this.grp_Send.TabIndex = 0;
            this.grp_Send.TabStop = false;
            this.grp_Send.Text = "接口调用";
            // 
            // txtInterface
            // 
            this.txtInterface.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtInterface.Location = new System.Drawing.Point(3, 19);
            this.txtInterface.Name = "txtInterface";
            this.txtInterface.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtInterface.Properties.Appearance.Options.UseFont = true;
            this.txtInterface.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtInterface.Size = new System.Drawing.Size(214, 26);
            this.txtInterface.TabIndex = 5;
            this.txtInterface.EditValueChanged += new System.EventHandler(this.txtInterface_EditValueChanged);
            // 
            // btn_SendMsg
            // 
            this.btn_SendMsg.Location = new System.Drawing.Point(135, 89);
            this.btn_SendMsg.Name = "btn_SendMsg";
            this.btn_SendMsg.Size = new System.Drawing.Size(82, 35);
            this.btn_SendMsg.TabIndex = 4;
            this.btn_SendMsg.Text = "发送";
            this.btn_SendMsg.UseVisualStyleBackColor = true;
            this.btn_SendMsg.Click += new System.EventHandler(this.btn_SendMsg_Click);
            // 
            // txtFuncParam
            // 
            this.txtFuncParam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFuncParam.Location = new System.Drawing.Point(223, 12);
            this.txtFuncParam.Name = "txtFuncParam";
            this.txtFuncParam.Size = new System.Drawing.Size(658, 207);
            this.txtFuncParam.TabIndex = 3;
            // 
            // txtFuncName
            // 
            this.txtFuncName.Location = new System.Drawing.Point(24, 51);
            this.txtFuncName.Name = "txtFuncName";
            this.txtFuncName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtFuncName.Properties.Appearance.Options.UseFont = true;
            this.txtFuncName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFuncName.Size = new System.Drawing.Size(193, 22);
            this.txtFuncName.TabIndex = 0;
            this.txtFuncName.EditValueChanged += new System.EventHandler(this.txtFuncName_EditValueChanged);
            // 
            // grp_Return
            // 
            this.grp_Return.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.grp_Return.Controls.Add(this.txtReturn);
            this.grp_Return.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_Return.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grp_Return.Location = new System.Drawing.Point(0, 225);
            this.grp_Return.Name = "grp_Return";
            this.grp_Return.Size = new System.Drawing.Size(893, 212);
            this.grp_Return.TabIndex = 1;
            this.grp_Return.TabStop = false;
            this.grp_Return.Text = "接口返回";
            // 
            // txtReturn
            // 
            this.txtReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReturn.Location = new System.Drawing.Point(3, 19);
            this.txtReturn.Name = "txtReturn";
            this.txtReturn.Size = new System.Drawing.Size(887, 190);
            this.txtReturn.TabIndex = 4;
            // 
            // frm_WCFInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 437);
            this.Controls.Add(this.grp_Return);
            this.Controls.Add(this.grp_Send);
            this.Name = "frm_WCFInterface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_WCFInterface";
            this.Load += new System.EventHandler(this.frm_WCFInterface_Load);
            this.grp_Send.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtInterface.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFuncParam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFuncName.Properties)).EndInit();
            this.grp_Return.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtReturn.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_Send;
        private System.Windows.Forms.GroupBox grp_Return;
        private DevExpress.XtraEditors.LookUpEdit txtFuncName;
        private DevExpress.XtraEditors.MemoEdit txtFuncParam;
        private DevExpress.XtraEditors.MemoEdit txtReturn;
        private System.Windows.Forms.Button btn_SendMsg;
        private DevExpress.XtraEditors.LookUpEdit txtInterface;
    }
}

