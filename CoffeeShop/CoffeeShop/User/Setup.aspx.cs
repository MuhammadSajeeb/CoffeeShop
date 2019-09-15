using Coffee.Core.Models;
using Coffee.Persistancis.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeShop.User
{
    public partial class Setup : System.Web.UI.Page
    {
        UserRepository _UserRepository = new UserRepository();

        public enum MessageType { Success, Failed, Error, Info, Warning };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Admin"] != null)
                {
                    LoadUsers();
                    UpdateButton.Visible = false;
                }
                else
                {
                    Response.Redirect("~/Authoriesed/Login.aspx");
                }
            }
        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        public void LoadUsers()
        {
            UserGridView.DataSource = _UserRepository.GetAllUsers();
            UserGridView.DataBind();
        }
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Users _Users = new Users();

                _Users.Person = txtPerson.Text;
                _Users.Password = txtPassword.Text;
                _Users.AdminBy = Session["Admin"].ToString();

                decimal AlreadyExistUser = _UserRepository.AlreadyExistName(_Users);
                if (AlreadyExistUser >= 1)
                {
                    ShowMessage("This Person Already Here!!!...", MessageType.Warning);
                }
                else
                {
                    int Savesuccess = _UserRepository.Add(_Users);
                    if (Savesuccess > 0)
                    {

                        ShowMessage("Successfully Saved Users....", MessageType.Success);
                        LoadUsers();
                        txtPerson.Text = "";
                        txtPassword.Text = "";
                        //Response.Redirect(Request.Url.AbsoluteUri);


                    }
                    else
                    {
                        ShowMessage("Failed Saving User", MessageType.Warning);
                    }

                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, MessageType.Error);
            }
        }

        protected void UserGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Users _Users = new Users();
                _Users.Id = Convert.ToInt32(e.CommandArgument);

                int deletesuccess = _UserRepository.Delete(_Users);
                if (deletesuccess > 0)
                {
                    LoadUsers();
                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, MessageType.Error);
            }
        }

        protected void UserGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}