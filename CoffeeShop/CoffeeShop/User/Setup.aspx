<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Setup.aspx.cs" Inherits="CoffeeShop.User.Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h4>Users Setup</h4>
    <br />
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label runat="server" ID="lblMessage" AssociatedControlID="lblMessage" CssClass="col-md-1 control-label"></asp:Label>
            <div class="col-md-10">
                <div class="messagealert" id="alert_container">
                </div>
                <asp:HiddenField ID="IdHiddenField" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-offset-1 col-md-5" style="width: 500px">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">User Add</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblPerson" AssociatedControlID="txtPerson" CssClass="col-md-2 control-label">Person</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtPerson" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPerson"
                                    CssClass="text-danger" ErrorMessage="The field is required." />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblPassword" AssociatedControlID="txtPassword" CssClass="col-md-2 control-label">Password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword"
                                    CssClass="text-danger" ErrorMessage="The field is required." />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-7 col-md-12">
                                <asp:Button ID="SaveButton" runat="server" Text="Add" CssClass="btn btn-info" Width="85px" OnClick="SaveButton_Click" />
                                <asp:Button ID="UpdateButton" runat="server" Text="Update" CssClass="btn btn-info" Width="85px"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-5">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">User Details</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-md-12">
                                <asp:GridView ID="UserGridView" runat="server" EmptyDataText="No Users Available Now" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="10" ForeColor="Black" GridLines="Horizontal" AllowPaging="False" CellSpacing="10" OnRowCommand="UserGridView_RowCommand" OnSelectedIndexChanged="UserGridView_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnDelete" CommandArgument='<%# Eval("Id") %>' CommandName="DeleteRow" ForeColor="#8C4510" runat="server" CausesValidation="false">Delete</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="Person" HeaderText="Person" />
                                        <asp:BoundField DataField="Password" HeaderText="Password"/>
                                        <asp:BoundField DataField="AdminBy" HeaderText="Referenced By" />
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
</asp:Content>
