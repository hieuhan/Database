/* ********************************************************************************
'     Document    :  PriceListsEdit.aspx.cs
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
public partial class admin_pages_PriceListsEdit : System.Web.UI.Page
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
                PriceLists m_PriceLists = new PriceLists();
                m_PriceLists.PriceList = EditId;
                m_PriceLists=m_PriceLists.Get();
                txPriceListName.Text = m_PriceLists.PriceListName.ToString();
                txPriceListDesc.Text = m_PriceLists.PriceListDesc.ToString();
                txPriceListTypeId.Text = m_PriceLists.PriceListTypeId.ToString();
                txIsDetail.Text = m_PriceLists.IsDetail.ToString();
                txStatusId.Text = m_PriceLists.StatusId.ToString();
                txDisplayOrder.Text = m_PriceLists.DisplayOrder.ToString();
                txCrUserId.Text = m_PriceLists.CrUserId.ToString();
                txCrDateTime.Text = m_PriceLists.CrDateTime.ToString("dd/MM/yyyy");
            }
        }
    protected void btnSave_Click(object sender, System.EventArgs e)
	{
		byte SysMessageTypeId = 0;
        int SysMessageId = 0;
        System.Int32 EditId;
        PriceLists m_PriceLists = new PriceLists(); 
        if(Request.QueryString["id"] == null)
        {
            EditId = 0;
        }
        else
        {
            EditId = System.Int32.Parse(Request.QueryString["id"].ToString());
            m_PriceLists.PriceList = EditId;
            m_PriceLists = m_PriceLists.Get();
        }        
        try
		{
            if(txPriceListName.Text == "")
			{
                JSAlertHelpers.Alert("Mời bạn nhập các thông tin bắt buộc!", this);
                return;
            }

            
            m_PriceLists.CrUserId = ActUserId;
            
            if(txPriceListName.Text != "")
				m_PriceLists.PriceListName = txPriceListName.Text;
            
            if(txPriceListDesc.Text != "")
				m_PriceLists.PriceListDesc = txPriceListDesc.Text;
            
            m_PriceLists.PriceListTypeId = byte.Parse(txPriceListTypeId.Text);
            
            m_PriceLists.IsDetail = byte.Parse(txIsDetail.Text);
            
            m_PriceLists.StatusId = byte.Parse(txStatusId.Text);
            
            m_PriceLists.DisplayOrder = int.Parse(txDisplayOrder.Text);
            
            m_PriceLists.UpdateUserId = int.Parse(txUpdateUserId.Text);
            
            m_PriceLists.PriceList = EditId;
            SysMessageTypeId = m_PriceLists.InsertOrUpdate(ConstantHelpers.Replicated, ActUserId, ref SysMessageId);
            
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
   