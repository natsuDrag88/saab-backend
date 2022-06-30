using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using saab.Model;

namespace saab.Util.Project
{
    public static class DateUtil
    {
        public static Dictionary<string, string> GetDictPeriod(string period)
        {
            var dictPeriod = new Dictionary<string, string>
            {
                ["year"] = null,
                ["month"] = null
            };
            var annualPeriod = period.Length == 4;
            var monthlyPeriod = period.Length == 6;
            if (annualPeriod)
            {
                dictPeriod["year"] = period;
            }

            if (monthlyPeriod)
            {
                var splitPeriod = SplitPeriod(period: period);
                dictPeriod["year"] = splitPeriod[0];
                dictPeriod["month"] = splitPeriod[1];
            }

            return dictPeriod;
        }

        private static string[] SplitPeriod(string period)
        {
            const int limitCharacters = 4;
            var separatePeriod = string.Join(string.Empty, period.Select((x, i) => i > 0 && i % limitCharacters == 0
                ? $" {x}"
                : x.ToString()));
            var splitPeriod = separatePeriod.Split(" ");
            return splitPeriod;
        }

        private static string[] SplitNamePeriod(string period)
        {
            var splitPeriodName = period.Split("_");
            return splitPeriodName;
        }

        public static string ConvertMonthToString(int month)
        {
            var dtInfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtInfo.GetMonthName(month).ToUpper();
        }

        public static string ConvertDateToPeriod(DateTime periodDate)
        {
            return periodDate.ToString("yyyyMM");
        }

        public static string ConvertDateToPeriodDb(DateTime periodDate)
        {
            var dtInfo = new CultureInfo("es-ES", false).DateTimeFormat;
            var month = dtInfo.GetMonthName(periodDate.Month).ToUpper();

            return $"{month}_{periodDate.ToString("yyyy")}";
        }

        public static string ConvertPeriodToPeriodDb(string period)
        {
            var periodDate = ConvertPeriodToDate(period);
            return ConvertDateToPeriodCustomDb(periodDate);
        }

        public static DateTime ConvertPeriodToDate(string period)
        {
            return DateTime.ParseExact(period, "yyyyMM", null);
        }

        public static DateTime ConvertPeriodDbToDate(string periodDb)
        {
            var splitPeriodName = periodDb.Split("_");
            var esCulture = new CultureInfo("es-ES", false);
            var monthNames = esCulture.DateTimeFormat.MonthNames;
            foreach (var month in monthNames)
            {
                if (!string.Equals(month, splitPeriodName[0], StringComparison.CurrentCultureIgnoreCase)) continue;
                var index = Array.IndexOf(monthNames, month) + 1;
                var period = $"{index.ToString().PadLeft(2, '0')}_{splitPeriodName[1]}";
                return DateTime.ParseExact(period, "MM_yyyy", null);
            }

            return new DateTime(0,0,0);
        }

        public static List<DateTime> ListBetweenTwoDates(DateTime start, DateTime end)
        {
            List<DateTime> li = new List<DateTime>();

            do
            {
                li.Add(start);
                start = start.AddMonths(1);
            } while (start <= end);

            return li;
        }

        public static string ConvertDateToPeriodCustomDb(DateTime date)
        {
            var dtInfo = new CultureInfo("es-ES", false).DateTimeFormat;
            var numberMonth = date.Month;
            return dtInfo.GetMonthName(numberMonth).ToUpper() + "_" + date.Year;
        }


        public static string ConvertStringMonthToInt(string period)
        {
            var dtInfo = new CultureInfo("es-ES", false).DateTimeFormat;
            var periodList = SplitNamePeriod(period: period);
            var numberMonth = (DateTime.ParseExact(periodList[0], "MMMM", dtInfo).Month) - 1;
            var monthLast = (numberMonth.ToString().Length > 1)
                ? numberMonth.ToString()
                : "0" + numberMonth;
            return monthLast;
        }

        public static string ConvertPeriodMonthNametoNumber(string period)
        {
            var dtInfo = new CultureInfo("es-ES", false).DateTimeFormat;
            var periodList = SplitNamePeriod(period: period);
            var numberMonth = (DateTime.ParseExact(periodList[0], "MMMM", dtInfo).Month);
            var month = (numberMonth.ToString().Length > 1)
                ? numberMonth.ToString()
                : "0" + numberMonth;
            return periodList[1] + month;
        }

        public static List<string> GetLastMonthbyYear(Dictionary<string, string> dictPeriod)
        {
            var listPeriods = new List<string>();
            for (var i = 1; i <= int.Parse(dictPeriod["month"]); i++)
            {
                var numberMonth = (i.ToString().Length > 1) ? i.ToString() : "0" + i.ToString();
                listPeriods.Add(ConvertPeriodToPeriodDb(period: dictPeriod["year"] + numberMonth));
            }

            return listPeriods;
        }

        public static DateTime GetInitialDate(string date)
        {
            return DateTime.Parse(date).AddMinutes(5);
        }

        public static DateTime GetFinalDate(string date)
        {
            return DateTime.Parse(date).AddDays(1);
        }

        public static List<DateTime> GetListPeriodsFiveMinutes(DateTime initialDate, DateTime finalDate)
        {
            var listPeriods = new List<DateTime>();
            var dtm = initialDate;
            while (dtm <= finalDate)
            {
                listPeriods.Add(dtm);
                dtm = dtm.AddMinutes(5);
            }
            return listPeriods;
        }
    }
}