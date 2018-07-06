/* ********************************************************************************
'     Document    :  Users.aspx.cs
' ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ICSoft.HelperLib;
using ICSoft.CMSLibFunringReport;
using System.Globalization;
using System.Data;

public partial class admin_pages_Users : System.Web.UI.Page
{	
    protected int ActUserId = 0;
    protected void Page_Load(object sender, EventArgs e)
	{
        ActUserId = SessionHelpers.GetUserId();
		try
        {
            if (!IsPostBack)
            {
               
                DropDownListHelpers.DDL_Bind(ddlUserStatusId, UserStatuss.Static_GetList(),  " ... ", "0");
                DropDownListHelpers.DDL_Bind(ddlUserStatusId, UserTypes.Static_GetList(),  " ... ", "0");
				ShowGrid();					
            }
            else if (CustomPaging.ChangePage)
            {
               ShowGrid();
            }
        }
        catch (Exception ex)
        {
        	sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
			
        }		
    }
    //--------------------------------------------------------------------------------
   
    private void ShowGrid()
	{
        try
		{
            Users m_Users = new Users();
            m_Users.UserName = txtSearch.Text;          
            m_Users.UserStatusId = System.Byte.Parse(ddlUserStatusId.SelectedValue);
            m_Users.UserTypeId = System.Byte.Parse(ddlUserTypeId.SelectedValue);
            string DateFrom = txtDateFrom.Text;
            string DateTo = txtDateTo.Text;
            int RowCount = 0;
            List<Users>  l_Users = m_Users.GetPage("", "", ddlOrderBy.SelectedValue, m_grid.PageSize, CustomPaging.PageIndex - 1, ref RowCount);
        
            m_grid.DataSource = l_Users;
            m_grid.DataBind();
            lblTong.Text = RowCount.ToString();
            CustomPaging.TotalPage = (RowCount == 0) ? 1 : (RowCount % m_grid.PageSize == 0) ? RowCount / m_grid.PageSize : (RowCount - RowCount % m_grid.PageSize) / m_grid.PageSize + 1;
		}
        catch (Exception ex)
		{
			sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
			
		}
    }
     // End Show Grid      
    protected void m_grid_OnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbDelete = (LinkButton)e.Row.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.OnClientClick = "return confirm('" + GetGlobalResourceObject("AdminResource", "DeleteConfirm").ToString() + "');";
            }                      
           
        }
    }   
    
    
    protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
        string message = "";       
		try
		{
             Users m_Users = new Users();
            m_Users.UserId = System.Int32.Parse(m_grid.DataKeys[e.RowIndex].Value.ToString());
            m_Users.Delete();
            
           
		}
        catch (Exception ex)
		{
			sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
			
		}
        ShowGrid();
    }
       
    protected void ddlOrderBy_SelectedIndexChanged(object sender, System.EventArgs e)
	{
       ShowGrid();
    }

    protected void ddlUserStatusId_SelectedIndexChanged(object sender, System.EventArgs e)
	{       
        ShowGrid();
    }
    protected void ddlUserTypeId_SelectedIndexChanged(object sender, System.EventArgs e)
	{       
        ShowGrid();
    }
    
    protected void btnSearch_Click(object sender, System.EventArgs e)
	{       
        ShowGrid();
    }
	protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
		{
            Users m_Users = new Users();
            foreach(GridViewRow m_Row in m_grid.Rows)
			{
                CheckBox chkAction = (CheckBox)m_Row.FindControl("chkAction");
                if(chkAction != null)
				{
                    if(chkAction.Checked)
					{
                        m_Users.UserId = System.Int32.Parse(m_grid.DataKeys[m_Row.RowIndex].Value.ToString());
                        m_Users.Delete();
                    }
                }
            }           
            ShowGrid();
		}
        catch (Exception ex)
		{
			sms.utils.Log.writeLog(ex.ToString(), ((new System.Diagnostics.StackTrace()).GetFrames()[0]).GetMethod().Name);
			
		}
    }
    protected void lbUnCheck_Click(object sender, EventArgs e)
    {

    }
    protected void lbReview_Click(object sender, EventArgs e)
    {

    }
 }
   