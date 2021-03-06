﻿using System.Collections;
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
    public class Warehouse
    {   
    #region Private Properties
	    private byte _WarehouseId;
	    private string _WarehouseName;
	    private string _WarehouseDesc;
	    private string _Address;
	    private string _Mobile;
	    private byte _WarehouseStatus;
        private DBAccess db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public Warehouse()
        {
            db = new DBAccess(HealthConstants.HEALTH_CONSTR);
		}
        //-----------------------------------------------------------------        
        public Warehouse(string constr)
        {
            db = (constr.Length > 0) ? new DBAccess(constr) : new DBAccess();
        }
        //-----------------------------------------------------------------
        ~Warehouse()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
      
        //-----------------------------------------------------------------    
	    public byte WarehouseId
        {
		    get
                {
			        return _WarehouseId;
		        }
		    set
            {
			    _WarehouseId = value;
		    }
	    }
    
        public string WarehouseName
		{
            get
            {
			    return _WarehouseName;
		    }
		    set
            {
			    _WarehouseName = value;
            }
		}    
        public string WarehouseDesc
		{
            get
            {
			    return _WarehouseDesc;
		    }
		    set
            {
			    _WarehouseDesc = value;
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
        public byte WarehouseStatus
		{
            get
            {
			    return _WarehouseStatus;
		    }
		    set
            {
			    _WarehouseStatus = value;
            }
		}    
    
    
      
    #endregion
        //-----------------------------------------------------------
    #region Method
        private List<Warehouse> Init(SqlCommand cmd)
        {
            SqlConnection con = db.getConnection();
            cmd.Connection = con;
            List<Warehouse> l_Warehouse = new List<Warehouse>();
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SmartDataReader smartReader = new SmartDataReader(reader);
                while (smartReader.Read())
                {
                    Warehouse m_Warehouse = new Warehouse();
                    m_Warehouse.WarehouseId = smartReader.GetByte("WarehouseId");
                    m_Warehouse.WarehouseName = smartReader.GetString("WarehouseName");
                    m_Warehouse.WarehouseDesc = smartReader.GetString("WarehouseDesc");
                    m_Warehouse.Address = smartReader.GetString("Address");
                    m_Warehouse.Mobile = smartReader.GetString("Mobile");
                    m_Warehouse.WarehouseStatus = smartReader.GetByte("WarehouseStatus");
                    l_Warehouse.Add(m_Warehouse);
                }
                reader.Close();
                return l_Warehouse;
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
                SqlCommand cmd = new SqlCommand("Warehouse_InsertOrUpdate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@WarehouseName", this.WarehouseName));
                cmd.Parameters.Add(new SqlParameter("@WarehouseDesc", this.WarehouseDesc));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@WarehouseStatus", this.WarehouseStatus));
                cmd.Parameters.Add(new SqlParameter("@WarehouseId", this.WarehouseId)).Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@SysMessageId", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SysMessageTypeId", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                db.ExecuteSQL(cmd);
                this.WarehouseId = byte.Parse(cmd.Parameters["@WarehouseId"].Value.ToString());
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
                SqlCommand cmd = new SqlCommand("Warehouse_Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Replicated", Replicated));
                cmd.Parameters.Add(new SqlParameter("@ActUserId", ActUserId));
                cmd.Parameters.Add(new SqlParameter("@WarehouseId",this.WarehouseId));
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
        public Warehouse Get()
        {
            Warehouse retVal = new Warehouse();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                
                List<Warehouse> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
		
        public List<Warehouse> GetPage(string DateFrom, string DateTo, string OrderBy, int PageSize, int PageNumber, ref int RowCount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Warehouse_GetPage");
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new SqlParameter("@WarehouseId", this.WarehouseId));
                cmd.Parameters.Add(new SqlParameter("@WarehouseName", this.WarehouseName));
                cmd.Parameters.Add(new SqlParameter("@WarehouseDesc", this.WarehouseDesc));
                cmd.Parameters.Add(new SqlParameter("@Address", this.Address));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this.Mobile));
                cmd.Parameters.Add(new SqlParameter("@WarehouseStatus", this.WarehouseStatus));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", DateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", DateTo));
                cmd.Parameters.Add(new SqlParameter("@OrderBy", OrderBy));
                cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", PageNumber));
                cmd.Parameters.Add("@RowCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                List<Warehouse> list = Init(cmd);
                RowCount = int.Parse(cmd.Parameters["@RowCount"].Value.ToString());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        //--------------------------------------------------------------
        public static string Static_GetDisplayString(byte WarehouseId)
        {
            string RetVal = "";
            Warehouse m_Warehouse = new Warehouse();
            m_Warehouse.WarehouseId = WarehouseId;
            m_Warehouse = m_Warehouse.Get();
            RetVal = m_Warehouse.WarehouseName;
            return RetVal;
        }
        
    #endregion
    } 
}

