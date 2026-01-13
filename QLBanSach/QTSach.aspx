<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="QTSach.aspx.cs" Inherits="QLBanSach.QTSach" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NoiDung" runat="server">
    <h2>TRANG QUẢN TRỊ SÁCH</h2>
    <hr />   
    <div class="row mb-2">
        <div class="col-md-6">
             <div class="form-inline">
                  Tựa sách <asp:TextBox ID="txtTen" runat="server" placeholder="Nhập tựa sách cần tìm" CssClass="form-control ml-2" Width="300"></asp:TextBox>
                  <asp:Button ID="btTraCuu" runat="server" Text="Tra cứu" CssClass="btn btn-info ml-2" OnClick="btTraCuu_Click" />                  
             </div>
        </div>
        <div class="col-md-6 text-right">
             <a href="ThemSach.aspx" class="btn btn-success">Thêm sách mới</a>
        </div>
    </div>

    <asp:GridView ID="gvSach" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="MaSach" AllowPaging="True" PageSize="5"
    CssClass="table table-bordered text-center" HeaderStyle-CssClass="bg-danger text-white"
    OnRowDeleting="gvSach_RowDeleting" 
    OnPageIndexChanging="gvSach_PageIndexChanging"
    OnRowEditing="gvSach_RowEditing" 
    OnRowCancelingEdit="gvSach_RowCancelingEdit" 
    OnRowUpdating="gvSach_RowUpdating">
    
    <Columns>
        <asp:TemplateField HeaderText="Tựa sách" ItemStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%# Eval("TenSach") %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtTenSua" runat="server" Text='<%# Bind("TenSach") %>' CssClass="form-control"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Ảnh bìa">
            <ItemTemplate>
                <img src='<%# "Bia_sach/" + Eval("Hinh") %>' width="100px" />
            </ItemTemplate>
            <EditItemTemplate>
                <img src='<%# "Bia_sach/" + Eval("Hinh") %>' width="100px" />
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Đơn giá">
            <ItemTemplate>
                <%# Eval("Dongia", "{0:N0}") %> đồng
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtGiaSua" runat="server" Text='<%# Bind("Dongia") %>' CssClass="form-control"></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Khuyến mãi">
            <ItemTemplate>
                <%# Eval("KhuyenMai").ToString() == "True" ? "X" : "" %>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:CheckBox ID="chkKMSua" runat="server" Checked='<%# Bind("KhuyenMai") %>' />
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Chọn thao tác">
            <ItemTemplate>
                <asp:Button ID="btnSua" runat="server" Text="Sửa" CommandName="Edit" CssClass="btn btn-info btn-sm" />
                <asp:Button ID="btnXoa" runat="server" Text="Xoá" CommandName="Delete" 
                    CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Bạn có chắc muốn xoá?');" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Button ID="btnGhi" runat="server" Text="Ghi" CommandName="Update" CssClass="btn btn-success btn-sm" />
                <asp:Button ID="btnKhong" runat="server" Text="Không" CommandName="Cancel" CssClass="btn btn-warning btn-sm" />
            </EditItemTemplate>
        </asp:TemplateField>
    </Columns>
    <PagerStyle CssClass="bg-warning text-center" HorizontalAlign="Center" />
</asp:GridView>
</asp:Content>
