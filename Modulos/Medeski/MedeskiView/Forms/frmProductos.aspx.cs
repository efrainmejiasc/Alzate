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
    public partial class frmProductos : System.Web.UI.Page
    {
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        CtrPersonas Cpersonas;
        CtrParametros Cparametros;
        CtrProductos Cproductos;
        CtrDrivers Cdrivers;
        CtrUtilidades Cutilidades;
        Hashtable productoSeleccionado = null;
        string[] camposProducto = new string[] { "prod_consecutivo", "prod_intermedio", "prod_contrato",
            "prod_tipo_licencia", "prod_criterio", "prod_componente", "prod_activo", "prod_codigo", 
            "prod_descripcion", "prod_responsable", "prod_serv_venta", "prod_driver1", "prod_driver2" , "prod_redistribuir",
            "GE_TPARAMETROS2.parm_descripcion" };


        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                Session["objeto"] = null;

                if (Session["mensaje"] != null)
                {
                    VentanaValidaciones.mostrarConfirmarAccion("Registro satisfactorio", "¿Desea Registrar Items para el Producto?");
                    Session["mensaje"] = null;
                }
                CargarDatos();
            }
            else
            {
                Response.Redirect(strUrl);
            }
        }

        #region Metodos

        public void CargarDatos()
        {
            try
            {
                Cpersonas = new CtrPersonas();
                Cparametros = new CtrParametros();
                Cdrivers = new CtrDrivers();
                Cproductos = new CtrProductos();
                Cutilidades = new CtrUtilidades();
                gvProductos.DataSource = Cproductos.GetAllGridView();
                gvProductos.DataBind();
                Cutilidades.ConfigurarGrid(gvProductos);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los registros. " + ex.Message);
            }
        }
        #endregion

        #region Eventos
        protected void NuevoClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmProductos_form.aspx");
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmItems_form.aspx");
        }

        protected void grid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                if (e.ButtonID != "btnConsultar" && e.ButtonID != "btnDetalles") return;

                productoSeleccionado = new Hashtable();
                foreach (string campo in camposProducto)
                {
                    productoSeleccionado[campo] = gvProductos.GetRowValues(e.VisibleIndex, campo);
                }

                GE_TPRODUCTOS objeto = new GE_TPRODUCTOS();

                objeto.prod_consecutivo = Convert.ToInt32(productoSeleccionado["prod_consecutivo"].ToString());
                objeto.prod_intermedio = Convert.ToInt32(productoSeleccionado["prod_intermedio"].ToString());
                objeto.prod_contrato = Convert.ToInt32(productoSeleccionado["prod_contrato"].ToString());
                objeto.prod_activo = Convert.ToInt32(productoSeleccionado["prod_activo"].ToString());
                objeto.prod_componente = Convert.ToInt32(productoSeleccionado["prod_componente"].ToString());
                objeto.prod_criterio = Convert.ToInt32(productoSeleccionado["prod_criterio"].ToString());
                objeto.prod_responsable = Convert.ToInt32(productoSeleccionado["prod_responsable"].ToString());
                objeto.prod_tipo_licencia = Convert.ToInt32(productoSeleccionado["prod_tipo_licencia"].ToString());

                int? nulo = null;
                objeto.prod_serv_venta = (productoSeleccionado["prod_serv_venta"] == null) ? nulo : Convert.ToInt32(productoSeleccionado["prod_serv_venta"]);
                objeto.prod_driver1 = (productoSeleccionado["prod_driver1"] == null) ? nulo : Convert.ToInt32(productoSeleccionado["prod_driver1"]);
                objeto.prod_driver2 = (productoSeleccionado["prod_driver2"] == null) ? nulo : Convert.ToInt32(productoSeleccionado["prod_driver2"]);
                objeto.prod_codigo = productoSeleccionado["prod_codigo"].ToString();
                objeto.prod_descripcion = productoSeleccionado["prod_descripcion"].ToString();
                objeto.prod_redistribuir = Convert.ToInt32(productoSeleccionado["prod_redistribuir"].ToString());

                GE_TPARAMETROS mParametro = new GE_TPARAMETROS();
                mParametro.parm_descripcion = productoSeleccionado["GE_TPARAMETROS2.parm_descripcion"].ToString();
                objeto.GE_TPARAMETROS2 = mParametro;

                Session["objeto"] = objeto;
                Session["producto"] = objeto;

                if (e.ButtonID == "btnConsultar")
                {                    
                    Response.Redirect("frmProductos_form.aspx");
                }
                else
                {
                    Response.Redirect("frmItems.aspx");
                }
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede consultar el registro. " + ex.Message);
            }
        }
        #endregion
    }
}