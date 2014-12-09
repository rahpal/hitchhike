using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Contracts.BusinessContract;
using Repository.Interfaces;
using SqlFaultTolerantHandler;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>();

            using (var connection = new FaultTolerantSqlConnection(SqlDataHelper.ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[dbo].[GetUsers]";
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader(CommandBehavior.Default))
                {
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            UserId = Convert.ToInt32(reader["UserId"], CultureInfo.InvariantCulture),
                            Username = Convert.ToString(reader["Username"], CultureInfo.InvariantCulture),
                            Firstname = Convert.ToString(reader["Firstname"], CultureInfo.InvariantCulture),
                            Lastname = Convert.ToString(reader["Lastname"], CultureInfo.InvariantCulture),
                            EmailAddress = Convert.ToString(reader["Lastname"], CultureInfo.InvariantCulture)
                        };

                        users.Add(user);
                    }
                }

                connection.Close();
            }

            return users;
        }
    }
}
