<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="SupplierEdit.aspx.cs" Inherits="admin_pages_SupplierEdit" Title="" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">   
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter" >
        
        <tr>
            <td>
                &nbsp;</td>
            <td>
               SupplierName
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txSupplierName" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               SupplierDesc
               :
            </td>
            <td>
                <asp:TextBox ID="txSupplierDesc" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
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
               ImagePath
               :
            </td>
            <td>
                <asp:TextBox ID="txImagePath" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
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
               Telephone
               :
            </td>
            <td>
                <asp:TextBox ID="txTelephone" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Comments
               :
            </td>
            <td>
                <asp:TextBox ID="txComments" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
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
               ReviewStatusId
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txReviewStatusId" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
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

