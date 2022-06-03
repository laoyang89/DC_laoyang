using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DC.SF.UI
{
    public partial class ShowItemControl : DevExpress.XtraEditors.XtraUserControl
    {
        public string Title;
        public string Value;
        public Color valueColor;
        public ShowItemControl()
        {
            InitializeComponent();
            Title = "Title:";
            Value = "Value";
            valueColor = Color.Black;
        }

        private void ShowItemControl_Load(object sender, EventArgs e)
        {
            lblTitle.Text = Title;
            lblValue.Text = Value;
            lblValue.ForeColor = valueColor;
        }

        public void RefreshView(string value, Color color)
        {
            lblValue.Text = value;
            lblValue.ForeColor = color;
        }
        
    }
}
