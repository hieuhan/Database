/* ********************************************************************************
'     Document    :  ActionsEdit.aspx.cs
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
public partial class admin_pages_ActionsEdit : System.Web.UI.Page
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
                    DropDownListHelpers.DDL_Bind(ddlActionStatusId, ActionStatuss.Static_GetList(),  " ... ", "0");
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
            System.Int16 EditId;
            if(Request.QueryString["id"] == null)
            {
                return;
            }
            else
            {
                EditId = System.Int16.Parse(Request.QueryString["id"].ToString());
                Actions m_Actions = new Actions();
                m_Actions.ActionId = EditId;
                m_Actions=m_Actions.Get();
                txActionName.Text = m_Actions.ActionName.ToString();
                txActionDesc.Text = m_Actions.ActionDesc.ToString();
                txParentActionId.Text = m_Actions.ParentActionId.ToString();
                txUrl.Text = m_Actions.Url.ToString();
                    DropDownListHelpers.DDL_Bind(ddlActionStatusId, ActionStatuss.Static_GetList(),  " ... ", m_Actions.ActionStatusId.ToString());
                txDisplay.Text = m_Actions.Display.ToString();
                txDisplayOrder.Text = m_Actions.DisplayOrder.ToString();
                txTreeOrder.Text = m_Actions.TreeOrder.ToString();
                txCrDateTime.Text = m_Actions.CrDateTime.ToString("dd/MM/yyyy");
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int16 EditId;
        Actions m_Actions = new Actions(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int16.Parse(Request.QueryString["id"].ToString());
            m_Actions.ActionId = EditId;
            m_Actions = m_Actions.Get();
        }        
        try
		{
            if(txActionName.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }

            
            m_Actions.CrUserId = ActUserId;
            
            if(txActionName.Text != "")
				m_Actions.ActionName = txActionName.Text;
            
            if(txActionDesc.Text != "")
				m_Actions.ActionDesc = txActionDesc.Text;
            
            m_Actions.ParentActionId = short.Parse(txParentActionId.Text);
            
            if(txUrl.Text != "")
				m_Actions.Url = txUrl.Text;
            
            m_Actions.ActionStatusId = System.Byte.Parse(ddlActionStatusId.SelectedItem.Value);
            
            m_Actions.Display = byte.Parse(txDisplay.Text);
            
            m_Actions.DisplayOrder = short.Parse(txDisplayOrder.Text);
            
            m_Actions.TreeOrder = int.Parse(txTreeOrder.Text);
            
            m_Actions.ActionId = EditId;
            SysMessageTypeId = m_Actions.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
   