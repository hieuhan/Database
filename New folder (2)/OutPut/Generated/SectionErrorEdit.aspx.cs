/* ********************************************************************************
'     Document    :  SectionErrorEdit.aspx.cs
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
public partial class admin_pages_SectionErrorEdit : System.Web.UI.Page
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
                SectionError m_SectionError = new SectionError();
                m_SectionError.SectionErrorId = EditId;
                m_SectionError=m_SectionError.Get();
                txSectionErrorName.Text = m_SectionError.SectionErrorName.ToString();
                txSectionErrorDesc.Text = m_SectionError.SectionErrorDesc.ToString();
                txDisplayOrder.Text = m_SectionError.DisplayOrder.ToString();
                txSectionServiceId.Text = m_SectionError.SectionServiceId.ToString();
                txErrorTypeStatusId.Text = m_SectionError.ErrorTypeStatusId.ToString();
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int32 EditId;
        SectionError m_SectionError = new SectionError(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            m_SectionError.SectionErrorId = EditId;
            m_SectionError = m_SectionError.Get();
        }        
        try
		{
            if(txSectionErrorName.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }
            if(txSectionErrorDesc.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }
            if(txDisplayOrder.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }
            if(txSectionServiceId.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }
            if(txErrorTypeStatusId.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }

            
            m_SectionError.CrUserId = ActUserId;
            
            if(txSectionErrorName.Text != "")
				m_SectionError.SectionErrorName = txSectionErrorName.Text;
            
            if(txSectionErrorDesc.Text != "")
				m_SectionError.SectionErrorDesc = txSectionErrorDesc.Text;
            
            m_SectionError.DisplayOrder = byte.Parse(txDisplayOrder.Text);
            
            m_SectionError.SectionServiceId = int.Parse(txSectionServiceId.Text);
            
            m_SectionError.ErrorTypeStatusId = byte.Parse(txErrorTypeStatusId.Text);
            
            m_SectionError.SectionErrorId = EditId;
            SysMessageTypeId = m_SectionError.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
   