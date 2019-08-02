using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedeskiView.Controllers;

namespace MedeskiView.Forms
{
    public partial class frmCParametros : System.Web.UI.Page
    {
        CtrCParametros ctrParam = new CtrCParametros();
        CtrParametros ctrCparam = new CtrParametros();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "parm_consecutivo", "clap_clase", "parm_descripcion", "parm_codigo", "parm_estado", "parm_fechadesde", "parm_fechahasta" };
        CtrVlrsParamGrales parametros = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + parametros.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                Session["objeto"] = null;

                if (Session["mensaje"] != null)
                {
                    VentanaValidaciones.mostrarRegistroExitoso();
                    Session["mensaje"] = null;
                }

                cargardatos();
            }
            else
                Response.Redirect(strUrl);
        }

        #region Metodos
        public void cargardatos()
        {
            try
            {
                GE_TCLASESPARAMETROS clase = Session["clase"] as GE_TCLASESPARAMETROS;
                gvParametros.DataSource = ctrParam.GetListbyClase(clase.clap_nombre);
                gvParametros.DataBind();
                Cutilidades.ConfigurarGrid(gvParametros);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se pueden cargar los registros. ", ex);
            }
        }
        #endregion

        #region Eventos
        protected void gvParametros_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("parm_estado"))
            {
                if (Convert.ToInt32(e.Value) == 1)
                {
                    e.DisplayText = "Activo";
                }
                else
                {
                    e.DisplayText = "Inactivo";
                }
            }
        }

        protected void gvParametros_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            camposSeleccionado = new Hashtable();
            foreach (string campo in camposClaseparametro)
            {
                camposSeleccionado[campo] = gvParametros.GetRowValues(e.VisibleIndex, campo);
            }

            GE_TPARAMETROS objeto = new GE_TPARAMETROS();

            objeto.parm_consecutivo = Convert.ToInt32(camposSeleccionado["parm_consecutivo"]);
            objeto.parm_descripcion = camposSeleccionado["parm_descripcion"].ToString();
            objeto.parm_codigo = camposSeleccionado["parm_codigo"].ToString();
            objeto.clap_clase = Convert.ToInt32(camposSeleccionado["clap_clase"].ToString());
            objeto.parm_estado = Convert.ToInt32(camposSeleccionado["parm_estado"].ToString());
            objeto.parm_fechadesde = DateTime.Parse(camposSeleccionado["parm_fechadesde"].ToString());
            objeto.parm_fechahasta = (camposSeleccionado["parm_fechahasta"] == null) ? (DateTime?) null : DateTime.Parse(camposSeleccionado["parm_fechahasta"].ToString());

            Session["objeto"] = objeto;
            Response.Redirect("frmCParametros_form.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmCParametros_form.aspx");
        }        

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmParametros.aspx");
        }
        #endregion
    }
}