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
    public class Roles
    {   
    #region Private Properties
	    private byte _RoleId;
	    private string _RoleName;
	    private string _RoleDesc;
        private DatabaseHelper db;
        
    #endregion
    
    #region Public Properties
        
        //-----------------------------------------------------------------
		public Roles()
        {
            db = new DatabaseHelper();
		}
        //-----------------------------------------------------------------        
        public Roles(string providerName , string connectionString)
        {
            db = new DatabaseHelper(providerName, connectionString);
        }
        //-----------------------------------------------------------------
        ~Roles()
        {

		}
        //-----------------------------------------------------------------
		public virtual void Dispose()
        {

		}
        //-----------------------------------------------------------------    
	    public byte RoleId
        {
		    get
            {
		        return _RoleId;
	        }
		    set
            {
			    _RoleId = value;
		    }
	    }
    
        public string RoleName
		{
            get
            {
			    return _RoleName;
		    }
		    set
            {
			    _RoleName = value;
            }
		}    
        public string RoleDesc
		{
            get
            {
			    return _RoleDesc;
		    }
		    set
            {
			    _RoleDesc = value;
            }
		}    
    
    
      
    #endregion

    #region Method
        /// <summary>
        /// Lấy danh sách đối tượng Roles từ DbDataReader
        /// </summary>
        /// <param name="cmd">DbCommand</param>
        /// <returns>list</returns>
        private List<Roles> Init(DbCommand cmd)
        {
            List<Roles> listRoles = new List<Roles>();
            try
            {
                using (DbDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        Roles mRoles = new Roles
                        {
                            RoleId = reader.ReadAs<byte>("RoleId"),
                            RoleName = reader.ReadAs<string>("RoleName"),
                            RoleDesc = reader.ReadAs<string>("RoleDesc"),
                        };
                        listRoles.Add(mRoles);
                    }
                }
                return listRoles;
            }
            catch (Exception err)
            {
                throw new Exception("Data error: " + err.Message);
            }
        }
        //-----------------------------------------------------------
        /// <summary>
        /// Thêm đối tượng Roles
        /// </summary>
        /// <param name="SysMessageId"></param>
        /// <returns>SysMessageTypeId</returns>
        public byte Insert(ref int SysMessageId)
        {
            byte retVal = 0;
            try
            {
                retVal = InsertOrUpdate(ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        //--------------------------------------------------------------
        /// <summary>
        /// Cập nhật đối tượng Roles
        /// </summary>
        /// <param name="SysMessageId"></param>
        /// <returns>SysMessageTypeId</returns>
        public byte Update(ref int SysMessageId)
        {
            byte retVal = 0;
            try
            {
                retVal = InsertOrUpdate(ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        //-----------------------------------------------------------
        /// <summary>
        /// Thêm/Sửa đối tượng Roles
        /// </summary>
        /// <param name="SysMessageId"></param>
        /// <returns>SysMessageTypeId</returns>
        public byte InsertOrUpdate(ref int SysMessageId)
        {
            byte retVal = 0;
            try
            {
                DbCommand cmd = db.GetStoredProcCommond("Roles_InsertOrUpdate");
                db.AddInParameter(cmd, "@RoleName", DbType.String, RoleName);
                db.AddInParameter(cmd, "@RoleDesc", DbType.String, RoleDesc);
                db.AddInputOutputParameter(cmd, "@RoleId", DbType.Byte, RoleId);
                db.AddOutParameter(cmd, "@SysMessageId", DbType.Int32);
                db.AddOutParameter(cmd, "@SysMessageTypeId", DbType.Byte);
                db.ExecuteNonQuery(cmd);
                RoleId = byte.Parse(db.GetParameter(cmd, "@RoleId").Value == null ? "0" : db.GetParameter(cmd, "@RoleId").Value);
                sysMessageId = Convert.ToInt16(db.GetParameter(cmd, "@SysMessageId").Value == null ? "0" : db.GetParameter(cmd, "@SysMessageId").Value);
                retVal = Convert.ToByte(db.GetParameter(cmd, "@SysMessageTypeId").Value == null ? "0" : db.GetParameter(cmd, "@SysMessageTypeId").Value);              
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        //-------------------------------------------------------------- 
        /// <summary>
        /// Xóa đối tượng Roles
        /// </summary>
        /// <param name="SysMessageId">SysMessageId</param>
        /// <returns>SysMessageTypeId</returns>
        public byte Delete(ref int SysMessageId)
        {
            byte retVal = 0;
            try
            {
                DbCommand cmd = db.GetStoredProcCommond("Roles_Delete");
                db.AddInParameter(cmd, "@RoleId", DbType.Byte, RoleId);
                db.AddOutParameter(cmd, "@SysMessageId", DbType.Int32);
                db.AddOutParameter(cmd, "@SysMessageTypeId", DbType.Byte);
                db.ExecuteNonQuery(cmd);
                sysMessageId = Convert.ToInt16(db.GetParameter(cmd, "@SysMessageId").Value == null ? "0" : db.GetParameter(cmd, "@SysMessageId").Value);
                retVal = Convert.ToByte(db.GetParameter(cmd, "@SysMessageTypeId").Value == null ? "0" : db.GetParameter(cmd, "@SysMessageTypeId").Value);              
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        //--------------------------------------------------------------     
        /// <summary>
        /// Trả về đối tượng Roles theo điều kiện đầu vào
        /// </summary>
        /// <returns>Roles</returns>
        public Roles Get()
        {
            Roles retVal = new Roles();
            int RowCount = 0;
            string DateFrom = "";
            string DateTo = "";
            string OrderBy = "";
            int PageSize = 1;
            int PageNumber = 0;
            try
            {
                List<Roles> list = GetPage(DateFrom, DateTo, OrderBy, PageSize, PageNumber, ref RowCount);
                if(list.Count>0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
		
      
        //-------------------------------------------------------------- 
        /// <summary>
        /// Danh sách Roles phân trang theo điều kiện đầu vào
        /// </summary>
        /// <param name="dateFrom">Từ ngày</param>
        /// <param name="dateTo">Đến ngày</param>
        /// <param name="orderBy">Sắp xếp theo</param>
        /// <param name="pageSize">Số records/trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="rowCount">Tổng số records</param>
        /// <returns>listRoles</returns>
        public List<Roles> GetPage(string dateFrom, string dateTo, string orderBy, int pageSize, int pageNumber, ref int rowCount)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommond("Roles_GetPage");
                db.AddInParameter(cmd, "@RoleId", DbType.Byte, RoleId);
                db.AddInParameter(cmd, "@RoleName", DbType.String, RoleName);
                db.AddInParameter(cmd, "@RoleDesc", DbType.String, RoleDesc);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, dateFrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, dateTo);
                db.AddInParameter(cmd, "@OrderBy", DbType.String, orderBy);
                db.AddInParameter(cmd, "@PageSize", DbType.Int32, pageSize);
                db.AddInParameter(cmd, "@PageNumber", DbType.Int32, pageNumber);
                db.AddOutParameter(cmd, "@RowCount", DbType.Int32);
                List<Roles> listRoles = Init(cmd);
                rowCount = Convert.ToInt32(db.GetParameter(cmd, "@RowCount").Value == null ? "0" : db.GetParameter(cmd, "@RowCount").Value);
                return listRoles;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        
        //--------------------------------------------------------------
        /// <summary>
        /// Trả về RoleName
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>RoleName</returns>
        public static string Static_GetRoleName(byte roleId)
        {
            Roles mRoles = new Roles
            {
                RoleId = roleId
            }.Get();
            return mRoles.RoleName;
        }
        
    #endregion
    } 
}

