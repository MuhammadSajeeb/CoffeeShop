﻿using Coffee.Core.Models;
using Coffee.Persistancis.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeShop.Authoriesed
{
    public partial class Login : System.Web.UI.Page
    {
        AuthorisedRepository _AuthorisedRepository = new AuthorisedRepository();

        public enum MessageType { Success, Failed, Error, Info, Warning };
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
            }
        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        protected void AdminLoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                Admin _Admin = new Admin();
                _Admin.Name = txtAdmin.Text;
                _Admin.Password = txtAdminPassword.Text;

                decimal AdminLogin = _AuthorisedRepository.AdminLogin(_Admin);
                if(AdminLogin>=1)
                {
                    Session["Admin"] = txtAdmin.Text;
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    ShowMessage("Please Check Admin Login", MessageType.Warning);
                }
            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, MessageType.Error);
            }
        }
        protected void AuthorisedLoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                Authorizations _Authorization = new Authorizations();
                _Authorization.Person = txtAuthoriesed.Text;
                _Authorization.Password = txtAuthorisedPassword.Text;

                decimal AuthorisedPersonLogin = _AuthorisedRepository.AuthorisedLogin(_Authorization);
                if (AuthorisedPersonLogin >= 1)
                {
                    Session["Authorised"] = txtAuthoriesed.Text;
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    ShowMessage("Please Check Authorised Login", MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, MessageType.Error);
            }
        }


    }
}