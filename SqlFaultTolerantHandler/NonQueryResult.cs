using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlFaultTolerantHandler
{
    /// <summary>
    /// Represents the results from a call to the ExecuteNonQuery method.
    /// </summary>
    /// <remarks>
    /// This helpers class is intended to be used exclusively for fetching the number of affected 
    /// records when executing a command using ExecuteNonQuery.
    /// </remarks>
    internal sealed class NonQueryResult
    {
        /// <summary>
        /// Gets or sets the number of record affected.
        /// </summary>
        public int RecordsAffected { get; set; }
    }
}
