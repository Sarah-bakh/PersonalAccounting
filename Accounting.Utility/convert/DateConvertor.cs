using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Utility.convert
{
    public  static class DateConvertor
    {
        public static string ToShamci(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date).ToString()+"/"+pc.GetMonth(date).ToString("00")+"/"+pc.GetDayOfMonth(date).ToString("00");
        }
        public static DateTime ToMiladi(DateTime datetime)
        {
            return new DateTime(datetime.Year,datetime.Month,datetime.Day,new System.Globalization.PersianCalendar ());
        }
    }
}
