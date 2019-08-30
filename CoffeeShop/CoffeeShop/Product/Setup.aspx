<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Setup.aspx.cs" Inherits="CoffeeShop.Product.Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h4>Product Setup</h4>
    <br />
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label runat="server" ID="lblMessage" AssociatedControlID="lblMessage" CssClass="col-md-1 control-label"></asp:Label>
            <div class="col-md-10">
                <div class="messagealert" id="alert_container">
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-offset-1 col-md-5" style="width: 500px">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Category Add</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblCategory" AssociatedControlID="txtCategory" CssClass="col-md-2 control-label">Category</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtCategory" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-7 col-md-12">
                                <asp:Button ID="AddCategoriesButton" runat="server" Text="Add" CssClass="btn btn-info" Width="85px" OnClick="AddCategoriesButton_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-5">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Category Details</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-md-12">
                                <asp:GridView ID="CategoriesGridView" runat="server" EmptyDataText="No Categories Available Now" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="10" ForeColor="Black" GridLines="Horizontal" AllowPaging="False" CellSpacing="10" OnRowCommand="CategoriesGridView_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnDelete" CommandArgument='<%# Eval("Id") %>' CommandName="DeleteRow" ForeColor="#8C4510" runat="server" CausesValidation="false">Delete</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:CommandField HeaderText="Action" SelectText="Edit" ShowSelectButton="True">
                                            <ItemStyle ForeColor="#CC0000" />
                                        </asp:CommandField>
                                    </Columns>
                                    <PagerStyle Font-Bold="true" Font-Size="Small" ForeColor="#3399FF" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-offset-1 col-md-5" style="width: 500px">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Item Add</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblCategories" AssociatedControlID="CategoriesDropDownList" CssClass="col-md-2 control-label">Categories</asp:Label>
                            <div class="col-md-10">
                                <asp:DropDownList ID="CategoriesDropDownList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="CategoriesDropDownList_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblItemName" AssociatedControlID="txtItemName" CssClass="col-md-2 control-label">Item</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtItemName" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblPrice" AssociatedControlID="txtPrice" CssClass="col-md-2 control-label">Price(BDT)</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtPrice" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" TextMode="Number" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-7 col-md-12">
                                <asp:Button ID="AddItemButton" runat="server" Text="Add" CssClass="btn btn-info" Width="85px" OnClick="AddItemButton_Click" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-md-5">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Item Details</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-md-12">
                                <asp:GridView ID="ItemGridView" runat="server" EmptyDataText="No Items Available Now" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="10" ForeColor="Black" GridLines="Horizontal" AllowPaging="False" CellSpacing="10" OnRowCommand="ItemGridView_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnDelete" CommandArgument='<%# Eval("Id") %>' CommandName="DeleteRow" ForeColor="#8C4510" runat="server" CausesValidation="false">Delete</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                        <asp:BoundField DataField="Price" HeaderText="Price" />
                                        <asp:CommandField HeaderText="Action" SelectText="Edit" ShowSelectButton="True">
                                            <ItemStyle ForeColor="#CC0000" />
                                        </asp:CommandField>
                                    </Columns>
                                    <PagerStyle Font-Bold="true" Font-Size="Small" ForeColor="#3399FF" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <style type="text/css">
        .messagealert {
            width: 470px;
        }
    </style>
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Failed':
                    cssclass = 'alert-danger'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
        }
    </script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" rel="stylesheet" />
    <script>
        $('#<%=CategoriesDropDownList.ClientID%>').chosen()
    </script>
</asp:Content>
