using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

using Repository.Interfaces;
using Contracts.BusinessContract;
using SqlFaultTolerantHandler;

namespace Repository
{
    public class SignupRepository : ISignupRepository
    {
        /// <summary>
        /// Create an active user. 
        /// </summary>
        /// <param name="user"> User Details </param>
        public void CreateUser(Signup user)
        {
            using (var connection = new FaultTolerantSqlConnection(SqlDataHelper.ConnectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[dbo].[InsertUserDetails]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Username", user.Username));
                command.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", !string.IsNullOrEmpty(user.LastName) ? user.LastName : (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Gender", user.Gender));
                command.Parameters.Add(new SqlParameter("@Password", user.Password));
                command.Parameters.Add(new SqlParameter("@PrimaryEmailAddress", user.PrimaryEmailAddress));
                command.Parameters.Add(new SqlParameter("@ContactNumber", !string.IsNullOrEmpty(user.ContactNumber) ? user.ContactNumber : (object)DBNull.Value));
                command.ExecuteNonQuery();

                connection.Close();

            }
        }

        /// <summary>
        /// Check for username existence
        /// </summary>
        /// <param name="username"> Entered Username </param>
        /// <returns> True/ false </returns>
        public bool CheckForUsernameExistence(string username)
        {
            using (var connection = new FaultTolerantSqlConnection(SqlDataHelper.ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[dbo].[CheckUsernameAvailability]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Username", username));
                command.Parameters.Add(new SqlParameter("@RecordCount", DbType.Int32)
                    {
                        Direction = ParameterDirection.Output,
                        Size = 32
                    });

                command.ExecuteNonQuery();

                connection.Close();

                return Convert.ToBoolean(command.Parameters["@RecordCount"].Value, CultureInfo.InvariantCulture);
            }
        }
    }
}
