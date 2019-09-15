using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoffeeShop
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Session["Admin"]!=null)
                {
                    lnAdminName.Visible = true;
                    lnAuthorised.Visible = false;
                    lnProduct.Visible = true;
                    lnItemSales.Visible = true;
                    lnLogout.Visible = true;
                    lnUser.Visible = true;

                }
                else if(Session["Authorised"] !=null)
                {
                    lnAdminName.Visible = false;
                    lnProduct.Visible = false;
                    lnUser.Visible = false;
                }
                else
                {
                    lnAdminName.Visible = false;
                    lnAuthorised.Visible = false;
                    lnProduct.Visible = false;
                    lnItemSales.Visible = false;
                    lnUser.Visible = false;
                    lnLogout.Visible = false;
                }
            }
        }
    }
}