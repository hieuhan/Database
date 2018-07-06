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
    public class SectionErrorType
    {   
    #region Private Properties
	    private int _SectionErrorTypeId;
	    private string _SectionErrorTypeName;
	    private string _SectionErrorTypeDesc;
	    private byte _ErrorCount;
	    private byte _DisplayOrder;
	    private int _SectionErrorId;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public SectionErrorType()
        {
            db = new DBAccess(HealthConstants.HEALTH_CONSTR);
		}
        //-----------------------------------------------------------------        
        public SectionErrorType(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~SectionErrorType()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
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
    
        public string SectionErrorTypeName
		{
            get
            {
			    return _SectionErrorTypeName;
		    }
		    set
            {
			    _SectionErrorTypeName = value;
            }
		}    
        public string SectionErrorTypeDesc
		{
            get
            {
			    return _SectionErrorTypeDesc;
		    }
		    set
            {
			    _SectionErrorTypeDesc = value;
            }
		}    
        public byte ErrorCount
		{
            get
            {
			    return _ErrorCount;
		    }
		    set
            {
			    _ErrorCount = value;
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
    
    
      
    #endregion
        //-----------------------------------------------------------
    #region Method
        private List<SectionErrorType> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<SectionErrorType> l_SectionErrorType = new List<SectionErrorType>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    SectionErrorType m_SectionErrorType = new SectionErrorType();
                    m_SectionErrorType.SectionErrorTypeId = smartReader.GetInt32("SectionErrorTypeId");
                    m_SectionErrorType.SectionErrorTypeName = smartReader.GetString("SectionErrorTypeName");
                    m_SectionErrorType.SectionErrorTypeDesc = smartReader.GetString("SectionErrorTypeDesc");
                    m_SectionErrorType.ErrorCount = smartReader.GetByte("ErrorCount");
                    m_SectionErrorType.DisplayOrder = smartReader.GetByte("DisplayOrder");
                    m_SectionErrorType.SectionErrorId = smartReader.GetInt32("SectionErrorId");
                    l_SectionErrorType.Add(m_SectionErrorType);
                }
                reader.Close();
                return l_SectionErrorType;
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
                SqlCommand cmd = new SqlCommand("SectionErrorType_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorTypeName", this.SectionErrorTypeName));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorTypeDesc", this.SectionErrorTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@ErrorCount", this.ErrorCount));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorId", this.SectionErrorId));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorTypeId", this.SectionErrorTypeId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SectionErrorTypeId = int.Parse(cmd.Parameters["@SectionErrorTypeId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("SectionErrorType_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorTypeId",this.SectionErrorTypeId));
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
        public SectionErrorType Get()
        {
            SectionErrorType retVal = new SectionErrorType();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<SectionErrorType> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<SectionErrorType> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SectionErrorType_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@SectionErrorTypeId", this.SectionErrorTypeId));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorTypeName", this.SectionErrorTypeName));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorTypeDesc", this.SectionErrorTypeDesc));
                cmd.Parameters.Add(new SqlParameter("@ErrorCount", this.ErrorCount));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@SectionErrorId", this.SectionErrorId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<SectionErrorType> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int SectionErrorTypeId)
        {
            string RetVal = "";
            SectionErrorType m_SectionErrorType = new SectionErrorType();
            m_SectionErrorType.SectionErrorTypeId = SectionErrorTypeId;
            m_SectionErrorType = m_SectionErrorType.Get();
            RetVal = m_SectionErrorType.SectionErrorTypeName;
            return RetVal;
        }
        
    #endregion
    } 
}

