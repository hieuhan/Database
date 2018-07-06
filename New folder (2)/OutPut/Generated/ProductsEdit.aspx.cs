/* ********************************************************************************
'     Document    :  ProductsEdit.aspx.cs
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
public partial class admin_pages_ProductsEdit : System.Web.UI.Page
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
                Products m_Products = new Products();
                m_Products.ProductId = EditId;
                m_Products=m_Products.Get();
                txProductName.Text = m_Products.ProductName.ToString();
                txImagePath.Text = m_Products.ImagePath.ToString();
                txManufacturerId.Text = m_Products.ManufacturerId.ToString();
                txUnitId.Text = m_Products.UnitId.ToString();
                txProductGroupId.Text = m_Products.ProductGroupId.ToString();
                txProductTypeId.Text = m_Products.ProductTypeId.ToString();
                txOriginId.Text = m_Products.OriginId.ToString();
                txWarrantyId.Text = m_Products.WarrantyId.ToString();
                txStatusId.Text = m_Products.StatusId.ToString();
                txProductContent.Text = m_Products.ProductContent.ToString();
                txDisplayOrder.Text = m_Products.DisplayOrder.ToString();
                txCrUserId.Text = m_Products.CrUserId.ToString();
                txCrDateTime.Text = m_Products.CrDateTime.ToString("dd/MM/yyyy");
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int32 EditId;
        Products m_Products = new Products(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            m_Products.ProductId = EditId;
            m_Products = m_Products.Get();
        }        
        try
		{
            if(txProductName.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }
            if(txProductContent.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }

            
            m_Products.CrUserId = ActUserId;
            
            if(txProductName.Text != "")
				m_Products.ProductName = txProductName.Text;
            
            if(txImagePath.Text != "")
				m_Products.ImagePath = txImagePath.Text;
            
            m_Products.ManufacturerId = int.Parse(txManufacturerId.Text);
            
            m_Products.UnitId = short.Parse(txUnitId.Text);
            
            m_Products.ProductGroupId = short.Parse(txProductGroupId.Text);
            
            m_Products.ProductTypeId = short.Parse(txProductTypeId.Text);
            
            m_Products.OriginId = short.Parse(txOriginId.Text);
            
            m_Products.WarrantyId = short.Parse(txWarrantyId.Text);
            
            m_Products.StatusId = byte.Parse(txStatusId.Text);
            
            if(txProductContent.Text != "")
				m_Products.ProductContent = txProductContent.Text;
            
            m_Products.DisplayOrder = int.Parse(txDisplayOrder.Text);
            
            m_Products.UpdateUserId = int.Parse(txUpdateUserId.Text);
            
            m_Products.ProductId = EditId;
            SysMessageTypeId = m_Products.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
   