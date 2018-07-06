/* ********************************************************************************
'     Document    :  OnePayTransEdit.aspx.cs
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
public partial class admin_pages_OnePayTransEdit : System.Web.UI.Page
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
                OnePayTrans m_OnePayTrans = new OnePayTrans();
                m_OnePayTrans.TransId = EditId;
                m_OnePayTrans=m_OnePayTrans.Get();
                txvpc_OrderInfo.Text = m_OnePayTrans.vpc_OrderInfo.ToString();
                txvpc_Amount.Text = m_OnePayTrans.vpc_Amount.ToString();
                txvpc_TicketNo.Text = m_OnePayTrans.vpc_TicketNo.ToString();
                txAgainLink.Text = m_OnePayTrans.AgainLink.ToString();
                txvpc_TxnResponseCode.Text = m_OnePayTrans.vpc_TxnResponseCode.ToString();
                txvpc_TransactionNo.Text = m_OnePayTrans.vpc_TransactionNo.ToString();
                txvcp_Message.Text = m_OnePayTrans.vcp_Message.ToString();
                txCustomerId.Text = m_OnePayTrans.CustomerId.ToString();
                txCrDateTime.Text = m_OnePayTrans.CrDateTime.ToString("dd/MM/yyyy");
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int32 EditId;
        OnePayTrans m_OnePayTrans = new OnePayTrans(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            m_OnePayTrans.TransId = EditId;
            m_OnePayTrans = m_OnePayTrans.Get();
        }        
        try
		{
            if(txvpc_TxnResponseCode.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }

            
            m_OnePayTrans.CrUserId = ActUserId;
            
            if(txvpc_OrderInfo.Text != "")
				m_OnePayTrans.vpc_OrderInfo = txvpc_OrderInfo.Text;
            
            m_OnePayTrans.vpc_Amount = int.Parse(txvpc_Amount.Text);
            
            if(txvpc_TicketNo.Text != "")
				m_OnePayTrans.vpc_TicketNo = txvpc_TicketNo.Text;
            
            if(txAgainLink.Text != "")
				m_OnePayTrans.AgainLink = txAgainLink.Text;
            
            m_OnePayTrans.vpc_TxnResponseCode = int.Parse(txvpc_TxnResponseCode.Text);
            
            if(txvpc_TransactionNo.Text != "")
				m_OnePayTrans.vpc_TransactionNo = txvpc_TransactionNo.Text;
            
            if(txvcp_Message.Text != "")
				m_OnePayTrans.vcp_Message = txvcp_Message.Text;
            
            m_OnePayTrans.CustomerId = int.Parse(txCustomerId.Text);
            
            if(txResponseTime.Text != "")
                m_OnePayTrans.ResponseTime = DateTime.Parse(txResponseTime.Text);
            
            
            m_OnePayTrans.TransId = EditId;
            SysMessageTypeId = m_OnePayTrans.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
   