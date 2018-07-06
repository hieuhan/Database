<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="MaterialsEdit.aspx.cs" Inherits="admin_pages_MaterialsEdit" Title="" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">   
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter" >
        
        <tr>
            <td>
                &nbsp;</td>
            <td>
               MaterialName
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txMaterialName" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               UnitId
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txUnitId" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               Percentage
               :
            </td>
            <td>
                <asp:TextBox ID="txPercentage" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
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
               CrUserId
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txCrUserId" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
       
    </table>
    <div style="text-align:center; padding: 20px">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom"  Text="Lưu thông tin" meta:resourcekey="btnSave" 
            onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

