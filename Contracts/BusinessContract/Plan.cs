using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Contracts.BusinessContract
{
    public class Plan
    {
        /// <summary>
        /// Plan Id
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// Source 
        /// </summary>
        [Required]
        public string Source { get; set; }

        /// <summary>
        /// Destination
        /// </summary>
        [Required]
        public string Destination { get; set; }

        /// <summary>
        /// Travel date 
        /// </summary>
        [DataMember(IsRequired = true)]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid value for Travel date")]
        public DateTime TravelDateTime { get; set; }

        /// <summary>
        /// Vehicle type
        /// </summary>
        [Required]
        public string VehicleType { get; set; }

        /// <summary>
        /// Total fare
        /// </summary>
        public int TotalFare { get; set; }
        
        /// <summary>
        /// Capacity
        /// </summary>
        public string Capacity { get; set; }

        /// <summary>
        /// Capacity
        /// </summary>
        public string Availability { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Follower Id
        /// </summary>
        public int FollowerId { get; set; }

        /// <summary>
        /// Creator Id
        /// </summary>
        public int CreatorId { get; set; }
    }
}
