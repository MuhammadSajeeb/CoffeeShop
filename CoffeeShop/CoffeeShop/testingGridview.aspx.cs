using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeShop
{
    public partial class testingGridview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //In page load we have to bind that grid view
            if (!IsPostBack)
            {
                BindGridView();
            }
        }
        private void BindGridView()
        {
            //Declare a datatable for the gridview
            DataTable dt = new DataTable();

            //Add Columns to the datatable
            dt.Columns.Add("No");
            dt.Columns.Add("Name");
            dt.Columns.Add("Designation");
            dt.Columns.Add("DOB");



            //Define a datarow for the datatable dt
            DataRow dr = dt.NewRow();


            //Now add the datarow to the datatable
            dt.Rows.Add(dr);

            //Now bind the datatable to gridview
            GridView1.DataSource = dt;
            GridView1.DataBind();

            //Now hide the extra row of the grid view
            GridView1.Rows[0].Visible = false;

            //Delete row 0 from the datatable
            dt.Rows[0].Delete();
            dt.AcceptChanges();

            //View the datatable to the viewstate
            ViewState["Data"] = dt;

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                //Findout the controls inside the gridview
                TextBox txtNo = (TextBox)GridView1.FooterRow.FindControl("txtFNo");
                TextBox txtName = (TextBox)GridView1.FooterRow.FindControl("txtFName");
                TextBox txtDesignation = (TextBox)GridView1.FooterRow.FindControl("txtFDesignation");
                TextBox txtDOB = (TextBox)GridView1.FooterRow.FindControl("txtFDOB");

                //Add the items to the gridview
                DataTable dt = new DataTable();

                //Assign the viewstate to the datatable
                dt = (DataTable)ViewState["Data"];


                DataRow dr = dt.NewRow();
                dr["No"] = txtNo.Text;
                dr["Name"] = txtName.Text;
                dr["Designation"] = txtDesignation.Text;
                dr["DOB"] = txtDOB.Text;

                //Add the datarow to the datatable
                dt.Rows.Add(dr);

                //Now bind the datatable to the gridview
                GridView1.DataSource = dt;
                GridView1.DataBind();

                //Add the details to viewstate also

                ViewState["Data"] = dt;

            }

        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Change the gridview to edit mode
            GridView1.EditIndex = e.NewEditIndex;

            //Now bind the gridview
            GridView1.DataSource = (DataTable)ViewState["Data"];
            GridView1.DataBind();

        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //Findout the controls inside the gridview
            TextBox txtNo = (TextBox)GridView1.Rows[e.RowIndex].Cells[0].FindControl("txtENo");
            TextBox txtName = (TextBox)GridView1.Rows[e.RowIndex].Cells[1].FindControl("txtEName");
            TextBox txtDesignation = (TextBox)GridView1.Rows[e.RowIndex].Cells[2].FindControl("txtEDesignation");
            TextBox txtDOB = (TextBox)GridView1.Rows[e.RowIndex].Cells[3].FindControl("txtEDOB");

            //Assign the ViewState to the datatable
            DataRow dr = ((DataTable)ViewState["Data"]).Rows[e.RowIndex];

            dr.BeginEdit();
            dr["No"] = txtNo.Text;
            dr["Name"] = txtName.Text;
            dr["Designation"] = txtDesignation.Text;
            dr["DOB"] = txtDOB.Text;

            dr.EndEdit();

            dr.AcceptChanges();

            GridView1.EditIndex = -1;

            //Now bind the datatable to the gridview
            GridView1.DataSource = (DataTable)ViewState["Data"];
            GridView1.DataBind();



        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ((DataTable)ViewState["Data"]).Rows[e.RowIndex].Delete();
            ((DataTable)ViewState["Data"]).AcceptChanges();



            if (((DataTable)ViewState["Data"]).Rows.Count > 0)
            {

                GridView1.DataSource = (DataTable)ViewState["Data"];
                GridView1.DataBind();

            }
            else
            {
                BindGridView();

            }

        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;

            GridView1.DataSource = (DataTable)ViewState["Data"];
            GridView1.DataBind();
        }

    }
}