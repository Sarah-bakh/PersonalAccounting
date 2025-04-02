using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Repository;
using Accounting.DataLayer.Services;

namespace Accounting.DataLayer.Context
{
    public class UnitOfWork : IDisposable
    {
        Accounting_DBEntities db = new Accounting_DBEntities();

        private ICustomerRepository _customerRepository; // implimentor class  2
        private GenricRepository<Accounting> _accountingRepository;
        private GenricRepository<Login> _loginRepository;
        
        public ICustomerRepository customerRepository
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository=new CustomerRepository(db);   
                }
                return _customerRepository;
            }
        }

        public GenricRepository<Accounting> AcccountingRepository
        {
            get
            {
                if (_accountingRepository == null) //اگر از روش نمونه ساخته نشده بود
                {
                    _accountingRepository = new GenricRepository<Accounting>(db);
                }
                return _accountingRepository;

            }
        }
        public GenricRepository<Login> LoginRepository 
        {
            get 
            {
                if (_loginRepository==null)
                {
                    _loginRepository=new GenricRepository<Login>(db);
                }
                return _loginRepository;
            }

        }
        public void Save()
        {
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();   
        }
    }
}
