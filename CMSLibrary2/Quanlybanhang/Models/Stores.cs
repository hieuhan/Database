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
    public class Stores
    {
        #region Private Properties
        private byte _StoreId;
        private string _StoreName;
        private string _StoreDesc;
        private string _Address;
        private string _Mobile;
        private string _Email;
        private string _Website;
        private DatabaseHelper db;

        #endregion

        #region Public Properties

        //-----------------------------------------------------------------
        public Stores()
        {
            db = new DatabaseHelper();
        }
        //-----------------------------------------------------------------
        ~Stores()
        {

        }
        //-----------------------------------------------------------------
        public virtual void Dispose()
        {

        }

        //-----------------------------------------------------------------    
        public byte StoreId
        {
            get
            {
                return _StoreId;
            }
            set
            {
                _StoreId = value;
            }
        }

        public string StoreName
        {
            get
            {
                return _StoreName;
            }
            set
            {
                _StoreName = value;
            }
        }
        public string StoreDesc
        {
            get
            {
                return _StoreDesc;
            }
            set
            {
                _StoreDesc = value;
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
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }
        public string Website
        {
            get
            {
                return _Website;
            }
            set
            {
                _Website = value;
            }
        }
        #endregion

        //--------------------------------------------------------------
        public byte Update(ref short SysMessageId)
        {
            byte RetVal = 0;
            try
            {
                RetVal = InsertOrUpdate(ref SysMessageId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }
        //-----------------------------------------------------------
        public byte InsertOrUpdate(ref short sysMessageId)
        {
            byte RetVal = 0;
            try
            {
                DbCommand cmd = db.GetStoredProcCommond("Stores_InsertOrUpdate");
                db.AddInputOutputParameter(cmd, "@StoreId", DbType.Byte,StoreId);
                db.AddInParameter(cmd, "@StoreName", DbType.String, StoreName);
                db.AddInParameter(cmd, "@StoreDesc", DbType.String, StoreDesc);
                db.AddInParameter(cmd, "@Address", DbType.String, Address);
                db.AddInParameter(cmd, "@Address", DbType.String, Address);
                db.AddInParameter(cmd, "@Mobile", DbType.String, Mobile);
                db.AddInParameter(cmd, "@Email", DbType.String, Email);
                db.AddInParameter(cmd, "@Website", DbType.String, Website);
                db.AddOutParameter(cmd, "@SysMessageId", DbType.Int32, Int32.MaxValue);
                db.AddOutParameter(cmd, "@SysMessageTypeId", DbType.Byte, Byte.MaxValue);
                db.ExecuteNonQuery(cmd);
                _StoreId = Convert.ToByte(db.GetParameter(cmd, "@StoreId").Value == null ? "0" : db.GetParameter(cmd, "@StoreId").Value);
                sysMessageId = Convert.ToInt16(db.GetParameter(cmd, "@SysMessageId").Value == null ? "0" : db.GetParameter(cmd, "@SysMessageId").Value);
                RetVal = Convert.ToByte(db.GetParameter(cmd, "@SysMessageTypeId").Value == null ? "0" : db.GetParameter(cmd, "@SysMessageTypeId").Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RetVal;
        }

    }
}