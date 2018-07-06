/* ********************************************************************************
'     Document    :  UnitsEdit.aspx.cs
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
public partial class admin_pages_UnitsEdit : System.Web.UI.Page
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
                Units m_Units = new Units();
                m_Units.UnitId = EditId;
                m_Units=m_Units.Get();
                txUnitName.Text = m_Units.UnitName.ToString();
                txUnitDesc.Text = m_Units.UnitDesc.ToString();
                txCrDateTime.Text = m_Units.CrDateTime.ToString("dd/MM/yyyy");
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int16 EditId;
        Units m_Units = new Units(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int16.Parse(Request.QueryString["id"].ToString());
            m_Units.UnitId = EditId;
            m_Units = m_Units.Get();
        }        
        try
		{
            if(txUnitName.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }

            
            m_Units.CrUserId = ActUserId;
            
            if(txUnitName.Text != "")
				m_Units.UnitName = txUnitName.Text;
            
            if(txUnitDesc.Text != "")
				m_Units.UnitDesc = txUnitDesc.Text;
            
            m_Units.UnitId = EditId;
            SysMessageTypeId = m_Units.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
   