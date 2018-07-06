/* ********************************************************************************
'     Document    :  ProductTypesEdit.aspx.cs
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
public partial class admin_pages_ProductTypesEdit : System.Web.UI.Page
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
                ProductTypes m_ProductTypes = new ProductTypes();
                m_ProductTypes.ProductTypeId = EditId;
                m_ProductTypes=m_ProductTypes.Get();
                txProductTypeName.Text = m_ProductTypes.ProductTypeName.ToString();
                txProductTypeDesc.Text = m_ProductTypes.ProductTypeDesc.ToString();
                txDisplayOrder.Text = m_ProductTypes.DisplayOrder.ToString();
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int16 EditId;
        ProductTypes m_ProductTypes = new ProductTypes(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int16.Parse(Request.QueryString["id"].ToString());
            m_ProductTypes.ProductTypeId = EditId;
            m_ProductTypes = m_ProductTypes.Get();
        }        
        try
		{
            if(txProductTypeName.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }

            
            m_ProductTypes.CrUserId = ActUserId;
            
            if(txProductTypeName.Text != "")
				m_ProductTypes.ProductTypeName = txProductTypeName.Text;
            
            if(txProductTypeDesc.Text != "")
				m_ProductTypes.ProductTypeDesc = txProductTypeDesc.Text;
            
            m_ProductTypes.DisplayOrder = short.Parse(txDisplayOrder.Text);
            
            m_ProductTypes.ProductTypeId = EditId;
            SysMessageTypeId = m_ProductTypes.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
   