<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CoffeeShop.Authoriesed.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h4>Account Login</h4>
    <br />
    <div class="form-horizontal" style="background-image: none">
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
                        <h3 class="panel-title">Admin Login</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblAdmin" AssociatedControlID="txtAdmin" CssClass="col-md-2 control-label">Admin</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtAdmin" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblPassword" AssociatedControlID="txtAdminPassword" CssClass="col-md-2 control-label">Password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtAdminPassword" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" TextMode="Password" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-7 col-md-12">
                                <asp:Button ID="AdminLoginButton" runat="server" Text="Login" CssClass="btn btn-info" Width="85px" OnClick="AdminLoginButton_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-5">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Authorised Person Login</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblAuthorised" AssociatedControlID="txtAuthoriesed" CssClass="col-md-2 control-label">Person</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtAuthoriesed" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblAuthorisedPassword" AssociatedControlID="txtAdminPassword" CssClass="col-md-2 control-label">Password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtAuthorisedPassword" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" TextMode="Password" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-7 col-md-12">
                                <asp:Button ID="AuthorisedLoginButton" runat="server" Text="Login" CssClass="btn btn-info" Width="85px" OnClick="AuthorisedLoginButton_Click" />
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
</asp:Content>
