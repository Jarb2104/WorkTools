using System;
using System.Collections.Generic;

namespace PayMint.StaticsAndConstants
{
    public static class Utils
    {
        /// <summary>
        /// Enumerate through a range of dates
        /// </summary>
        /// <param name="from">Range Start</param>
        /// <param name="thru">Range End</param>
        /// <returns>Current Datetime</returns>
        public static IEnumerable<DateTime> EachDayBetween(DateTime dtStart, DateTime dtEnd)
        {
            for (DateTime day = dtStart.Date; day.Date <= dtEnd.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
