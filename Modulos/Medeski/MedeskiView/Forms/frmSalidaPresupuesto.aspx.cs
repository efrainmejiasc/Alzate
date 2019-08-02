using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using MedeskiView.Controllers;

namespace MedeskiView.Forms
{
    public partial class frmSalidaPresupuesto : System.Web.UI.Page
    {
        CtrVwSalidaPresupuesto Cvwsalida;
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
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                Cvwsalida = new CtrVwSalidaPresupuesto();
                Cutilidades = new CtrUtilidades();
                gvSalida.DataSource = Cvwsalida.GetAllxUser(strUsuario[0].ToString().ToUpper());
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