using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Contracts.BusinessContract;
using SqlFaultTolerantHandler;
using Repository.Interfaces;

namespace Repository
{
    public class PlanRepository : IPlanRepository
    {
        /// <summary>
        /// Get all travel plan by user
        /// </summary>
        /// <param name="userId">userid</param>
        /// <returns> List of Travel plan </returns>
        public IEnumerable<Plan> GetAllTravelPlanByUser(int userId)
        {
            IList<Plan> planList = new List<Plan>();

            using (var connection = new FaultTolerantSqlConnection(SqlDataHelper.ConnectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[dbo].[GetTravelPlanByUser]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@UserIdentityId", userId));
                command.Parameters.Add(new SqlParameter("@RecordCount", DbType.Int32)
                {
                    Direction = ParameterDirection.Output,
                    Size = 32
                });

                using (var reader = command.ExecuteReader(CommandBehavior.Default))
                {
                    while (reader.Read())
                    {
                        var plan = new Plan
                        {
                            PlanId = Convert.ToInt32(reader["PlanId"], CultureInfo.CurrentCulture),
                            Source =
                                reader["Source"] == DBNull.Value
                                    ? string.Empty
                                    : Convert.ToString(reader["Source"], CultureInfo.CurrentCulture),
                            Destination =
                                reader["Destination"] == DBNull.Value
                                    ? string.Empty
                                    : Convert.ToString(reader["Destination"], CultureInfo.CurrentCulture),
                            TravelDateTime = Convert.ToDateTime(reader["TravelDateTime"], CultureInfo.CurrentCulture),
                            VehicleType =
                                reader["VehicleType"] == DBNull.Value
                                    ? string.Empty
                                    : Convert.ToString(reader["VehicleType"], CultureInfo.CurrentCulture),
                            Capacity =
                                reader["Capacity"] == DBNull.Value
                                    ? string.Empty
                                    : Convert.ToString(reader["Capacity"], CultureInfo.CurrentCulture),
                            TotalFare = Convert.ToInt32(reader["TotalFare"], CultureInfo.CurrentCulture)
                        };

                        planList.Add(plan);
                    }
                }
            }

            return planList;
        }

        /// <summary>
        /// Delete the record 
        /// </summary>
        public void UpdateTravelPlan(int planId)
        {
            using (var connection = new FaultTolerantSqlConnection(SqlDataHelper.ConnectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[dbo].[UpdateTravelPlan]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@TravelId", planId));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
