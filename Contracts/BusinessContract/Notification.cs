using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BusinessContract
{
    public class Notification
    {
        /// <summary>
        /// Notification Id
        /// </summary>
        public int NotificationId { get; set; }

        /// <summary>
        /// State of notification
        /// </summary>
        public int state { get; set; }
    }

    /// <summary>
    /// State of notification
    /// </summary>
    public enum NotificationState
    {
        /// <summary>
        /// Notification is in pending state
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Notification accepted by the one who planned
        /// </summary>
        Accepted = 1,

        /// <summary>
        /// Notification rejected by the one who planned
        /// </summary>
        Rejected = 2
    }
}
