using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Repository.Interfaces;
using Contracts.BusinessContract;
using SqlFaultTolerantHandler;

namespace Repository
{
    public class HomeRepository : IHomeRepository
    {
        /// <summary>
        /// Create plan for user
        /// </summary>
        /// <param name="planData"></param>
        public void CreateTravelPlan(Plan planData, int userId)
        {
            using (var connection = new FaultTolerantSqlConnection(SqlDataHelper.ConnectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[dbo].[InsertTravelPlanDetails]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Source", planData.Source));
                command.Parameters.Add(new SqlParameter("@Destination", planData.Destination));
                command.Parameters.Add(new SqlParameter("@TravelDateTime", planData.TravelDateTime));
                command.Parameters.Add(new SqlParameter("@VehicleType", planData.VehicleType));
                command.Parameters.Add(new SqlParameter("@Capacity", planData.Capacity));
                command.Parameters.Add(new SqlParameter("@TotalFare", planData.TotalFare));
                command.Parameters.Add(new SqlParameter("@UserIdentityId", userId));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        /// <summary>
        /// Get travel plan count
        /// </summary>
        /// <param name="userId"></param>
        public int GetTravelPlanCountByUser(int userId)
        {
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

                command.ExecuteNonQuery();

                connection.Close();

                return Convert.ToInt32(command.Parameters["@RecordCount"].Value, CultureInfo.InvariantCulture);
            }
        }
    }
}
