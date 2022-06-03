using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace WCFInterfaceClient
{
    public partial class frm_WCFInterface : Form
    {
        public frm_WCFInterface()
        {
            InitializeComponent();
        }

        private void frm_WCFInterface_Load(object sender, EventArgs e)
        {
            DataTable dt_interface = new DataTable();
            dt_interface.Columns.Add("name");
            dt_interface.Columns.Add("value");
            DataRow dr_interface1 = dt_interface.NewRow();
            dr_interface1[0] = "ICommonService";
            dr_interface1[1] = "0";
            dt_interface.Rows.Add(dr_interface1);

            DataRow dr_interface2 = dt_interface.NewRow();
            dr_interface2[0] = "IExpandingBusinessService";
            dr_interface2[1] = "1";
            dt_interface.Rows.Add(dr_interface2);

            txtInterface.Properties.DisplayMember = "name";
            txtInterface.Properties.ValueMember = "value";
            txtInterface.Properties.DataSource = dt_interface;
        }

        private void txtFuncName_EditValueChanged(object sender, EventArgs e)
        {
            Type finfo = null;
            //ICommonService
            if (txtInterface.Text.Equals("ICommonService"))
            {
                finfo = typeof(ICommonService);
            }
            else if (txtInterface.Text.Equals("IExpandingBusinessService"))
            {
                finfo = typeof(IExpandingBusinessService);
            }

            if (finfo == null)
            {
                this.txtReturn.Text = "加载接口信息失败-2!";
                return;
            }
            MethodInfo method = finfo.GetMethod(txtFuncName.Text);
            //参数集合
            ParameterInfo[] paramInfos = method.GetParameters();
            txtFuncParam.Text = "";
            foreach (ParameterInfo item in paramInfos)
            {
                string strValue = "";
                if(txtFuncName.Text.Equals("Login")
                    && item.Name.Equals("UserName"))
                {
                    strValue = "testeq";
                }
                else if (txtFuncName.Text.Equals("Login")
                    && item.Name.Equals("Password"))
                {
                    strValue = "testeq1";
                }
                txtFuncParam.Text += item.Name + "("+ item.ParameterType.ToString() + ")="+ strValue;
                txtFuncParam.Text += System.Environment.NewLine;
            }
            return;
        }

        private void btn_SendMsg_Click(object sender, EventArgs e)
        {
            try
            {
                CommonServiceClient comm_service = new CommonServiceClient();
                ExpandingBusinessServiceClient exp_service = new ExpandingBusinessServiceClient();

                switch (txtFuncName.Text)
                {
                    case "GetData":
                        int ivalue = 0;
                        string[] values = txtFuncParam.Lines[0].Split('=');
                        try
                        {
                            if(values.Length<2)
                            {
                                this.txtReturn.Text = "参数有误";
                            }
                            else
                            {
                                ivalue = int.Parse(values[1]);
                                this.txtReturn.Text = comm_service.GetData(ivalue);
                            }
                            
                        }
                        catch(Exception ex)
                        {
                            this.txtReturn.Text = ex.Message;
                        }
                        break;
                    case "GetServerDateTime":
                        try
                        {
                            this.txtReturn.Text = comm_service.GetServerDateTime().ToString();

                        }
                        catch (Exception ex)
                        {
                            this.txtReturn.Text = ex.Message;
                        }
                        break;
                    case "Login":
                        try
                        {
                            string[] Login_values1 = txtFuncParam.Lines[0].Split('=');
                            string[] Login_values2 = txtFuncParam.Lines[1].Split('=');
                            string[] Login_values3 = txtFuncParam.Lines[2].Split('=');
                            string[] Login_values4 = txtFuncParam.Lines[3].Split('=');
                            if (Login_values1.Length < 2
                                || Login_values2.Length<2)
                            {
                                this.txtReturn.Text = "参数有误";
                            }
                            else
                            {
                                string DataSetID = "";
                                if (Login_values3.Length>1)
                                {
                                    DataSetID = Login_values3[1];
                                }
                                string DataSetDBName = "";
                                if (Login_values4.Length > 1)
                                {
                                    DataSetDBName = Login_values4[1];
                                }
                                this.txtReturn.Text = comm_service.Login(Login_values1[1], Login_values2[1], DataSetID, DataSetDBName);
                            }

                        }
                        catch (Exception ex)
                        {
                            this.txtReturn.Text = ex.Message;
                        }
                        break;
                    case "Logout":
                        try
                        {
                            string[] Logout_values1 = txtFuncParam.Lines[0].Split('=');
                            string[] Logout_values2 = txtFuncParam.Lines[1].Split('=');
                            string[] Logout_values3 = txtFuncParam.Lines[2].Split('=');
                            string[] Logout_values4 = txtFuncParam.Lines[3].Split('=');
                            if (Logout_values1.Length < 2
                                || Logout_values2.Length < 2)
                            {
                                this.txtReturn.Text = "参数有误";
                            }
                            else
                            {
                                string DataSetID = "";
                                if (Logout_values3.Length > 1)
                                {
                                    DataSetID = Logout_values3[1];
                                }
                                string DataSetDBName = "";
                                if (Logout_values4.Length > 1)
                                {
                                    DataSetDBName = Logout_values4[1];
                                }
                                this.txtReturn.Text = comm_service.Logout(Logout_values1[1], Logout_values2[1], DataSetID, DataSetDBName);
                            }

                        }
                        catch (Exception ex)
                        {
                            this.txtReturn.Text = ex.Message;
                        }
                        break;
                    case "GetCommonDataDict":
                        try
                        {
                            string[] GetCommonDataDict_values1 = txtFuncParam.Lines[0].Split('=');
                            string[] GetCommonDataDict_values2 = txtFuncParam.Lines[1].Split('=');
                            if (GetCommonDataDict_values1.Length < 2
                                || GetCommonDataDict_values2.Length < 2)
                            {
                                this.txtReturn.Text = "参数有误";
                            }
                            else
                            {
                                this.txtReturn.Text = comm_service.GetCommonDataDict(GetCommonDataDict_values1[1]
                                    , GetCommonDataDict_values2[1]);
                            }

                        }
                        catch (Exception ex)
                        {
                            this.txtReturn.Text = ex.Message;
                        }
                        break;
                    case "PassStationCheck":
                        try
                        {
                            string[] PassStationCheck_values1 = txtFuncParam.Lines[0].Split('=');
                            string[] PassStationCheck_values2 = txtFuncParam.Lines[1].Split('=');
                            string[] PassStationCheck_values3 = txtFuncParam.Lines[2].Split('=');
                            string[] PassStationCheck_values4 = txtFuncParam.Lines[3].Split('=');
                            string[] PassStationCheck_values5 = txtFuncParam.Lines[4].Split('=');
                            string[] PassStationCheck_values6 = txtFuncParam.Lines[5].Split('=');
                            if (PassStationCheck_values1.Length < 2
                                || PassStationCheck_values2.Length < 2
                                || PassStationCheck_values3.Length < 2
                                || PassStationCheck_values4.Length < 2
                                || PassStationCheck_values5.Length < 2
                                || PassStationCheck_values6.Length < 2)
                            {
                                this.txtReturn.Text = "参数有误";
                            }
                            else
                            {
                                this.txtReturn.Text = exp_service.PassStationCheck(PassStationCheck_values1[1]
                                    , PassStationCheck_values2[1]
                                    , PassStationCheck_values3[1]
                                    , PassStationCheck_values4[1]
                                    , PassStationCheck_values5[1]
                                    , PassStationCheck_values6[1]);
                            }

                        }
                        catch (Exception ex)
                        {
                            this.txtReturn.Text = ex.Message;
                        }
                        break;
                }
                
            }
            catch
            {

            }
        }

        private void txtInterface_EditValueChanged(object sender, EventArgs e)
        {
            Type finfo = null;
            //ICommonService
            if (txtInterface.Text.Equals("ICommonService"))
            {
                finfo = typeof(ICommonService);
            }
            else if (txtInterface.Text.Equals("IExpandingBusinessService"))
            {
                finfo = typeof(IExpandingBusinessService);
            }

            if (finfo==null)
            {
                this.txtReturn.Text = "加载接口信息失败-1!";
                return;
            }
            int iNum = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("value");
            foreach (MethodInfo m in finfo.GetMethods())
            {
                DataRow dr_GetData = dt.NewRow();
                dr_GetData[0] = m.Name;
                dr_GetData[1] = iNum.ToString();
                dt.Rows.Add(dr_GetData);
                iNum++;
                Console.WriteLine("Method:{0}", m.Name);
            }

            txtFuncName.Properties.DisplayMember = "name";
            txtFuncName.Properties.ValueMember = "value";
            txtFuncName.Properties.DataSource = dt;
        }
    }
}
