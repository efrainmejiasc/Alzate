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
    public partial class frmGastosViaje : System.Web.UI.Page
    {
        CtrCentroCosto Ccosto;
        CtrProductos Cproducto;
        CtrProductosItems Cproditems;
        CtrParametros Cparametros;
        CtrPeriodoPresupuesto Cperiodo;
        CtrPeriodoTransaccion CperiodoTr;
        CtrPersonas Cpersona;
        CtrVarEconomicas CvarEcon;
        CtrVlrsParamGrales Cparamgrales;
        CtrGastosViaje Cgastosviaje;
        CtrPeriodoTransaccPersonas CperiodoTrPr;
        CtrUtilidades Cutilidades;
        string strSubCateg = "GASTOS DE AREA";
        Hashtable registroSeleccionado = null;
        string[] camposRegistro = new string[] { "petr_consecutivo", "GE_TCENTROSCOSTOS.cost_consecutivo", "GE_TPRODUCTOSITEMS.GE_TPRODUCTOS.prod_consecutivo", "GE_TPRODUCTOSITEMS.prit_consecutivo", "petr_mes",
                                                "petr_activo", "petr_moneda", "petr_cantidad", "petr_observacion", "petr_valor", "petr_amortizar", "petr_meses_amortizar", "petr_trm", "petr_tipo_viaje" };
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

                Ccosto = new CtrCentroCosto();
                Cproducto = new CtrProductos();
                Cproditems = new CtrProductosItems();
                Cparametros = new CtrParametros();
                Cperiodo = new CtrPeriodoPresupuesto();
                CperiodoTr = new CtrPeriodoTransaccion();
                Cpersona = new CtrPersonas();
                CvarEcon = new CtrVarEconomicas();
                Cparamgrales = new CtrVlrsParamGrales();
                Cgastosviaje = new CtrGastosViaje();
                CperiodoTrPr = new CtrPeriodoTransaccPersonas();
                Cutilidades = new CtrUtilidades();

                gvPrItems.DataSource = CperiodoTr.GetAllGridViewViaje(strUsuario[0].ToString().ToUpper(), strSubCateg, "VIAJE");
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
            Response.Redirect("frmGastosViaje_form.aspx");
        }

        protected void grid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                if (e.ButtonID != "btnConsultar" && e.ButtonID != "btnEliminar") return;

                registroSeleccionado = new Hashtable();
                foreach (string campo in camposRegistro)
                {
                    registroSeleccionado[campo] = gvPrItems.GetRowValues(e.VisibleIndex, campo);
                }

                GE_TPERIODOTRANSACCIONES objeto = new GE_TPERIODOTRANSACCIONES();

                objeto.petr_consecutivo = Convert.ToInt32(registroSeleccionado["petr_consecutivo"].ToString());
                objeto.petr_activo = Convert.ToInt32(registroSeleccionado["petr_activo"].ToString());
                objeto.petr_cantidad = Convert.ToInt32(registroSeleccionado["petr_cantidad"].ToString());

                GE_TCENTROSCOSTOS centro = new GE_TCENTROSCOSTOS();
                centro.cost_consecutivo = Convert.ToInt32(registroSeleccionado["GE_TCENTROSCOSTOS.cost_consecutivo"].ToString());                
                objeto.GE_TCENTROSCOSTOS = centro;
                
                objeto.petr_mes = Convert.ToInt32(registroSeleccionado["petr_mes"].ToString());
                objeto.petr_observacion = registroSeleccionado["petr_observacion"].ToString();
                objeto.petr_valor = Convert.ToDecimal(registroSeleccionado["petr_valor"].ToString());
                objeto.petr_trm = Convert.ToDecimal(registroSeleccionado["petr_trm"].ToString());
                objeto.petr_moneda = Convert.ToInt32(registroSeleccionado["petr_moneda"].ToString());
                objeto.petr_tipo_viaje = Convert.ToInt32(registroSeleccionado["petr_tipo_viaje"].ToString());

                Session["objeto"] = objeto;
                Response.Redirect("frmGastosViaje_form.aspx");
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede consultar el registro." + ex.Message);
            }
        }
        #endregion
    }
}