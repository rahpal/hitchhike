using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlFaultTolerantHandler
{
    public class ExceptionLog
    {
        /// <summary>
        /// RequestId
        /// </summary>
        public Guid RequestId { set; get; }

        /// <summary>
        /// HostName
        /// </summary>
        public string HostName { set; get; }

        /// <summary>
        /// ClientIPAddress
        /// </summary>
        public string ClientIPAddress { set; get; }

        /// <summary>
        /// WindowPrincipalName
        /// </summary>
        public string WindowPrincipalName { set; get; }

        /// <summary>
        /// RequestUri
        /// </summary>
        public string RequestUri { set; get; }

        /// <summary>
        /// ComponentName
        /// </summary>
        public string ComponentName { set; get; }

        /// <summary>
        /// ExceptionMessage
        /// </summary>
        public string ExceptionMessage { set; get; }

        /// <summary>
        /// StackTrace
        /// </summary>
        public string StackTrace { set; get; }
    }
}
