/* ********************************************************************************
'     Document    :  MediaGroupsEdit.aspx.cs
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
public partial class admin_pages_MediaGroupsEdit : System.Web.UI.Page
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
            System.Int16 EditId;
            if(Request.QueryString["id"] == null)
            {
                return;
            }
            else
            {
                EditId = System.Int16.Parse(Request.QueryString["id"].ToString());
                MediaGroups m_MediaGroups = new MediaGroups();
                m_MediaGroups.MediaGroupId = EditId;
                m_MediaGroups=m_MediaGroups.Get();
                txMediaGroupName.Text = m_MediaGroups.MediaGroupName.ToString();
                txMediaGroupDesc.Text = m_MediaGroups.MediaGroupDesc.ToString();
                txParentGroupId.Text = m_MediaGroups.ParentGroupId.ToString();
                txCrUserId.Text = m_MediaGroups.CrUserId.ToString();
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int16 EditId;
        MediaGroups m_MediaGroups = new MediaGroups(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int16.Parse(Request.QueryString["id"].ToString());
            m_MediaGroups.MediaGroupId = EditId;
            m_MediaGroups = m_MediaGroups.Get();
        }        
        try
		{

            
            m_MediaGroups.CrUserId = ActUserId;
            
            if(txMediaGroupName.Text != "")
				m_MediaGroups.MediaGroupName = txMediaGroupName.Text;
            
            if(txMediaGroupDesc.Text != "")
				m_MediaGroups.MediaGroupDesc = txMediaGroupDesc.Text;
            
            m_MediaGroups.ParentGroupId = short.Parse(txParentGroupId.Text);
            
            m_MediaGroups.MediaGroupId = EditId;
            SysMessageTypeId = m_MediaGroups.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
   