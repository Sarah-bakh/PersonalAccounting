﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DataLayer.Context;
using Accounting.ViewModels.Accounting;

namespace Accounting.Busineess
{
    public class Account
    {
        public static ReportViewModel ReportFromMain()
        {
            ReportViewModel rp = new ReportViewModel();
            using (UnitOfWork db=new UnitOfWork())
            {
                DateTime startDate=new DateTime(DateTime.Now.Year,DateTime.Now.Month,01);
                DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month,30);
                var recive = db.AcccountingRepository.GET(a=>a.TypeID==1 && a.DateTime>=startDate&&a.DateTime<=endDate).Select(a=>a.Amount).ToList();
                var pay = db.AcccountingRepository.GET(a => a.TypeID == 2 && a.DateTime >= startDate && a.DateTime <= endDate).Select(a => a.Amount).ToList();
                rp.Recive = recive.Sum();
                rp.Pay = pay.Sum();
                rp.AccountBalance = (recive.Sum()- pay.Sum());
            }
                return rp;
        }
    }
}
