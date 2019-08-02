using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedeskiView.Controllers;

namespace MedeskiView.Forms
{
    public partial class frmSalir : System.Web.UI.Page
    {
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect(strUrl);
            }
            else
                Response.Redirect(strUrl);
        }
    }
}