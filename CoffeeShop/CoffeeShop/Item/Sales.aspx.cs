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
using System.Data.SqlClient;
using iTextSharp.text.pdf.draw;

namespace CoffeeShop.Item
{
    public partial class Sales : System.Web.UI.Page
    {
        DataTable dataTable = new DataTable();
        ItemSalesRepository _ItemSalesRepository = new ItemSalesRepository();
        MainRepository _MainRepository = new MainRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AutoCodeGenerate();
                GetALLCategories();
                ItemsDropDownList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Items", "0"));
                UpdateButton.Visible = true;
                DeleteButton.Visible = true;
                txtDiscount.Text = "0";
                txtCashierName.Text = "Mr.Sajib";
                txtDate.Text = DateTime.Now.ToString("MMMM dd,yyyy"+" " + "HH : MM : SS ");
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
            //string serial;
            //serial = txtSerial.Text;

            //ItemSalesGridView.DataSource = _ItemSalesRepository.GetAllSaleItems(serial);
            //ItemSalesGridView.DataBind();

            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Qty", typeof(int));
            dataTable.Columns.Add("Perprice", typeof(decimal));
            dataTable.Columns.Add("Subprice", typeof(decimal));
            DataRow dr = null;
            if (ViewState["Details"] != null)
            {
                for (int i = 0; i < 1; i++)
                {
                    dataTable = (DataTable)ViewState["Details"];
                    if (dataTable.Rows.Count > 0)
                    {
                        dr = dataTable.NewRow();
                        dr["Name"] = ItemsDropDownList.SelectedItem.ToString();
                        dr["Qty"] = txtQty.Text;
                        dr["Perprice"] = txtItemPrice.Text;
                        dr["Subprice"] = txtSubPrice.Text;
                        dataTable.Rows.Add(dr);

                        ItemSalesGridView.DataSource = dataTable;
                        ItemSalesGridView.DataBind();
                    }
                }
            }
            else
            {
                dr = dataTable.NewRow();
                dr["Name"] = ItemsDropDownList.SelectedItem.ToString();
                dr["Qty"] = txtQty.Text;
                dr["Perprice"] = txtItemPrice.Text;
                dr["Subprice"] = txtSubPrice.Text;
                dataTable.Rows.Add(dr);

                ItemSalesGridView.DataSource = dataTable;
                ItemSalesGridView.DataBind();
            }
            ViewState["Details"] = dataTable;

        }
        public void LoadReport()
        {
            ReportGridView.DataSource = dataTable;
            ReportGridView.DataBind();
        }
        public void GridviewRowSum()
        {
            //string serial;
            //serial = txtSerial.Text;

            //decimal Total = _ItemSalesRepository.SumOrdere(serial);
            //txtSubTotal.Text = Convert.ToDecimal(Total).ToString();

            decimal SubTotal = 0;
            foreach (GridViewRow row in ItemSalesGridView.Rows)
            {

                SubTotal = SubTotal + Convert.ToDecimal(row.Cells[4].Text); //Where Cells is the column. Just changed the index of cells
            }
            txtSubTotal.Text = SubTotal.ToString();

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

            LoadSaleItems();
            LoadReport();
            GridviewRowSum();

        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {

            //Dummy data for Invoice (Bill).
   
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] {
                            new DataColumn("Product", typeof(string)),
                            new DataColumn("Price", typeof(int)),
                            new DataColumn("Quantity", typeof(int)),
                            new DataColumn("Total", typeof(int))});
            dt.Rows.Add(  "Sun Glasses", 200, 5, 1000);
            dt.Rows.Add(  "Jeans", 400, 2, 800);
            dt.Rows.Add(  "Trousers", 300, 3, 900);
            dt.Rows.Add(  "Shirts", 550, 2, 1100);

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();

                    //Generate Invoice (Bill) Header.
                    sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                    sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Sajeeb Coffee House</b></td></tr>");
                    sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Your Reciept</b></td></tr>");
                    sb.Append("<tr><td colspan = '2'></td></tr>");
                    sb.Append("<tr><td><b>Serial No: </b>");
                    sb.Append(txtSerial.Text);
                    sb.Append("</td><td align = 'right'><b>Date : </b>");
                    sb.Append(DateTime.Now.ToString("MMMM dd,yyyy" +" "+ "HH : MM : ss : tt "));
                    sb.Append(" </td></tr>");
                    sb.Append("<tr><td colspan = '2'><b>Cashier Name: </b>");
                    sb.Append(txtCashierName.Text);
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td colspan = '2'><b>Customer Name: </b>");
                    sb.Append(txtCustomerName.Text);
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
                    sb.Append("</td><td align = 'right'><b> Sub total : </b>");
                    sb.Append("100");
                    sb.Append("</td></tr>");
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
                    Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + txtSerial.Text + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename='Royal University Of Dhaka'.pdf");
            Document document = new Document();
            document = new Document(PageSize.B7);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

            document.Open();
            float[] titwidth = new float[1] { 200 };
            PdfPCell cell;
            PdfPTable dth = new PdfPTable(titwidth);
            dth.WidthPercentage = 100;

