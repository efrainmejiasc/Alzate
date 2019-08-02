using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedeskiView.Controllers;
using DevExpress.Web;
using System.Collections;

namespace MedeskiView.Forms
{
    public partial class frmCentroCostos : System.Web.UI.Page
    {
        CtrCentroCosto ctrCentrocostos = new CtrCentroCosto();
        CtrPersonas ctrPersonas = new CtrPersonas();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        CtrParametros ctrParametros = new CtrParametros();
        CtrCentroOperacion ctrCentroOperacion = new CtrCentroOperacion();
        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "cost_consecutivo", "cost_codigo", "cost_descripcion", "cost_centro_operacion", "cost_responsable", "GE_TCOMPANIAS.comp_nombre", "GE_TPARAMETROS.parm_descripcion", "GE_TPARAMETROS2.parm_descripcion", "cost_activo" };
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
        protected void CargarDatos()
        {
            try
            {
                GE_TCOMPANIAS compania = Session["compania"] as GE_TCOMPANIAS;
                grid.DataSource = ctrCentrocostos.GetAllCuentaxParametros().Where(x => x.GE_TCOMPANIAS.comp_nombre == compania.comp_nombre).ToList();
                grid.DataBind();
                Cutilidades.ConfigurarGrid(grid);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los datos. " + ex.Message);
            }
        }
        #endregion


        #region Eventos

        protected void IdGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("cost_activo"))
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
            else if (e.Column.FieldName.Equals("GE_TPARAMETROS2.parm_descripcion"))
            {
                if (e.Value == "")
                {
                    e.DisplayText = "N/A";
                }
            }
        }

        protected void IdGrid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {

                GE_TCENTROSCOSTOS objeto = new GE_TCENTROSCOSTOS();

                camposSeleccionado = new Hashtable();
                foreach (string campo in camposClaseparametro)
                {
                    camposSeleccionado[campo] = grid.GetRowValues(e.VisibleIndex, campo);
                }

                objeto.cost_consecutivo = Convert.ToInt32(camposSeleccionado["cost_consecutivo"].ToString());
                objeto.cost_codigo = camposSeleccionado["cost_codigo"].ToString();
                objeto.cost_descripcion = camposSeleccionado["cost_descripcion"].ToString();
                objeto.cost_responsable = camposSeleccionado["cost_responsable"] != null ? camposSeleccionado["cost_responsable"].ToString() : null;
                objeto.cost_centro_operacion = camposSeleccionado["cost_centro_operacion"].ToString();
                    
                GE_TPARAMETROS param1 = new GE_TPARAMETROS();
                param1.parm_descripcion = camposSeleccionado["GE_TPARAMETROS.parm_descripcion"].ToString();
                objeto.GE_TPARAMETROS = param1;
                
                GE_TPARAMETROS param2 = new GE_TPARAMETROS();
                param2.parm_descripcion = camposSeleccionado["GE_TPARAMETROS2.parm_descripcion"].ToString();
                objeto.GE_TPARAMETROS2 = param2;

                GE_TCOMPANIAS comp = new GE_TCOMPANIAS();
                comp.comp_nombre = camposSeleccionado["GE_TCOMPANIAS.comp_nombre"] != null ? camposSeleccionado["GE_TCOMPANIAS.comp_nombre"].ToString() : null;
                objeto.GE_TCOMPANIAS = comp;

                objeto.cost_activo = Convert.ToInt32(camposSeleccionado["cost_activo"].ToString());

                Session["objeto"] = objeto;
                Response.Redirect("frmCentroCostos_form.aspx");

            }catch(Exception ex)
            {
                VentanaValidaciones.mostrarError("No se puede mostrar la info." + ex.ToString());
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmCentroCostos_form.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmCompanias.aspx");
        }

        #endregion
    }
}