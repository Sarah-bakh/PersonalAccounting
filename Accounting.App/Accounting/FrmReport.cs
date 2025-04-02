using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.DataLayer.Context;
using Accounting.Utility.convert;
using Accounting.ViewModels.Customer;

namespace Accounting.App
{
    public partial class FrmReport : Form
    {
        public int TypeID = 0;
        public FrmReport()
        {
            InitializeComponent();
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db=new UnitOfWork())
            {
                List<ListCustomerViewModel> list = new List<ListCustomerViewModel>();
                list.Add(new ListCustomerViewModel() {CustomerID=0,FullName="انتخاب کنید" });

                list.AddRange(db.customerRepository.GetNameCustomers());
                cbCustomer.DataSource = list;
                cbCustomer.DisplayMember = "FullName";
                cbCustomer.ValueMember = "customerID";
            }
                if (TypeID == 1)
                {
                    this.Text = "گزارش دریافتی ها ";
                }
                else
                {
                    this.Text = "گزارش پرداختی ها ";
                }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Filter();
           

        }
        //Filter
        void Filter()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                List<DataLayer.Accounting> Result = new List<DataLayer.Accounting>();
                //DateTime ? startDate;
                //DateTime ? endDate;
             //  string date=txtFromDate.Text;
                if ((int)cbCustomer.SelectedValue!=0)
                {
                    int customerID = int.Parse(cbCustomer.SelectedValue.ToString());
                    Result.AddRange(db.AcccountingRepository.GET(a=>a.TypeID==TypeID && a.CustomerID== customerID));
                }
                else
                {
                    Result.AddRange(db.AcccountingRepository.GET(a => a.TypeID == TypeID));
                }
                //if (txtFromDate.Text!= "    /  /  ")
                //{
                //   // startDate=Convert.ToDateTime(txtFromDate.Text);
                //}
                //if (txtToDate.Text != "    /  /  ")
                //{
                //    endDate=Convert.ToDateTime(txtToDate.Text);
                //}
                    
               
                dgReport.Rows.Clear();
                foreach (var accounting in Result)
                {
                    string accountingName = db.customerRepository.GetCustomerNameByID(accounting.CustomerID).ToString();


                    dgReport.Rows.Add(accounting.ID, accountingName, accounting.Amount, accounting.DateTime.ToShamci(), accounting.Description);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRfresh_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                if (RtlMessageBox.Show("آیا از حذف سطر انتخاب شده مطمن هستید؟", "هشدار", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                    using (UnitOfWork db = new UnitOfWork())
                    {
                        db.AcccountingRepository.Delete(id);
                        db.Save();
                        Filter();
                    }
                }

            }
            else
            {
                RtlMessageBox.Show("لطفا سطر مورد نظر را انتخاب نمایید");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                using (UnitOfWork db = new UnitOfWork())
                {
                   FrmNewAccounting frmNew = new FrmNewAccounting();    
                    frmNew.AccountingID = id;
                    frmNew.Enabled = true;
                    if (frmNew.ShowDialog()==DialogResult.OK)
                    {
                        Filter();
                    }
                }

            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dtPrint = new DataTable();
            dtPrint.Columns.Add("Customer");
            dtPrint.Columns.Add("Amount");
            dtPrint.Columns.Add("Date");
            dtPrint.Columns.Add("Description");
            foreach (DataGridViewRow item in dgReport.Rows)
            {
                dtPrint.Rows.Add(
                    item.Cells[0].Value.ToString(),
                    item.Cells[1].Value.ToString(),
                    item.Cells[2].Value.ToString(),
                    item.Cells[3].Value.ToString()

                    );

            }
            stiPrint.Load(Application.StartupPath+"/Report.mrt");
            stiPrint.RegData("DT",dtPrint);
            stiPrint.Show();
        }
    }
}
