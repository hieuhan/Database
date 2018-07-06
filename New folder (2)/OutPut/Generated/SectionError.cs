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
    public class SectionError
    {   
    #region Private Properties
	    private int _SectionErrorId;
	    private string _SectionErrorName;
	    private string _SectionErrorDesc;
	    private byte _DisplayOrder;
	    private int _SectionServiceId;
	    private byte _ErrorTypeStatusId;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public SectionError()
        {
            db = new DBAccess(HealthConstants.HEALTH_CONSTR);
		}
        //-----------------------------------------------------------------        
        public SectionError(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~SectionError()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public int SectionErrorId
        {
		    get
                {
			        return _SectionErrorId;
		        }
		    set
            {
			    _SectionErrorId = value;
		    }
	    }
    
        public string SectionErrorName
		{
            get
            {
			    return _SectionErrorName;
		    }
		    set
            {
			    _SectionErrorName = value;
            }
		}    
        public string SectionErrorDesc
		{
            get
            {
			    return _SectionErrorDesc;
		    }
		    set
            {
			    _SectionErrorDesc = value;
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
        public byte ErrorTypeStatusId
		{
            get
            {
			    return _ErrorTypeStatusId;
		    }
		    set
            {
			    _ErrorTypeStatusId = value;
            }
		}    
    
    
      
    #endregion
        //-----------------------------------------------------------
    #region Method
        private List<SectionError> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SectionError> l_SectionError = new List<SectionError>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SectionError m_SectionError = new SectionError();
                    m_SectionError.SectionErrorId = smartReader.GetInt32("SectionErrorId");
                    m_SectionError.SectionErrorName = smartReader.GetString("SectionErrorName");
                    m_SectionError.SectionErrorDesc = smartReader.GetString("SectionErrorDesc");
                    m_SectionError.DisplayOrder = smartReader.GetByte("DisplayOrder");
                    m_SectionError.SectionServiceId = smartReader.GetInt32("SectionServiceId");
                    m_SectionError.ErrorTypeStatusId = smartReader.GetByte("ErrorTypeStatusId");
                    l_SectionError.Add(m_SectionError);
                }
                reader.Close();
                return l_SectionError;
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
                SqlCommand cmd = new SqlCommand("SectionError_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorName", this.SectionErrorName));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorDesc", this.SectionErrorDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@SectionServiceId", this.SectionServiceId));
                cmd.Parameters.Add(new SqlParameter("@ErrorTypeStatusId", this.ErrorTypeStatusId));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorId", this.SectionErrorId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SectionErrorId = int.Parse(cmd.Parameters["@SectionErrorId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("SectionError_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorId",this.SectionErrorId));
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
        public SectionError Get()
        {
            SectionError retVal = new SectionError();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<SectionError> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<SectionError> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SectionError_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@SectionErrorId", this.SectionErrorId));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorName", this.SectionErrorName));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorDesc", this.SectionErrorDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@SectionServiceId", this.SectionServiceId));
                cmd.Parameters.Add(new SqlParameter("@ErrorTypeStatusId", this.ErrorTypeStatusId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<SectionError> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int SectionErrorId)
        {
            string RetVal = "";
            SectionError m_SectionError = new SectionError();
            m_SectionError.SectionErrorId = SectionErrorId;
            m_SectionError = m_SectionError.Get();
            RetVal = m_SectionError.SectionErrorName;
            return RetVal;
        }
        
    #endregion
    } 
}

