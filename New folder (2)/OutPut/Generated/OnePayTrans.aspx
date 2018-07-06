<%@ Page Language="C#" MasterPageFile="~/admin/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="OnePayTrans.aspx.cs" Inherits="admin_pages_OnePayTrans" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/admin/UserControls/CustomPaging.ascx" TagName="CustomPaging" TagPrefix="uc1" %>
<%@ Import Namespace="ICSoft.HelperLib" %>
<asp:Content ID="contentAccountType" runat="server" ContentPlaceHolderID="m_contentBody">
    <script type="text/javascript">
        var cdialog = $('<div id="divEdit"></div>');
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });

            $('a.popup').live('click', function (e) {
                var page = $(this).attr("href");
                cdialog
                .html('<iframe id="ifEdit" style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 460,
                    width: 600,
                    title: $(this).attr("title"),
                    close: function (event, ui) {
                        $(this).remove();
                        window.location = $('#<%= btnSearch.ClientID %>').attr("href");
                        //$('#<%= btnSearch.ClientID %>').click();
                    }
                });
                cdialog.dialog('open');
                e.preventDefault();
            });
        });
        function InitializeRequest(sender, args) {
            
        }

        function EndRequest(sender, args) {
            $("#<%= txtDateFrom.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#<%= txtDateTo.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        }
    </script>
        <table cellpadding="3" cellspacing="3" class="tableBorder" style="width: 98%;" >
            <tr>
            <td>
            <table cellpadding="3" cellspacing="3" style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="lblOrderBy" runat="server" Text="Sắp xếp:" meta:resourcekey="lblOrderBy"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOrderBy" runat="server" DataTextField="OrderByDesc" 
                            DataValueField="OrderBy" Width="250px" CssClass="userselect" 
                            AutoPostBack="True" 
                            onselectedindexchanged="ddlOrderBy_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td style="width:90px; white-space:nowrap;">
                      </td>
                    <td style="width:260px"> 
                       
                    </td>
                    
                </tr>
                <tr>
                    <td style="width:60px; white-space:nowrap;">
                        <asp:Label ID="lblDateFrom" runat="server" Text="Từ ngày:" meta:resourcekey="lblDateFrom"></asp:Label>
                    </td>
                    <td style="width:260px">
                        <asp:TextBox ID="txtDateFrom" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                        <asp:Label ID="lblDateTo" runat="server" Text="đến:" meta:resourcekey="lblDateTo"></asp:Label>
                        <asp:TextBox ID="txtDateTo" runat="server" CssClass="tukhoatimekiem" Width="100px"></asp:TextBox>
                        </td>
                    <td style="width:90px; white-space:nowrap;">
                        <asp:Label ID="lblKeyword" runat="server" Text="Từ khóa:" meta:resourcekey="lblKeyword"></asp:Label>
                    </td>
                    <td><asp:TextBox ID="txtSearch" runat="server" CssClass="tukhoatimekiem" Width="240px"></asp:TextBox>&nbsp;&nbsp;
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="timkiembutom" 
                            Text="Tìm kiếm" meta:resourcekey="btnSearch" onclick="btnSearch_Click">
                                </asp:LinkButton>
                        </td>
                </tr>
            </table>
            <div class="clear5px"></div>
        	<div class="vien"></div>
        	<div class="clear5px"></div>  
            <div class="khungchucnang">
            <div class="chucnangleft">
        		<span class="tieudetongcong">
                    <asp:Label ID="lblTotalText" runat="server" Text="Tổng cộng:" meta:resourcekey="lblTotalText"></asp:Label>
                </span>
                <asp:Label ID="lblTong" runat="server" Text="" CssClass="tieudetongcong2"></asp:Label> 
        	</div>
        	<div class="chucnangright">
                <asp:LinkButton ID="lbDelete" runat="server" CssClass="xoatin" Text="Xóa"  OnClientClick="return confirm('Bạn có chắc muốn xóa toàn bộ dữ liệu đã chọn?')"
                    meta:resourcekey="lbDelete" onclick="lbDelete_Click">
                </asp:LinkButton>
                <asp:LinkButton ID="lbUnCheck" runat="server" CssClass="boduyet" 
                    Text="Bỏ duyệt" meta:resourcekey="lbUnCheck" onclick="lbUnCheck_Click"> 
                </asp:LinkButton>
                <asp:LinkButton ID="lbReview" runat="server" CssClass="duyettin" Text="Duyệt" 
                    meta:resourcekey="lbReview" onclick="lbReview_Click"> 
                </asp:LinkButton>					
        		<a href="OnePayTransEdit.aspx" title="Thêm mới " class="themmoi popup" > 
                    <asp:Label ID="lblAddNew" runat="server" Text="Thêm mới" meta:resourcekey="lblAddNew"></asp:Label>
                </a>
        	</div>
            <div style="text-align:left; width:200px; float:right">
            </div>
        	<div class="clear5px"></div>
            <div class="contenbangdulieu">
                <asp:GridView ID="m_grid" DataKeyNames="TransId" runat="server" ShowHeaderWhenEmpty="True"
                    AutoGenerateColumns="False" CssClass="filter-table" OnRowDeleting="m_grid_RowDeleting"  OnRowDataBound = "m_grid_OnRowDataBound"
                    Width="100%" CellPadding="0" CellSpacing="0" BorderWidth="0" PageSize="50" >
                    <HeaderStyle CssClass="trbangdulieutieude" />
                    <FooterStyle CssClass="grid_foot" />
                    <RowStyle CssClass="trbangdulieutieudenoidung" />
                    <AlternatingRowStyle CssClass="trbangdulieutieudenoidung" />
                    <SelectedRowStyle CssClass="trbangdulieutieudenoidung" />
                    <EditRowStyle CssClass="trbangdulieutieudenoidung" />
                    <PagerStyle CssClass="grid_page" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                            <asp:Label ID="lbNo" runat="server" Text='<%# Container.DataItemIndex + (CustomPaging.PageIndex - 1) * m_grid.PageSize + 1%>' 
                             ToolTip ='<%# Eval("TransId").ToString() %>' >                                    
                            </asp:Label>
                            </ItemTemplate>      
                            <ItemStyle Width="5%" />            
                        </asp:TemplateField>        
                        <asp:TemplateField HeaderText="TransId">  
                            <ItemTemplate> 
                                <asp:Literal ID="ltTransId" runat="server" EnableViewState="false" Text='<%# Eval("TransId").ToString() %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="vpc_OrderInfo">  
                            <ItemTemplate> 
                                <asp:Literal ID="ltvpc_OrderInfo" runat="server" EnableViewState="false" Text='<%# Eval("vpc_OrderInfo") %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="vpc_Amount">  
                            <ItemTemplate> 
                                <asp:Literal ID="ltvpc_Amount" runat="server" EnableViewState="false" Text='<%# Eval("vpc_Amount").ToString() %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="vpc_TicketNo">  
                            <ItemTemplate> 
                                <asp:Literal ID="ltvpc_TicketNo" runat="server" EnableViewState="false" Text='<%# Eval("vpc_TicketNo") %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="AgainLink">  
                            <ItemTemplate> 
                                <asp:Literal ID="ltAgainLink" runat="server" EnableViewState="false" Text='<%# Eval("AgainLink") %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="vpc_TxnResponseCode">  
                            <ItemTemplate> 
                                <asp:Literal ID="ltvpc_TxnResponseCode" runat="server" EnableViewState="false" Text='<%# Eval("vpc_TxnResponseCode").ToString() %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="vpc_TransactionNo">  
                            <ItemTemplate> 
                                <asp:Literal ID="ltvpc_TransactionNo" runat="server" EnableViewState="false" Text='<%# Eval("vpc_TransactionNo") %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="vcp_Message">  
                            <ItemTemplate> 
                                <asp:Literal ID="ltvcp_Message" runat="server" EnableViewState="false" Text='<%# Eval("vcp_Message") %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="CustomerId">  
                            <ItemTemplate> 
                                <asp:Literal ID="ltCustomerId" runat="server" EnableViewState="false" Text='<%# Eval("CustomerId").ToString() %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="ResponseTime">  
                            <ItemTemplate>  
                             <asp:Literal ID="ltResponseTime" runat="server" EnableViewState="false" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("ResponseTime")) %>'></asp:Literal> 
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sửa">
                            <ItemStyle HorizontalAlign="center"  Wrap="false" />
                            <HeaderStyle Width="6%" />
                            <ItemTemplate>
                                <a href='OnePayTransEdit.aspx?id=<%# Eval("TransId") %>' class="iconadmsua popup" ></a>
                                <asp:LinkButton ID="lbtDelete" runat="server" CausesValidation="False" OnClientClick="return confirm('Bạn có chắc muốn xóa dữ liệu này?');"
                                    CommandName="Delete" Text="" CssClass="iconadmxoa" ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                            <HeaderTemplate>
                                <input type="checkbox" name="SelectAllCheckBox" onclick="SelectAll(this)">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAction" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>                  
            </div>
            <div class="clear5px"></div>    
            <uc1:CustomPaging ID="CustomPaging" runat="server" />                
            <div class="clear5px"></div>   
          </div>
          </td>
          </tr>
          </table>
 </asp:Content>
