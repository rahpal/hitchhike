using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlFaultTolerantHandler
{
    public static class SqlDataHelper
    {
        #region Constants

        /// <summary>
        ///     The connection string name
        /// </summary>
        private const string HitchHikeConnectionString = "HitchHikeConnectionString";

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the connection string
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[HitchHikeConnectionString].ConnectionString;
            }
        }

        #endregion
    }
}
