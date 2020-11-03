using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayMint.Models
{
    class WorkDetailsModel : IWorkDetailsModel
    {
        /// <summary>
        /// Gets or Sets a date to represent the Year and Month to be use for calculations
        /// </summary>
        public DateTime WorkMonth { get; set; }

        /// <summary>
        /// Gets the total days available to work in the WorkMonth
        /// </summary>
        public int WorkDays 
        { 
            get
            {
                DateTime dtStart = Convert.ToDateTime($"01-{WorkMonth.Month}-{WorkMonth.Year}");
                DateTime dtEnd = Convert.ToDateTime($"{DateTime.DaysInMonth(WorkMonth.Year, WorkMonth.Month)}-{WorkMonth.Month}-{WorkMonth.Year}");
                return GetBusinessDays(dtStart, dtEnd);
            }
        }

        /// <summary>
        /// Gets or Sets the total days worked
        /// </summary>
        public int WorkedDays { get; set; }

        /// <summary>
        /// Gets or Sets the daily work hours
        /// </summary>
        public decimal DailyWorkHours { get; set; }

        /// <summary>
        /// Gets or Sets the daily work hours set by the user
        /// </summary>
        public decimal DailyWorkedHours { get; set; }

        /// <summary>
        /// Gets the total days available to work in a month multiplied by the total work hours
        /// </summary>
        public decimal TotalWorkHours 
        {
            get
            {
                return WorkDays * DailyWorkedHours;
            }
        }

        /// <summary>
        /// Gets the total days worked introduced by the user multiplied by the total work hours
        /// </summary>
        public decimal TotalWorkedHours
        {
            get 
            {
                return WorkedDays * DailyWorkedHours;
            }   
        }

        /// <summary>
        /// Returns the number of working days between to given dates
        /// </summary>
        /// <param name="startD">Including starting date</param>
        /// <param name="endD">Including end date</param>
        /// <returns>An integer with the count of regular business day</returns>
        private int GetBusinessDays(DateTime startD, DateTime endD)
        {
            decimal calcBusinessDays = (decimal)(1 + ((endD - startD).TotalDays * 5 - (startD.DayOfWeek - endD.DayOfWeek) * 2) / 7);

            if (endD.DayOfWeek == DayOfWeek.Saturday)
            {
                calcBusinessDays--;
            }
            if (startD.DayOfWeek == DayOfWeek.Sunday)
            {
                calcBusinessDays--;
            }

            return (int)calcBusinessDays;
        }
    }
}
