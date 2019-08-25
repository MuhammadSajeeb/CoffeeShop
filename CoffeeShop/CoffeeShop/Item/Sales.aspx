<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="CoffeeShop.Item.Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <div class="form-horizontal">
            <hr />
            <asp:HiddenField ID="IdHiddenField" runat="server" />
            <div class="form-group">
                <div class="col-md-offset-2 col-md-2">
                    <asp:Label runat="server" ID="lblSerial" AssociatedControlID="txtSerial" CssClass="control-label">Serial</asp:Label>
                    <asp:TextBox runat="server" ID="txtSerial" CssClass="form-control" TextMode="Number" ReadOnly="true" />
                </div>
                <div class="col-md-2">
                    <asp:Label runat="server" ID="lblDate" AssociatedControlID="txtDate" CssClass="control-label">Date</asp:Label>
                    <asp:TextBox runat="server" ID="txtDate" CssClass="form-control" ReadOnly="true" />
                </div>
                <div class="col-md-2">
                    <asp:Label runat="server" ID="lblCashierName" AssociatedControlID="txtCashierName" CssClass="control-label">Cashier Name</asp:Label>
                    <asp:TextBox runat="server" ID="txtCashierName" CssClass="form-control" ReadOnly="true" />
                </div>
                <div class="col-md-2">
                    <asp:Label runat="server" ID="lblCustomerName" AssociatedControlID="txtCustomerName" CssClass="control-label">Customer Name</asp:Label>
                    <asp:TextBox runat="server" ID="txtCustomerName" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerName"
                        CssClass="text-danger" ErrorMessage="The Customer Name field is required." />
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-offset-2 col-md-2">
                    <asp:Label runat="server" ID="lblCategories" AssociatedControlID="CategoriesDropDownList" CssClass="control-label">Select Category</asp:Label>
                    <asp:DropDownList ID="CategoriesDropDownList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="CategoriesDropDownList_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label runat="server" ID="lblItems" AssociatedControlID="ItemsDropDownList" CssClass="control-label">Select Item</asp:Label>
                    <asp:DropDownList ID="ItemsDropDownList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ItemsDropDownList_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label runat="server" ID="lblItemPrice" AssociatedControlID="txtItemPrice" CssClass="control-label">Item Price</asp:Label>
                    <asp:TextBox runat="server" ID="txtItemPrice" CssClass="form-control" ReadOnly="true" />
                </div>
                <div class="col-md-2">
                    <asp:Label runat="server" ID="lblSubPrice" AssociatedControlID="txtSubPrice" CssClass="control-label">Total Price</asp:Label>
                    <asp:TextBox runat="server" ID="txtSubPrice" CssClass="form-control" ReadOnly="true" />
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-offset-2 col-md-2">
                    <asp:Label runat="server" ID="lblQty" AssociatedControlID="txtQty" CssClass="control-label">Quantity</asp:Label>
                    <asp:TextBox runat="server" ID="txtQty" CssClass="form-control" TextMode="Number" AutoPostBack="true" OnTextChanged="txtQty_TextChanged" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtQty"
                        CssClass="text-danger" ErrorMessage="The Quantity field is required." />
                </div>
                <div class="col-md-offset-1 col-md-3">
                    <br />
                    <asp:Button runat="server" ID="AddButton" Text="Add" CssClass="btn btn-info" OnClick="AddButton_Click" />
                    <asp:Button runat="server" ID="UpdateButton" Text="Update" CssClass="btn btn-info" OnClick="UpdateButton_Click" />
                    <asp:Button runat="server" ID="DeleteButton" Text="Delete" CssClass="btn btn-info" OnClick="DeleteButton_Click" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-5">
                    <asp:GridView ID="ItemSalesGridView" runat="server" EmptyDataText="No Order Available Here" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="10" ForeColor="Black" GridLines="Horizontal" AllowPaging="True" PageSize="2" CellSpacing="10" OnPageIndexChanging="ItemSalesGridView_PageIndexChanging" OnSelectedIndexChanged="ItemSalesGridView_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Qty" HeaderText="Qty" />
                            <asp:BoundField DataField="Per_Price" HeaderText="Per price" />
                            <asp:BoundField DataField="Sub_price" HeaderText="Total Price" />
                            <asp:CommandField HeaderText="Action" SelectText="Edit" ShowSelectButton="True">
                                <ItemStyle ForeColor="#CC0000" />
                            </asp:CommandField>
                        </Columns>
                        <PagerStyle Font-Bold="true" Font-Size="Small" ForeColor="#3399FF" />
                    </asp:GridView>
                </div>
                <div class="col-md-2">

                    <asp:Label runat="server" ID="lblSubTotal" AssociatedControlID="txtSubTotal" CssClass="control-label">Sub Total</asp:Label>
                    <asp:TextBox runat="server" ID="txtSubTotal" CssClass="form-control" ReadOnly="true" AutoPostBack="true" />

                    <asp:Label runat="server" ID="lblTotalCost" AssociatedControlID="txtTotalCost" CssClass="control-label">Grand Total</asp:Label>
                    <asp:TextBox runat="server" ID="txtTotalCost" CssClass="form-control" ReadOnly="true" />
                </div>

                <div class="col-md-2">
                    <asp:Label runat="server" ID="lblDiscount" AssociatedControlID="txtDiscount" CssClass="control-label">Discount</asp:Label>
                    <asp:TextBox runat="server" ID="txtDiscount" CssClass="form-control" />

                    <asp:Label runat="server" ID="lblPaidAmount" AssociatedControlID="txtPaidAmount" CssClass="control-label">Paid Amount</asp:Label>
                    <asp:TextBox runat="server" ID="txtPaidAmount" CssClass="form-control" OnTextChanged="txtPaidAmount_TextChanged" AutoPostBack="true" />
                </div>

                <div class="col-md-offset-2 col-md-2">
                    <asp:Label runat="server" ID="lbl" AssociatedControlID="txtItemPrice" CssClass="control-label">Changes : </asp:Label>
                    <asp:Label runat="server" ID="lblChanges" AssociatedControlID="txtItemPrice" CssClass="control-label" ForeColor="#66ccff">0.00</asp:Label>
                </div>
                <div class="col-md-offset-2 col-md-3">
                    <br />
                    <asp:Button runat="server" ID="TotalButton" Text="Total" CssClass="btn btn-info" OnClick="TotalButton_Click" />
                    <asp:Button runat="server" ID="PrintButton" Text="Print" CssClass="btn btn-info" OnClick="PrintButton_Click" />
                </div>
            </div>
        </div>
        <link href="../Content/Gridviewstylesheet.css" rel="stylesheet" />
    </div>
</asp:Content>
