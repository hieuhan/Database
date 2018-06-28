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
    public class DatabaseHelper
    {
        private static readonly string DbProviderName = ConfigurationManager.AppSettings["ProviderName"];
        private static readonly string DbConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

        private DbConnection _connection;

        public DatabaseHelper()
        {
            _connection = CreateConnection(DbConnectionString);
        }
        public DatabaseHelper(string connectionString)
        {
            _connection = CreateConnection(connectionString);
        }

        public static DbConnection CreateConnection()
        {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(DbProviderName);
            DbConnection dbconn = dbFactory.CreateConnection();
            if (dbconn != null)
            {
                dbconn.ConnectionString = DbConnectionString;
            }
            return dbconn;
        }
        public static DbConnection CreateConnection(string connectionString)
        {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(DbProviderName);
            DbConnection dbconn = dbFactory.CreateConnection();
            if (dbconn != null)
            {
                dbconn.ConnectionString = connectionString;
            }
            return dbconn;
        }

        public DbCommand GetStoredProcCommond(string storedProcedure)
        {
            DbCommand dbCommand = _connection.CreateCommand();
            dbCommand.CommandText = storedProcedure;
            dbCommand.CommandType = CommandType.StoredProcedure;
            return dbCommand;
        }
        public DbCommand GetSqlStringCommond(string sqlQuery)
        {
            DbCommand dbCommand = _connection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.CommandType = CommandType.Text;
            return dbCommand;
        }

        #region Parameters
        public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
        {
            foreach (DbParameter dbParameter in dbParameterCollection)
            {
                cmd.Parameters.Add(dbParameter);
            }
        }
        public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dbParameter);
        }
        public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddInputOutputParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(dbParameter);
        }
        public DbParameter GetParameter(DbCommand cmd, string parameterName)
        {
            return cmd.Parameters[parameterName];
        }


        #endregion

        #region Execute Database Functions

        public DataSet ExecuteDataSet(DbCommand cmd)
        {
            DataSet ds = new DataSet();
            try
            {
                DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbProviderName);
                DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
                if (dbDataAdapter != null)
                {
                    dbDataAdapter.SelectCommand = cmd;
                    dbDataAdapter.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataTable ExecuteDataTable(DbCommand cmd)
        {
            DataTable dataTable = new DataTable();
            try
            {
                DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbProviderName);
                DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
                if (dbDataAdapter != null)
                {
                    dbDataAdapter.SelectCommand = cmd;
                    dbDataAdapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            DbDataReader reader =  null;
            try
            {
                cmd.Connection.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reader;
        }
        public int ExecuteNonQuery(DbCommand cmd)
        {
            int ret = -1;
            try
            {
                cmd.Connection.Open();
                ret = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
            }
            return ret;
        }

        public object ExecuteScalar(DbCommand cmd)
        {
            object ret;
            try
            {
                cmd.Connection.Open();
                ret = cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
            }
            return ret;
        }

        #endregion

        #region Execute Database Functions Transactions

        public DataSet ExecuteDataSet(DbCommand cmd, DatabaseTransactions databaseTransaction)
        {
            DataSet ds = new DataSet();
            try
            {
                cmd.Connection = databaseTransaction.DbConnection;
                cmd.Transaction = databaseTransaction.DbTransactions;
                DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbProviderName);
                DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
                if (dbDataAdapter != null)
                {
                    dbDataAdapter.SelectCommand = cmd;
                    dbDataAdapter.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                databaseTransaction.RollBack();
                throw ex;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
            }
            return ds;
        }

        public DataTable ExecuteDataTable(DbCommand cmd, DatabaseTransactions databaseTransaction)
        {
            DataTable dataTable = new DataTable();
            try
            {
                cmd.Connection = databaseTransaction.DbConnection;
                cmd.Transaction = databaseTransaction.DbTransactions;
                DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbProviderName);
                DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
                if (dbDataAdapter != null)
                {
                    dbDataAdapter.SelectCommand = cmd;
                    dbDataAdapter.Fill(dataTable);
                }
            }
            catch (Exception ex) 
            {
                databaseTransaction.RollBack();
                throw ex;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
            }
            return dataTable;
        }

        public DbDataReader ExecuteReader(DbCommand cmd, DatabaseTransactions databaseTransaction)
        {
            DbDataReader reader;
            try
            {
                cmd.Connection.Close();
                cmd.Connection = databaseTransaction.DbConnection;
                cmd.Transaction = databaseTransaction.DbTransactions;
                reader = cmd.ExecuteReader();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
            }
            return reader;
        }
        public int ExecuteNonQuery(DbCommand cmd, DatabaseTransactions databaseTransaction)
        {
            int ret = -1;
            try
            {
                cmd.Connection.Close();
                cmd.Connection = databaseTransaction.DbConnection;
                cmd.Transaction = databaseTransaction.DbTransactions;
                ret = cmd.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
            }
            return ret;
        }

        public object ExecuteScalar(DbCommand cmd, DatabaseTransactions databaseTransaction)
        {
            object obj = null;
            try
            {
                cmd.Connection.Close();
                cmd.Connection = databaseTransaction.DbConnection;
                cmd.Transaction = databaseTransaction.DbTransactions;
                obj = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Parameters.Clear();
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
            }
            return obj;
        }

        #endregion

    }

    public class DatabaseTransactions : IDisposable
    {
        private DbConnection _conn;
        private DbTransaction _dbTransactions;
        public DbConnection DbConnection
        {
            get { return _conn; }
        }
        public DbTransaction DbTransactions
        {
            get { return _dbTransactions; }
        }

        public DatabaseTransactions()
        {
            _conn = DatabaseHelper.CreateConnection();
            _conn.Open();
            _dbTransactions = _conn.BeginTransaction();
        }
        public DatabaseTransactions(string connectionString)
        {
            _conn = DatabaseHelper.CreateConnection(connectionString);
            _conn.Open();
            _dbTransactions = _conn.BeginTransaction();
        }
        public void Commit()
        {
            _dbTransactions.Commit();
            Colse();
        }

        public void RollBack()
        {
            _dbTransactions.Rollback();
            Colse();
        }

        public void Dispose()
        {
            Colse();
        }

        public void Colse()
        {
            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
        }
    }

    public static class SmartReader
    {
        public static T ReadAs<T>(this DbDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                return !reader.IsDBNull(ordinal) ? (T)reader.GetValue(ordinal) : default(T);
            }
            catch (IndexOutOfRangeException exception)
            {
                throw new ApplicationException("'" + columnName + "' is invalid", exception);
            }
        }
    }
}
