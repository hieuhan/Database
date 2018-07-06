<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="CustomersEdit.aspx.cs" Inherits="admin_pages_CustomersEdit" Title="" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">   
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter" >
        
        <tr>
            <td>
                &nbsp;</td>
            <td>
               FullName
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txFullName" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
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
               CustomerGroupId
               :
            </td>
            <td>
                <asp:TextBox ID="txCustomerGroupId" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Note
               :
            </td>
            <td>
                <asp:TextBox ID="txNote" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               StatusId
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txStatusId" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               DebitBalance
               :
            </td>
            <td>
                <asp:TextBox ID="txDebitBalance" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               PaymentLimit
               :
            </td>
            <td>
                <asp:TextBox ID="txPaymentLimit" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
          <tr>
            <td>
                &nbsp;</td>
            <td>
               DateOfBirth:
            </td>
            <td>
                <asp:TextBox CssClass= "tbdatepicker" ID="txDateOfBirth" runat="server" width="70px"></asp:TextBox>                 
            </td>
        </tr> 
          <tr>
            <td>
                &nbsp;</td>
            <td>
               LastTradingDay:
            </td>
            <td>
                <asp:TextBox CssClass= "tbdatepicker" ID="txLastTradingDay" runat="server" width="70px"></asp:TextBox>                 
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

