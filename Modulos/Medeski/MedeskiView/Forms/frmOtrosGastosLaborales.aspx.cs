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
    public partial class frmOtrosGastosLaborales : System.Web.UI.Page
    {
        CtrCentroCosto Ccosto;
        CtrProductos Cproducto;
        CtrProductosItems Cproditems;
        CtrParametros Cparametros;
        CtrPeriodoPresupuesto Cperiodo;
        CtrPeriodoTransaccion CperiodoTr;
        CtrPersonas Cpersona;
        CtrUtilidades Cutilidades;
        CtrVarEconomicas CvarEcon;
        string strSubCateg = "GASTOS DE AREA";
        Hashtable registroSeleccionado = null;
        string[] camposRegistro = new string[] { "petr_consecutivo", "GE_TCENTROSCOSTOS.cost_consecutivo", "GE_TPRODUCTOSITEMS.GE_TPRODUCTOS.prod_consecutivo", "GE_TPRODUCTOSITEMS.prit_consecutivo", "petr_mes",
                                                "petr_activo", "petr_moneda", "petr_cantidad", "petr_observacion", "petr_valor" };
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                CargarDatos();
                CargarListas();
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
                Cutilidades = new CtrUtilidades();
                CvarEcon = new CtrVarEconomicas();
                if (!txtId.Contains("txtId")) txtId["txtId"] = "";
                gvPrItems.DataSource = CperiodoTr.GetAllGridViewViaje(strUsuario[0].ToString().ToUpper(), strSubCateg, "OTROS");
                gvPrItems.DataBind();
                Cutilidades.ConfigurarGrid(gvPrItems);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se pueden cargar los registros. ", ex);
            }
        }

        public void CargarListas()
        {
            try
            {
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                /*Cargar CentroCosto*/
                cmbCCosto.Items.Clear();
                cmbCCosto.Items.Add("Seleccionar Centro Costo", null);
                IEnumerable<GE_TCENTROSCOSTOS> cc = Ccosto.GetAllUsuarioxCuenta(strUsuario[0].ToString().ToUpper(), strSubCateg);
                foreach (GE_TCENTROSCOSTOS c in cc)
                {
                    cmbCCosto.Items.Add(c.cost_descripcion, c.cost_consecutivo);
                }

                cmbActivo.Items.Clear();
                cmbActivo.Items.Add("Activo?", null);
                cmbActivo.Items.Add("SI", 1);
                cmbActivo.Items.Add("NO", 0);

                /*cargar Monedas*/
                cmbMoneda.Items.Clear();
                cmbMoneda.Items.Add("Seleccionar Moneda", null);
                IList<GE_TPARAMETROS> par = Cparametros.GetListbyClase("MONEDA");
                foreach (GE_TPARAMETROS pr in par)
                {
                    cmbMoneda.Items.Add(pr.parm_descripcion, pr.parm_consecutivo);
                }

                /*cargar Meses*/
                cmbMes.Items.Clear();
                cmbMes.Items.Add("Seleccionar Mes", null);
                IEnumerable<GE_TPARAMETROS> p = Cparametros.GetListbyClaseOrdenada("MESES");
                foreach (GE_TPARAMETROS pr in p)
                {
                    cmbMes.Items.Add(pr.parm_descripcion, pr.parm_consecutivo);
                }

                /*cargar cantidad*/
                cmbCantidad.Items.Clear();
                cmbCantidad.Items.Add("Seleccionar Cantidad", null);

                for (int i = 1; i <= 20; i++)
                {
                    cmbCantidad.Items.Add(i.ToString(), i);
                }

                if (cmbProducto.Items.Count == 0)
                    cmbProducto.Items.Add("Seleccionar Producto", null);

                if (cmbItem.Items.Count == 0)
                    cmbItem.Items.Add("Seleccionar Item", null);

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se pueden cargar los combos. ", ex);
            }
        }

        public void FillProducto(int inCeco)
        {
            Char delimiter = ';';
            string[] strUsuario = null;
            strUsuario = Session["usuario"].ToString().Split(delimiter);
            IEnumerable<GE_TPRODUCTOS> pr = Cproducto.GetAllUsuarioxCuenta(strUsuario[0].ToString(), strSubCateg, inCeco);
            cmbProducto.Items.Clear();
            cmbProducto.Items.Add("Seleccionar Producto", null);
            foreach (GE_TPRODUCTOS p in pr)
            {
                cmbProducto.Items.Add(p.prod_descripcion, p.prod_consecutivo);
            }
            cmbProducto.Value = null;
        }

        public void FillItem(int inProd)
        {
            Char delimiter = ';';
            string[] strUsuario = null;
            strUsuario = Session["usuario"].ToString().Split(delimiter);
            IEnumerable<GE_TPRODUCTOSITEMS> pr = Cproditems.GetAllUsuarioxCuenta(strUsuario[0].ToString(), strSubCateg, Convert.ToInt32(cmbCCosto.Value.ToString()), inProd);
            cmbItem.Items.Clear();
            cmbItem.Items.Add("Seleccionar Item", null);
            foreach (GE_TPRODUCTOSITEMS p in pr)
            {
                cmbItem.Items.Add(p.prit_item, p.prit_consecutivo);
            }

            cmbItem.Value = null;
        }

        private void Limpiar()
        {
            cmbActivo.Value = null;
            cmbCantidad.Value = 1;
            cmbCCosto.Value = null;
            cmbItem.Value = null;
            cmbMes.Value = null;
            cmbMoneda.Value = null;
            cmbProducto.Value = null;
            txtSvalor.Text = "";
            txtId["txtId"] = "";
        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarTxtMonedaObligatorio("Valor", txtSvalor.Text, 50);
                VentanaValidaciones.validarTxtNumericoObligatorio("Centro Costo", cmbCCosto.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Producto", cmbProducto.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Item", cmbItem.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Moneda", cmbMoneda.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Mes", cmbMes.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Cantidad", cmbCantidad.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Activo", cmbActivo.Value, 6);
                VentanaValidaciones.validarTxtObligatorio("Observación", txtObservacion.Text, 500);

            }
            catch
            {
                return false;
            }
            return true;
        }

        private decimal ValidarTrm(int inMes, int inMoneda, int inAno)
        {
            decimal vlr = 1;

            try
            {
                GE_TVARECONOMICAS var = CvarEcon.GetByAnoMes(inMes, inMoneda, inAno);

                if (var != null)
                    vlr = var.vari_valor;
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se puede leer el registro", ex);
            }

            return vlr;
        }

        #endregion

        #region Eventos
        protected void NuevoClicked(object sender, EventArgs e)
        {
            Limpiar();
            CargarListas();
        }
        protected void GuardarClicked(object sender, EventArgs e)
        {
            try
            {
                if (!validar())
                {
                    return;
                }

                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);
                DateTime dtFecha = DateTime.Now;
                int inPeriodo = 0;
                IList<GE_TPERIODOPRESUPUESTO> per = Cperiodo.GetAllActive();

                if (per.Count > 0)
                    inPeriodo = per[0].peri_consecutivo;

                GE_TPERSONAS p = Cpersona.GetbyUsuario(strUsuario[0].ToString().ToUpper());

                GE_TPERIODOTRANSACCIONES pt = new GE_TPERIODOTRANSACCIONES();
                pt.petr_activo = Convert.ToInt32(cmbActivo.Value.ToString());
                pt.petr_amortizar = 0;
                pt.petr_cantidad = Convert.ToInt32(cmbCantidad.Value.ToString());
                pt.petr_centrocosto = Convert.ToInt32(cmbCCosto.Value.ToString());
                pt.petr_fecha = dtFecha;
                pt.petr_fecha_act = dtFecha;
                pt.petr_mes = Convert.ToInt32(cmbMes.Value.ToString());
                pt.petr_moneda = Convert.ToInt32(cmbMoneda.Value.ToString());
                pt.petr_observacion = txtObservacion.Text;
                pt.petr_periodo = inPeriodo;
                pt.petr_persona = p.pers_consecutivo;
                pt.petr_producto_item = Convert.ToInt32(cmbItem.Value.ToString());
                pt.petr_proveedor = 1;
                pt.petr_tipo_viaje = null;
                pt.petr_trm = ValidarTrm(Convert.ToInt32(cmbMes.Value.ToString()), Convert.ToInt32(cmbMoneda.Value.ToString()), per[0].peri_ano);
                pt.petr_valor = Convert.ToDecimal(txtSvalor.Text);
                pt.petr_valor_amortizar = 0;
                pt.petr_usuario = strUsuario[0].ToString();
                pt.petr_usuario_act = strUsuario[0].ToString();
                pt.petr_meses_amortizar = null;
                pt.petr_tipo = "OTROS";

                if (!String.IsNullOrEmpty(txtId["txtId"].ToString()))
                {
                    pt.petr_consecutivo = Convert.ToInt32(txtId["txtId"].ToString());
                    CperiodoTr.Update(pt);

                }
                else
                {
                    CperiodoTr.Add(pt);
                }
                
                CperiodoTr.LoadTransactions(pt.petr_consecutivo);
                Limpiar();
                CargarDatos();
                CargarListas();
                VentanaValidaciones.mostrarRegistroExitoso();

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se puede guardar el registro", ex);
            }

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

                txtId["txtId"] = registroSeleccionado["petr_consecutivo"].ToString();
                cmbActivo.Value = registroSeleccionado["petr_activo"].ToString();
                cmbCantidad.Value = registroSeleccionado["petr_cantidad"].ToString();
                cmbCCosto.Value = registroSeleccionado["GE_TCENTROSCOSTOS.cost_consecutivo"].ToString();
                FillProducto(Convert.ToInt32(cmbCCosto.Value));
                cmbProducto.Value = registroSeleccionado["GE_TPRODUCTOSITEMS.GE_TPRODUCTOS.prod_consecutivo"].ToString();
                FillItem(Convert.ToInt32(cmbProducto.Value));
                cmbItem.Value = registroSeleccionado["GE_TPRODUCTOSITEMS.prit_consecutivo"].ToString();
                cmbMes.Value = registroSeleccionado["petr_mes"].ToString();
                cmbMoneda.Value = registroSeleccionado["petr_moneda"].ToString();
                txtObservacion.Text = registroSeleccionado["petr_observacion"].ToString();
                txtSvalor.Text = registroSeleccionado["petr_valor"].ToString();
                Session["opModificar"] = "1";
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se puede consultar el registro", ex);
            }
        }


        protected void cmbCCosto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCCosto.Value != null)
                FillProducto(Convert.ToInt32(cmbCCosto.Value));


            cmbItem.Value = null;
        }

        protected void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.Value != null)
                FillItem(Convert.ToInt32(cmbProducto.Value));
        }


        #endregion
    }

}