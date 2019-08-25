using Coffee.Core.Models;
using Coffee.Persistancis.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeShop.Item
{
    public partial class Sales : System.Web.UI.Page
    {
        ItemSalesRepository _ItemSalesRepository = new ItemSalesRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSaleItems();
                AutoCodeGenerate();
                GetALLCategories();
                ItemsDropDownList.Items.Insert(0, new ListItem("Select Items", "0"));
                UpdateButton.Visible = false;
                DeleteButton.Visible = false;
            }
        }
        public void AutoCodeGenerate()
        {
            decimal AlreadyExistData = _ItemSalesRepository.AlreadyExistData();
            int code = 1;
            if (AlreadyExistData >= 1)
            {
                var GetLastCode = _ItemSalesRepository.GetLastCode();
                if (GetLastCode != null)
                {
                    code = Convert.ToInt32(GetLastCode.Serial);
                    code++;
                }
                txtSerial.Text = code.ToString("");
            }
            else
            {
                txtSerial.Text = "1";
            }
        }
        public void GetALLCategories()
        {
            CategoriesDropDownList.DataSource = _ItemSalesRepository.GetAllCategories();
            CategoriesDropDownList.DataTextField = "Name";
            CategoriesDropDownList.DataValueField = "Id";
            CategoriesDropDownList.DataBind();
            CategoriesDropDownList.Items.Insert(0, new ListItem("Select Categories", "0"));

        }
        public void LoadSaleItems()
        {
            string serial;
            serial = txtSerial.Text;

            ItemSalesGridView.DataSource = _ItemSalesRepository.GetAllSaleItems(serial);
            ItemSalesGridView.DataBind();
        }
        public void GridviewRowSum()
        {
            string serial;
            serial = txtSerial.Text;

            decimal Total = _ItemSalesRepository.SumOrdere(serial);
            txtSubTotal.Text = Convert.ToDecimal(Total).ToString();
        }
        public void DiscountCalculation()
        {
            decimal subtotal = Convert.ToDecimal(txtSubTotal.Text);
            decimal discount = Convert.ToDecimal(txtDiscount.Text);

            decimal cal = discount / 100;
            decimal discountAmount = cal * subtotal;
            decimal Costamount = subtotal - discountAmount;
            txtTotalCost.Text = Convert.ToInt32(Costamount).ToString();

        }
        protected void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                ItemSales _ItemSales = new ItemSales();
                _ItemSales.Name = ItemsDropDownList.SelectedItem.ToString();
                _ItemSales.Qty = Convert.ToInt32(txtQty.Text);
                _ItemSales.Per_Price = Convert.ToDecimal(txtItemPrice.Text);
                _ItemSales.Sub_Price = Convert.ToDecimal(txtSubPrice.Text);
                _ItemSales.Serial = txtSerial.Text;

                int successAdd = _ItemSalesRepository.Add(_ItemSales);
                if (successAdd > 0)
                {
                    LoadSaleItems();
                    GridviewRowSum();
                }
                else
                {

                }
            }
            catch
            {

            }
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {

        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {

        }

        protected void CategoriesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(CategoriesDropDownList.SelectedValue);
                ItemsDropDownList.DataSource = _ItemSalesRepository.GetAllItemsByCategories(Id);
                ItemsDropDownList.DataTextField = "Name";
                ItemsDropDownList.DataValueField = "Id";
                ItemsDropDownList.DataBind();
                ItemsDropDownList.Items.Insert(0, new ListItem("Select Items", "0"));

            }
            catch
            {

            }
        }

        protected void ItemsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(ItemsDropDownList.SelectedValue);
                var getPrice = _ItemSalesRepository.GetPriceByItem(Id);
                if (getPrice != null)
                {
                    txtItemPrice.Text = Convert.ToDecimal(getPrice.Price).ToString();
                }
            }
            catch
            {
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtSubPrice.Text = (decimal.Parse(txtItemPrice.Text) * decimal.Parse(txtQty.Text)).ToString();

            }
            catch
            { }
        }

        protected void ItemSalesGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ItemSalesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ItemSalesGridView.PageIndex = e.NewPageIndex;
            LoadSaleItems();
        }

        protected void TotalButton_Click(object sender, EventArgs e)
        {
            DiscountCalculation();
        }

        protected void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            decimal totalcost = Convert.ToDecimal(txtTotalCost.Text);
            decimal PaidAmount = Convert.ToDecimal(txtPaidAmount.Text);

            decimal changes = PaidAmount - totalcost;
            lblChanges.Text = Convert.ToDecimal(changes).ToString();
        }
    }
}