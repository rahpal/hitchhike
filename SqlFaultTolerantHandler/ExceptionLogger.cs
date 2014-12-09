using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlFaultTolerantHandler
{
    /// <summary>
    ///  Exception Logger class
    /// </summary>
    public static class ExceptionLogger
    {
        /// <summary>
        /// Process exception
        /// </summary>
        /// <param name="exception"> Base exception </param>
        public static void ProcessException(Exception exception)
        {
            HttpContext currentContext = HttpContext.Current;

            if (currentContext != null)
            {
                var logger = new ExceptionLog
                {
                    RequestId = Guid.NewGuid(),
                    HostName = currentContext.Server.MachineName,
                    ClientIPAddress = null,
                    WindowPrincipalName = currentContext.Request.LogonUserIdentity.Name,
                    RequestUri = currentContext.Request.RawUrl.ToString(),
                    ComponentName = null,
                    ExceptionMessage = exception.Message,
                    StackTrace = exception.StackTrace
                };

                // Pass object to Insert function
                InsertExceptionToLog(logger);
            }
        }

        /// <summary>
        /// Insert Exception to DB
        /// </summary>
        /// <param name="log"> Exception details</param>
        private static void InsertExceptionToLog(ExceptionLog log)
        {
            using (var connection = new FaultTolerantSqlConnection(SqlDataHelper.ConnectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[dbo].[InsertExceptionDetails]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@RequestId", log.RequestId));
                command.Parameters.Add(new SqlParameter("@HostName", log.HostName));
                command.Parameters.Add(new SqlParameter("@ClientIPAddress", !string.IsNullOrEmpty(log.ClientIPAddress) ? log.ClientIPAddress : (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@WindowPrincipalName", log.WindowPrincipalName));
                command.Parameters.Add(new SqlParameter("@RequestUri", log.RequestUri));
                command.Parameters.Add(new SqlParameter("@ComponentName", !string.IsNullOrEmpty(log.ComponentName) ? log.ComponentName : (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ExceptionMessage", log.ExceptionMessage));
                command.Parameters.Add(new SqlParameter("@StackTrace", log.StackTrace));
                command.ExecuteNonQuery();

                connection.Close();

            }
        }
    }
}
