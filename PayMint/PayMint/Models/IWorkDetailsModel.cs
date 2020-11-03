using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayMint.Models
{
    public interface IWorkDetailsModel
    {
        /// <summary>
        /// A datetime representation to get the month and year for calculations
        /// </summary>
        DateTime WorkMonth { get; set; }

        /// <summary>
        /// Calculation for the number of days available for work in a month
        /// </summary>
        int WorkDays { get; }

        /// <summary>
        /// The amount of actual worked days
        /// </summary>
        int WorkedDays { get; set; }

        /// <summary>
        /// Regular working hours
        /// </summary>
        decimal DailyWorkHours { get; set; }

        /// <summary>
        /// Actual worked hours each day
        /// </summary>
        decimal DailyWorkedHours { get; set; }

        /// <summary>
        /// Total of work hours based on available work days in a month and actual working hours
        /// </summary>
        decimal TotalWorkHours { get; }

        /// <summary>
        /// Total of work hours based on actual worked days and actual working hours
        /// </summary>
        decimal TotalWorkedHours { get; }
    }
}
