using PayMint.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayMint.Controls
{
    public interface ITableBuilder
    {
        /// <summary>
        /// Process the available data to generate the Payment Request table template
        /// </summary>
        /// <returns>A dictionary containig each row of the final table</returns>
        Dictionary<string, string> GenerateTable();
    }
}
