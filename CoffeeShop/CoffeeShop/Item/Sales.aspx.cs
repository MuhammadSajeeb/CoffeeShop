using Coffee.Core.Models;
using Coffee.Persistancis.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Text;

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
                ItemsDropDownList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Items", "0"));
                UpdateButton.Visible = true;
                DeleteButton.Visible = false;
                txtDiscount.Text = "0";
                txtCashierName.Text = "Mr.Sajib";
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
            CategoriesDropDownList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Categories", "0"));

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
                    ItemsDropDownList.ClearSelection();
                    txtQty.Text = "";
                    txtItemPrice.Text = "";
                    txtSubPrice.Text = "";
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

            //Dummy data for Invoice (Bill).
            string companyName = "ASPSnippets";
            int orderNo = 2303;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] {
                            new DataColumn("ProductId", typeof(string)),
                            new DataColumn("Product", typeof(string)),
                            new DataColumn("Price", typeof(int)),
                            new DataColumn("Quantity", typeof(int)),
                            new DataColumn("Total", typeof(int))});
            dt.Rows.Add(101, "Sun Glasses", 200, 5, 1000);
            dt.Rows.Add(102, "Jeans", 400, 2, 800);
            dt.Rows.Add(103, "Trousers", 300, 3, 900);
            dt.Rows.Add(104, "Shirts", 550, 2, 1100);

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();

                    //Generate Invoice (Bill) Header.
                    sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                    sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Order Sheet</b></td></tr>");
                    sb.Append("<tr><td colspan = '2'></td></tr>");
                    sb.Append("<tr><td><b>Order No: </b>");
                    sb.Append(orderNo);
                    sb.Append("</td><td align = 'right'><b>Date: </b>");
                    sb.Append(DateTime.Now);
                    sb.Append(" </td></tr>");
                    sb.Append("<tr><td colspan = '2'><b>Company Name: </b>");
                    sb.Append(companyName);
                    sb.Append("</td></tr>");
                    sb.Append("</table>");
                    sb.Append("<br />");

                    //Generate Invoice (Bill) Items Grid.
                    sb.Append("<table border = '1'>");
                    sb.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        sb.Append("<th style = 'background-color: #D20B0C;color:#ffffff'>");
                        sb.Append(column.ColumnName);
                        sb.Append("</th>");
                    }
                    sb.Append("</tr>");
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            sb.Append("<td>");
                            sb.Append(row[column]);
                            sb.Append("</td>");
                        }
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr><td align = 'right' colspan = '");
                    sb.Append(dt.Columns.Count - 1);
                    sb.Append("'>Total</td>");
                    sb.Append("<td>");
                    sb.Append(dt.Compute("sum(Total)", ""));
                    sb.Append("</td>");
                    sb.Append("</tr></table>");

                    //Export HTML String as PDF.
                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfDoc = new Document(PageSize.B7, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + orderNo + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
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
                ItemsDropDownList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Items", "0"));
                txtItemPrice.Text = "";
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
                    txtQty.Text = "";
                    txtQty.Focus();
                    txtSubPrice.Text = "";
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
            //ItemSalesGridView.PageIndex = e.NewPageIndex;
            //LoadSaleItems();
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

        protected void PrintButton_Click(object sender, EventArgs e)
        {
            try
            {
                AccountSales _AccountSales = new AccountSales();
                _AccountSales.Serial = txtSerial.Text;
                _AccountSales.CashierName = txtCashierName.Text;
                _AccountSales.CustomerName = txtCustomerName.Text;
                _AccountSales.GrandTotal = Convert.ToDecimal(txtTotalCost.Text);
                _AccountSales.Discount = Convert.ToDecimal(txtDiscount.Text);
                _AccountSales.PaidAmount = Convert.ToDecimal(txtPaidAmount.Text);
                _AccountSales.ChangesAmount = Convert.ToDecimal(lblChanges.Text);

                int savesuccess = _ItemSalesRepository.AddAccountSale(_AccountSales);
                if(savesuccess>0)
                {
                    AutoCodeGenerate();
                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                lblSerial.Text = ex.Message;
            }
        }

        protected void ItemSalesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                int successdelete = _ItemSalesRepository.Delete(Convert.ToInt32(e.CommandArgument));
                if(successdelete>0)
                {
                    lblCashierName.Text = "delete";
                    LoadSaleItems();
                }
                else
                {

                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}