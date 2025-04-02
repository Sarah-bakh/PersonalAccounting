using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.DataLayer.Context;

namespace Accounting.App
{
    public partial class FrmCustomers : Form
    {
        public FrmCustomers()
        {
            InitializeComponent();
        }

        private void FrmCustomers_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        void BindGrid()
        {
            using (UnitOfWork db =new UnitOfWork())
            {
                dgvCustomers.AutoGenerateColumns=false;
                dgvCustomers.DataSource = db.customerRepository.GetAllCustomers();
            }
        }

        private void btnRefreshCustomer_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
           BindGrid();

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCustomers.DataSource=db.customerRepository.GetCustomerByFilter(txtFilter.Text);
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow!=null)
            {
                using (UnitOfWork db=new UnitOfWork())
                {
                    string name = dgvCustomers.CurrentRow.Cells[1].Value.ToString();
                    if (RtlMessageBox.Show($"آیا از حذف {name}مطمن هستید","توجه",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
                    {
                        int customerid = int.Parse(dgvCustomers.CurrentRow.Cells[0].Value.ToString());

                        db.customerRepository.DeleteCustomer(customerid);
                        db.Save();
                        BindGrid();

                    }

                }

            }
            else
            {
                RtlMessageBox.Show("لطفا شخصی را انتخاب کنید .");

            }
        }

        private void btnAddNewCustomer_Click(object sender, EventArgs e)
        {
            FrmAddOrEditCustomercs frmAdd = new FrmAddOrEditCustomercs();
            frmAdd.ShowDialog();
            if (frmAdd.ShowDialog()==DialogResult.OK)  // رفرش فرم زیرین
            {
                BindGrid(); 
            }
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            int customerId = int.Parse(dgvCustomers.CurrentRow.Cells[0].Value.ToString());
            FrmAddOrEditCustomercs frmAddOrEdit =new FrmAddOrEditCustomercs();
             frmAddOrEdit.CustomerID= customerId;
            if (frmAddOrEdit.ShowDialog()==DialogResult.OK)
            {
                BindGrid();
            }
        }
    }
}
