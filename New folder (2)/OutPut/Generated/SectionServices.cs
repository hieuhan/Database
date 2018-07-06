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
    public class SectionServices
    {   
    #region Private Properties
	    private int _SectionServiceId;
	    private string _SectionServiceName;
	    private string _SectionServiceDesc;
	    private byte _DisplayOrder;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public SectionServices()
        {
            db = new DBAccess(HealthConstants.HEALTH_CONSTR);
		}
        //-----------------------------------------------------------------        
        public SectionServices(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~SectionServices()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public int SectionServiceId
        {
		    get
                {
			        return _SectionServiceId;
		        }
		    set
            {
			    _SectionServiceId = value;
		    }
	    }
    
        public string SectionServiceName
		{
            get
            {
			    return _SectionServiceName;
		    }
		    set
            {
			    _SectionServiceName = value;
            }
		}    
        public string SectionServiceDesc
		{
            get
            {
			    return _SectionServiceDesc;
		    }
		    set
            {
			    _SectionServiceDesc = value;
            }
		}    
        public byte DisplayOrder
		{
            get
            {
			    return _DisplayOrder;
		    }
		    set
            {
			    _DisplayOrder = value;
            }
		}    
    
    
      
    #endregion
        //-----------------------------------------------------------
    #region Method
        private List<SectionServices> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SectionServices> l_SectionServices = new List<SectionServices>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SectionServices m_SectionServices = new SectionServices();
                    m_SectionServices.SectionServiceId = smartReader.GetInt32("SectionServiceId");
                    m_SectionServices.SectionServiceName = smartReader.GetString("SectionServiceName");
                    m_SectionServices.SectionServiceDesc = smartReader.GetString("SectionServiceDesc");
                    m_SectionServices.DisplayOrder = smartReader.GetByte("DisplayOrder");
                    l_SectionServices.Add(m_SectionServices);
                }
                reader.Close();
                return l_SectionServices;
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
                SqlCommand cmd = new SqlCommand("SectionServices_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SectionServiceName", this.SectionServiceName));
                cmd.Parameters.Add(new SqlParameter("@SectionServiceDesc", this.SectionServiceDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@SectionServiceId", this.SectionServiceId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SectionServiceId = int.Parse(cmd.Parameters["@SectionServiceId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("SectionServices_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SectionServiceId",this.SectionServiceId));
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
        public SectionServices Get()
        {
            SectionServices retVal = new SectionServices();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<SectionServices> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<SectionServices> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SectionServices_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@SectionServiceId", this.SectionServiceId));
                cmd.Parameters.Add(new SqlParameter("@SectionServiceName", this.SectionServiceName));
                cmd.Parameters.Add(new SqlParameter("@SectionServiceDesc", this.SectionServiceDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<SectionServices> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int SectionServiceId)
        {
            string RetVal = "";
            SectionServices m_SectionServices = new SectionServices();
            m_SectionServices.SectionServiceId = SectionServiceId;
            m_SectionServices = m_SectionServices.Get();
            RetVal = m_SectionServices.SectionServiceName;
            return RetVal;
        }
        
    #endregion
    } 
}

