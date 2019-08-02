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
    public partial class frmItems : System.Web.UI.Page
    {
        CtrProductosItems CproductosItems = new CtrProductosItems();
        CtrProductos Cproductos = new CtrProductos();
        CtrCuentas Ccuentas = new CtrCuentas();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        Hashtable productoSeleccionado = null;
        string[] camposProducto = new string[] { "prit_consecutivo", "GE_TPRODUCTOS.prod_consecutivo", "GE_TCUENTAS.cuen_consecutivo", "prit_activo", "prit_item", "prit_tipo" };
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
                CproductosItems = new CtrProductosItems();
                Cproductos = new CtrProductos();
                Ccuentas = new CtrCuentas();
                Cutilidades = new CtrUtilidades();
                GE_TPRODUCTOS producto = Session["producto"] as GE_TPRODUCTOS;
                gvItems.DataSource = CproductosItems.GetAllGridViewXprod(producto.prod_consecutivo);
                gvItems.DataBind();
                Cutilidades.ConfigurarGrid(gvItems);

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se pueden cargar los registros. ", ex);
            }
        }
        #endregion

        #region Eventos
        protected void NuevoClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmItems_form.aspx");
        }

        protected void grid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                productoSeleccionado = new Hashtable();
                foreach (string campo in camposProducto)
                {
                    productoSeleccionado[campo] = gvItems.GetRowValues(e.VisibleIndex, campo);
                }

                GE_TPRODUCTOSITEMS objeto = new GE_TPRODUCTOSITEMS();

                objeto.prit_consecutivo = Convert.ToInt32(productoSeleccionado["prit_consecutivo"].ToString());
                objeto.prit_tipo = productoSeleccionado["prit_tipo"].ToString();

                GE_TPRODUCTOS producto = new GE_TPRODUCTOS();
                producto.prod_consecutivo = Convert.ToInt32(productoSeleccionado["GE_TPRODUCTOS.prod_consecutivo"].ToString());
                objeto.GE_TPRODUCTOS = producto;
                
                objeto.prit_activo = Convert.ToInt32(productoSeleccionado["prit_activo"].ToString());

                GE_TCUENTAS cuenta = new GE_TCUENTAS();
                cuenta.cuen_consecutivo = Convert.ToInt32(productoSeleccionado["GE_TCUENTAS.cuen_consecutivo"].ToString());
                objeto.GE_TCUENTAS = cuenta;
                objeto.prit_item = productoSeleccionado["prit_item"].ToString();
                
                Session["objeto"] = objeto;
                Response.Redirect("frmItems_form.aspx");
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se puede consultar el registro", ex);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmProductos.aspx");
        }
        
        #endregion
    }
}