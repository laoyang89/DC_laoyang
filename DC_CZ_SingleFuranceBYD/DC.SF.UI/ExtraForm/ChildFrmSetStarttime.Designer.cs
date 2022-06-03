namespace DC.SF.UI.ExtraForm
{
    partial class ChildFrmSetStarttime
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
            this.lblStarttime = new System.Windows.Forms.Label();
            this.txtStarttime = new System.Windows.Forms.TextBox();
            this.btnConfirmSetstarttime = new System.Windows.Forms.Button();
            this.btnCancelSetstarttime = new System.Windows.Forms.Button();
            this.lblEndtime = new System.Windows.Forms.Label();
            this.txtEndtime = new System.Windows.Forms.TextBox();
            this.lblTimeType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStarttime
            // 
            this.lblStarttime.AutoSize = true;
            this.lblStarttime.Location = new System.Drawing.Point(74, 98);
            this.lblStarttime.Name = "lblStarttime";
            this.lblStarttime.Size = new System.Drawing.Size(115, 14);
            this.lblStarttime.TabIndex = 0;
            this.lblStarttime.Text = "设置工艺开始时间：";
            // 
            // txtStarttime
            // 
            this.txtStarttime.Location = new System.Drawing.Point(213, 96);
            this.txtStarttime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStarttime.Name = "txtStarttime";
            this.txtStarttime.Size = new System.Drawing.Size(154, 22);
            this.txtStarttime.TabIndex = 1;
            // 
            // btnConfirmSetstarttime
            // 
            this.btnConfirmSetstarttime.Location = new System.Drawing.Point(76, 219);
            this.btnConfirmSetstarttime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfirmSetstarttime.Name = "btnConfirmSetstarttime";
            this.btnConfirmSetstarttime.Size = new System.Drawing.Size(84, 29);
            this.btnConfirmSetstarttime.TabIndex = 2;
            this.btnConfirmSetstarttime.Text = "确认设置";
            this.btnConfirmSetstarttime.UseVisualStyleBackColor = true;
            this.btnConfirmSetstarttime.Click += new System.EventHandler(this.btnConfirmSetstarttime_Click);
            // 
            // btnCancelSetstarttime
            // 
            this.btnCancelSetstarttime.Location = new System.Drawing.Point(253, 219);
            this.btnCancelSetstarttime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancelSetstarttime.Name = "btnCancelSetstarttime";
            this.btnCancelSetstarttime.Size = new System.Drawing.Size(84, 29);
            this.btnCancelSetstarttime.TabIndex = 5;
            this.btnCancelSetstarttime.Text = "取消设置";
            this.btnCancelSetstarttime.UseVisualStyleBackColor = true;
            this.btnCancelSetstarttime.Click += new System.EventHandler(this.btnCancelSetstarttime_Click);
            // 
            // lblEndtime
            // 
            this.lblEndtime.AutoSize = true;
            this.lblEndtime.Location = new System.Drawing.Point(74, 155);
            this.lblEndtime.Name = "lblEndtime";
            this.lblEndtime.Size = new System.Drawing.Size(115, 14);
            this.lblEndtime.TabIndex = 6;
            this.lblEndtime.Text = "设置工艺结束时间：";
            // 
            // txtEndtime
            // 
            this.txtEndtime.Location = new System.Drawing.Point(213, 149);
            this.txtEndtime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEndtime.Name = "txtEndtime";
            this.txtEndtime.Size = new System.Drawing.Size(154, 22);
            this.txtEndtime.TabIndex = 7;
            // 
            // lblTimeType
            // 
            this.lblTimeType.AutoSize = true;
            this.lblTimeType.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeType.Location = new System.Drawing.Point(95, 28);
            this.lblTimeType.Name = "lblTimeType";
            this.lblTimeType.Size = new System.Drawing.Size(244, 19);
            this.lblTimeType.TabIndex = 8;
            this.lblTimeType.Text = "时间格式：2000/01/01 23:00:00";
            // 
            // ChildFrmSetStarttime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 295);
            this.Controls.Add(this.lblTimeType);
            this.Controls.Add(this.txtEndtime);
            this.Controls.Add(this.lblEndtime);
            this.Controls.Add(this.btnCancelSetstarttime);
            this.Controls.Add(this.btnConfirmSetstarttime);
            this.Controls.Add(this.txtStarttime);
            this.Controls.Add(this.lblStarttime);
            this.MinimizeBox = false;
            this.Name = "ChildFrmSetStarttime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出入炉时间";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStarttime;
        private System.Windows.Forms.TextBox txtStarttime;
        private System.Windows.Forms.Button btnConfirmSetstarttime;
        private System.Windows.Forms.Button btnCancelSetstarttime;
        private System.Windows.Forms.Label lblEndtime;
        private System.Windows.Forms.TextBox txtEndtime;
        private System.Windows.Forms.Label lblTimeType;
    }
}