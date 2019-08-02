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
    public partial class frmGastosViaje_form : System.Web.UI.Page
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

                CargarListas();

                if (!IsPostBack)
                {
                    if (Session["objeto"] != null)
                    {
                        GE_TPERIODOTRANSACCIONES mObjeto = Session["objeto"] as GE_TPERIODOTRANSACCIONES;

                        txtId["txtId"] = mObjeto.petr_consecutivo.ToString();
                        cmbActivo.Value = mObjeto.petr_activo.ToString();
                        cmbCantidad.Value = mObjeto.petr_cantidad.ToString();
                        cmbCCosto.Value = mObjeto.GE_TCENTROSCOSTOS.cost_consecutivo.ToString();
                        cmbMes.Value = mObjeto.petr_mes.ToString();
                        txtObservacion.Text = mObjeto.petr_observacion.ToString();
                        txtSvalor.Text = mObjeto.petr_valor.ToString();
                        cmbTipoViaje.Text = (Convert.ToDecimal(mObjeto.petr_trm.ToString()) == 1) ? "NACIONAL" : "EXTERIOR";
                        CalcularCambioTipoViaje();
                        cmbMoneda.Value = mObjeto.petr_moneda.ToString();
                        cmbDestino.Value = mObjeto.petr_tipo_viaje.ToString();

                        IList<GE_TPERIODOTRANSACCPERSONAS> pers = CperiodoTrPr.GetByIdPeriodoTransacc(Convert.ToInt32(mObjeto.petr_consecutivo.ToString()));

                        foreach (ListEditItem item in cbLPersonas.Items)
                        {
                            foreach (GE_TPERIODOTRANSACCPERSONAS p in pers)
                            {
                                if (Convert.ToInt32(item.Value.ToString()) == p.ptrp_persona)
                                    item.Selected = true;
                            }
                        }
                    }
                }
            }
            else
                Response.Redirect(strUrl);
        }

        #region Metodos

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

                if (cmbCCosto.Items.Count == 2)
                {
                    cmbCCosto.Items[1].Selected = true;
                }


                cmbActivo.Items.Clear();
                cmbActivo.Items.Add("Activo?", null);
                cmbActivo.Items.Add("SI", 1);
                cmbActivo.Items.Add("NO", 0);

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

                cbLPersonas.DataSource = null;
                cbLPersonas.DataSource = Cpersona.GetAllActivexArea(strUsuario[0]);
                cbLPersonas.DataBind();

                /*cargar Tipo Viaje*/
                cmbTipoViaje.Items.Clear();
                cmbTipoViaje.Items.Add("Seleccionar Tipo Viaje", null);
                IEnumerable<GE_TPARAMETROS> pv = Cparametros.GetListbyClaseOrdenada("TIPO_VIAJE");
                foreach (GE_TPARAMETROS pr in pv)
                {
                    cmbTipoViaje.Items.Add(pr.parm_descripcion, pr.parm_consecutivo);
                }

                if (cmbTipoViaje.Items.Count == 2)
                {
                    cmbTipoViaje.Items[1].Selected = true;
                    CalcularCambioTipoViaje();
                }

                if (cmbDestino.Items.Count == 0)
                    cmbDestino.Items.Add("Seleccionar Destino", null);

                if (cmbMoneda.Items.Count == 0)
                    cmbMoneda.Items.Add("Seleccionar Moneda", null);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los datos de los combos. " + ex.Message);
            }
        }

        private void Limpiar()
        {
            cmbMoneda.Items.Clear();
            cmbTipoViaje.Items.Clear();
            cmbActivo.Items.Clear();
            cmbCantidad.Items.Clear();
            cmbCCosto.Items.Clear();
            cmbDestino.Items.Clear();
            cmbMes.Items.Clear();
            cmbMoneda.Items.Clear();
            cmbTipoViaje.Items.Clear();
            txtSvalor.Text = "";
            txtObservacion.Text = "";
            cbLPersonas.UnselectAll();

            CargarListas();

            cmbActivo.Value = null;
            cmbTipoViaje.Value = null;
            cmbCantidad.Value = null;
            cmbCCosto.Value = null;
            cmbDestino.Value = null;
            cmbMes.Value = null;
            cmbMoneda.Value = null;
            cmbTipoViaje.Value = null;
            txtId["txtId"] = "";
        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarTxtMonedaObligatorio("Valor", txtSvalor.Text, 50);
                VentanaValidaciones.validarTxtNumericoObligatorio("Centro Costo", cmbCCosto.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Moneda", cmbMoneda.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Mes", cmbMes.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Cantidad", cmbCantidad.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Activo", cmbActivo.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Tipo Viaje", cmbTipoViaje.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Destino", cmbDestino.Value, 6);
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
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede leer el registro." + ex.Message);
            }

            return vlr;
        }

        public void IngresarPersonas(GE_TPERIODOTRANSACCIONES pt)
        {
            try
            {
                decimal deSalario = 0;
                CperiodoTrPr.Delete(pt.petr_consecutivo);

                if (Session["codMoneda"].ToString() == "COP")
                    deSalario = Convert.ToDecimal(Cparamgrales.GetByClase("SMMLV").vhpg_valor);

                 foreach (ListEditItem item in cbLPersonas.SelectedItems)
                 {
                     GE_TPERSONAS pe = Cpersona.GetbyConsecutivo(Convert.ToInt32(item.Value.ToString()));
                     GE_TCALCULOGASTOSVIAJE gastos = Cgastosviaje.GetGrupoDestino(pe.pers_grupo, pt.petr_tipo_viaje.Value);
                     GE_TPERIODOTRANSACCPERSONAS ptp = new GE_TPERIODOTRANSACCPERSONAS();
                     ptp.ptrp_cantidad_dias = pt.petr_cantidad;
                     ptp.ptrp_fecha = pt.petr_fecha;
                     ptp.ptrp_usuario = pt.petr_usuario;
                     ptp.ptrp_mes = pt.petr_mes;
                     ptp.ptrp_periodo_transacc = pt.petr_consecutivo;
                     ptp.ptrp_persona = Convert.ToInt32(item.Value.ToString());
                     ptp.ptrp_smmlv = (pt.petr_trm == 1) ? deSalario : pt.petr_trm;
                     ptp.ptrp_tarifa_alimentacion = gastos.tari_alimentacion;
                     ptp.ptrp_tarifa_hotel = gastos.tari_hotel;
                     CperiodoTrPr.Add(ptp);
                 }
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede realizar la acción." + ex.Message);
            }

        }

        public void CalcularCambioTipoViaje()
        {
            try
            {
                string strClase = "";
                string strItemPers = "";
                string strItemReg = "";
                string strCod = "";

                if (cmbTipoViaje.Value != null)
                {
                    if (cmbTipoViaje.Text.ToUpper().Equals("NACIONAL"))
                    {
                        strClase = "VIAJE_NAL";
                        strItemReg = Cparamgrales.GetByClase("ITEM_PASAJE_NAL").vhpg_valor;
                        strItemPers = Cparamgrales.GetByClase("ITEM_ALOJAMIENTO_NAL").vhpg_valor;
                        strCod = "COP";
                    }
                    else
                    {
                        if (cmbTipoViaje.Text.ToUpper().Equals("EXTERIOR"))
                        {
                            strClase = "VIAJE_EXT";
                            strItemReg = Cparamgrales.GetByClase("ITEM_PASAJE_EXT").vhpg_valor;
                            strItemPers = Cparamgrales.GetByClase("ITEM_ALOJAMIENTO_EXT").vhpg_valor;
                            strCod = "USD";
                        }
                    }

                    cmbDestino.Items.Clear();
                    cmbDestino.Items.Add("Seleccionar Destino", null);
                    IList<GE_TPARAMETROS> par = Cparametros.GetListbyClase(strClase);
                    foreach (GE_TPARAMETROS pr in par)
                    {
                        cmbDestino.Items.Add(pr.parm_descripcion, pr.parm_consecutivo);
                    }

                    cmbMoneda.Items.Clear();
                    cmbMoneda.Items.Add("Seleccionar Moneda", null);
                    IEnumerable<GE_TPARAMETROS> parm = Cparametros.GetByClaseCodigo("MONEDA", strCod);
                    foreach (GE_TPARAMETROS pr in parm)
                    {
                        cmbMoneda.Items.Add(pr.parm_descripcion, pr.parm_consecutivo);
                    }


                    GE_TPRODUCTOSITEMS pReg = Cproditems.GetItemxNombre(strItemReg);
                    GE_TPRODUCTOSITEMS pPer = Cproditems.GetItemxNombre(strItemPers);
                    Session["inItemReg"] = pReg.prit_consecutivo;
                    Session["inItemPers"] = pPer.prit_consecutivo;
                    Session["claseReg"] = strClase;
                    Session["codMoneda"] = strCod;
                }
                else
                {
                    cmbDestino.Items.Clear();
                    cmbDestino.Items.Add("Seleccionar Destino", null);
                    cmbMoneda.Items.Clear();
                    cmbMoneda.Items.Add("Seleccionar Moneda", null);
                }

                cmbDestino.Value = null;
                cmbMoneda.Value = null;
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede realizar la acción." + ex.Message);
            }
        }

        #endregion

        #region Eventos

        protected void RegresarClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmGastosViaje.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void GuardarClicked(object sender, EventArgs e)
        {
            try
            {
                if (!validar())
                {
                    return;
                }

                if (cbLPersonas.SelectedItems.Count == 0)
                {
                    VentanaValidaciones.mostrarError("Se debe seleccionar una persona para el viaje");
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
                pt.petr_producto_item = Convert.ToInt32(Session["inItemReg"].ToString());
                pt.petr_proveedor = 1;
                pt.petr_tipo_viaje = Convert.ToInt32(cmbDestino.Value.ToString());
                pt.petr_trm = ValidarTrm(Convert.ToInt32(cmbMes.Value.ToString()), Convert.ToInt32(cmbMoneda.Value.ToString()), per[0].peri_ano);
                pt.petr_valor = Convert.ToDecimal(txtSvalor.Text);
                pt.petr_valor_amortizar = 0;
                pt.petr_usuario = strUsuario[0].ToString();
                pt.petr_usuario_act = strUsuario[0].ToString();
                pt.petr_meses_amortizar = null;
                pt.petr_tipo = "VIAJE";

                if (txtId.Contains("txtId") && !String.IsNullOrEmpty(txtId["txtId"].ToString()))
                {
                    pt.petr_consecutivo = Convert.ToInt32(txtId["txtId"].ToString());
                    CperiodoTr.Update(pt);
                }
                else
                {
                    CperiodoTr.Add(pt);
                }

                CperiodoTr.LoadTransactions(pt.petr_consecutivo);
                IngresarPersonas(pt);
                CperiodoTr.LoadTransactionsPersons(pt.petr_consecutivo);

                Session["mensaje"] = "OK";
                Response.Redirect("frmGastosViaje.aspx", false);

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede guardar el registro." + ex.Message);
            }

        }

        protected void cmbTipoViaje_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcularCambioTipoViaje();
        }

        #endregion
    }
}