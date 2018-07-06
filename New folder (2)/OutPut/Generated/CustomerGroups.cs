using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Web.UI.WebControls;
using System.Threading;
using sms.common;
using sms.utils;
namespace cms.Lib
{
    public class CustomerGroups
    {   
    #region Private Properties
	    private short _CustomerGroupId;
	    private string _CustomerGroupName;
	    private string _CustomerGroupDesc;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public CustomerGroups()
        {
            db = new DBAccess(HealthConstants.HEALTH_CONSTR);
		}
        //-----------------------------------------------------------------        
        public CustomerGroups(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~CustomerGroups()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public short CustomerGroupId
        {
		    get
                {
			        return _CustomerGroupId;
		        }
		    set
            {
			    _CustomerGroupId = value;
		    }
	    }
    
        public string CustomerGroupName
		{
            get
            {
			    return _CustomerGroupName;
		    }
		    set
            {
			    _CustomerGroupName = value;
            }
		}    
        public string CustomerGroupDesc
		{
            get
            {
			    return _CustomerGroupDesc;
		    }
		    set
            {
			    _CustomerGroupDesc = value;
            }
		}    
        public int CrUserId
		{
            get
            {
			    return _CrUserId;
		    }
		    set
            {
			    _CrUserId = value;
            }
		}    
        public DateTime CrDateTime
		{
            get
            {
			    return _CrDateTime;
		    }
		    set
            {
			    _CrDateTime = value;
            }
		}    
    
    
      
    #endregion
        //-----------------------------------------------------------
    #region Method
        private List<CustomerGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<CustomerGroups> l_CustomerGroups = new List<CustomerGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    CustomerGroups m_CustomerGroups = new CustomerGroups();
                    m_CustomerGroups.CustomerGroupId = smartReader.GetInt16("CustomerGroupId");
                    m_CustomerGroups.CustomerGroupName = smartReader.GetString("CustomerGroupName");
                    m_CustomerGroups.CustomerGroupDesc = smartReader.GetString("CustomerGroupDesc");
                    m_CustomerGroups.CrUserId = smartReader.GetInt32("CrUserId");
                    m_CustomerGroups.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_CustomerGroups.Add(m_CustomerGroups);
                }
                reader.Close();
                return l_CustomerGroups;
            }
            catch (SqlException err)
            {
                throw new ApplicationException("Data error: " + err.Message);
            }
            finally
            {
                db.closeConnection(con);
            }
        }
        //-----------------------------------------------------------
        public byte Insert(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------
        public byte Update(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                RetVal = InsertOrUpdate(Replicated, ActUserId, ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte InsertOrUpdate(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerGroups_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupName", this.CustomerGroupName));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupDesc", this.CustomerGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", this.CustomerGroupId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.CustomerGroupId = short.Parse(cmd.Parameters["@CustomerGroupId"].Value.ToString());
                SysMessageId = Convert.ToInt32((cmd.Parameters["@SysMessageId"].Value == null) ? "0" : cmd.Parameters["@SysMessageId"].Value);
                RetVal = Convert.ToByte((cmd.Parameters["@SysMessageTypeId"].Value == null) ? "0" : cmd.Parameters["@SysMessageTypeId"].Value);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------            
        public byte Delete(byte Replicated, int ActUserId, ref int SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId",this.CustomerGroupId));
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                SysMessageId = int.Parse(cmd.Parameters["@SysMessageId"].Value.ToString());
                RetVal = Byte.Parse(cmd.Parameters["@SysMessageTypeId"].Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //--------------------------------------------------------------     
        public CustomerGroups Get()
        {
            CustomerGroups retVal = new CustomerGroups();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<CustomerGroups> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<CustomerGroups> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CustomerGroups_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupId", this.CustomerGroupId));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupName", this.CustomerGroupName));
                cmd.Parameters.Add(new SqlParameter("@CustomerGroupDesc", this.CustomerGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<CustomerGroups> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(short CustomerGroupId)
        {
            string RetVal = "";
            CustomerGroups m_CustomerGroups = new CustomerGroups();
            m_CustomerGroups.CustomerGroupId = CustomerGroupId;
            m_CustomerGroups = m_CustomerGroups.Get();
            RetVal = m_CustomerGroups.CustomerGroupName;
            return RetVal;
        }
        
    #endregion
    } 
}

