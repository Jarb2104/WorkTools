using System;

namespace PayMint.Models
{
    class BonusFeeModel : IBonusFeeModel
    {
        /// <summary>
        /// Gets or Sets all the details related to the hours and working days
        /// </summary>
        public IWorkDetailsModel workDetails { get; set; }

        /// <summary>
        /// Gets or Sets the arranged monthly fee
        /// </summary>
        public decimal MonthlyFee { get; set; }

        /// <summary>
        /// Gets the amount to be charge based on the days worked during the month
        /// </summary>
        public decimal MonthCharge 
        { 
            get
            {
                return Math.Round((MonthlyFee / workDetails.WorkDays) * workDetails.WorkedDays, 2, MidpointRounding.AwayFromZero);
            } 
        }

        /// <summary>
        /// Gets or Sets the Currency to be used in the table
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or Sets the arranged bonus percent for extra hours
        /// </summary>
        public decimal BonusPercent { get; set; }

        /// <summary>
        /// Gets the amount plus bonus to be charge based on the days worked during the month
        /// </summary>
        public decimal BonusMonthCharge 
        { 
            get
            {
                return Math.Round(MonthCharge * (1 + BonusPercent / 100), 2, MidpointRounding.AwayFromZero);
            }
        }

        /// <summary>
        /// Bonus fee model constructor
        /// </summary>
        public BonusFeeModel()
        {
        }
    }
}