            cell = new PdfPCell(new Phrase("Sajeeb Cofe House", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD)));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f;
            //cell.FixedHeight = 20f;
            dth.AddCell(cell);

            cell = new PdfPCell(new Phrase("Moghbazer,Mirbagh,Shop-02 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            dth.AddCell(cell);

            cell = new PdfPCell(new Phrase("Your Reciept", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            dth.AddCell(cell);

            document.Add(dth);
            LineSeparator line = new LineSeparator(0f, 100, null, Element.ALIGN_CENTER, -2);
            document.Add(line);
            PdfPTable dtempty = new PdfPTable(1);
            // cell.BorderWidth = 0f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;

            //int columnsCount = ReportGridView.HeaderRow.Cells.Count;
            float[] gridtable = new float[4] { 12,5,8,8 };
            // Create the PDF Table specifying the number of columns
            PdfPTable pdfTable = new PdfPTable(gridtable);
            pdfTable.WidthPercentage = 100;

            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;

            //pdfTable.DefaultCell.Padding = 5;
            //pdfTable.WidthPercentage = 100;
            //pdfTable.DefaultCell.HorizontalAlignment = 2;
            //pdfTable.DefaultCell.VerticalAlignment = 2;
            //pdfTable.DefaultCell.BorderWidth = 0f;
            //cell.BorderColor= BaseColor.LIGHT_GRAY;

            foreach (TableCell gridViewHeaderCell in ReportGridView.HeaderRow.Cells)
            {
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text));
                pdfTable.AddCell(pdfCell);

            }

            foreach (GridViewRow gridViewRow in ReportGridView.Rows)
            {
                if (gridViewRow.RowType == DataControlRowType.DataRow)
                {
                    // Loop thru each cell in GrdiView data row
                    foreach (TableCell gridViewCell in gridViewRow.Cells)
                    {

                        PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewCell.Text, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));

                        pdfTable.AddCell(pdfCell);
                    }
                }
            }

            float[] titwidths = new float[1] { 200 };
            PdfPTable grnd = new PdfPTable(titwidths);
            dth.WidthPercentage = 100;


            cell = new PdfPCell(new Phrase("Grand Total : "+txtSubTotal.Text+" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell.FixedHeight = 10f;
            dtempty.AddCell(cell);
            document.Add(dtempty);
            document.Add(pdfTable);
            document.Add(grnd);

            document.Close();
            Response.Flush();
            Response.End();
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
                foreach (GridViewRow gr in ItemSalesGridView.Rows)
                {
                    ItemSales _ItemSales = new ItemSales();
                    _ItemSales.Name = (gr.Cells[1].Text);
                    _ItemSales.Qty=Convert.ToInt32(gr.Cells[2].Text);
                    _ItemSales.Per_Price = Convert.ToDecimal(gr.Cells[3].Text);
                    _ItemSales.Sub_Price = Convert.ToDecimal(gr.Cells[4].Text);
                    _ItemSales.Serial = txtSerial.Text;

                    int savesuccess = _ItemSalesRepository.Add(_ItemSales);
                }

                AccountSales _AccountSales = new AccountSales();
                _AccountSales.Serial = txtSerial.Text;
                _AccountSales.CashierName = txtCashierName.Text;
                _AccountSales.CustomerName = txtCustomerName.Text;
                _AccountSales.GrandTotal = Convert.ToDecimal(txtTotalCost.Text);
                _AccountSales.Discount = Convert.ToDecimal(txtDiscount.Text);
                _AccountSales.PaidAmount = Convert.ToDecimal(txtPaidAmount.Text);
                _AccountSales.ChangesAmount = Convert.ToDecimal(lblChanges.Text);

                int savesuccess1 = _ItemSalesRepository.AddAccountSale(_AccountSales);
                if (savesuccess1 > 0)
                {
                    //AutoCodeGenerate();
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
                else
                {

                }
            }
            catch
            {}
        }

        protected void ItemSalesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //try
            //{

            //    int successdelete = _ItemSalesRepository.Delete(Convert.ToInt32(e.CommandArgument));
            //    if(successdelete>0)
            //    {
            //        lblCashierName.Text = "delete";
            //        LoadSaleItems();
            //        LoadReport();
            //        GridviewRowSum();
            //    }
            //    else
            //    {

            //    }
            //}
            //catch(Exception ex)
            //{

            //}
        }
        protected void ItemSalesGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            dataTable = (DataTable)ViewState["Details"];
            if (dataTable.Rows.Count > 0)
            {
                dataTable.Rows[e.RowIndex].Delete();
                ItemSalesGridView.DataSource = dataTable;
                ItemSalesGridView.DataBind();

                ReportGridView.DataSource = dataTable;
                ReportGridView.DataBind();
            }
            ViewState["Details"] = dataTable;
            GridviewRowSum();

        }
        private static Phrase FormatPhrase(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8));
        }
        private static Phrase FormatHeaderPhrase(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD));
        }
        private static Phrase FormatNormal(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL));
        }
        private static Phrase FormatHeaderPhrase1(string value)
        {
            return new Phrase(value, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, iTextSharp.text.Font.BOLD));
        }


    }
}