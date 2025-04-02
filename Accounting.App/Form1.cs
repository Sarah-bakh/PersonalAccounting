using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.App.Properties;
using Accounting.Utility.convert;
using Accounting.ViewModels.Accounting;
using Accounting.Busineess;
using Accounting.DataLayer;

namespace Accounting.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            FrmCustomers frmCustomers = new FrmCustomers();
            frmCustomers.ShowDialog();
        }

        private void btnNewAccounting_Click(object sender, EventArgs e)
        {
          
            FrmNewAccounting frmNewAccounting = new FrmNewAccounting();
            frmNewAccounting.Enabled = true;
            frmNewAccounting.ShowDialog();
          
        }

        private void btnReportPay_Click(object sender, EventArgs e)
        {
            FrmReport frmReport= new FrmReport();
            frmReport.TypeID = 2;
            frmReport.Enabled = true;
            frmReport.ShowDialog();
        }

        private void btnReportRecive_Click(object sender, EventArgs e)
        {
            FrmReport frmReport= new FrmReport();
            frmReport.TypeID = 1;
            frmReport.Enabled = true;
            frmReport.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.Hide();
            frmLogin frmLogin = new frmLogin();
            if (frmLogin.ShowDialog()==DialogResult.OK)
            {
                this.Show();
                lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
                lblDate.Text = DateConvertor.ToShamci(DateTime.Now);
                Report();
            }
            else
            {
                Application.Exit();
            }
        }
        void Report()
        {
            ReportViewModel report = Account.ReportFromMain();
            lblPay.Text=report.Pay.ToString("#,0");
            lblRecive.Text=report.Recive.ToString("#,0");
            accountLable.Text=report.AccountBalance.ToString("#,0"); 
        }
        private void timerForLBL_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnEditLogin_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
           frmLogin.IsEdit = true;  
            frmLogin.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Report();
        }
    }
}
