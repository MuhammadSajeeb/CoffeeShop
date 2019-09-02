<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Recaptcha.aspx.cs" Inherits="CoffeeShop.Recaptcha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ReCaptchContainer"></div>
    <label id="lblMessage" runat="server" clientidmode="static"></label>
    <br />
    <button type="button">Submit</button>

    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
    <script src="https://www.google.com/recaptcha/api.js?onload=renderRecaptcha&render=explicit" async defer></script>
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>

    <script type="text/javascript">  
        var your_site_key = '6LcVI7YUAAAAAKtTUTJhcpUKLXz0yKxiGc30xfc-';
        var renderRecaptcha = function () {
            grecaptcha.render('ReCaptchContainer', {
                'sitekey': '6LcVI7YUAAAAAKtTUTJhcpUKLXz0yKxiGc30xfc-',
                'callback': reCaptchaCallback,
                theme: 'light', //light or dark    
                type: 'image',// image or audio    
                size: 'normal'//normal or compact    
            });
        };

        var reCaptchaCallback = function (response) {
            if (response !== '') {
                jQuery('#lblMessage').css('color', 'green').html('Success');
            }
        };

        jQuery('button[type="button"]').click(function (e) {
            var message = 'Please checck the checkbox';
            if (typeof (grecaptcha) != 'undefined') {
                var response = grecaptcha.getResponse();
                (response.length === 0) ? (message = 'Captcha verification failed') : (message = 'Success!');
            }
            jQuery('#lblMessage').html(message);
            jQuery('#lblMessage').css('color', (message.toLowerCase() == 'success!') ? "green" : "red");
        });

    </script>
</asp:Content>
