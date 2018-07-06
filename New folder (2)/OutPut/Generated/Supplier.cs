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
    public class Supplier
    {   
    #region Private Properties
	    private int _SupplierId;
	    private string _SupplierName;
	    private string _SupplierDesc;
	    private string _Address;
	    private string _ImagePath;
	    private string _Mobile;
	    private string _Telephone;
	    private string _Comments;
	    private int _DisplayOrder;
	    private byte _ReviewStatusId;
	    private DateTime _CrDateTime;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public Supplier()
        {
            db = new DBAccess(HealthConstants.HEALTH_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Supplier(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Supplier()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public int SupplierId
        {
		    get
                {
			        return _SupplierId;
		        }
		    set
            {
			    _SupplierId = value;
		    }
	    }
    
        public string SupplierName
		{
            get
            {
			    return _SupplierName;
		    }
		    set
            {
			    _SupplierName = value;
            }
		}    
        public string SupplierDesc
		{
            get
            {
			    return _SupplierDesc;
		    }
		    set
            {
			    _SupplierDesc = value;
            }
		}    
        public string Address
		{
            get
            {
			    return _Address;
		    }
		    set
            {
			    _Address = value;
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
        public string Mobile
		{
            get
            {
			    return _Mobile;
		    }
		    set
            {
			    _Mobile = value;
            }
		}    
        public string Telephone
		{
            get
            {
			    return _Telephone;
		    }
		    set
            {
			    _Telephone = value;
            }
		}    
        public string Comments
		{
            get
            {
			    return _Comments;
		    }
		    set
            {
			    _Comments = value;
            }
		}    
        public int DisplayOrder
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
        public byte ReviewStatusId
		{
            get
            {
			    return _ReviewStatusId;
		    }
		    set
            {
			    _ReviewStatusId = value;
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
        private List<Supplier> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Supplier> l_Supplier = new List<Supplier>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Supplier m_Supplier = new Supplier();
                    m_Supplier.SupplierId = smartReader.GetInt32("SupplierId");
                    m_Supplier.SupplierName = smartReader.GetString("SupplierName");
                    m_Supplier.SupplierDesc = smartReader.GetString("SupplierDesc");
                    m_Supplier.Address = smartReader.GetString("Address");
                    m_Supplier.ImagePath = smartReader.GetString("ImagePath");
                    m_Supplier.Mobile = smartReader.GetString("Mobile");
                    m_Supplier.Telephone = smartReader.GetString("Telephone");
                    m_Supplier.Comments = smartReader.GetString("Comments");
                    m_Supplier.DisplayOrder = smartReader.GetInt32("DisplayOrder");
                    m_Supplier.ReviewStatusId = smartReader.GetByte("ReviewStatusId");
                    m_Supplier.CrDateTime = smartReader.GetDateTime("CrDateTime");
                    l_Supplier.Add(m_Supplier);
                }
                reader.Close();
                return l_Supplier;
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
                SqlCommand cmd = new SqlCommand("Supplier_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SupplierName", this.SupplierName));
                cmd.Parameters.Add(new SqlParameter("@SupplierDesc", this.SupplierDesc));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Telephone", this.Telephone));
                cmd.Parameters.Add(new SqlParameter("@Comments", this.Comments));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@SupplierId", this.SupplierId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.SupplierId = int.Parse(cmd.Parameters["@SupplierId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("Supplier_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@SupplierId",this.SupplierId));
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
        public Supplier Get()
        {
            Supplier retVal = new Supplier();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<Supplier> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<Supplier> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Supplier_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@SupplierId", this.SupplierId));
                cmd.Parameters.Add(new SqlParameter("@SupplierName", this.SupplierName));
                cmd.Parameters.Add(new SqlParameter("@SupplierDesc", this.SupplierDesc));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@Telephone", this.Telephone));
                cmd.Parameters.Add(new SqlParameter("@Comments", this.Comments));
                cmd.Parameters.Add(new SqlParameter("@DisplayOrder", this.DisplayOrder));
                cmd.Parameters.Add(new SqlParameter("@ReviewStatusId", this.ReviewStatusId));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Supplier> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(int SupplierId)
        {
            string RetVal = "";
            Supplier m_Supplier = new Supplier();
            m_Supplier.SupplierId = SupplierId;
            m_Supplier = m_Supplier.Get();
            RetVal = m_Supplier.SupplierName;
            return RetVal;
        }
        
    #endregion
    } 
}

