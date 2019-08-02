using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedeskiView.Controllers;

namespace MedeskiView.Forms
{
    public partial class frmCuadroServicios : System.Web.UI.Page
    {
        CtrUtilidades Cutilidades;
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

        public void CargarDatos()
        {
            try
            {

                Cutilidades = new CtrUtilidades();
                gvSalida.DataSource = Cutilidades.GenerarCuadroServicio();
                gvSalida.DataBind();
                Cutilidades.ConfigurarGrid(gvSalida);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se pueden cargar los registros. ", ex);
            }
        }
    }
}