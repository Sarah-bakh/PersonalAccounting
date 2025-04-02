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
using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using ValidationComponents;

namespace Accounting.App
{
    public partial class FrmAddOrEditCustomercs : Form
    {
        public int CustomerID = 0;
        public FrmAddOrEditCustomercs()
        {
            InitializeComponent();
        }
        UnitOfWork db = new UnitOfWork();
        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog()==DialogResult.OK)
            {
                pcCustomer.ImageLocation = openFile.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                string location = pcCustomer.ImageLocation;
                string imageName=Guid.NewGuid().ToString()+Path.GetExtension(location);
                string path = Application.StartupPath + "/image/";//masir
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                pcCustomer.Image.Save(path+imageName);
               
               
                
                Customer customer = new Customer()
                {
                    Adress=txtAdress.Text,
                    FullName=txtName.Text,
                    Mobile=txtMobile.Text,
                    CustomerImage= imageName,
                    Email=txtMail.Text
                };
                if (CustomerID == 0)
                {
                    db.customerRepository.InsertCustomer(customer);
                }
                else
                {
                    customer.CustomerID = CustomerID;
                    db.customerRepository.UpdateCustomer(customer);
                }
                    db.Save();
                    DialogResult = DialogResult.OK;
                
            }
        }
        // edit form load
        private void FrmAddOrEditCustomercs_Load(object sender, EventArgs e)
        {
            if (CustomerID!=0)
            {
                this.Text = "ویرایش شخص";
                btnSave.Text = "ویرایش";
                var customer = db.customerRepository.GetCustomersByID(CustomerID);
                txtMail.Text = customer.Email;
                txtName.Text = customer.FullName;
                txtMobile.Text = customer.Mobile;
                txtAdress.Text = customer.Adress;
                pcCustomer.ImageLocation = Application.StartupPath + "/image/"+customer.CustomerImage;
            }
        }
    }
}
