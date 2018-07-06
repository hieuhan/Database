//-----------------------------------------------------------------------
// <copyright file="Actions.cs">
//     Author: hieuht
//     CreateDate: 29/06/2018 01:27:13
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
namespace ClassLibrary
{
    public class Actions
    {
        #region Private Properties
        private short _ActionId;
        private string _ActionName;
        private string _ActionDesc;
        private short _ParentActionId;
        private string _Url;
        private byte _Display;
        private short _DisplayOrder;
        private int _TreeOrder;
        private DateTime _CrDateTime;
        private byte _ActionStatusId;
        private DatabaseAccess db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public Actions()
        {
            db = new DatabaseAccess();
        }
        //-----------------------------------------------------------------        
        public Actions(string providerName, string connectionString)
        {
            db = new DatabaseAccess(providerName, connectionString);
        }
        //-----------------------------------------------------------------
        ~Actions()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }
        //-----------------------------------------------------------------    
        public short ActionId
        {
            get
            {
                return _ActionId;
            }
            set
            {
                _ActionId = value;
            }
        }

        public string ActionName
        {
            get
            {
                return _ActionName;
            }
            set
            {
                _ActionName = value;
            }
        }
        public string ActionDesc
        {
            get
            {
                return _ActionDesc;
            }
            set
            {
                _ActionDesc = value;
            }
        }
        public short ParentActionId
        {
            get
            {
                return _ParentActionId;
            }
            set
            {
                _ParentActionId = value;
            }
        }
        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                _Url = value;
            }
        }
        public byte Display
        {
            get
            {
                return _Display;
            }
            set
            {
                _Display = value;
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
        public int TreeOrder
        {
            get
            {
                return _TreeOrder;
            }
            set
            {
                _TreeOrder = value;
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

        public byte ActionStatusId
        {
            get
            {
                return _ActionStatusId;
            }
            set
            {
                _ActionStatusId = value;
            }
        }


        #endregion

        #region Method
        /// <summary>
        /// Lấy danh sách đối tượng Actions từ DbDataReader
        /// </summary>
        /// <param name="cmd">DbCommand</param>
        /// <returns>list</returns>
        private List<Actions> Init(DbCommand cmd)
        {
            List<Actions> listActions = new List<Actions>();
            try
            {
                using (DbDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        Actions mActions = new Actions
                        {
                            ActionId = reader.ReadAs<short>("ActionId"),
                            ActionName = reader.ReadAs<string>("ActionName"),
                            ActionDesc = reader.ReadAs<string>("ActionDesc"),
                            ParentActionId = reader.ReadAs<short>("ParentActionId"),
                            Url = reader.ReadAs<string>("Url"),
                            ActionStatusId = reader.ReadAs<byte>("ActionStatusId"),
                            Display = reader.ReadAs<byte>("Display"),
                            DisplayOrder = reader.ReadAs<short>("DisplayOrder"),
                            TreeOrder = reader.ReadAs<int>("TreeOrder"),
                            CrDateTime = reader.ReadAs<DateTime>("CrDateTime"),
                        };
                        listActions.Add(mActions);
                    }
                }
                return listActions;
            }
            catch (Exception err)
            {
                throw new Exception("Data error: " + err.Message);
            }
        }
        //-----------------------------------------------------------
        /// <summary>
        /// Thêm đối tượng Actions
        /// </summary>
        /// <param name="sysMessageId"></param>
        /// <returns>sysMessageTypeId</returns>
        public byte Insert(ref int sysMessageId)
        {
            byte retVal = 0;
            try
            {
                retVal = InsertOrUpdate(ref sysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        //--------------------------------------------------------------
        /// <summary>
        /// Cập nhật đối tượng Actions
        /// </summary>
        /// <param name="sysMessageId"></param>
        /// <returns>sysMessageTypeId</returns>
        public byte Update(ref int sysMessageId)
        {
            byte retVal = 0;
            try
            {
                retVal = InsertOrUpdate(ref sysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }
        //-----------------------------------------------------------
        /// <summary>
        /// Thêm/Sửa đối tượng Actions
        /// </summary>
        /// <param name="sysMessageId"></param>
        /// <returns>sysMessageTypeId</returns>
        public byte InsertOrUpdate(ref int sysMessageId)
        {
            byte retVal = 0;
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("Actions_InsertOrUpdate");
                db.AddInParameter(cmd, "@ActionName", DbType.String, ActionName);
                db.AddInParameter(cmd, "@ActionDesc", DbType.String, ActionDesc);
                db.AddInParameter(cmd, "@ParentActionId", DbType.Int16, ParentActionId);
                db.AddInParameter(cmd, "@Url", DbType.String, Url);
                db.AddInParameter(cmd, "@ActionStatusId", DbType.Byte, ActionStatusId);
                db.AddInParameter(cmd, "@Display", DbType.Byte, Display);
                db.AddInParameter(cmd, "@DisplayOrder", DbType.Int16, DisplayOrder);
                db.AddInParameter(cmd, "@TreeOrder", DbType.Int32, TreeOrder);
                db.AddInputOutputParameter(cmd, "@ActionId", DbType.Int16, ActionId);
                db.AddOutParameter(cmd, "@sysMessageId", DbType.Int32);
                db.AddOutParameter(cmd, "@SysMessageTypeId", DbType.Byte);
                db.ExecuteNonQuery(cmd);
                ActionId = Convert.ToInt16(db.GetParameter(cmd, "@ActionId").Value == null ? "0" : db.GetParameter(cmd, "@ActionId").Value);
                sysMessageId = Convert.ToInt16(db.GetParameter(cmd, "@sysMessageId").Value == null ? "0" : db.GetParameter(cmd, "@sysMessageId").Value);
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
        /// Xóa đối tượng Actions
        /// </summary>
        /// <param name="sysMessageId">sysMessageId</param>
        /// <returns>sysMessageTypeId</returns>
        public byte Delete(ref int sysMessageId)
        {
            byte retVal = 0;
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("Actions_Delete");
                db.AddInParameter(cmd, "@ActionId", DbType.Int16, ActionId);
                db.AddOutParameter(cmd, "@sysMessageId", DbType.Int32);
                db.AddOutParameter(cmd, "@SysMessageTypeId", DbType.Byte);
                db.ExecuteNonQuery(cmd);
                sysMessageId = Convert.ToInt16(db.GetParameter(cmd, "@sysMessageId").Value == null ? "0" : db.GetParameter(cmd, "@sysMessageId").Value);
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
        /// Trả về đối tượng Actions theo điều kiện đầu vào
        /// </summary>
        /// <returns>Actions</returns>
        public Actions Get()
        {
            Actions retVal = new Actions();
            int rowCount = 0, pageSize = 20, pageNumber = 0;
            string dateFrom = string.Empty, dateTo = string.Empty, orderBy = string.Empty;
            try
            {
                List<Actions> list = GetPage(dateFrom, dateTo, orderBy, pageSize, pageNumber, ref rowCount);
                if (list.Count > 0) retVal = list[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }


        //-------------------------------------------------------------- 
        /// <summary>
        /// Danh sách Actions phân trang theo điều kiện đầu vào
        /// </summary>
        /// <param name="dateFrom">Từ ngày</param>
        /// <param name="dateTo">Đến ngày</param>
        /// <param name="orderBy">Sắp xếp theo</param>
        /// <param name="pageSize">Số records/trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="rowCount">Tổng số records</param>
        /// <returns>listActions</returns>
        public List<Actions> GetPage(string dateFrom, string dateTo, string orderBy, int pageSize, int pageNumber, ref int rowCount)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("Actions_GetPage");
                db.AddInParameter(cmd, "@ActionId", DbType.Int16, ActionId);
                db.AddInParameter(cmd, "@ActionName", DbType.String, ActionName);
                db.AddInParameter(cmd, "@ActionDesc", DbType.String, ActionDesc);
                db.AddInParameter(cmd, "@ParentActionId", DbType.Int16, ParentActionId);
                db.AddInParameter(cmd, "@Url", DbType.String, Url);
                db.AddInParameter(cmd, "@ActionStatusId", DbType.Byte, ActionStatusId);
                db.AddInParameter(cmd, "@Display", DbType.Byte, Display);
                db.AddInParameter(cmd, "@DisplayOrder", DbType.Int16, DisplayOrder);
                db.AddInParameter(cmd, "@TreeOrder", DbType.Int32, TreeOrder);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, dateFrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, dateTo);
                db.AddInParameter(cmd, "@OrderBy", DbType.String, orderBy);
                db.AddInParameter(cmd, "@PageSize", DbType.Int32, pageSize);
                db.AddInParameter(cmd, "@PageNumber", DbType.Int32, pageNumber);
                db.AddOutParameter(cmd, "@RowCount", DbType.Int32);
                List<Actions> listActions = Init(cmd);
                rowCount = Convert.ToInt32(db.GetParameter(cmd, "@RowCount").Value == null ? "0" : db.GetParameter(cmd, "@RowCount").Value);
                return listActions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //--------------------------------------------------------------
        /// <summary>
        /// Trả về ActionName theo actionId
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns>ActionName</returns>
        public static string Static_GetActionName(short actionId)
        {
            Actions mActions = new Actions
            {
                ActionId = actionId
            }.Get();
            return mActions.ActionName;
        }

        #endregion
    }
}

