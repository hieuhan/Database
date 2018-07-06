<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="CustomerFieldsEdit.aspx.cs" Inherits="admin_pages_CustomerFieldsEdit" Title="" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">   
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter" >
        
        <tr>
            <td>
                &nbsp;</td>
            <td>
               CustomerId
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txCustomerId" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               FieldId
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txFieldId" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               DocGroupId
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txDocGroupId" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               DisplayOrder
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txDisplayOrder" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
          <tr>
            <td>
                &nbsp;</td>
            <td>
               CrDateTime:
            </td>
            <td>
                <asp:TextBox CssClass= "tbdatepicker" ID="txCrDateTime" runat="server" width="70px"></asp:TextBox>&nbsp;<font color="red">(*)</font>                 
            </td>
        </tr> 
       
    </table>
    <div style="text-align:center; padding: 20px">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom"  Text="Lưu thông tin" meta:resourcekey="btnSave" 
            onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

