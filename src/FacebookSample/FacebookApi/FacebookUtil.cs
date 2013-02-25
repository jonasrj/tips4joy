using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookApi
{
    internal class FacebookUtil
    {
        internal static Dictionary<string, string> monthName;

        static FacebookUtil()
        {
            monthName = new Dictionary<string, string>();
            monthName.Add("01", "JAN");
            monthName.Add("02", "FEV");
            monthName.Add("03", "MAR");
            monthName.Add("04", "ABR");
            monthName.Add("05", "MAI");
            monthName.Add("06", "JUN");
            monthName.Add("07", "JUL");
            monthName.Add("08", "AUG");
            monthName.Add("09", "SET");
            monthName.Add("10", "OUT");
            monthName.Add("11", "NOV");
            monthName.Add("12", "DEZ");
        }

        internal static string GetDay(string date)
        {
            return date.Substring(3, 2);
        }

        internal static string GetMounth(string date)
        {
            return date.Substring(0, 2);
        }

        internal static string GetMounthName(string date)
        {
            return FacebookUtil.monthName[date.Substring(0, 2)];
        }

        internal static string CalcDiffUntilBirthDay(int day, int mounth)
        {
            DateTime dBirthDay;
            int dateDiff = 0;
            string aux = string.Empty;


            dBirthDay = new DateTime(DateTime.Now.Year, mounth, day).Date;
            dateDiff = GetDaysBeforeBirthday(dBirthDay);

            if (dateDiff == 0)
            {
                return "É hoje!";
            }
            else
            {
                if (dateDiff == 1) { aux = "Falta {0} dia!"; } else { aux = "Faltam {0} dias!"; }
                return string.Format(aux, dateDiff);
            }

        }

        internal static int GetDaysBeforeBirthday(DateTime birthdate)
        {
            DateTime nextBday = new DateTime(DateTime.Now.Year, birthdate.Month, birthdate.Day);
            if (DateTime.Today > nextBday) { nextBday = nextBday.AddYears(1); }
            return (nextBday - DateTime.Today).Days;

        }
    }
}
