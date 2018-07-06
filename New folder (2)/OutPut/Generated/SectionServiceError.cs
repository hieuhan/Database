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
    public class SectionServiceError
    {   
    #region Private Properties
	    private int _SectionServiceErrorId;
	    private int _SectionServiceId;
	    private int _SectionErrorId;
	    private string _ErrorContent;
	    private byte _StatusId;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public SectionServiceError()
        {
            db = new DBAccess(HealthConstants.HEALTH_CONSTR);
		}
        //-----------------------------------------------------------------        
        public SectionServiceError(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~SectionServiceError()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public int SectionServiceErrorId
        {
		    get
                {
			        return _SectionServiceErrorId;
		        }
		    set
            {
			    _SectionServiceErrorId = value;
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
        public string ErrorContent
		{
            get
            {
			    return _ErrorContent;
		    }
		    set
            {
			    _ErrorContent = value;
            }
		}    
        public byte StatusId
		{
            get
            {
			    return _StatusId;
		    }
		    set
            {
			    _StatusId = value;
            }
		}    
    
    
      
    #endregion
        //-----------------------------------------------------------
    #region Method
        private List<SectionServiceError> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SectionServiceError> l_SectionServiceError = new List<SectionServiceError>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SectionServiceError m_SectionServiceError = new SectionServiceError();
                    m_SectionServiceError.SectionServiceErrorId = smartReader.GetInt32("SectionServiceErrorId");
                    m_SectionServiceError.SectionServiceId = smartReader.GetInt32("SectionServiceId");
                    m_SectionServiceError.SectionErrorId = smartReader.GetInt32("SectionErrorId");
                    m_SectionServiceError.ErrorContent = smartReader.GetString("ErrorContent");
                    m_SectionServiceError.StatusId = smartReader.GetByte("StatusId");
                    l_SectionServiceError.Add(m_SectionServiceError);
                }
                reader.Close();
                return l_SectionServiceError;
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
                SqlCommand cmd = new SqlCommand("SectionServiceError_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SectionServiceId", this.SectionServiceId));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorId", this.SectionErrorId));
                cmd.Parameters.Add(new SqlParameter("@ErrorContent", this.ErrorContent));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@SectionServiceErrorId", this.SectionServiceErrorId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SectionServiceErrorId = int.Parse(cmd.Parameters["@SectionServiceErrorId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("SectionServiceError_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SectionServiceErrorId",this.SectionServiceErrorId));
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
        public SectionServiceError Get()
        {
            SectionServiceError retVal = new SectionServiceError();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<SectionServiceError> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<SectionServiceError> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SectionServiceError_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@SectionServiceErrorId", this.SectionServiceErrorId));
                cmd.Parameters.Add(new SqlParameter("@SectionServiceId", this.SectionServiceId));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorId", this.SectionErrorId));
                cmd.Parameters.Add(new SqlParameter("@ErrorContent", this.ErrorContent));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<SectionServiceError> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int SectionServiceErrorId)
        {
            string RetVal = "";
            SectionServiceError m_SectionServiceError = new SectionServiceError();
            m_SectionServiceError.SectionServiceErrorId = SectionServiceErrorId;
            m_SectionServiceError = m_SectionServiceError.Get();
            RetVal = m_SectionServiceError.SectionServiceErrorName;
            return RetVal;
        }
        
    #endregion
    } 
}

