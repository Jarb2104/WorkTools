using PayMint.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace PayMint.Controls
{
    public class TableBuilder : ITableBuilder
    {
        /// <summary>
        /// Information about the payment arranged when working extra hours
        /// </summary>
        private IBonusFeeModel FeeData { get; set; }

        /// <summary>
        /// TableBuilder constructor
        /// </summary>
        /// <param name="PaymentDetails">All payment details needed to generate a Payment Request Table</param>
        public TableBuilder(IBonusFeeModel PaymentDetails)
        {
            FeeData = PaymentDetails;
        }

        /// <summary>
        /// Process the information about the payment arranged to generate the Payment Request table template
        /// </summary>
        /// <returns>A dictionary containig each row of the final table</returns>
        public Dictionary<string, string> GenerateTable()
        {
            Dictionary<string, string> dcResult = new Dictionary<string, string>();

            dcResult.Add($"  Total of hours worked", $" {FeeData.workDetails.TotalWorkedHours:#.00}");
            dcResult.Add($"  Total of working hours", $" {FeeData.workDetails.TotalWorkHours:#.00}");
            dcResult.Add($"  Monthly fee", $" {FeeData.MonthlyFee:#.00}");
            dcResult.Add($"  Total", $" {FeeData.BonusMonthCharge:#.00}");
            dcResult.Add($"  Currency", $" {FeeData.Currency}");

            return dcResult;
        }
    }
}
