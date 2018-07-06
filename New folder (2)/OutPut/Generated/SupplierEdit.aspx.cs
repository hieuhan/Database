/* ********************************************************************************
'     Document    :  SupplierEdit.aspx.cs
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
public partial class admin_pages_SupplierEdit : System.Web.UI.Page
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
                Supplier m_Supplier = new Supplier();
                m_Supplier.SupplierId = EditId;
                m_Supplier=m_Supplier.Get();
                txSupplierName.Text = m_Supplier.SupplierName.ToString();
                txSupplierDesc.Text = m_Supplier.SupplierDesc.ToString();
                txAddress.Text = m_Supplier.Address.ToString();
                txImagePath.Text = m_Supplier.ImagePath.ToString();
                txMobile.Text = m_Supplier.Mobile.ToString();
                txTelephone.Text = m_Supplier.Telephone.ToString();
                txComments.Text = m_Supplier.Comments.ToString();
                txDisplayOrder.Text = m_Supplier.DisplayOrder.ToString();
                txReviewStatusId.Text = m_Supplier.ReviewStatusId.ToString();
                txCrDateTime.Text = m_Supplier.CrDateTime.ToString("dd/MM/yyyy");
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int32 EditId;
        Supplier m_Supplier = new Supplier(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            m_Supplier.SupplierId = EditId;
            m_Supplier = m_Supplier.Get();
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

            
            m_Supplier.CrUserId = ActUserId;
            
            if(txSupplierName.Text != "")
				m_Supplier.SupplierName = txSupplierName.Text;
            
            if(txSupplierDesc.Text != "")
				m_Supplier.SupplierDesc = txSupplierDesc.Text;
            
            if(txAddress.Text != "")
				m_Supplier.Address = txAddress.Text;
            
            if(txImagePath.Text != "")
				m_Supplier.ImagePath = txImagePath.Text;
            
            if(txMobile.Text != "")
				m_Supplier.Mobile = txMobile.Text;
            
            if(txTelephone.Text != "")
				m_Supplier.Telephone = txTelephone.Text;
            
            if(txComments.Text != "")
				m_Supplier.Comments = txComments.Text;
            
            m_Supplier.DisplayOrder = int.Parse(txDisplayOrder.Text);
            
            m_Supplier.ReviewStatusId = byte.Parse(txReviewStatusId.Text);
            
            m_Supplier.SupplierId = EditId;
            SysMessageTypeId = m_Supplier.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
   