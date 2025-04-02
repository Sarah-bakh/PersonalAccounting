using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Repository;
using Accounting.ViewModels.Customer;


namespace Accounting.DataLayer.Services
{
    public class CustomerRepository : ICustomerRepository
    {
       
        private Accounting_DBEntities db ;
        public CustomerRepository(Accounting_DBEntities context) // constractor method
        {
            db = context;
        }

        public List<Customer> GetAllCustomers()
        {
            return db.Customers.ToList();
        }
        IEnumerable<Customer> ICustomerRepository.GetCustomerByFilter(string parameter)
        {
            return db.Customers.Where(c => c.FullName.Contains(parameter) || c.Email.Contains(parameter) || c.Mobile.Contains(parameter)).ToList();
        }

        public Customer GetCustomersByID(int customerId)
        {
            return db.Customers.Find(customerId);
        }
        List<ListCustomerViewModel> ICustomerRepository.GetNameCustomers(string filter)
        {
            if (filter == "")
            {
                return db.Customers.Select(c=> new ListCustomerViewModel()
                {
                    CustomerID=c.CustomerID,
                    FullName=c.FullName
                }).ToList();
            }
            return db.Customers.Where(c => c.FullName.Contains(filter)).Select(c => new ListCustomerViewModel()
            {
                CustomerID = c.CustomerID,
                FullName = c.FullName
            }).ToList();
        }
       

        public bool InsertCustomer(Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                return true;
            }
            catch
            {
                return false;   
                
            }
        }

       
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                var local = db.Set<Customer>()
                         .Local
                         .FirstOrDefault(f => f.CustomerID == customer.CustomerID);
                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }
                db.Entry(customer).State=EntityState.Modified; 
                return true;
            }
            catch
            {
                return false;

            }
        }
        public bool DeleteCustomer(Customer customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;

            }
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                var customer = GetCustomersByID(customerId);
                DeleteCustomer(customer);   
                return true;
            }
            catch
            {
                return false;

            }
        }

        int ICustomerRepository.GetCustomerIDByName(string Name)
        {
            return db.Customers.First(c=>c.FullName==Name).CustomerID;
        }

        string ICustomerRepository.GetCustomerNameByID(int customerId)
        {
           return db.Customers.Find(customerId).FullName;
        }
    }
}
