using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using MedeskiView.Controllers;

namespace MedeskiView.Forms
{
    public partial class frmDistribucionValorMAS_Detalle : System.Web.UI.Page
    {
        CtrUtilidades Cutilidades;
        CtrVwVlrDistribMAS CVwVlrDistribMAS;
        CtrVwVlrEncabDistribMAS CVwVlrEncabDistribMAS;

       CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                CargarDatos();
            }
            else
                Response.Redirect(strUrl);            
        }

        protected void grid_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = CustomDataSource;
        }


        public void CargarDatos ()
        {
            try
            {
                Cutilidades = new CtrUtilidades();
                Cutilidades.ConfigurarGrid(grid);
     
                CVwVlrDistribMAS = new CtrVwVlrDistribMAS();
                IList<VW_VLR_DISTRIB_MAS> iL = CVwVlrDistribMAS.GetAll();
                grid.DataSource = iL;
                Session["DataSource"] = iL;
                grid.DataBind();

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los items. " + ex.Message);
            }
        }

        private IList<VW_VLR_DISTRIB_MAS> CustomDataSource
        {
            get
            {
                IList<VW_VLR_DISTRIB_MAS> result = Session["DataSource"] as IList<VW_VLR_DISTRIB_MAS>;
                Session["DataSource"] = result;
                return result;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmDistribucionValorMAS.aspx");
        }

        
    }
}