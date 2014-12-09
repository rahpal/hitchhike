using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.BusinessContract;
using Repository.Interfaces;
using SqlFaultTolerantHandler;

namespace Repository
{
    public class SearchRepository : ISearchRepository
    {
        /// <summary>
        /// All planned travels based on search criteria
        /// </summary>
        /// <param name="searchData">Parameters on which planned travels ned to be searched</param>
        /// <param name="userId"></param>
        /// <returns>Returns all searched results</returns>
        public IEnumerable<Search> GetSearchedTravelPlans(Plan searchData, int userId)
        {
            var searchedResults = new List<Search>();

            using (var connection = new FaultTolerantSqlConnection(SqlDataHelper.ConnectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[dbo].[GetAllSearchPlan]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@From", searchData.Source));
                command.Parameters.Add(new SqlParameter("@To", searchData.Destination));
                command.Parameters.Add(new SqlParameter("@UserIdentityId", userId));

                using (var reader = command.ExecuteReader(CommandBehavior.Default))
                {
                    while (reader.Read())
                    {
                        var result = new Search
                        {
                            Travel = new Plan
                            {
                                PlanId = Convert.ToInt32(reader["PlanId"], CultureInfo.InvariantCulture),
                                Source = reader["Source"] == DBNull.Value
                                    ? string.Empty
                                    : Convert.ToString(reader["Source"], CultureInfo.InvariantCulture),
                                Destination =
                                    reader["Destination"] == DBNull.Value
                                        ? string.Empty
                                        : Convert.ToString(reader["Destination"], CultureInfo.InvariantCulture),
                                TravelDateTime = Convert.ToDateTime(reader["TravelDateTime"], CultureInfo.InvariantCulture),
                                VehicleType =
                                    reader["VehicleType"] == DBNull.Value
                                        ? string.Empty
                                        : Convert.ToString(reader["VehicleType"], CultureInfo.InvariantCulture),
                                Capacity =
                                    reader["Capacity"] == DBNull.Value
                                        ? string.Empty
                                        : Convert.ToString(reader["Capacity"], CultureInfo.InvariantCulture),
                                TotalFare = Convert.ToInt32(reader["TotalFare"], CultureInfo.InvariantCulture)
                            },
                            User = new User
                            {
                                UserId = Convert.ToInt32(reader["UserId"], CultureInfo.InvariantCulture),
                                Username = reader["Username"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Username"], CultureInfo.InvariantCulture),
                                Firstname = reader["Firstname"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Firstname"], CultureInfo.InvariantCulture),
                                Lastname = reader["Lastname"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Lastname"], CultureInfo.InvariantCulture),
                                //Gender = (Gender)reader["Gender"],
                                EmailAddress = reader["EmailAddress"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EmailAddress"], CultureInfo.InvariantCulture)
                            },
                            IsFollowing = reader["FollowingId"] != DBNull.Value
                        };

                        searchedResults.Add(result);
                    }
                }

            }

            return searchedResults;
        }

        /// <summary>
        /// All planned travels based on search criteria
        /// </summary>
        /// <param name="searchData">Parameters on which planned travels ned to be searched</param>
        /// <returns>Returns all searched results</returns>
        public void JoinTravelPlan(Plan searchData)
        {
            using (var connection = new FaultTolerantSqlConnection(SqlDataHelper.ConnectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "[dbo].[InsertPlanFollowedDetails]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@PlanId", searchData.PlanId));
                command.Parameters.Add(new SqlParameter("@CreatorId", searchData.CreatorId));
                command.Parameters.Add(new SqlParameter("@FollowerId", searchData.FollowerId));
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
