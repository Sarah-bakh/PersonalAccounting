﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using ValidationComponents;

namespace Accounting.App
{
    public partial class FrmNewAccounting : Form
    {
        public int AccountingID = 0;  //CHECK FORM SITUATION IS EDIT NEXT FORM?
        public FrmNewAccounting()
        {
            InitializeComponent();
        }
       

        private void FrmNewAccounting_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork()) {
                dgvCustomers.AutoGenerateColumns = false;
                dgvCustomers.DataSource = db.customerRepository.GetNameCustomers();
                if (AccountingID!=0)
                {
                    var account=db.AcccountingRepository.GetById(AccountingID);
                    txtAmount.Text = account.Amount.ToString();
                    txtDescription.Text = account.Description.ToString();
                    txtName.Text=db.customerRepository.GetCustomerNameByID(account.CustomerID);
                    if (account.TypeID==1) 
                    {
                        rbRecive.Checked = true;
                    }
                    else
                    {
                        rbPay.Checked = false;
                    }
                    this.Text = "ویرایش";
                    btnSave.Text = "ویرایش";
                }
            }

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCustomers.AutoGenerateColumns = false;
                dgvCustomers.DataSource = db.customerRepository.GetNameCustomers(txtFilter.Text);
            }

        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork()) {

                if (BaseValidator.IsFormValid(this.components))
                {
                    if (rbPay.Checked || rbRecive.Checked)
                    {

                        DataLayer.Accounting accounting = new DataLayer.Accounting()
                        {
                            Amount = int.Parse(txtAmount.Value.ToString()),
                            CustomerID = db.customerRepository.GetCustomerIDByName(txtName.Text),
                            TypeID = (rbRecive.Checked) ? 1 : 2,
                            DateTime = DateTime.Now,
                            Description = txtDescription.Text

                        };
                        Console.WriteLine($"BEFORE SAVE :{accounting.ID}");
                        if (AccountingID==0) 
                        {
                            db.AcccountingRepository.Insert(accounting);
                        }
                        else
                        {
                            accounting.ID = AccountingID;
                            db.AcccountingRepository.Update(accounting);
                        }
                            db.Save();

                        DialogResult = DialogResult.OK;
                    }
                    else

                    {
                        RtlMessageBox.Show("لطفا نوع تراکنش را مشخص نمایید.");
                    }

                }

            }
          
        }
            
    }
}
