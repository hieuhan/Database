<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="StoresEdit.aspx.cs" Inherits="admin_pages_StoresEdit" Title="" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">   
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter" >
        
        <tr>
            <td>
                &nbsp;</td>
            <td>
               StoreName
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txStoreName" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               StoreDesc
               :
            </td>
            <td>
                <asp:TextBox ID="txStoreDesc" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Address
               :
            </td>
            <td>
                <asp:TextBox ID="txAddress" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Mobile
               :
            </td>
            <td>
                <asp:TextBox ID="txMobile" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Email
               :
            </td>
            <td>
                <asp:TextBox ID="txEmail" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Website
               :
            </td>
            <td>
                <asp:TextBox ID="txWebsite" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
       
    </table>
    <div style="text-align:center; padding: 20px">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom"  Text="Lưu thông tin" meta:resourcekey="btnSave" 
            onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

