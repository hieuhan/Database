<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="UsersEdit.aspx.cs" Inherits="admin_pages_UsersEdit" Title="" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">   
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter" >
        
        <tr>
            <td>
                &nbsp;</td>
            <td>
               UserName
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txUserName" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Password
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txPassword" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               FullName
               :
            </td>
            <td>
                <asp:TextBox ID="txFullName" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
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
               GenderId
               :
            </td>
            <td>
                <asp:TextBox ID="txGenderId" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
         <tr>
            <td>
                &nbsp;</td>
            <td>
               UserStatusId:
            </td>
            <td>
                <asp:DropDownList ID="ddlUserStatusId" runat="server" DataTextField="UserStatusName" DataValueField="UserStatusId">
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td>
                &nbsp;</td>
            <td>
               UserTypeId:
            </td>
            <td>
                <asp:DropDownList ID="ddlUserTypeId" runat="server" DataTextField="UserTypeName" DataValueField="UserTypeId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
               DefaultActionId
               :
            </td>
            <td>
                <asp:TextBox ID="txDefaultActionId" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
          <tr>
            <td>
                &nbsp;</td>
            <td>
               BirthDay:
            </td>
            <td>
                <asp:TextBox CssClass= "tbdatepicker" ID="txBirthDay" runat="server" width="70px"></asp:TextBox>                 
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

