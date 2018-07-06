/* ********************************************************************************
'     Document    :  PriceListDetailsEdit.aspx.cs
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
public partial class admin_pages_PriceListDetailsEdit : System.Web.UI.Page
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
                PriceListDetails m_PriceListDetails = new PriceListDetails();
                m_PriceListDetails.PriceListDetailId = EditId;
                m_PriceListDetails=m_PriceListDetails.Get();
                txPriceListId.Text = m_PriceListDetails.PriceListId.ToString();
                txProductId.Text = m_PriceListDetails.ProductId.ToString();
                txUnitId.Text = m_PriceListDetails.UnitId.ToString();
                txPrice.Text = m_PriceListDetails.Price.ToString();
                txCrUserId.Text = m_PriceListDetails.CrUserId.ToString();
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int32 EditId;
        PriceListDetails m_PriceListDetails = new PriceListDetails(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            m_PriceListDetails.PriceListDetailId = EditId;
            m_PriceListDetails = m_PriceListDetails.Get();
        }        
        try
		{
            if(txPriceListId.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }
            if(txProductId.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }

            
            m_PriceListDetails.CrUserId = ActUserId;
            
            m_PriceListDetails.PriceListId = int.Parse(txPriceListId.Text);
            
            m_PriceListDetails.ProductId = int.Parse(txProductId.Text);
            
            m_PriceListDetails.UnitId = short.Parse(txUnitId.Text);
            
            if(txPrice.Text != "")
				m_PriceListDetails.Price = txPrice.Text;
            
            m_PriceListDetails.PriceListDetailId = EditId;
            SysMessageTypeId = m_PriceListDetails.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
   