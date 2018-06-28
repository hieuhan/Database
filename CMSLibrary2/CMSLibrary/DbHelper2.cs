using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSLibrary
{
    public class DbHelper
    {
        public DbHelper()
        {
            _provider = DbProviderTypes.SqlServer;
            _connectionString = GetConnectionStringByName("CONNECTION_STRING");
        }

        public DbHelper(string connectionString, DbProviderTypes provider = DbProviderTypes.SqlServer)
        {
            _connectionString = connectionString;
            _provider = provider;
        }

        #region private members
        private string _connectionString;
        private DbConnection _connection;
        private DbCommand _command;
        private DbProviderFactory _providerFactory = null;
        private DbProviderTypes _provider;
        private DbDataReader _reader;
        #endregion

        #region Properties
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _connectionString = value;
                }
            }
        }
        public DbConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        public DbCommand Command
        {
            get
            {
                return _command;
            }
        }

        public DbDataReader Reader
        {
            get
            {
                return _reader;
            }
        }

        public DbProviderFactory ProviderFactory
        {
            get
            {
                switch (_provider)
                {
                    case DbProviderTypes.SqlServer:
                        _providerFactory = SqlClientFactory.Instance;
                        break;
                    //case DbProviders.Oracle:
                    //    _providerFactory = OracleClientFactory.Instance;
                    //    break;
                    case DbProviderTypes.OleDb:
                        _providerFactory = OleDbFactory.Instance;
                        break;
                    case DbProviderTypes.ODBC:
                        _providerFactory = OdbcFactory.Instance;
                        break;
                        //case DbProviders.MySql:
                        //    _providerFactory = MySqlClientFactory.Instance;
                        //    break;
                        //case DbProviders.NpgSql:
                        //    _providerFactory = NpgsqlFactory.Instance;
                        //    break;
                }
                _connection = _providerFactory.CreateConnection();
                _command = _providerFactory.CreateCommand();
                Connection.ConnectionString = ConnectionString;
                Command.Connection = Connection;
                return _providerFactory;
            }
        }
        #endregion

        #region Methods

        private void CreateDbObjects()
        {
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                try
                {
                    _connection = ProviderFactory.CreateConnection();
                    _command = _providerFactory.CreateCommand();
                    Connection.ConnectionString = ConnectionString;
                    Command.Connection = Connection;
                }
                catch (Exception ex)
                {
                    if (Connection != null)
                    {
                        _connection = null;
                    }
                    throw new Exception(ex.Message);
                }
            }
        }

        public string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            //ConnectionStringSettingsCollection setting =
            //    ConfigurationManager.ConnectionStrings;
            //ConnectionStringSettings settings =
            //    ConfigurationManager.ConnectionStrings[name];

            //if (settings != null)
            //    returnValue = settings.ConnectionString;
            returnValue = ConfigurationManager.AppSettings["CONNECTION_STRING"] ??
                ConfigurationManager.AppSettings["CONNECTION_STRING"];
            return returnValue;
        }

        #region Parameters
        public int AddParameter(string name, object value)
        {
            int i = -1;
            DbParameter param = ProviderFactory.CreateParameter();
            if (param != null)
            {
                param.ParameterName = name;
                param.Value = value;
                i = Command.Parameters.Add(param);
            }
            return i;
        }

        public int AddParameter(string name, object value, ParameterDirection parameterDirection)
        {
            int i = -1;
            DbParameter param = ProviderFactory.CreateParameter();
            if (param != null)
            {
                param.ParameterName = name;
                param.Value = value;
                param.Direction = parameterDirection;
                i = Command.Parameters.Add(param);
            }
            return i;
        }

        public int AddParameter(string name, DbType dbType, ParameterDirection parameterDirection)
        {
            int i = -1;
            DbParameter param = ProviderFactory.CreateParameter();
            if (param != null)
            {
                param.ParameterName = name;
                param.DbType = dbType;
                param.Direction = parameterDirection;
                i = Command.Parameters.Add(param);
            }
            return i;
        }

        public int AddParameter(DbParameter parameter)
        {
            return Command.Parameters.Add(parameter);
        }
        public void ClearParameter()
        {
            if (Command != null)
            {
                if (Command.Parameters.Count > 0)
                {
                    Command.Parameters.Clear();
                }
            }
        }
        #endregion

        #region Transactions
        private void BeginTransaction()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            Command.Transaction = Connection.BeginTransaction();
        }
        private void CommitTransaction()
        {
            Command.Transaction.Commit();
            Connection.Close();
        }
        private void RollbackTransaction()
        {
            Command.Transaction.Rollback();
            Connection.Close();
        }
        #endregion

        #region Execute database functions
        public int ExecuteNonQuery(string query, CommandType commandtType = CommandType.StoredProcedure)
        {
            Command.CommandText = query;
            Command.CommandType = commandtType;
            int i = -1;
            try
            {
                CreateDbObjects();
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                BeginTransaction();
                i = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception(ex.Message);
            }
            finally
            {
                CommitTransaction();
                Command.Parameters.Clear();
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                    Connection.Dispose();
                    //command.Dispose();
                }
            }
            return i;
        }
        public object ExecuteScaler(string query, CommandType commandtType = CommandType.StoredProcedure)
        {
            object obj = null;
            try
            {
                CreateDbObjects();
                Command.CommandText = query;
                Command.CommandType = commandtType;
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                BeginTransaction();
                obj = Command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception(ex.Message);
            }
            finally
            {
                CommitTransaction();
                Command.Parameters.Clear();
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                    Connection.Dispose();
                    Command.Dispose();
                }
            }
            return obj;
        }
        public DbDataReader ExecuteReader(string query, CommandType commandtType = CommandType.StoredProcedure)
        {
            try
            {
                CreateDbObjects();
                Command.CommandText = query;
                Command.CommandType = commandtType;
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                if (Connection.State == ConnectionState.Open)
                {
                    _reader = Command.ExecuteReader(CommandBehavior.CloseConnection);
                }
                else
                {
                    _reader = Command.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Command.Parameters.Clear();
            }
            return _reader;
        }
        public DataSet GetDataSet(string query, CommandType commandtType = CommandType.StoredProcedure)
        {
            DataSet ds = new DataSet();
            try
            {
                CreateDbObjects();
                Command.CommandText = query;
                Command.CommandType = commandtType;
                using (DbDataAdapter adapter = ProviderFactory.CreateDataAdapter())
                {
                    if (adapter != null)
                    {
                        adapter.SelectCommand = Command;
                        adapter.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Command.Parameters.Clear();
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                    Connection.Dispose();
                    Command.Dispose();
                }
            }
            return ds;
        }
        #endregion

        //public T ReadAs<T>(string col)
        //{
        //    object val = Reader[col];
        //    if (val is DBNull)
        //    {
        //        return default(T);
        //    }
        //    return (T)val;
        //}

        //public T ReadAs<T>(int ordinal)
        //{
        //    if (Reader.IsDBNull(ordinal)) 
        //        return default(T); 
        //    if (typeof(T).BaseType?.Name == "Enum") 
        //        return (T)Enum.Parse(typeof(T), Reader.GetValue(ordinal).ToString(), true);
        //    return (T)Reader.GetValue(ordinal); 
        //}


        public T ReadAs<T>(string columnName)
        {
            try
            {
                int ordinal = Reader.GetOrdinal(columnName);
                return !Reader.IsDBNull(ordinal) ? (T) Reader.GetValue(ordinal) : default(T);
            }
            catch (IndexOutOfRangeException exception)
            {
                throw new ApplicationException("'" + columnName + "' is invalid", exception);
            }
        }

        #endregion

        #region Enums
        public enum DbProviderTypes
        {
            SqlServer,
            OleDb,
            Oracle,
            ODBC,
            MySql,
            SqLite,
            NpgSql
        }
        #endregion
    }
}
