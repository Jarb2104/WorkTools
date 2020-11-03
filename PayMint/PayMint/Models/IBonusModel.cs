using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayMint.Models
{
    public interface IBonusModel
    {
        /// <summary>
        /// Any percentage to be added to the Monthly charge for working extra hours
        /// </summary>
        decimal BonusPercent { get; set; }

        /// <summary>
        /// The calculation of the Monthly fee plus the percentage corresponding to the bonus
        /// </summary>
        decimal BonusMonthCharge { get; }
    }
}
