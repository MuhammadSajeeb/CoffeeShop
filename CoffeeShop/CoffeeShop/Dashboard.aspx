<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CoffeeShop.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
    <br/>
    <div class="jumbotron">
        <div class="row w-100">
            <div class="col-md-6">
                <div class="card border-info mx-sm-1 p-3">
                    <%--<div class="card border-info shadow text-info p-3 my-card" ><span class="fa fa-award" aria-hidden="true"></span></div>--%>
                    <div class="text-info text-center mt-3">
                        <h4>This Month</h4>
                    </div>
                    <div class="text-info text-center mt-3">
                        <h4>Total Sale of Quantity</h4>
                    </div>
                    <div class="text-info text-center mt-2">
                        <h1>234</h1>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="card border-success mx-sm-1 p-3">
                    <%--<div class="card border-success shadow text-success p-3 my-card"><span class="fa fa-eye" aria-hidden="true"></span></div>--%>
                    <div class="text-info text-center mt-3">
                        <h4>This Month</h4>
                    </div>
                    <div class="text-success text-center mt-3">
                        <h4>Total Sale Amount</h4>
                    </div>
                    <div class="text-success text-center mt-2">
                        <h1>9332</h1>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <div class="row w-100">
            <div class="col-md-6">
                <div class="card border-danger mx-sm-1 p-3">
                    <%--<div class="card border-danger shadow text-danger p-3 my-card" ><span class="fa fa-heart" aria-hidden="true"></span></div>--%>
                    <div class="text-danger text-center mt-3">
                        <h4>All Of Sale Quantity</h4>
                    </div>
                    <div class="text-danger text-center mt-2">
                        <h1>346</h1>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="card border-warning mx-sm-1 p-3">
                    <%--<div class="card border-warning shadow text-warning p-3 my-card" ><span class="fa fa-inbox" aria-hidden="true"></span></div>--%>
                    <div class="text-warning text-center mt-3">
                        <h4>All Of Sale Amount</h4>
                    </div>
                    <div class="text-warning text-center mt-2">
                        <h1>346</h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css">
</asp:Content>
