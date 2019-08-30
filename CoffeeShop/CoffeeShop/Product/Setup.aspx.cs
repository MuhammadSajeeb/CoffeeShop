using Coffee.Core.Models;
using Coffee.Persistancis.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeShop.Product
{
    public partial class Setup : System.Web.UI.Page
    {
        CategoriesRepository _CategoriesRepository = new CategoriesRepository();
        ItemRepository _ItemRepository = new ItemRepository();

        public enum MessageType { Success, Failed, Error, Info, Warning };
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadCategories();
                GetAllCategories();
            }
        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        public void LoadCategories()
        {
            CategoriesGridView.DataSource = _CategoriesRepository.GetAll();
            CategoriesGridView.DataBind();
        }
        public void LoadItem()
        {
            int Id = Convert.ToInt32(CategoriesDropDownList.SelectedValue);
            ItemGridView.DataSource = _ItemRepository.GetAll(Id);
            ItemGridView.DataBind();
        }
        public void GetAllCategories()
        {
            CategoriesDropDownList.DataSource = _ItemRepository.GetAllCategories();
            CategoriesDropDownList.DataTextField = "Name";
            CategoriesDropDownList.DataValueField = "Id";
            CategoriesDropDownList.DataBind();

            CategoriesDropDownList.Items.Insert(0, new ListItem("Chose Category", "0"));
        }
        protected void AddCategoriesButton_Click(object sender, EventArgs e)
        {
            try
            {
                Categories _Categories = new Categories();
                _Categories.Name = txtCategory.Text;

                decimal AlreadyExistCaegory = _CategoriesRepository.AlreadyExistName(_Categories);
                if(AlreadyExistCaegory>=1)
                {
                    ShowMessage("This Category Already Here!!!...", MessageType.Warning);
                }
                else
                {
                    int Savesuccess = _CategoriesRepository.Add(_Categories);
                    if(Savesuccess > 0)
                    {
                        ShowMessage("Successfully Saved Category....", MessageType.Success);
                        LoadCategories();
                        txtCategory.Text = "";
                        //        Response.Redirect(Request.Url.AbsoluteUri);

                    }
                    else
                    {
                        ShowMessage("Failed Saving Category", MessageType.Warning);
                    }
                    
                }
            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, MessageType.Error);
            }
        }

        protected void AddItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                ItemsM _ItemsM = new ItemsM();
                _ItemsM.Name = txtItemName.Text;
                _ItemsM.Price = Convert.ToDecimal(txtPrice.Text);
                _ItemsM.CategoriesId = Convert.ToInt32(CategoriesDropDownList.SelectedValue);

                decimal AlreadyExistItem = _ItemRepository.AlreadyExistItem(_ItemsM);
                if(AlreadyExistItem>=1)
                {
                    ShowMessage("This Item Already Here!!..This Category", MessageType.Warning);
                }
                else
                {
                    int savesuccess = _ItemRepository.Add(_ItemsM);
                    if(savesuccess>0)
                    {
                        ShowMessage("Successfully Saved Item", MessageType.Success);
                        LoadItem();
                        txtItemName.Text = "";
                        txtPrice.Text = "";
                        CategoriesDropDownList.ClearSelection();
                    }
                    else
                    {
                        ShowMessage("Failed Saving Item", MessageType.Warning);
                    }
                }
            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, MessageType.Warning);
            }
        }

        protected void CategoriesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItem();
        }

        protected void CategoriesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Categories _Categories = new Categories();
                _Categories.Id = Convert.ToInt32(e.CommandArgument);

                int deletesuccess = _CategoriesRepository.Delete(_Categories);
                if(deletesuccess>0)
                {
                    LoadCategories();
                }

            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, MessageType.Error);
            }
        }

        protected void ItemGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ItemsM _ItemsM = new ItemsM();
                _ItemsM.Id = Convert.ToInt32(e.CommandArgument);

                int deletesuccess = _ItemRepository.Delete(_ItemsM);
                if(deletesuccess>0)
                {
                    LoadItem();
                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, MessageType.Error);
            }
        }
    }
}