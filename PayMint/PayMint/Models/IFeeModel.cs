using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayMint.Models
{
    public interface IFeeModel
    {
        /// <summary>
        /// Work details related to hours and days
        /// </summary>
        IWorkDetailsModel workDetails { get; set; }

        /// <summary>
        /// The arranged monthly fee
        /// </summary>
        decimal MonthlyFee { get; set; }

        /// <summary>
        /// The calculation of the monthly charge based on the working details and monthly fee
        /// </summary>
        decimal MonthCharge { get; }

        /// <summary>
        /// The currency of the table
        /// </summary>
        string Currency { get; set; }
    }
}
