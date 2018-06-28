using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CMSLibrary;

namespace Quanlybanhang.Models
{
    public class Actions
    {
        private short _ActionId;

        private string _ActionName;

        private string _ActionDesc;

        private short _ParentActionId;

        private string _Url;

        private byte _ActionStatusId;

        private byte _Display;

        private short _ActionOrder;

        private int _TreeOrder;

        private DatabaseHelper db;

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

        public byte ActionStatusId
        {
            get
            {
                return _ActionStatusId;
            }
            set
            {
                this._ActionStatusId = value;
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

        public short ActionOrder
        {
            get
            {
                return _ActionOrder;
            }
            set
            {
                _ActionOrder = value;
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

        public Actions()
        {
            db = new DatabaseHelper();
        }

        ~Actions()
        {
        }

        public virtual void Dispose()
        {
        }

        private List<Actions> Init(DbCommand cmd)
        {
            List<Actions> listActions = new List<Actions>();
            try
            {
                using (DbDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        Actions actions = new Actions
                        {
                            ActionId = reader.ReadAs<short>("ActionId"),
                            ActionName = reader.ReadAs<string>("ActionName"),
                            ActionDesc = reader.ReadAs<string>("ActionDesc"),
                            Url = reader.ReadAs<string>("Url"),
                            ActionStatusId = reader.ReadAs<byte>("ActionStatusId"),
                            ParentActionId = reader.ReadAs<short>("ParentActionId"),
                            Display = reader.ReadAs<byte>("Display"),
                            ActionOrder = reader.ReadAs<short>("ActionOrder")
                        };
                        listActions.Add(actions);
                    }
                }
                return listActions;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<Actions> GetPage(string dateFrom, string dateTo, string orderBy, int pageSize, int pageNumber, ref int rowCount)
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommond("Actions_GetPage");
                db.AddInParameter(cmd, "@ActionId", DbType.Int16, ActionId);
                db.AddInParameter(cmd, "@ActionName", DbType.String, ActionName);
                db.AddInParameter(cmd, "@ActionDesc", DbType.String, ActionDesc);
                db.AddInParameter(cmd, "@Url", DbType.String, Url);
                db.AddInParameter(cmd, "@ParentActionId", DbType.Int16, ParentActionId);
                db.AddInParameter(cmd, "@ActionOrder", DbType.Int16, ActionOrder);
                db.AddInParameter(cmd, "@Display", DbType.Byte, Display);
                db.AddInParameter(cmd, "@ActionStatusId", DbType.Byte, ActionStatusId);
                db.AddInParameter(cmd, "@DateFrom", DbType.String, dateFrom);
                db.AddInParameter(cmd, "@DateTo", DbType.String, dateTo);
                db.AddInParameter(cmd, "@OrderBy", DbType.String, orderBy);
                db.AddInParameter(cmd, "@PageSize", DbType.Int32, pageSize);
                db.AddInParameter(cmd, "@PageNumber", DbType.Int32, pageNumber);
                db.AddOutParameter(cmd, "@RowCount", DbType.Int32, Int32.MaxValue);
                List<Actions> list = Init(cmd);
                rowCount = Convert.ToInt32(db.GetParameter(cmd, "@RowCount").Value == null ? "0" : db.GetParameter(cmd, "@RowCount").Value);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}