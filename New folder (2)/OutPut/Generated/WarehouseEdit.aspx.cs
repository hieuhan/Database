/* ********************************************************************************
'     Document    :  WarehouseEdit.aspx.cs
' ********************************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Reflection;
using System.IO;
using ICSoft.HelperLib;
using sms.common;
using sms.utils;
using HealthLib;
public partial class admin_pages_WarehouseEdit : System.Web.UI.Page
{		
	
    private int ActUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
	{
        
		try
        {
            ActUserId = SessionHelpers.GetUserId();
            if (!IsPostBack)
            {
                if(Request.QueryString["id"] == null)
                {
                }
                else
                    bindData();
            }
        }
        catch (Exception ex)
        {
        	sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
			JSAlertHelpers.Alert(ex.Message, this);
        }		
    }
    //--------------------------------------------------------------------------------
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindData();
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindData();
    }
    private void bindData()
        {    
            System.Byte EditId;
            if(Request.QueryString["id"] == null)
            {
                return;
            }
            else
            {
                EditId = System.Byte.Parse(Request.QueryString["id"].ToString());
                Warehouse m_Warehouse = new Warehouse();
                m_Warehouse.WarehouseId = EditId;
                m_Warehouse=m_Warehouse.Get();
                txWarehouseName.Text = m_Warehouse.WarehouseName.ToString();
                txWarehouseDesc.Text = m_Warehouse.WarehouseDesc.ToString();
                txAddress.Text = m_Warehouse.Address.ToString();
                txMobile.Text = m_Warehouse.Mobile.ToString();
                txWarehouseStatus.Text = m_Warehouse.WarehouseStatus.ToString();
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Byte EditId;
        Warehouse m_Warehouse = new Warehouse(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Byte.Parse(Request.QueryString["id"].ToString());
            m_Warehouse.WarehouseId = EditId;
            m_Warehouse = m_Warehouse.Get();
        }        
        try
		{
            if(txWarehouseName.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }

            
            m_Warehouse.CrUserId = ActUserId;
            
            if(txWarehouseName.Text != "")
				m_Warehouse.WarehouseName = txWarehouseName.Text;
            
            if(txWarehouseDesc.Text != "")
				m_Warehouse.WarehouseDesc = txWarehouseDesc.Text;
            
            if(txAddress.Text != "")
				m_Warehouse.Address = txAddress.Text;
            
            if(txMobile.Text != "")
				m_Warehouse.Mobile = txMobile.Text;
            
            m_Warehouse.WarehouseStatus = byte.Parse(txWarehouseStatus.Text);
            
            m_Warehouse.WarehouseId = EditId;
            SysMessageTypeId = m_Warehouse.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
            StringBuilder csText = new StringBuilder();
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            csText.Clear();
            csText.Append("<script type=\"text/javascript\">");
            csText.Append("window.parent.jQuery('#divEdit').dialog('close');");            
            csText.Append("</script>");
            cs = Page.ClientScript;
            cs.RegisterClientScriptBlock(this.GetType(), "system_message", csText.ToString());
		}
        catch (Exception ex)
		{
			sms.utils.LogFiles.LogError(((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name + "\t" + ex.ToString());
			JSAlertHelpers.Alert(ex.Message, this);
		}
    }
    
   
    
 }
   