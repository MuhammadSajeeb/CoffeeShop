<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CoffeeShop.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <div id="myCarousel" class="carousel slide" data-ride="carousel" data-interval="2000">
                <!-- Indicators -->
                <ol class="carousel-indicators">
                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                    <li data-target="#myCarousel" data-slide-to="1"></li>
                    <li data-target="#myCarousel" data-slide-to="2"></li>

                </ol>

                <!-- Wrapper for slides -->
                <div class="carousel-inner">
                    <div class="item active">
                        <img src="images/pic1.jpg" alt="Amazon Echo">
                    </div>

                    <div class="item">
                        <img src="images/pic2.jpg" alt="PS4">
                    </div>

                    <div class="item">
                        <img src="images/pic3.jpg" alt="Samsung">
                    </div>

                </div>

                <!-- Left and right controls -->
                <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                    <span class="glyphicon glyphicon-chevron-left"></span>
                    <span class="sr-only">Previous</span>
                </a>
                <a class="right carousel-control" href="#myCarousel" data-slide="next">
                    <span class="glyphicon glyphicon-chevron-right"></span>
                    <span class="sr-only">Next</span>
                </a>
            </div>
        </div>
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
