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
    public class Warranty
    {   
    #region Private Properties
	    private short _WarrantyId;
	    private string _WarrantyName;
	    private string _WarrantyDesc;
	    private short _DisplayOrder;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public Warranty()
        {
            db = new DBAccess(HealthConstants.HEALTH_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Warranty(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Warranty()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public short WarrantyId
        {
		    get
                {
			        return _WarrantyId;
		        }
		    set
            {
			    _WarrantyId = value;
		    }
	    }
    
        public string WarrantyName
		{
            get
            {
			    return _WarrantyName;
		    }
		    set
            {
			    _WarrantyName = value;
            }
		}    
        public string WarrantyDesc
		{
            get
            {
			    return _WarrantyDesc;
		    }
		    set
            {
			    _WarrantyDesc = value;
            }
		}    
        public short DisplayOrder
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
        private List<Warranty> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Warranty> l_Warranty = new List<Warranty>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Warranty m_Warranty = new Warranty();
                    m_Warranty.WarrantyId = smartReader.GetInt16("WarrantyId");
                    m_Warranty.WarrantyName = smartReader.GetString("WarrantyName");
                    m_Warranty.WarrantyDesc = smartReader.GetString("WarrantyDesc");
                    m_Warranty.DisplayOrder = smartReader.GetInt16("DisplayOrder");
                    l_Warranty.Add(m_Warranty);
                }
                reader.Close();
                return l_Warranty;
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
                SqlCommand cmd = new SqlCommand("Warranty_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@WarrantyName", this.WarrantyName));
                cmd.Parameters.Add(new SqlParameter("@WarrantyDesc", this.WarrantyDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@WarrantyId", this.WarrantyId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.WarrantyId = short.Parse(cmd.Parameters["@WarrantyId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("Warranty_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@WarrantyId",this.WarrantyId));
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
        public Warranty Get()
        {
            Warranty retVal = new Warranty();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<Warranty> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<Warranty> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Warranty_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@WarrantyId", this.WarrantyId));
                cmd.Parameters.Add(new SqlParameter("@WarrantyName", this.WarrantyName));
                cmd.Parameters.Add(new SqlParameter("@WarrantyDesc", this.WarrantyDesc));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Warranty> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(short WarrantyId)
        {
            string RetVal = "";
            Warranty m_Warranty = new Warranty();
            m_Warranty.WarrantyId = WarrantyId;
            m_Warranty = m_Warranty.Get();
            RetVal = m_Warranty.WarrantyName;
            return RetVal;
        }
        
    #endregion
    } 
}

