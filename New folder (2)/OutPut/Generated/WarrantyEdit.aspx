﻿<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdminEdit.master" AutoEventWireup="true" CodeFile="WarrantyEdit.aspx.cs" Inherits="admin_pages_WarrantyEdit" Title="" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">   
    <table cellpadding="3" cellspacing="0" style="width:100%; font-weight:lighter" >
        
        <tr>
            <td>
                &nbsp;</td>
            <td>
               WarrantyName
               &nbsp;<span style="color:red">(*)</span>:
            </td>
            <td>
                <asp:TextBox ID="txWarrantyName" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                &nbsp;</td>
            <td>
               WarrantyDesc
               :
            </td>
            <td>
                <asp:TextBox ID="txWarrantyDesc" runat="server" CssClass="tukhoatimekiem" Width="90%" ></asp:TextBox>
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
       
    </table>
    <div style="text-align:center; padding: 20px">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="savebutom"  Text="Lưu thông tin" meta:resourcekey="btnSave" 
            onclick="btnSave_Click">
        </asp:LinkButton>
    </div>
</asp:Content>

