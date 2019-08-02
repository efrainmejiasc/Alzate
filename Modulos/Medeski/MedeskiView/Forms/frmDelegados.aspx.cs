using DevExpress.Web;
using MedeskiView.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedeskiView.Forms
{
    public partial class frmDelegados : System.Web.UI.Page
    {
        CtrPersonas Ctrpersonas = new CtrPersonas();
        CtrParametros Cparametros = new CtrParametros();
        CtrDelegados Cdelegados = new CtrDelegados();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "dele_consecutivo"};
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                Session["objeto"] = null;

                if (Session["mensaje"] != null)
                {
                    VentanaValidaciones.mostrarRegistroExitoso();
                    Session["mensaje"] = null;
                }
                CargarDatos();
            }
            else
                Response.Redirect(strUrl);
        }

        #region Metodos
        
        public void CargarDatos()
        {
            try
            {
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                Ctrpersonas = new CtrPersonas();
                gvPrItems.DataSource = Cdelegados.GetAllDelegados(strUsuario[0].ToString().ToUpper()).OrderBy(x => x.GE_TPARAMETROS.parm_descripcion);
                gvPrItems.DataBind();
                Cutilidades.ConfigurarGrid(gvPrItems);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los registros. " + ex.Message);
            }
        }
        #endregion

        #region Eventos

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmDelegados_form.aspx");
        }

        protected void grid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {

                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                camposSeleccionado = new Hashtable();
                foreach (string campo in camposClaseparametro)
                {
                    camposSeleccionado[campo] = gvPrItems.GetRowValues(e.VisibleIndex, campo);
                }

                GE_TDELEGADOS delegado = Cdelegados.GetSingle(Convert.ToInt32(camposSeleccionado["dele_consecutivo"].ToString()));
                delegado.dele_usuario_act = strUsuario[0].ToString();
                delegado.dele_fecha_act = DateTime.Now;
                delegado.dele_activo = 0;
                Cdelegados.Update(delegado);

                CargarDatos();
                VentanaValidaciones.mostrarMensajePersonalizado("Ok", "Delegado eliminado con exito");
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarErrorEliminar();
            }
        }
        #endregion
    }
}