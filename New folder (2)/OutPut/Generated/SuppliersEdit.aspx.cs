/* ********************************************************************************
'     Document    :  SuppliersEdit.aspx.cs
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
public partial class admin_pages_SuppliersEdit : System.Web.UI.Page
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
                Suppliers m_Suppliers = new Suppliers();
                m_Suppliers.SupplierId = EditId;
                m_Suppliers=m_Suppliers.Get();
                txSupplierName.Text = m_Suppliers.SupplierName.ToString();
                txAddress.Text = m_Suppliers.Address.ToString();
                txMobile.Text = m_Suppliers.Mobile.ToString();
                txEmail.Text = m_Suppliers.Email.ToString();
                txContact.Text = m_Suppliers.Contact.ToString();
                txDebitBalance.Text = m_Suppliers.DebitBalance.ToString();
                txNote.Text = m_Suppliers.Note.ToString();
                txDisplayOrder.Text = m_Suppliers.DisplayOrder.ToString();
                txReviewStatusId.Text = m_Suppliers.ReviewStatusId.ToString();
                txCrDateTime.Text = m_Suppliers.CrDateTime.ToString("dd/MM/yyyy");
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int32 EditId;
        Suppliers m_Suppliers = new Suppliers(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            m_Suppliers.SupplierId = EditId;
            m_Suppliers = m_Suppliers.Get();
        }        
        try
		{
            if(txSupplierName.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }
            if(txReviewStatusId.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }

            
            m_Suppliers.CrUserId = ActUserId;
            
            if(txSupplierName.Text != "")
				m_Suppliers.SupplierName = txSupplierName.Text;
            
            if(txAddress.Text != "")
				m_Suppliers.Address = txAddress.Text;
            
            if(txMobile.Text != "")
				m_Suppliers.Mobile = txMobile.Text;
            
            if(txEmail.Text != "")
				m_Suppliers.Email = txEmail.Text;
            
            if(txContact.Text != "")
				m_Suppliers.Contact = txContact.Text;
            
            if(txDebitBalance.Text != "")
				m_Suppliers.DebitBalance = txDebitBalance.Text;
            
            if(txNote.Text != "")
				m_Suppliers.Note = txNote.Text;
            
            m_Suppliers.DisplayOrder = int.Parse(txDisplayOrder.Text);
            
            m_Suppliers.ReviewStatusId = byte.Parse(txReviewStatusId.Text);
            
            m_Suppliers.SupplierId = EditId;
            SysMessageTypeId = m_Suppliers.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
   