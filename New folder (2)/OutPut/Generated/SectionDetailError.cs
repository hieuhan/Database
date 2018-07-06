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
    public class SectionDetailError
    {   
    #region Private Properties
	    private int _SectionDetailErrorId;
	    private int _SectionDetailId;
	    private int _SectionErrorId;
	    private string _ErrorContent;
	    private byte _StatusId;
	    private int _SectionErrorTypeId;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public SectionDetailError()
        {
            db = new DBAccess(HealthConstants.HEALTH_CONSTR);
		}
        //-----------------------------------------------------------------        
        public SectionDetailError(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~SectionDetailError()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public int SectionDetailErrorId
        {
		    get
                {
			        return _SectionDetailErrorId;
		        }
		    set
            {
			    _SectionDetailErrorId = value;
		    }
	    }
    
        public int SectionDetailId
		{
            get
            {
			    return _SectionDetailId;
		    }
		    set
            {
			    _SectionDetailId = value;
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
        public int SectionErrorTypeId
		{
            get
            {
			    return _SectionErrorTypeId;
		    }
		    set
            {
			    _SectionErrorTypeId = value;
            }
		}    
    
    
      
    #endregion
        //-----------------------------------------------------------
    #region Method
        private List<SectionDetailError> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SectionDetailError> l_SectionDetailError = new List<SectionDetailError>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SectionDetailError m_SectionDetailError = new SectionDetailError();
                    m_SectionDetailError.SectionDetailErrorId = smartReader.GetInt32("SectionDetailErrorId");
                    m_SectionDetailError.SectionDetailId = smartReader.GetInt32("SectionDetailId");
                    m_SectionDetailError.SectionErrorId = smartReader.GetInt32("SectionErrorId");
                    m_SectionDetailError.ErrorContent = smartReader.GetString("ErrorContent");
                    m_SectionDetailError.StatusId = smartReader.GetByte("StatusId");
                    m_SectionDetailError.SectionErrorTypeId = smartReader.GetInt32("SectionErrorTypeId");
                    l_SectionDetailError.Add(m_SectionDetailError);
                }
                reader.Close();
                return l_SectionDetailError;
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
                SqlCommand cmd = new SqlCommand("SectionDetailError_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SectionDetailId", this.SectionDetailId));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorId", this.SectionErrorId));
                cmd.Parameters.Add(new SqlParameter("@ErrorContent", this.ErrorContent));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorTypeId", this.SectionErrorTypeId));
                cmd.Parameters.Add(new SqlParameter("@SectionDetailErrorId", this.SectionDetailErrorId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SectionDetailErrorId = int.Parse(cmd.Parameters["@SectionDetailErrorId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("SectionDetailError_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SectionDetailErrorId",this.SectionDetailErrorId));
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
        public SectionDetailError Get()
        {
            SectionDetailError retVal = new SectionDetailError();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<SectionDetailError> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<SectionDetailError> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SectionDetailError_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@SectionDetailErrorId", this.SectionDetailErrorId));
                cmd.Parameters.Add(new SqlParameter("@SectionDetailId", this.SectionDetailId));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorId", this.SectionErrorId));
                cmd.Parameters.Add(new SqlParameter("@ErrorContent", this.ErrorContent));
                cmd.Parameters.Add(new SqlParameter("@StatusId", this.StatusId));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorTypeId", this.SectionErrorTypeId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<SectionDetailError> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int SectionDetailErrorId)
        {
            string RetVal = "";
            SectionDetailError m_SectionDetailError = new SectionDetailError();
            m_SectionDetailError.SectionDetailErrorId = SectionDetailErrorId;
            m_SectionDetailError = m_SectionDetailError.Get();
            RetVal = m_SectionDetailError.SectionDetailErrorName;
            return RetVal;
        }
        
    #endregion
    } 
}

