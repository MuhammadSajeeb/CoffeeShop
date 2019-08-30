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
        public void CreatePdf()
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + txtSerial.Text + "" + s + "" + DateTime.Now.ToString() + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Document document = new Document();
            document = new Document(PageSize.B6);
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

            cell = new PdfPCell(new Phrase("Cashier Name : " + txtCashierName.Text + " ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 0;
            cell.BorderWidth = 0f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            dth.AddCell(cell);

            cell = new PdfPCell(new Phrase("Customer Name : " + txtCustomerName.Text + " ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 0;
            cell.BorderWidth = 0f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            dth.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date : " + DateTime.Now.ToString("dd MMMM,yyyy" + " " + "HH : mm : ss : tt ") + " ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            dth.AddCell(cell);

            document.Add(dth);
            LineSeparator line = new LineSeparator(0f, 100, null, Element.ALIGN_CENTER, -2);
            document.Add(line);
            PdfPTable dtempty = new PdfPTable(1);
            document.Add(dtempty);
            // cell.BorderWidth = 0f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;

            //int columnsCount = ReportGridView.HeaderRow.Cells.Count;
            float[] gridtable = new float[4] { 12, 5, 8, 8 };
            // Create the PDF Table specifying the number of columns
            PdfPTable pdfTable = new PdfPTable(gridtable);
            pdfTable.WidthPercentage = 100;

            foreach (TableCell gridViewHeaderCell in ReportGridView.HeaderRow.Cells)
            {
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD)));
                pdfCell.HorizontalAlignment = 1;
                pdfCell.VerticalAlignment = 1;
                pdfCell.BorderWidth = 0f;
                pdfCell.Padding = 5;
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
                        pdfCell.HorizontalAlignment = 1;
                        pdfCell.VerticalAlignment = 1;
                        pdfCell.BorderWidth = 0f;
                        pdfCell.Padding = 2;
                        pdfTable.AddCell(pdfCell);
                    }
                }
            }

            PdfPTable dtempty1 = new PdfPTable(1);
            document.Add(dtempty1);

            LineSeparator line1 = new LineSeparator(0f, 100, null, Element.ALIGN_CENTER, -2);


            PdfPTable dtempty2 = new PdfPTable(1);
            document.Add(dtempty2);

            float[] titwidths = new float[1] { 200 };
            PdfPTable grnd = new PdfPTable(titwidths);
            dth.WidthPercentage = 100;


            cell = new PdfPCell(new Phrase("Grand Total : " + txtSubTotal.Text + " ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell = new PdfPCell(new Phrase("Discount Percent : " + txtDiscount.Text + " ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell = new PdfPCell(new Phrase("Payable Amount : " + txtTotalCost.Text + " ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell = new PdfPCell(new Phrase("Paid Amount : " + txtPaidAmount.Text + " ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell = new PdfPCell(new Phrase("Changes Amount : " + lblChanges.Text + " ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell.FixedHeight = 10f;
            dtempty.AddCell(cell);
            document.Add(dtempty);
            document.Add(pdfTable);
            dtempty1.AddCell(cell);
            document.Add(dtempty1);
            document.Add(line1);
            dtempty2.AddCell(cell);
            document.Add(dtempty2);
            document.Add(grnd);

            document.Close();
            Response.Flush();
            Response.End();
        }
        protected void AddButton_Click(object sender, EventArgs e)
        {

            LoadSaleItems();
            LoadReport();
            GridviewRowSum();

        }

        string s = "_";
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + txtSerial.Text + "" + s + "" + DateTime.Now.ToString() + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Document document = new Document();
            document = new Document(PageSize.B6,20f,20f,25f,0f);//left,right,top,bottom
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

            cell = new PdfPCell(new Phrase("Cashier Name : "+txtCashierName.Text+" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 0;
            cell.BorderWidth = 0f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            dth.AddCell(cell);

            cell = new PdfPCell(new Phrase("Customer Name : " + txtCustomerName.Text + " ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 0;
            cell.VerticalAlignment = 0;
            cell.BorderWidth = 0f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            dth.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date : " + DateTime.Now.ToString("dd MMMM,yyyy" + " " + "HH : mm : ss : tt ") + " ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            dth.AddCell(cell);

            document.Add(dth);
            LineSeparator line = new LineSeparator(0f, 100, null, Element.ALIGN_CENTER, -2);
            document.Add(line);
            PdfPTable dtempty = new PdfPTable(1);
            document.Add(dtempty);
            // cell.BorderWidth = 0f;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;

            //int columnsCount = ReportGridView.HeaderRow.Cells.Count;
            float[] gridtable = new float[4] { 12,5,8,8 };
            // Create the PDF Table specifying the number of columns
            PdfPTable pdfTable = new PdfPTable(gridtable);
            pdfTable.WidthPercentage = 100;

            foreach (TableCell gridViewHeaderCell in ReportGridView.HeaderRow.Cells)
            {
                PdfPCell pdfCell = new PdfPCell(new Phrase(gridViewHeaderCell.Text, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD)));
                pdfCell.HorizontalAlignment = 1;
                pdfCell.VerticalAlignment = 1;
                pdfCell.BorderWidth = 0f;
                pdfCell.Padding = 5;
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
                        pdfCell.HorizontalAlignment = 1;
                        pdfCell.VerticalAlignment = 1;
                        pdfCell.BorderWidth = 0f;
                        pdfCell.Padding = 2;
                        pdfTable.AddCell(pdfCell);
                    }
                }
            }

            PdfPTable dtempty1 = new PdfPTable(1);
            document.Add(dtempty1);

            LineSeparator line1 = new LineSeparator(0f, 100, null, Element.ALIGN_CENTER, -2);
            

            PdfPTable dtempty2 = new PdfPTable(1);
            document.Add(dtempty2);

            float[] titwidths = new float[3] { 75, 5, 20 };
            PdfPTable grnd = new PdfPTable(titwidths);
            dth.WidthPercentage = 100;


            cell = new PdfPCell(new Phrase("Grand Total", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell = new PdfPCell(new Phrase(txtSubTotal.Text, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment =2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            // second row strat

            cell = new PdfPCell(new Phrase("Discount Amount", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell = new PdfPCell(new Phrase(txtDiscount.Text, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            //third row start

            cell = new PdfPCell(new Phrase("Payable Amount", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell = new PdfPCell(new Phrase(txtTotalCost.Text, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            //fourth row start

            cell = new PdfPCell(new Phrase("Paid Amount", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 1;
            cell.VerticalAlignment = 1;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell = new PdfPCell(new Phrase(txtPaidAmount.Text, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);


            //five row start

            cell = new PdfPCell(new Phrase("Changes Amount", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            cell = new PdfPCell(new Phrase(lblChanges.Text, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL)));
            cell.HorizontalAlignment = 2;
            cell.VerticalAlignment = 2;
            cell.BorderWidth = 0f;
            cell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            grnd.AddCell(cell);

            //empty table for blank space
            PdfPTable dtempty3 = new PdfPTable(1);
            document.Add(dtempty3);

            //new table create
            float[] footer = new float[1] { 200 };
            PdfPCell fcell;
            PdfPTable ft = new PdfPTable(footer);
            ft.WidthPercentage = 100;

            fcell = new PdfPCell(new Phrase("------- Thank You -------", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD)));
            fcell.HorizontalAlignment = 1;
            fcell.VerticalAlignment = 1;
            fcell.BorderWidth = 0f;
            fcell.Padding = 3;
            //cell.BorderColor = BaseColor.LIGHT_GRAY;
            //cell.FixedHeight = 20f;
            ft.AddCell(fcell);

            cell.FixedHeight = 10f;
            dtempty.AddCell(cell);
            document.Add(dtempty);
            document.Add(pdfTable);
            dtempty1.AddCell(cell);
            document.Add(dtempty1);
            document.Add(line1);
            dtempty2.AddCell(cell);
            document.Add(dtempty2);
            document.Add(grnd);
            dtempty3.AddCell(cell);
            document.Add(dtempty3);
            document.Add(ft);

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

        protected void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            decimal totalcost = Convert.ToDecimal(txtTotalCost.Text);
            decimal PaidAmount = Convert.ToDecimal(txtPaidAmount.Text);

            decimal changes = PaidAmount - totalcost;
            lblChanges.Text = Convert.ToDecimal(changes).ToString();
        }
        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            DiscountCalculation();
        }

        protected void PrintButton_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;
            var sql = _MainRepository.ConnectionString();
            using (SqlConnection Sqlcon=new SqlConnection(sql))
            {
                Sqlcon.Open();
                transaction = Sqlcon.BeginTransaction();
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

                    SqlCommand command1 = new SqlCommand("Insert Into AccountSales(Serial,CashierName,CustomerName,GrandTotal,Discount,PaidAmount,ChangesAmount,Date) Values ('" + _AccountSales.Serial + "','" + _AccountSales.CashierName + "','" + _AccountSales.CustomerName + "','" + _AccountSales.GrandTotal + "','" + _AccountSales.Discount + "','" + _AccountSales.PaidAmount + "','" + _AccountSales.ChangesAmount + "','" + DateTime.Now.ToShortDateString() + "')", Sqlcon, transaction);
                    command1.CommandType = CommandType.Text;
                    command1.ExecuteNonQuery(); //AccountSale Save;

                    //ItemSales Start With foreach

                    foreach (GridViewRow gr in ItemSalesGridView.Rows)
                    {
                        ItemSales _ItemSales = new ItemSales();
                        _ItemSales.Name = (gr.Cells[1].Text);
                        _ItemSales.Qty = Convert.ToInt32(gr.Cells[2].Text);
                        _ItemSales.Per_Price = Convert.ToDecimal(gr.Cells[3].Text);
                        _ItemSales.Sub_Price = Convert.ToDecimal(gr.Cells[4].Text);
                        _ItemSales.Serial = txtSerial.Text;

                        SqlCommand command = new SqlCommand("Insert Into ItemSales(Name,Qty,Per_Price,Sub_Price,Serial,Date) Values ('" + _ItemSales.Name + "','" + _ItemSales.Qty + "','" + _ItemSales.Per_Price + "','" + _ItemSales.Sub_Price + "','" + _ItemSales.Serial + "','" + DateTime.Now.ToShortDateString() + "')", Sqlcon, transaction);
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();


                    }

                    transaction.Commit();
                    CreatePdf();
                    Response.Redirect(Request.Url.AbsoluteUri);

                }
                catch
                {
                    transaction.Rollback();
                }
                finally
                {
                    Sqlcon.Close();
                }
            }

            //try
            //{


            //    AccountSales _AccountSales = new AccountSales();
            //    _AccountSales.Serial = txtSerial.Text;
            //    _AccountSales.CashierName = txtCashierName.Text;
            //    _AccountSales.CustomerName = txtCustomerName.Text;
            //    _AccountSales.GrandTotal = Convert.ToDecimal(txtTotalCost.Text);
            //    _AccountSales.Discount = Convert.ToDecimal(txtDiscount.Text);
            //    _AccountSales.PaidAmount = Convert.ToDecimal(txtPaidAmount.Text);
            //    _AccountSales.ChangesAmount = Convert.ToDecimal(lblChanges.Text);

            //    int savesuccess1 = _ItemSalesRepository.AddAccountSale(_AccountSales);
            //    if (savesuccess1 > 0)
            //    {
            //        //AutoCodeGenerate();
            //        Response.Redirect(Request.Url.AbsoluteUri);
            //    }
            //    else
            //    {

            //    }
            //}
            //catch
            //{ }
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