/* ********************************************************************************
'     Document    :  SectionErrorTypeEdit.aspx.cs
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
public partial class admin_pages_SectionErrorTypeEdit : System.Web.UI.Page
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
            System.Int32 EditId;
            if(Request.QueryString["id"] == null)
            {
                return;
            }
            else
            {
                EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
                SectionErrorType m_SectionErrorType = new SectionErrorType();
                m_SectionErrorType.SectionErrorTypeId = EditId;
                m_SectionErrorType=m_SectionErrorType.Get();
                txSectionErrorTypeName.Text = m_SectionErrorType.SectionErrorTypeName.ToString();
                txSectionErrorTypeDesc.Text = m_SectionErrorType.SectionErrorTypeDesc.ToString();
                txErrorCount.Text = m_SectionErrorType.ErrorCount.ToString();
                txDisplayOrder.Text = m_SectionErrorType.DisplayOrder.ToString();
                txSectionErrorId.Text = m_SectionErrorType.SectionErrorId.ToString();
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int32 EditId;
        SectionErrorType m_SectionErrorType = new SectionErrorType(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            m_SectionErrorType.SectionErrorTypeId = EditId;
            m_SectionErrorType = m_SectionErrorType.Get();
        }        
        try
		{
            if(txSectionErrorTypeName.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }
            if(txSectionErrorTypeDesc.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }
            if(txErrorCount.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }
            if(txDisplayOrder.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }

            
            m_SectionErrorType.CrUserId = ActUserId;
            
            if(txSectionErrorTypeName.Text != "")
				m_SectionErrorType.SectionErrorTypeName = txSectionErrorTypeName.Text;
            
            if(txSectionErrorTypeDesc.Text != "")
				m_SectionErrorType.SectionErrorTypeDesc = txSectionErrorTypeDesc.Text;
            
            m_SectionErrorType.ErrorCount = byte.Parse(txErrorCount.Text);
            
            m_SectionErrorType.DisplayOrder = byte.Parse(txDisplayOrder.Text);
            
            m_SectionErrorType.SectionErrorId = int.Parse(txSectionErrorId.Text);
            
            m_SectionErrorType.SectionErrorTypeId = EditId;
            SysMessageTypeId = m_SectionErrorType.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
   