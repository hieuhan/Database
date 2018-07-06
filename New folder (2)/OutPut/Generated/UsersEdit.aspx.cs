/* ********************************************************************************
'     Document    :  UsersEdit.aspx.cs
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
public partial class admin_pages_UsersEdit : System.Web.UI.Page
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
                    DropDownListHelpers.DDL_Bind(ddlUserStatusId, UserStatuss.Static_GetList(),  " ... ", "0");
                    DropDownListHelpers.DDL_Bind(ddlUserStatusId, UserTypes.Static_GetList(),  " ... ", "0");
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
                Users m_Users = new Users();
                m_Users.UserId = EditId;
                m_Users=m_Users.Get();
                txUserName.Text = m_Users.UserName.ToString();
                txPassword.Text = m_Users.Password.ToString();
                txFullName.Text = m_Users.FullName.ToString();
                txAddress.Text = m_Users.Address.ToString();
                txEmail.Text = m_Users.Email.ToString();
                txMobile.Text = m_Users.Mobile.ToString();
                txGenderId.Text = m_Users.GenderId.ToString();
                    DropDownListHelpers.DDL_Bind(ddlUserStatusId, UserStatuss.Static_GetList(),  " ... ", m_Users.UserStatusId.ToString());
                    DropDownListHelpers.DDL_Bind(ddlUserStatusId, UserTypes.Static_GetList(),  " ... ", m_Users.UserTypeId.ToString());
                txDefaultActionId.Text = m_Users.DefaultActionId.ToString();
                txBirthDay.Text = m_Users.BirthDay.ToString("dd/MM/yyyy");
                txCrDateTime.Text = m_Users.CrDateTime.ToString("dd/MM/yyyy");
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int32 EditId;
        Users m_Users = new Users(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            m_Users.UserId = EditId;
            m_Users = m_Users.Get();
        }        
        try
		{
            if(txUserName.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }
            if(txPassword.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }

            
            m_Users.CrUserId = ActUserId;
            
            if(txUserName.Text != "")
				m_Users.UserName = txUserName.Text;
            
            if(txPassword.Text != "")
				m_Users.Password = txPassword.Text;
            
            if(txFullName.Text != "")
				m_Users.FullName = txFullName.Text;
            
            if(txAddress.Text != "")
				m_Users.Address = txAddress.Text;
            
            if(txEmail.Text != "")
				m_Users.Email = txEmail.Text;
            
            if(txMobile.Text != "")
				m_Users.Mobile = txMobile.Text;
            
            m_Users.GenderId = byte.Parse(txGenderId.Text);
            
            m_Users.UserStatusId = System.Byte.Parse(ddlUserStatusId.SelectedItem.Value);
            
            m_Users.UserTypeId = System.Byte.Parse(ddlUserTypeId.SelectedItem.Value);
            
            m_Users.DefaultActionId = short.Parse(txDefaultActionId.Text);
            
            if(txBirthDay.Text != "")
                m_Users.BirthDay = DateTime.Parse(txBirthDay.Text);
            
            
            m_Users.UserId = EditId;
            SysMessageTypeId = m_Users.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
   