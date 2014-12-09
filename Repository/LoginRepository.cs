using System;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Repository.Interfaces;
using Contracts.BusinessContract;
using SqlFaultTolerantHandler;

namespace Repository
{
    public class LoginRepository : ILoginRepository
    {
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="model"> Username and password </param>
        /// <returns></returns>
        public User Authenticate(Login model)
        {
            User user = new User();

            using (var connection = new FaultTolerantSqlConnection(SqlDataHelper.ConnectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[dbo].[GetAuthenticatedUser]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Username", model.Username));
                command.Parameters.Add(new SqlParameter("@Password", model.Password));

                using (var reader = command.ExecuteReader(CommandBehavior.Default))
                {
                    if (reader.Read())
                    {
                        user.UserId = (int)reader["UserId"];
                        user.Username = reader["Username"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Username"], CultureInfo.CurrentCulture);
                        user.Firstname = reader["Firstname"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Firstname"], CultureInfo.CurrentCulture);
                        user.Lastname = reader["Lastname"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Lastname"], CultureInfo.CurrentCulture);
                        user.Gender = (Gender)reader["Gender"];
                        user.EmailAddress = reader["EmailAddress"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailAddress"], CultureInfo.CurrentCulture);
                        user.IsAuthenticated = true;
                    }
                }

                connection.Close();

                return user;
            }
        }
    }
}
