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
                     
                }
                else if(Session["Authorised"]!=null)
                {
                    lnProduct.Visible = false;
                     
                }
                else
                {
                    lnProduct.Visible = false;
                    lnItemSales.Visible = false;
                    lnLogout.Visible = false;
                }
            }
        }
    }
}