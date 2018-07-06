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
    public class MediaGroups
    {   
    #region Private Properties
	    private short _MediaGroupId;
	    private string _MediaGroupName;
	    private string _MediaGroupDesc;
	    private short _ParentGroupId;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public MediaGroups()
        {
            db = new DBAccess(HealthConstants.HEALTH_CONSTR);
		}
        //-----------------------------------------------------------------        
        public MediaGroups(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~MediaGroups()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public short MediaGroupId
        {
		    get
                {
			        return _MediaGroupId;
		        }
		    set
            {
			    _MediaGroupId = value;
		    }
	    }
    
        public string MediaGroupName
		{
            get
            {
			    return _MediaGroupName;
		    }
		    set
            {
			    _MediaGroupName = value;
            }
		}    
        public string MediaGroupDesc
		{
            get
            {
			    return _MediaGroupDesc;
		    }
		    set
            {
			    _MediaGroupDesc = value;
            }
		}    
        public short ParentGroupId
		{
            get
            {
			    return _ParentGroupId;
		    }
		    set
            {
			    _ParentGroupId = value;
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
        private List<MediaGroups> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<MediaGroups> l_MediaGroups = new List<MediaGroups>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    MediaGroups m_MediaGroups = new MediaGroups();
                    m_MediaGroups.MediaGroupId = smartReader.GetInt16("MediaGroupId");
                    m_MediaGroups.MediaGroupName = smartReader.GetString("MediaGroupName");
                    m_MediaGroups.MediaGroupDesc = smartReader.GetString("MediaGroupDesc");
                    m_MediaGroups.ParentGroupId = smartReader.GetInt16("ParentGroupId");
                    m_MediaGroups.CrUserId = smartReader.GetInt32("CrUserId");
                    m_MediaGroups.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_MediaGroups.Add(m_MediaGroups);
                }
                reader.Close();
                return l_MediaGroups;
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
                SqlCommand cmd = new SqlCommand("MediaGroups_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MediaGroupName", this.MediaGroupName));
                cmd.Parameters.Add(new SqlParameter("@MediaGroupDesc", this.MediaGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@ParentGroupId", this.ParentGroupId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@MediaGroupId", this.MediaGroupId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MediaGroupId = short.Parse(cmd.Parameters["@MediaGroupId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("MediaGroups_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MediaGroupId",this.MediaGroupId));
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
        public MediaGroups Get()
        {
            MediaGroups retVal = new MediaGroups();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<MediaGroups> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<MediaGroups> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("MediaGroups_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@MediaGroupId", this.MediaGroupId));
                cmd.Parameters.Add(new SqlParameter("@MediaGroupName", this.MediaGroupName));
                cmd.Parameters.Add(new SqlParameter("@MediaGroupDesc", this.MediaGroupDesc));
                cmd.Parameters.Add(new SqlParameter("@ParentGroupId", this.ParentGroupId));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<MediaGroups> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(short MediaGroupId)
        {
            string RetVal = "";
            MediaGroups m_MediaGroups = new MediaGroups();
            m_MediaGroups.MediaGroupId = MediaGroupId;
            m_MediaGroups = m_MediaGroups.Get();
            RetVal = m_MediaGroups.MediaGroupName;
            return RetVal;
        }
        
    #endregion
    } 
}

