using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Services;
using System.Data.Entity;
using Accounting.DataLayer.Repository;
using Accounting.DataLayer;
using Accounting.DataLayer.Context;


namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork db = new UnitOfWork();
            db.customerRepository.GetAllCustomers();
           

       
        }
    }
}
