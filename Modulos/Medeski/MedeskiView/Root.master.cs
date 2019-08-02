using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using MedeskiView.Controllers;

namespace MedeskiView {
    public partial class RootMaster : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) 
        {
            GE_TPERIODOPRESUPUESTO periodo = new CtrPeriodoPresupuesto().GetPeriodoActivo();
            Session["peri_activo"] = periodo.peri_ano + "-" + periodo.peri_paso;
        }

        protected void keyPress(object sender, EventArgs e)
        {

        }
        
    }
}