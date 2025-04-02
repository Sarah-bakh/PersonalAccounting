using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.ViewModels.Customer;

namespace Accounting.DataLayer.Repository
{
    public interface ICustomerRepository
    {
        
        
            List<Customer> GetAllCustomers();
            IEnumerable<Customer> GetCustomerByFilter(string parameter);
            Customer GetCustomersByID(int customerId);
            List<ListCustomerViewModel> GetNameCustomers(string filter ="");
            bool InsertCustomer(Customer customer);
            bool UpdateCustomer(Customer customer);
            bool DeleteCustomer(Customer customer);
            bool DeleteCustomer(int customerId);
           int GetCustomerIDByName(string Name);
           string GetCustomerNameByID(int customerId);
           

        
    }
}
