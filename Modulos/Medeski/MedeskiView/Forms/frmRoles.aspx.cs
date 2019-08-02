using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedeskiView.Controllers;
using DevExpress.Web;
using System.Collections;
using System.Data;

namespace MedeskiView.Forms
{
    public partial class frmRoles : System.Web.UI.Page
    {
        CtrRoles ctrRoles = new CtrRoles();
        CtrVlrsParamGrales parametros = new CtrVlrsParamGrales();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "GE_TPARAMETROS.parm_descripcion", "rolm_consecutivo", "rolm_nombre", "rolm_descripcion", "rolm_estado" };

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
                cargarDatos();
            }
            else
                Response.Redirect(strUrl);
        }

        public void cargarDatos()
        {
            try
            {
                IdGrid.DataSource = ctrRoles.GetRoles();
                IdGrid.DataBind();
                Cutilidades.ConfigurarGrid(IdGrid);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los registros. " + ex.Message);
            }
        }

        #region Eventos
        protected void IdGrid_CustomColumnDisplayText(object sender,DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("rolm_estado"))
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

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmRoles_form.aspx");
        }

        protected void IdGrid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            camposSeleccionado = new Hashtable();
            foreach (string campo in camposClaseparametro)
            {
                camposSeleccionado[campo] = IdGrid.GetRowValues(e.VisibleIndex, campo);
            }

            GE_TROLES objeto = new GE_TROLES();

            objeto.rolm_consecutivo = Convert.ToInt32(camposSeleccionado["rolm_consecutivo"]);
            objeto.rolm_nombre = camposSeleccionado["rolm_nombre"].ToString();
            objeto.rolm_descripcion = camposSeleccionado["rolm_descripcion"].ToString();
            objeto.rolm_estado = Convert.ToInt32(camposSeleccionado["rolm_estado"].ToString());

            GE_TPARAMETROS param = new GE_TPARAMETROS();
            param.parm_descripcion = camposSeleccionado["GE_TPARAMETROS.parm_descripcion"].ToString();
            objeto.GE_TPARAMETROS = param;
            
            Session["objeto"] = objeto;
            Response.Redirect("frmRoles_form.aspx");

        }
        #endregion
    }
}