using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BusinessContract
{
    public class Search
    {
        /// <summary>
        /// Travel plans
        /// </summary>
        public Plan Travel { get; set; }

        /// <summary>
        /// User context
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Is Following
        /// </summary>
        public bool IsFollowing { get; set; }
    }
}
