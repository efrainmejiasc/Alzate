using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using MedeskiView.Controllers;
using Medeski.BusinessLogic.Class;
using System.Collections.Specialized;

namespace MedeskiView.Forms
{
    public partial class frmCuentasEspeciales : System.Web.UI.Page
    {
        CtrCentroCosto Ccosto;
        CtrProductos Cproducto;
        CtrProductosItems Cproditems;
        CtrParametros Cparametros;
        CtrPeriodoPresupuesto Cperiodo;
        CtrPeriodoTransaccion CperiodoTr;
        CtrSalidaPresupuesto CsalidaP = new CtrSalidaPresupuesto();
        CtrPersonas Cpersona;
        CtrCuentas Ccuenta;
        CtrVarEconomicas CvarEcon;
        CtrUtilidades Cutilidades;

        IEnumerable<GE_TPARAMETROS> pMeses;

        string strSubCateg = "CUENTAS ESPECIALES";
        Hashtable registroSeleccionado = null;
        string[] camposRegistro = new string[] { "petr_consecutivo", "GE_TCENTROSCOSTOS.GE_TCOMPANIAS.comp_nombre", "GE_TCENTROSCOSTOS.cost_empresa", "GE_TCENTROSCOSTOS.cost_consecutivo", "GE_TPRODUCTOSITEMS.GE_TPRODUCTOS.prod_consecutivo", "GE_TPRODUCTOSITEMS.prit_consecutivo",
                                                "petr_activo", "petr_moneda", "petr_cantidad", "petr_observacion", "petr_valor", "petr_amortizar" };
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

                CargarListas();
                CargarDatos();
            }
            else
                Response.Redirect(strUrl);
            if (!IsPostBack)
                cmbSubcategoria.SelectedIndex = 0;
        }

        #region Metodos
        public void CargarListas()
        {
            try
            {
                cmbSubcategoria.Items.Clear();
                cmbSubcategoria.Items.Add("Cuentas Especiales", "CE");
                cmbSubcategoria.Items.Add("Gastos de Área", "GA"); 
                cmbSubcategoria.Items.Add("Otros Gastos de Área", "OT");
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarError("No se puede cargar las listas. " + ex.Message);
            }
        }

        public void CargarDatos()
        {
            try
            {
                Ccosto = new CtrCentroCosto();
                Cproducto = new CtrProductos();
                Cproditems = new CtrProductosItems();
                Cparametros = new CtrParametros();
                Cperiodo = new CtrPeriodoPresupuesto();
                CperiodoTr = new CtrPeriodoTransaccion();
                Cpersona = new CtrPersonas();
                Ccuenta = new CtrCuentas();
                CvarEcon = new CtrVarEconomicas();
                Cutilidades = new CtrUtilidades();

                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                if (cmbSubcategoria.Value == null)
                {
                    gvPrItems.DataSource = CperiodoTr.GetAllGridView(strUsuario[0].ToString().ToUpper(), "CE");
                }
                else
                {
                    gvPrItems.DataSource = CperiodoTr.GetAllGridView(strUsuario[0].ToString().ToUpper(), cmbSubcategoria.Value.ToString());
                }

                
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
            try
            {
                VentanaValidaciones.validarComboObligatorio("Subcategoría", cmbSubcategoria.Value);

                if (cmbSubcategoria.Value == null)
                {
                    Session["mSubtipo"] = "CE";
                }
                else
                {
                    Session["mSubtipo"] = cmbSubcategoria.Value.ToString();
                }

                Session["objeto"] = null;
                Response.Redirect("frmCuentasEspeciales_form.aspx");
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarError(ex.Message);
            }            
        }

        protected void grid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                registroSeleccionado = new Hashtable();
                foreach (string campo in camposRegistro)
                {
                    registroSeleccionado[campo] = gvPrItems.GetRowValues(e.VisibleIndex, campo);
                }

                GE_TPERIODOTRANSACCIONES objeto = new GE_TPERIODOTRANSACCIONES();

                objeto.petr_consecutivo = Convert.ToInt32(registroSeleccionado["petr_consecutivo"].ToString());
                objeto.petr_activo = Convert.ToInt32(registroSeleccionado["petr_activo"].ToString());
                objeto.petr_cantidad = Convert.ToInt32(registroSeleccionado["petr_cantidad"].ToString());

                GE_TCENTROSCOSTOS ccosto = new GE_TCENTROSCOSTOS();
                ccosto.cost_consecutivo = Convert.ToInt32(registroSeleccionado["GE_TCENTROSCOSTOS.cost_consecutivo"].ToString());
                ccosto.cost_empresa = Convert.ToInt32(registroSeleccionado["GE_TCENTROSCOSTOS.cost_empresa"].ToString());  
                objeto.GE_TCENTROSCOSTOS = ccosto;

                GE_TPRODUCTOSITEMS pritem = new GE_TPRODUCTOSITEMS();
                pritem.prit_consecutivo = Convert.ToInt32(registroSeleccionado["GE_TPRODUCTOSITEMS.prit_consecutivo"].ToString());

                GE_TPRODUCTOS prod = new GE_TPRODUCTOS();
                prod.prod_consecutivo = Convert.ToInt32(registroSeleccionado["GE_TPRODUCTOSITEMS.GE_TPRODUCTOS.prod_consecutivo"].ToString());
                pritem.GE_TPRODUCTOS = prod;

                objeto.GE_TPRODUCTOSITEMS = pritem;
                
                objeto.petr_moneda = Convert.ToInt32(registroSeleccionado["petr_moneda"].ToString());
                objeto.petr_observacion = registroSeleccionado["petr_observacion"].ToString();
                objeto.petr_valor = Convert.ToDecimal(registroSeleccionado["petr_valor"].ToString());

                if (cmbSubcategoria.Value == null)
                {
                    Session["mSubtipo"] = "CE";
                }
                else
                {
                    Session["mSubtipo"] = cmbSubcategoria.Value.ToString();
                }
                Session["objeto"] = objeto;
                Response.Redirect("frmCuentasEspeciales_form.aspx");                
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede consultar el registro." + ex.Message);
            }
        }

        
        protected void cmbSubcategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatos();
        }
        #endregion
    }
}