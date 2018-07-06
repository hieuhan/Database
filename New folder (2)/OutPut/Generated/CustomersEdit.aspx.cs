/* ********************************************************************************
'     Document    :  CustomersEdit.aspx.cs
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
public partial class admin_pages_CustomersEdit : System.Web.UI.Page
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
                Customers m_Customers = new Customers();
                m_Customers.CustomerId = EditId;
                m_Customers=m_Customers.Get();
                txFullName.Text = m_Customers.FullName.ToString();
                txMobile.Text = m_Customers.Mobile.ToString();
                txEmail.Text = m_Customers.Email.ToString();
                txAddress.Text = m_Customers.Address.ToString();
                txGenderId.Text = m_Customers.GenderId.ToString();
                txCustomerGroupId.Text = m_Customers.CustomerGroupId.ToString();
                txNote.Text = m_Customers.Note.ToString();
                txStatusId.Text = m_Customers.StatusId.ToString();
                txDebitBalance.Text = m_Customers.DebitBalance.ToString();
                txPaymentLimit.Text = m_Customers.PaymentLimit.ToString();
                txDateOfBirth.Text = m_Customers.DateOfBirth.ToString("dd/MM/yyyy");
                txLastTradingDay.Text = m_Customers.LastTradingDay.ToString("dd/MM/yyyy");
                txCrDateTime.Text = m_Customers.CrDateTime.ToString("dd/MM/yyyy");
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int32 EditId;
        Customers m_Customers = new Customers(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            m_Customers.CustomerId = EditId;
            m_Customers = m_Customers.Get();
        }        
        try
		{
            if(txFullName.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }
            if(txStatusId.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }

            
            m_Customers.CrUserId = ActUserId;
            
            if(txFullName.Text != "")
				m_Customers.FullName = txFullName.Text;
            
            if(txMobile.Text != "")
				m_Customers.Mobile = txMobile.Text;
            
            if(txEmail.Text != "")
				m_Customers.Email = txEmail.Text;
            
            if(txAddress.Text != "")
				m_Customers.Address = txAddress.Text;
            
            m_Customers.GenderId = byte.Parse(txGenderId.Text);
            
            m_Customers.CustomerGroupId = short.Parse(txCustomerGroupId.Text);
            
            if(txNote.Text != "")
				m_Customers.Note = txNote.Text;
            
            m_Customers.StatusId = byte.Parse(txStatusId.Text);
            
            if(txDebitBalance.Text != "")
				m_Customers.DebitBalance = txDebitBalance.Text;
            
            if(txPaymentLimit.Text != "")
				m_Customers.PaymentLimit = txPaymentLimit.Text;
            
            if(txDateOfBirth.Text != "")
                m_Customers.DateOfBirth = DateTime.Parse(txDateOfBirth.Text);
            
            
            if(txLastTradingDay.Text != "")
                m_Customers.LastTradingDay = DateTime.Parse(txLastTradingDay.Text);
            
            
            m_Customers.CustomerId = EditId;
            SysMessageTypeId = m_Customers.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
   