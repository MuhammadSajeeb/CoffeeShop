<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="testingGridview.aspx.cs" Inherits="CoffeeShop.testingGridview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="UserNo">
                <ItemTemplate>
                    <asp:Label ID="lblNo" runat="server" Text='<%#Eval("No") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtFNo" runat="server" ></asp:TextBox>
                </FooterTemplate>
                <EditItemTemplate>
                <asp:TextBox ID="txtENo" runat="server" Text='<%#Eval("No") %>'></asp:TextBox>
                </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                     <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtFName" runat="server"></asp:TextBox>
                </FooterTemplate>
                <EditItemTemplate>
                <asp:TextBox ID="txtEName" runat="server" Text='<%#Eval("Name") %>'></asp:TextBox>
                </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Designation">
                     <ItemTemplate>
                    <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtFDesignation" runat="server"></asp:TextBox>
                </FooterTemplate>
                <EditItemTemplate>
                <asp:TextBox ID="txtEDesignation" runat="server" Text='<%#Eval("Designation") %>'></asp:TextBox>
                </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DateofBirth">
                     <ItemTemplate>
                    <asp:Label ID="lblDOB" runat="server" Text='<%#Eval("DOB") %>'></asp:Label>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtFDOB" runat="server"></asp:TextBox>
                </FooterTemplate>
                <EditItemTemplate>
                <asp:TextBox ID="txtEDOB" runat="server" Text='<%#Eval("DOB") %>'></asp:TextBox>
                </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                <FooterTemplate>
                    <asp:Button runat="server" Text="Add" CommandName="Add"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>

</asp:Content>
