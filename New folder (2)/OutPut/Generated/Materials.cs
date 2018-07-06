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
    public class Materials
    {   
    #region Private Properties
	    private int _MaterialId;
	    private string _MaterialName;
	    private short _UnitId;
	    private single _Percentage;
	    private string _ImagePath;
	    private int _CrUserId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public Materials()
        {
            db = new DBAccess(HealthConstants.HEALTH_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Materials(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Materials()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public int MaterialId
        {
		    get
                {
			        return _MaterialId;
		        }
		    set
            {
			    _MaterialId = value;
		    }
	    }
    
        public string MaterialName
		{
            get
            {
			    return _MaterialName;
		    }
		    set
            {
			    _MaterialName = value;
            }
		}    
        public short UnitId
		{
            get
            {
			    return _UnitId;
		    }
		    set
            {
			    _UnitId = value;
            }
		}    
        public single Percentage
		{
            get
            {
			    return _Percentage;
		    }
		    set
            {
			    _Percentage = value;
            }
		}    
        public string ImagePath
		{
            get
            {
			    return _ImagePath;
		    }
		    set
            {
			    _ImagePath = value;
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
        private List<Materials> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Materials> l_Materials = new List<Materials>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Materials m_Materials = new Materials();
                    m_Materials.MaterialId = smartReader.GetInt32("MaterialId");
                    m_Materials.MaterialName = smartReader.GetString("MaterialName");
                    m_Materials.UnitId = smartReader.GetInt16("UnitId");
                    m_Materials.Percentage = smartReader.Get("Percentage");
                    m_Materials.ImagePath = smartReader.GetString("ImagePath");
                    m_Materials.CrUserId = smartReader.GetInt32("CrUserId");
                    m_Materials.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_Materials.Add(m_Materials);
                }
                reader.Close();
                return l_Materials;
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
                SqlCommand cmd = new SqlCommand("Materials_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MaterialName", this.MaterialName));
                cmd.Parameters.Add(new SqlParameter("@UnitId", this.UnitId));
                cmd.Parameters.Add(new SqlParameter("@Percentage", this.Percentage));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@MaterialId", this.MaterialId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.MaterialId = int.Parse(cmd.Parameters["@MaterialId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("Materials_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@MaterialId",this.MaterialId));
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
        public Materials Get()
        {
            Materials retVal = new Materials();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<Materials> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<Materials> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Materials_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@MaterialId", this.MaterialId));
                cmd.Parameters.Add(new SqlParameter("@MaterialName", this.MaterialName));
                cmd.Parameters.Add(new SqlParameter("@UnitId", this.UnitId));
                cmd.Parameters.Add(new SqlParameter("@Percentage", this.Percentage));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@CrUserId", this.CrUserId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Materials> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int MaterialId)
        {
            string RetVal = "";
            Materials m_Materials = new Materials();
            m_Materials.MaterialId = MaterialId;
            m_Materials = m_Materials.Get();
            RetVal = m_Materials.MaterialName;
            return RetVal;
        }
        
    #endregion
    } 
}

