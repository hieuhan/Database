<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="ActionsEdit.aspx.cs" Inherits="admin_pages_ActionsEdit" Title="" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">   
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter" >
        
        <tr>
            <td>
                &nbsp;</td>
            <td>
               ActionName
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txActionName" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               ActionDesc
               :
            </td>
            <td>
                <asp:TextBox ID="txActionDesc" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               ParentActionId
               :
            </td>
            <td>
                <asp:TextBox ID="txParentActionId" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Url
               :
            </td>
            <td>
                <asp:TextBox ID="txUrl" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
         <tr>
            <td>
                &nbsp;</td>
            <td>
               ActionStatusId:
            </td>
            <td>
                <asp:DropDownList ID="ddlActionStatusId" runat="server" DataTextField="ActionStatusName" DataValueField="ActionStatusId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Display
               :
            </td>
            <td>
                <asp:TextBox ID="txDisplay" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               DisplayOrder
               :
            </td>
            <td>
                <asp:TextBox ID="txDisplayOrder" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               TreeOrder
               :
            </td>
            <td>
                <asp:TextBox ID="txTreeOrder" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
          <tr>
            <td>
                &nbsp;</td>
            <td>
               CrDateTime:
            </td>
            <td>
                <asp:TextBox CssClass= "tbdatepicker" ID="txCrDateTime" runat="server" width="70px"></asp:TextBox>                 
            </td>
        </tr> 
       
    </table>
    <div style="text-align:center; padding: 20px">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom"  Text="Lưu thông tin" meta:resourcekey="btnSave" 
            onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

