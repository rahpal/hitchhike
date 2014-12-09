using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlFaultTolerantHandler
{
    public class FaultTolerantSqlConnection : IDisposable
    {
        #region Fields

        /// <summary>
        ///     SQL connection.
        /// </summary>
        private readonly SqlConnection underlyingConnection;

        /// <summary>
        ///     Connection string.
        /// </summary>
        private string connectionString;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the FaultTolerantSqlConnection class with a given connection string.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string used to open the SQL Azure database.
        /// </param>
        public FaultTolerantSqlConnection(string connectionString)
        {
            this.connectionString = connectionString;
            this.underlyingConnection = new SqlConnection(connectionString);
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="FaultTolerantSqlConnection" /> class.
        /// </summary>
        ~FaultTolerantSqlConnection()
        {
            this.Dispose(false);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Opens a database connection with retry.
        /// </summary>
        /// <returns>
        ///     An <see cref="SqlConnection" /> representing the open connection.
        /// </returns>
        public SqlConnection Open()
        {
            if (this.underlyingConnection.State != ConnectionState.Open)
            {
                this.underlyingConnection.Open();
            }

            return this.underlyingConnection;
        }

        /// <summary>
        ///     Closes a database connection.
        /// </summary>
        public void Close()
        {
            if (this.underlyingConnection == null)
            {
                return;
            }

            if (this.underlyingConnection.State == ConnectionState.Open)
            {
                this.underlyingConnection.Close();
            }
        }

        /// <summary>
        ///     Creates and returns a SQL Command object associated with the underlying SQL Connection.
        /// </summary>
        /// <returns>
        ///     Object type of <see cref="SqlCommand" />.
        /// </returns>
        public SqlCommand CreateCommand()
        {
            return this.underlyingConnection.CreateCommand();
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or
        ///     resetting managed and unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        ///     resetting managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// A flag indicating that managed resources must be released.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.underlyingConnection.State == ConnectionState.Open)
                {
                    this.underlyingConnection.Close();
                }

                this.underlyingConnection.Dispose();
            }
        }

        #endregion
    }
}
