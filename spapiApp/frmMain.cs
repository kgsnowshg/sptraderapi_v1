using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;


namespace spapiApp
{
    
    public partial class frmMain : Form
    {
        delegate void SetTextCallback(string str);   //定义委托
        public frmMain()
        {
            InitializeComponent();
            Spcommon.APIDLL.RegisterShowTextEvent(PrintMainData);


            if (Spcommon.S_Prot == 8080)
            {
                btnAcc.Visible = false;
                btnRelease.Visible = false;
                this.Text = "SP API Trader [Client:" + Spcommon.S_Userid + "]";
            }
            else
            {
                this.Text = "SP API Trader [AE:" + Spcommon.S_Userid + "]";
            }

            PrintMainData("Business Date: " + Spcommon.APIDLL.unixTimeToStandTime(Spcommon.Business_Date, 2) + "[" + Spcommon.Business_Date.ToString() + "]");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string login_info;
            login_info = "User Id:" + Spcommon.acc_no + " max_bal：" + Spcommon.max_bal.ToString() + " max_pos:" + Spcommon.max_pos.ToString() +" max_order:"+ Spcommon.max_order.ToString();
            //this.lbxMainData.Items.Add(login_info);           
        }

        public  void PrintMainData(string text)
        {
            if (lbxMainData.InvokeRequired)  //控件是否跨线程？如果是，则执行括号里代码
            {
                SetTextCallback setListCallback = new SetTextCallback(PrintMainData);   //实例化委托对象
                lbxMainData.Invoke(setListCallback, text);   //重新调用SetListBox函数
            }
            else  //否则，即是本线程的控件，控件直接操作
            {
                lbxMainData.Items.Add(text);
                lbxMainData.SelectedIndex = lbxMainData.Items.Count - 1;
            }
        }

        private void btnOrderCount_Click(object sender, EventArgs e)
        {
            //LoadInput(16);
            if (Spcommon.S_Prot == 8080)
                Spcommon.APIDLL.R_SPAPI_GetOrderCount();
            else
                LoadInput(19);
        }

        private void btnOrderAdd_Click(object sender, EventArgs e)
        {
            LoadInput(1);
            
        }

        private void btnOrderAll_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetOrder();
           
        }

        private void btnOrderDel_Click(object sender, EventArgs e)
        {
            LoadInput(2);
        }

        private void btnPosByProd_Click(object sender, EventArgs e)
        {
            LoadInput(4);
        }

        private void btnOrderChange_Click(object sender, EventArgs e)
        {
            LoadInput(3);
        }

        private void btnTradeByProd_Click(object sender, EventArgs e)
        {
            LoadInput(5);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadInput(6);
        }

        private void button101_Click(object sender, EventArgs e)
        {
            LoadInput(101);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            LoadInput(7);
        }

        public void LoadInput(int idx)
        {
            frmInput Input = new frmInput(idx);
            Input.Width = 306;
            Input.Height = 225;
            Input.ShowDialog();
        }
     
        private void btnPosCount_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetPosCount();
        }

        private void btnPosAll_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetPos();
        }

        private void btnTradeCount_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetTradeCount();
        }

        private void btnTradeAll_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetTrade();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetPriceCount();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetPrice();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Confirm to Exit?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                int rc;
                rc = Spcommon.APIDLL.R_SPAPI_Logout();
                if (rc == 0)
                {
                    //Spcommon.APIDLL.R_SPAPI_Uninitialize();
                    Application.ExitThread();
                }
            }
            else
                e.Cancel = true;
        }

        private void btnPrice_Click(object sender, EventArgs e)
        {
            LoadInput(8);
        }

        private void btnSubTicker_Click(object sender, EventArgs e)
        {
            LoadInput(9);
        }

        private void btnUniTicker_Click(object sender, EventArgs e)
        {
            LoadInput(10);
        }

        private void btnAccInfo_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetAccInfo();
        }

        private void btnBalCount_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetAccBalCount();
        }

        private void btnBalAll_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetAccBal();
        }

        private void btnBalByCcy_Click(object sender, EventArgs e)
        {
            LoadInput(11);
        }

        private void btnVersion_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetDllVersion();
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            LoadInput(12);
        }

        private void btnOrderInactive_Click(object sender, EventArgs e)
        {
            LoadInput(13);
        }

        private void btnInstInfoCount_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetInstrumentAndCount();
        }

        private void btnProdInfoCount_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetProductAndCount();
        }

        private void btnInstByCode_Click(object sender, EventArgs e)
        {
            LoadInput(14);
        }

        private void btnProdByCode_Click(object sender, EventArgs e)
        {
            LoadInput(15);
        }

        private void btnTradeReport_Click(object sender, EventArgs e)
        {
            //LoadInput(17);
            Spcommon.APIDLL.R_SPAPI_LoadTradeReport();
        }

        private void btnDllVers_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetDllVersionDouble();
        }

        private void btnOrderReport_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_LoadOrderReport();
        }

        private void btnchangepsw_Click(object sender, EventArgs e)
        {
            LoadInput(16);
        }

        private void btnLoadinst_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_LoadInstrumentList();
        }

        private void btnLoadprod_Click(object sender, EventArgs e)
        {
            //Spcommon.APIDLL.R_SPAPI_LoadProductInfoList();
            //load all product hide
        }

        private void btnLoadInstByCode_Click(object sender, EventArgs e)
        {
            LoadInput(17);
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            LoadInput(18);
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_AccountLogout();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Spcommon.login_status == 0)
            {
                //frmLogin login = new frmLogin();
                //login.ShowDialog();
                Spcommon.APIDLL.R_SPAPI_SetBackgroundPoll(true);
                Spcommon.APIDLL.R_SPAPI_SetLoginInfo(Spcommon.S_Server, Spcommon.S_Prot, Spcommon.S_License, Spcommon.S_App_id, Spcommon.S_Userid, Spcommon.S_Password);
                Spcommon.APIDLL.R_SPAPI_Login();
            }
            else
            {
                Spcommon.APIDLL.LoginStatusBack(Spcommon.login_status);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            int rc;
            rc = Spcommon.APIDLL.R_SPAPI_Logout();
            Console.WriteLine("Logout:" + rc.ToString());
        }

        private void btnSetLogPath_Click(object sender, EventArgs e)
        {
            LoadInput(20);
        }

        private void btnCtrlLevel_Click(object sender, EventArgs e)
        {
            LoadInput(21);
        }

        private void btnccyrcount_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetCcyRateCount();
        }

        private void btnccyrate_Click(object sender, EventArgs e)
        {
            Spcommon.APIDLL.R_SPAPI_GetCcyRate();
        }

        private void btnbyccy_Click(object sender, EventArgs e)
        {
            LoadInput(22);
        }
    }
}
