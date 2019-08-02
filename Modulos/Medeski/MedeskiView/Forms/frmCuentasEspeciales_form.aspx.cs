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
    public partial class frmCuentasEspeciales_form : System.Web.UI.Page
    {
        CtrCentroCosto Ccosto = new CtrCentroCosto();
        CtrProductos Cproducto = new CtrProductos();
        CtrProductosItems Cproditems = new CtrProductosItems();
        CtrParametros Cparametros = new CtrParametros();
        CtrPeriodoPresupuesto Cperiodo = new CtrPeriodoPresupuesto();
        CtrPeriodoTransaccion CperiodoTr = new CtrPeriodoTransaccion();
        CtrSalidaPresupuesto CsalidaP = new CtrSalidaPresupuesto();
        CtrPersonas Cpersona = new CtrPersonas();
        CtrCuentas Ccuenta = new CtrCuentas();
        CtrVarEconomicas CvarEcon = new CtrVarEconomicas();
        CtrUtilidades Cutilidades = new CtrUtilidades();

        IList<GE_TCENTROSCOSTOS> lstUserCcostos;
        IList<GE_TCOMPANIAS> lstCompanias;
        IEnumerable<GE_TPARAMETROS> pMeses;

        string strSubCateg = "CUENTAS ESPECIALES";
        Hashtable registroSeleccionado = null;
        string[] camposRegistro = new string[] { "petr_consecutivo", "GE_TCENTROSCOSTOS.cost_consecutivo", "GE_TPRODUCTOSITEMS.GE_TPRODUCTOS.prod_consecutivo", "GE_TPRODUCTOSITEMS.prit_consecutivo",
                                                "petr_activo", "petr_moneda", "petr_cantidad", "petr_observacion", "petr_valor", "petr_amortizar" };
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                Cparametros = new CtrParametros();
                pMeses = Cparametros.GetListbyClaseOrdenada("MESES").OrderBy(x => Convert.ToInt32(x.parm_codigo)).ToList();

                CargarDatos();
                CargarListas();

                if (!IsPostBack)
                {
                    cmbCCosto.Items.Add("Seleccionar Centro Costo", null);
                    cmbCCosto.SelectedIndex = 0;

                    if (Session["objeto"] != null)
                    {
                        GE_TPERIODOTRANSACCIONES mObjeto = Session["objeto"] as GE_TPERIODOTRANSACCIONES;

                        txtId["txtId"] = mObjeto.petr_consecutivo.ToString();

                        if (mObjeto.petr_activo != null)
                        {
                            ListEditItem liEstado = cmbActivo.Items.FindByValue(mObjeto.petr_activo.ToString());
                            liEstado.Selected = true;
                        } 
                        
                        txtCantidad.Value = mObjeto.petr_cantidad.ToString();

                        if (mObjeto.GE_TCENTROSCOSTOS.cost_empresa != null)
                        {
                            ListEditItem liCompania = cmbEmpresa.Items.FindByValue(mObjeto.GE_TCENTROSCOSTOS.cost_empresa.ToString());
                            if (liCompania != null)
                            {
                                liCompania.Selected = true;
                            }
                        }
                        
                        if (mObjeto.GE_TCENTROSCOSTOS.cost_consecutivo != null)
                        {
                            FillCCossto(Convert.ToInt32(mObjeto.GE_TCENTROSCOSTOS.cost_empresa));
                            ListEditItem liCentroCostos = cmbCCosto.Items.FindByValue(mObjeto.GE_TCENTROSCOSTOS.cost_consecutivo.ToString());
                            if (liCentroCostos != null)
                            {
                                liCentroCostos.Selected = true;
                                FillProducto(mObjeto.GE_TCENTROSCOSTOS.cost_consecutivo);
                            }
                                
                        }

                        if (mObjeto.GE_TPRODUCTOSITEMS.GE_TPRODUCTOS.prod_consecutivo != null)
                        {
                            ListEditItem liProductos = cmbProducto.Items.FindByValue(mObjeto.GE_TPRODUCTOSITEMS.GE_TPRODUCTOS.prod_consecutivo.ToString());
                            if (liProductos != null)
                            {
                                liProductos.Selected = true;

                                FillItem(Convert.ToInt32(cmbProducto.Value));
                                if (mObjeto.GE_TPRODUCTOSITEMS.prit_consecutivo != null)
                                {
                                    ListEditItem liItems = cmbItem.Items.FindByValue(mObjeto.GE_TPRODUCTOSITEMS.prit_consecutivo.ToString());
                                    if (liItems != null)
                                        liItems.Selected = true;
                                }
                            }                        
                        }
                        // cmbMes.Value = registroSeleccionado["petr_mes"].ToString();
                        if (mObjeto.petr_moneda != null)
                        {
                            ListEditItem liMonedas = cmbMoneda.Items.FindByValue(mObjeto.petr_moneda.ToString());
                            if (liMonedas != null)
                            {
                                liMonedas.Selected = true;
                            }
                                
                        }
                        
                        txtObservacion.Text = mObjeto.petr_observacion.ToString();
                        txtSvalor.Text = mObjeto.petr_valor.ToString();
                        
                        int moneda = Convert.ToInt32(cmbMoneda.Value.ToString());
                        // txtSvalor.DisplayFormatString = Cparametros.GetByConsecutivo(moneda).parm_infoadicional + "0";

                        CambiarMesItem();
                        // cmbMesesAmortizar.Value = (registroSeleccionado["petr_meses_amortizar"] != null) ? registroSeleccionado["petr_meses_amortizar"].ToString() : null;
                        Session["opModificar"] = "1";

                        IList<GE_TSALIDAPRESUPUESTO> salida = CsalidaP.GetByPeriodoTransacc(Convert.ToInt32(txtId["txtId"]));

                        List<DTOMeses> res = new List<DTOMeses>();
                        Cparametros = new CtrParametros();
                        IList<GE_TPARAMETROS> meses = Cparametros.GetListbyClase("MESES").OrderBy(x => Convert.ToInt32(x.parm_codigo)).ToList();

                        decimal sumaMeses = 0;

                        GE_TPARAMETROS param = Cparametros.GetByConsecutivo(mObjeto.petr_moneda);
                        decimal trmA = 1;
                        decimal trmB = 1;

                        int inPeriodo = Cperiodo.GetPeriodoActivo().peri_ano;
                
                        for (var i = 0; i < meses.Count() / 2; i++)
                        {
                            decimal valorA = 0;

                            if (!param.parm_codigo.Equals("COP"))
                            {
                                grid_meses.Columns[3].Visible = true;
                                grid_meses.Columns[7].Visible = true;

                                grid_meses.Columns[5].Visible = true;
                                grid_meses.Columns[9].Visible = true;
                            }
                            else
                            {
                                grid_meses.Columns[3].Visible = false;
                                grid_meses.Columns[7].Visible = false;

                                grid_meses.Columns[5].Visible = false;
                                grid_meses.Columns[9].Visible = false;
                            }

                        
                            trmA = ValidarTrm(meses.ElementAt(i).parm_consecutivo, Convert.ToInt32(cmbMoneda.Value.ToString()), inPeriodo);                                                        
                            var inSalidaA = salida.Any(x => x.sali_mes == meses.ElementAt(i).parm_consecutivo);
                            if (inSalidaA)
                            {
                                GE_TSALIDAPRESUPUESTO presup = salida.Where(x => x.sali_mes == meses.ElementAt(i).parm_consecutivo).First();
                                valorA = Convert.ToDecimal(presup.sali_valor);
                            }


                            trmB = ValidarTrm(meses.ElementAt(i + 6).parm_consecutivo, Convert.ToInt32(cmbMoneda.Value.ToString()), inPeriodo);                            
                            decimal valorB = 0;
                            var inSalidaB = salida.Any(x => x.sali_mes == meses.ElementAt(i + 6).parm_consecutivo);
                            if (inSalidaB)
                            {
                            
                                GE_TSALIDAPRESUPUESTO presup = salida.Where(x => x.sali_mes == meses.ElementAt(i + 6).parm_consecutivo).First();
                                valorB = Convert.ToDecimal(presup.sali_valor);
                            }

                            res.Add(new DTOMeses()
                            {
                                consec_a = meses.ElementAt(i).parm_consecutivo,
                                mes_a = meses.ElementAt(i).parm_descripcion,
                                valor_a = valorA,
                                trm_a = trmA,
                                equiv_trm_a = valorA/trmA,

                                consec_b = meses.ElementAt(i + 6).parm_consecutivo,
                                mes_b = meses.ElementAt(i + 6).parm_descripcion,
                                valor_b = valorB,
                                trm_b = trmB,
                                equiv_trm_b = valorB / trmB,
                            });
                            sumaMeses += (valorA + valorB);
                        }

                        txtSuma["txtSuma"] = sumaMeses;

                        Session["DataSourceT"] = res;

                        grid_meses.Visible = true;
                        grid_meses.DataSource = res;
                        grid_meses.DataBind();

                    }                     
                    // Session["DataSourceT"] = null;
                }
                else
                {

                }
            }
            else
                Response.Redirect(strUrl);
        }

        #region Metodos

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

                List<DTOMeses> res = new List<DTOMeses>();
                
                for (var i = 0; i < pMeses.Count() / 2; i++ )
                {
                    DTOMeses mes = new DTOMeses();
                    mes.consec_a = pMeses.ElementAt(i).parm_consecutivo;
                    mes.mes_a = pMeses.ElementAt(i).parm_descripcion;
                    mes.valor_a = 0;
                    mes.consec_b = pMeses.ElementAt(i + 6).parm_consecutivo;
                    mes.mes_b = pMeses.ElementAt(i + 6).parm_descripcion;
                    mes.valor_b = 0;

                    res.Add(mes);
                }
                
                Session["DataSourceT"] = Session["DataSourceT"] != null ? Session["DataSourceT"] : res;

                grid_meses.DataSource = res;
                grid_meses.DataBind();
                Cutilidades.ConfigurarGrid(grid_meses);
            }
            catch (Exception ex)
            {
                //chkAmortizar.Checked = false;
                //chkAmortEntre.Checked = false;
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los registros. " + ex.Message);
            }
        }

        public void CargarListas()
        {
            try
            {
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                string mSubtipo = Session["mSubtipo"].ToString().Equals("CE") ? "CE" : "GA";

                /*Cargar Empresas*/
                lstUserCcostos = Ccosto.GetAllUsuarioCentros(strUsuario[0].ToString().ToUpper(), mSubtipo);

                lstCompanias = lstUserCcostos.Select(x => x.GE_TCOMPANIAS).ToList();

                lstCompanias = lstCompanias.GroupBy(x => x.comp_consecutivo).Select(x => x.First()).ToList();

                cmbEmpresa.Items.Clear();
                cmbEmpresa.Items.Add("Seleccionar Compañia", null);
                foreach (GE_TCOMPANIAS c in lstCompanias)
                {
                    cmbEmpresa.Items.Add(c.comp_nombre, c.comp_consecutivo);
                }

                /*
                cmbCCosto.Items.Clear();
                cmbCCosto.Items.Add("Seleccionar Centro Costo", null);
                IEnumerable<GE_TCENTROSCOSTOS> cc = Ccosto.GetAllUsuarioxCuenta(strUsuario[0].ToString().ToUpper(), strSubCateg);
                foreach (GE_TCENTROSCOSTOS c in cc)
                {
                    cmbCCosto.Items.Add(c.cost_descripcion, c.cost_consecutivo);
                }
                */

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
                cmbMesIni.Items.Clear();
                cmbMesIni.Items.Add("Seleccionar Mes Inicial", null);
                foreach (GE_TPARAMETROS pr in pMeses)
                {
                    cmbMesIni.Items.Add(pr.parm_descripcion, pr.parm_consecutivo);
                }

                /*cargar cantidad*/
                /*
                cmbCantidad.Items.Clear();
                cmbCantidad.Items.Add("Seleccionar Cantidad", null);

                for (int i = 1; i <= 20; i++)
                {
                    cmbCantidad.Items.Add(i.ToString(), i);
                }
                */

                if (cmbProducto.Items.Count == 0)
                    cmbProducto.Items.Add("Seleccionar Producto", null);

                if (cmbItem.Items.Count == 0)
                    cmbItem.Items.Add("Seleccionar Item", null);
                /*
                if (cmbMesesAmortizar.Items.Count == 0)
                {
                    cmbMesesAmortizar.Items.Add("Seleccionar mes final para amortización", null);
                    cmbMesesAmortizar.Enabled = false;
                }
                 */
            }
            catch (Exception ex)
            {
                //chkAmortizar.Checked = false;
                //chkAmortEntre.Checked = false;
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los combos. " + ex.Message);
            }
        }

        public void FillProducto(int inCeco)
        {
            if (!Session["mSubtipo"].ToString().Equals("CE"))
            {
                GE_TPRODUCTOS mProd = null;

                if (Session["mSubtipo"].ToString().Equals("GA"))
                {
                    mProd = Cproducto.GetAllGridView().Where(x => x.GE_TPARAMETROS.parm_descripcion.Equals("LABORALES")).FirstOrDefault();
                }
                else if (Session["mSubtipo"].ToString().Equals("OT"))
                {
                    mProd = Cproducto.GetAllGridView().Where(x => x.GE_TPARAMETROS.parm_descripcion.Equals("OTROS GASTOS")).FirstOrDefault();
                }
                
                cmbProducto.Items.Clear();
                cmbProducto.Items.Add(mProd.prod_descripcion, mProd.prod_consecutivo);

                ListEditItem liProd = cmbProducto.Items.FindByText(mProd.prod_descripcion);
                liProd.Selected = true; 
                
                FillItem(mProd.prod_consecutivo);
            }
            else
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

                if (cmbProducto.Items.Count == 2)
                {
                    FillItem (Convert.ToInt32(cmbProducto.Items[1].Value));
                }
            }
            
        }

        public void FillItem(int inProd)
        {
            Char delimiter = ';';
            string[] strUsuario = null;
            strUsuario = Session["usuario"].ToString().Split(delimiter);
            // IEnumerable<GE_TPRODUCTOSITEMS> pr = Cproditems.GetAllUsuarioxCuenta(strUsuario[0].ToString(), strSubCateg, Convert.ToInt32(cmbCCosto.Value.ToString()), inProd);
            
            string mSubtipo = Session["mSubtipo"].ToString().Equals("CE") ? "CE" : "GA";

            IEnumerable<GE_TPRODUCTOSITEMS> pr = Cproditems.GetByProducto(inProd).Where(x => x.prit_activo == 1 && x.prit_tipo.Equals(mSubtipo)).ToList();
            cmbItem.Items.Clear();
            cmbItem.Items.Add("Seleccionar Item", null);
            foreach (GE_TPRODUCTOSITEMS p in pr)
            {
                cmbItem.Items.Add(p.prit_item, p.prit_consecutivo);
            }

            cmbItem.Value = null;
            CambiarMesItem();
        }

        private void Limpiar()
        {
            chkAmortizar.Checked = false;
            chkAmortEntre.Checked = false;
            cmbMoneda.Items.Clear();
            cmbActivo.Items.Clear();
            // txtCantidad.Items.Clear();
            cmbCCosto.Items.Clear();
            // cmbMes.Items.Clear();
            cmbMoneda.Items.Clear();
            cmbProducto.Items.Clear();
            cmbItem.Items.Clear();
            txtSvalor.Text = "";
            txtObservacion.Text = "";
            // cmbMesesAmortizar.Items.Clear();

            Session["DataSourceT"] = null;
            grid_meses.DataBind();

            CargarDatos();
            CargarListas();

            cmbActivo.Value = null;
            txtCantidad.Value = null;
            cmbCCosto.Value = null;
            cmbItem.Value = null;
            // cmbMes.Value = null;
            cmbMoneda.Value = null;
            cmbProducto.Value = null;
            txtSvalor.Text = "";
            // cmbMesesAmortizar.Value = null;
            txtObservacion.Text = "";
            txtId["txtId"] = "";
            txtSuma["txtSuma"] = "";
            

            cmbMesIni.Value = null;
            cmbMesFin.Value = null;
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
                // VentanaValidaciones.validarTxtNumericoObligatorio("Mes", cmbMes.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Cantidad", txtCantidad.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Activo", cmbActivo.Value, 6);
                VentanaValidaciones.validarTxtObligatorio("Observación", txtObservacion.Text, 500);

                if (chkAmortEntre.Checked == true)
                {
                    VentanaValidaciones.validarTxtObligatorio("Mes Final", cmbMesFin.Value, 6);
                }
                else
                {
                    VentanaValidaciones.validarRadioObligatorio("Tipo de Amortización", chkAmortizar.Value);
                }
            }
            catch
            {
                //chkAmortizar.Checked = false;
                //chkAmortEntre.Checked = false;
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
                chkAmortizar.Checked = false;
                chkAmortEntre.Checked = false;
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se puede leer el registro", ex);
            }

            return vlr;
        }

        public void CambiarMesItem()
        {
            /*
            try
            {
                if (/(cmbMes.Value != null) &&  * /(cmbItem.Value != null))
                {
                    GE_TPRODUCTOSITEMS it = Cproditems.GetItemxNombre(cmbItem.Text);
                    GE_TCUENTAS c = Ccuenta.GetById(it.prit_cuenta);

                    if (c.cuen_amortizar == 1)
                    {
                        /*cargar meses amortizar*/
                        /*
                        cmbMesesAmortizar.Items.Clear();
                        cmbMesesAmortizar.Items.Add("Seleccionar mes final para amortización", null);
                        cmbMesesAmortizar.Enabled = true;
                        IEnumerable<GE_TPARAMETROS> p = Cparametros.GetListbyClaseOrdenadaParametro("MESES", Convert.ToInt32(cmbMes.Value.ToString()));

                        foreach (GE_TPARAMETROS pr in p)
                        {
                            cmbMesesAmortizar.Items.Add(pr.parm_descripcion, pr.parm_consecutivo);
                        }

                        cmbMesesAmortizar.Value = null;
                         * /
                    }
                    else
                    {
                         /*
                        cmbMesesAmortizar.Items.Clear();
                        cmbMesesAmortizar.Items.Add("Seleccionar mes final para amortización", null);
                        cmbMesesAmortizar.Enabled = false;
                         *  /
                    }
                }
                else
                {
                    /*
                    cmbMesesAmortizar.Items.Clear();
                    cmbMesesAmortizar.Items.Add("Seleccionar mes final para amortización", null);
                    cmbMesesAmortizar.Enabled = false;
                     *  /
                }
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede cargar el combo." + ex.Message);
            }
            */
        }

        protected void FillCCossto(int empresa)
        {
            cmbCCosto.Items.Clear();
            cmbCCosto.Items.Add("Seleccionar Centro Costo", null);
            IList<GE_TCENTROSCOSTOS> cc = lstUserCcostos.Where(x => x.GE_TCOMPANIAS.comp_consecutivo == empresa).ToList();

            foreach (GE_TCENTROSCOSTOS c in cc)
            {
                cmbCCosto.Items.Add(c.cost_codigo, c.cost_consecutivo);
            }

            if (cmbCCosto.Items.Count == 2)
            {
                cmbCCosto.Items[1].Selected = true;
                FillProducto(Convert.ToInt32(cmbCCosto.Items[1].Value));
            }
                
        }
        #endregion

        #region Eventos

        protected void cmbMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid_meses.Visible = false;

            int moneda = Convert.ToInt32(cmbMoneda.Value.ToString());
            // txtSvalor.DisplayFormatString =  "0C";

            chkAmortEntre.Checked = false;
            chkAmortizar.Checked = false;
            cmbMesIni.Value = null;            
            cmbMesFin.Items.Clear();
            
            IList<DTOMeses> iList = Session["DataSourceT"] as IList<DTOMeses>;
            List<DTOMeses> res = new List<DTOMeses>();

            GE_TPARAMETROS param = Cparametros.GetByConsecutivo(moneda);
            decimal trmA = 1;
            decimal trmB = 1;

            int periAno = Cperiodo.GetPeriodoActivo().peri_ano;

            foreach (var mes in iList)
            {
                if (!param.parm_codigo.Equals("COP"))
                {
                    trmA = ValidarTrm(mes.consec_a, Convert.ToInt32(cmbMoneda.Value.ToString()), periAno);
                    trmB = ValidarTrm(mes.consec_b, Convert.ToInt32(cmbMoneda.Value.ToString()), periAno);
                    
                    grid_meses.Columns[3].Visible = true;
                    grid_meses.Columns[7].Visible = true;

                    grid_meses.Columns[5].Visible = true;
                    grid_meses.Columns[9].Visible = true;
                }
                else
                {
                    grid_meses.Columns[3].Visible = false;
                    grid_meses.Columns[7].Visible = false;

                    grid_meses.Columns[5].Visible = false;
                    grid_meses.Columns[9].Visible = false;
                }

                mes.trm_a = trmA;
                mes.equiv_trm_a = 0;

                mes.trm_b = trmB;
                mes.equiv_trm_b = 0;

                mes.valor_a = 0;
                mes.valor_b = 0;

                res.Add(mes);
            }

            Session["DataSourceT"] = res;
            grid_meses.DataSource = res;
            grid_meses.DataBind();
        }

        protected void RegresarClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmCuentasEspeciales.aspx");
        }
        
        protected void grid_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = CustomDataSourceT;
        }

        protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            UpdateItem(e.Keys, e.NewValues);
            CancelEditing(e);
        }
        
        protected void CancelEditing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            grid_meses.CancelEdit();        
        }

        private IList<DTOMeses> CustomDataSource
        {
            get
            {
                IList<DTOMeses> result = Session["DataSource"] as IList<DTOMeses>;
                Session["DataSource"] = result;
                return result;
            }
        }

        private IList<DTOMeses> CustomDataSourceT
        {
            get
            {
                IList<DTOMeses> result = Session["DataSourceT"] as IList<DTOMeses>;
                Session["DataSourceT"] = result;
                return result;
            }
        }

        protected void UpdateItem(OrderedDictionary keys, OrderedDictionary newValues)
        {
            var id = keys["mes_a"];
            IList<DTOMeses> iList = Session["DataSourceT"] as IList<DTOMeses>;

            foreach (DTOMeses d in iList)
            {
                if (d.mes_a.Equals(id))
                {
                    d.mes_a = newValues["mes_a"].ToString();
                    d.valor_a = Convert.ToDecimal(newValues["valor_a"]);
                    d.mes_b = newValues["mes_b"].ToString();
                    d.valor_b = Convert.ToDecimal(newValues["valor_b"]);
                    break;
                }
            }
            Session["DataSourceT"] = iList;
        }


        protected void NuevoClicked(object sender, EventArgs e)
        {
            Limpiar();
        }



        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!validar())
                {
                    txtSuma["txtSuma"] = "";
                    return;
                }

                grid_meses.UpdateEdit();
                IList<DTOMeses> lstMeses = (IList<DTOMeses>)grid_meses.DataSource;
                
                decimal sumaAmort = 0;
                int moneda = Convert.ToInt32(cmbMoneda.Value.ToString());

                GE_TPARAMETROS param = Cparametros.GetByConsecutivo(moneda);
                decimal trmA = 1;
                decimal trmB = 1;

                int periAno = Cperiodo.GetPeriodoActivo().peri_ano;

                foreach (var mes in lstMeses)
                {
                    if (!param.parm_codigo.Equals("COP"))
                    {
                        trmA = ValidarTrm(mes.consec_a, Convert.ToInt32(cmbMoneda.Value.ToString()), periAno);
                        trmB = ValidarTrm(mes.consec_a, Convert.ToInt32(cmbMoneda.Value.ToString()), periAno);
                    }

                    sumaAmort += Convert.ToDecimal(mes.valor_a) / trmA + Convert.ToDecimal(mes.valor_b) / trmB; // Desde el GridView Segunda Columna                    
                }

                if (!sumaAmort.ToString("0").Equals(Convert.ToDecimal(txtSvalor.Text).ToString("0")))
                {
                    txtSuma["txtSuma"] = "";
                    VentanaValidaciones.mostrarError("La suma de meses (" + sumaAmort.ToString("0") + ") debe ser igual a " + Convert.ToDecimal(txtSvalor.Text).ToString("N0"));
                    return;
                }

                GE_TPRODUCTOSITEMS it = Cproditems.GetItemxNombre(cmbItem.Text);
                GE_TCUENTAS c = Ccuenta.GetById(it.prit_cuenta);
                int inAmortizar = 0;

                if (c.cuen_amortizar == 1)
                {
                    /*
                        if (cmbMesesAmortizar.Value == null)
                        {
                            VentanaValidaciones.mostrarError("El campo mes final de amortización es obligatorio");
                            return;
                        }
                        else
                     */
                    inAmortizar = 1;
                }

                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);
                DateTime dtFecha = DateTime.Now;
                int inPeriodo = Cperiodo.GetPeriodoActivo().peri_consecutivo;
                
                GE_TPERSONAS p = Cpersona.GetbyUsuario(strUsuario[0].ToString().ToUpper());

                GE_TPERIODOTRANSACCIONES pt = new GE_TPERIODOTRANSACCIONES();
                pt.petr_activo = Convert.ToInt32(cmbActivo.Value.ToString());
                pt.petr_amortizar = inAmortizar;
                pt.petr_cantidad = Convert.ToInt32(txtCantidad.Value.ToString());
                pt.petr_centrocosto = Convert.ToInt32(cmbCCosto.Value.ToString());
                pt.petr_fecha = dtFecha;
                pt.petr_fecha_act = dtFecha;
                // pt.petr_mes = Convert.ToInt32(cmbMes.Value.ToString());
                pt.petr_mes = 1;

                pt.petr_moneda = Convert.ToInt32(cmbMoneda.Value.ToString());
                pt.petr_observacion = txtObservacion.Text;
                pt.petr_periodo = inPeriodo;
                pt.petr_persona = p.pers_consecutivo;
                pt.petr_producto_item = Convert.ToInt32(cmbItem.Value.ToString());
                pt.petr_proveedor = 1;
                pt.petr_tipo_viaje = null;
                //pt.petr_trm = ValidarTrm(Convert.ToInt32(cmbMes.Value.ToString()), Convert.ToInt32(cmbMoneda.Value.ToString()), inPeriodo);
                pt.petr_trm = 1;
                
                pt.petr_valor = Convert.ToDecimal(txtSvalor.Text);
                pt.petr_valor_amortizar = 0;
                pt.petr_usuario = strUsuario[0].ToString();
                pt.petr_usuario_act = strUsuario[0].ToString();
                // pt.petr_meses_amortizar = (cmbMesesAmortizar.Value == null) ? (int?)null : Convert.ToInt32(cmbMesesAmortizar.Value.ToString());
                pt.petr_meses_amortizar = 1;

                pt.petr_tipo = Session["mSubtipo"].ToString();

                if (txtId.Contains("txtId") && !String.IsNullOrEmpty(txtId["txtId"].ToString()))
                {
                    pt.petr_consecutivo = Convert.ToInt32(txtId["txtId"].ToString());
                    CperiodoTr.Update(pt);

                    CsalidaP.DeleteByPeriodoTransacc(pt.petr_consecutivo);
                }
                else
                {
                    CperiodoTr.Add(pt);
                }

                // CperiodoTr.LoadTransactions(pt.petr_consecutivo); // Stored Procedure
                
                // Periodo Activo
                CtrPeriodoPresupuesto CtrPeriodo = new CtrPeriodoPresupuesto();
                int idPeriodo = CtrPeriodo.GetPeriodoActivo().peri_consecutivo;
                
                foreach (var mes in lstMeses)
                {
                    GE_TSALIDAPRESUPUESTO salida1 = new GE_TSALIDAPRESUPUESTO();
                    salida1.sali_periodo_transacc = pt.petr_consecutivo;
                    salida1.sali_persona = pt.petr_persona;
                    salida1.sali_centrocosto = pt.petr_centrocosto;
                    salida1.sali_producto_item = pt.petr_producto_item;
                    salida1.sali_moneda = pt.petr_moneda;
                    salida1.sali_mes = mes.consec_a; // Desde el GridView Primer Columna
                    salida1.sali_valor = Convert.ToDecimal(mes.valor_a); // Desde el GridView Primer Columna
                    salida1.sali_usuario = pt.petr_usuario;
                    salida1.sali_fecha = pt.petr_fecha;
                    salida1.sali_tipo = pt.petr_tipo;                    
                    salida1.sali_periodo = idPeriodo;
                    CsalidaP.Add(salida1);


                    GE_TSALIDAPRESUPUESTO salida2 = new GE_TSALIDAPRESUPUESTO();
                    salida2.sali_periodo_transacc = pt.petr_consecutivo;
                    salida2.sali_persona = pt.petr_persona;
                    salida2.sali_centrocosto = pt.petr_centrocosto;
                    salida2.sali_producto_item = pt.petr_producto_item;                    
                    salida2.sali_moneda = pt.petr_moneda;
                    salida2.sali_mes = mes.consec_b; // Desde el GridView Segunda Columna
                    salida2.sali_valor = Convert.ToDecimal(mes.valor_b); // Desde el GridView Segunda Columna
                    salida2.sali_usuario = pt.petr_usuario;
                    salida2.sali_fecha = pt.petr_fecha;
                    salida2.sali_tipo = pt.petr_tipo;
                    salida2.sali_periodo = idPeriodo;                    
                    CsalidaP.Add(salida2);
                }
                
                Session["DataSourceT"] = null;
                Session["mensaje"] = "OK";
                Response.Redirect("frmCuentasEspeciales.aspx", false);
            }
            catch (Exception ex)
            {
                //chkAmortizar.Checked = false;
                //chkAmortEntre.Checked = false;
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede guardar el registro." + ex.Message);
            }

        }

        protected void cmbCCosto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCCosto.Value != null)
            {
                FillProducto(Convert.ToInt32(cmbCCosto.Value));
            }   
        }

        protected void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.Value != null)
                FillItem(Convert.ToInt32(cmbProducto.Value));
        }

        protected void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            CambiarMesItem();

        }

        protected void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CambiarMesItem();
        }


        protected void cmbMesIni_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmbMesFin.Items.Clear();
            cmbMesFin.Items.Add("Seleccionar Mes Final", null);

            if (cmbMesIni.Value != null)
            {
                foreach (GE_TPARAMETROS pr in pMeses)
                {
                    if (pr.parm_consecutivo < Convert.ToInt32(cmbMesIni.Value.ToString()))
                        continue;

                    cmbMesFin.Items.Add(pr.parm_descripcion, pr.parm_consecutivo);
                }
            }            
            
            cmbMesFin.Value = null;
        }
        
        protected void amortizarClicked(object sender, EventArgs e)
        {
            try
            {
                VentanaValidaciones.validarTxtMonedaObligatorio("Valor", txtSvalor.Text, 50);
                VentanaValidaciones.validarComboObligatorio("Tipo de Moneda", cmbMoneda.Value);

                int moneda = Convert.ToInt32(cmbMoneda.Value.ToString());

                decimal valor = Convert.ToDecimal(txtSvalor.Text);

                bool isCheckedTodos = chkAmortizar.Checked;
                bool isCheckedEntre = chkAmortEntre.Checked;

                decimal amortizacion = 0;

                if (isCheckedTodos)
                {
                    grid_meses.Visible = true;

                    cmbMesIni.Value = null;
                    cmbMesIni.Enabled = false;
                    
                    cmbMesFin.Value = null;
                    cmbMesFin.Enabled = false;

                    amortizacion = valor / 12;                

                    IList<DTOMeses> iList = Session["DataSourceT"] as IList<DTOMeses>;
                    List<DTOMeses> res = new List<DTOMeses>();

                    GE_TPARAMETROS param = Cparametros.GetByConsecutivo(moneda);
                    decimal trmA = 1;
                    decimal trmB = 1;
                    
                    int inPeriodo = Cperiodo.GetPeriodoActivo().peri_ano;

                    foreach (var mes in iList)
                    {

                        if (!param.parm_codigo.Equals("COP"))
                        {
                            trmA = ValidarTrm(mes.consec_a, Convert.ToInt32(cmbMoneda.Value.ToString()), inPeriodo);
                            trmB = ValidarTrm(mes.consec_a, Convert.ToInt32(cmbMoneda.Value.ToString()), inPeriodo);
                        }
                        
                        mes.valor_a = amortizacion * trmA;
                        mes.valor_b = amortizacion * trmB;

                        mes.equiv_trm_a = mes.valor_a / trmA;
                        mes.equiv_trm_b = mes.valor_b / trmB;

                        mes.trm_a = trmA; 
                        mes.trm_b = trmB;                        

                        res.Add(mes);                        
                    }

                    txtSuma["txtSuma"] = valor;

                    Session["DataSourceT"] = res;

                    grid_meses.DataSource = res;
                    grid_meses.DataBind();
                }
                else
                {
                    cmbMesIni.Enabled = true;                    
                    cmbMesFin.Enabled = true;
                }

            }
            catch(Exception ex)
            {
                //chkAmortizar.Checked = false;
                //chkAmortEntre.Checked = false;
                VentanaValidaciones.mostrarError("No se puede mostrar la información. " + ex.Message);
            }

            return;
        }


        protected void cmbMesFin_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                VentanaValidaciones.validarTxtMonedaObligatorio("Valor", txtSvalor.Text, 50);

                grid_meses.Visible = true;

                int moneda = Convert.ToInt32(cmbMoneda.Value.ToString());

                decimal valor = Convert.ToDecimal(txtSvalor.Text);
                
                decimal amortizacion = 0;
                int entreMeses = 0;

                bool flag = false;
                foreach (GE_TPARAMETROS pr in pMeses)
                {
                    if (flag)
                    {
                        entreMeses++;
                    } 
                    
                    if (pr.parm_consecutivo == Convert.ToInt32(cmbMesIni.Value.ToString()))
                    {
                        entreMeses = 1;
                        flag = true;
                    }

                    if (pr.parm_consecutivo == Convert.ToInt32(cmbMesFin.Value.ToString()))
                    {
                        flag = false;
                        break;
                    }
                }

                amortizacion = valor / entreMeses;
                
                List<DTOMeses> res = new List<DTOMeses>();
                
                GE_TPARAMETROS param = Cparametros.GetByConsecutivo(moneda);
                decimal trmA = 1;
                decimal trmB = 1;

                int inPeriodo = Cperiodo.GetPeriodoActivo().peri_ano;
                
                for (var i = 0; i < pMeses.Count() / 2; i++)
                {
                    DTOMeses mes = new DTOMeses();
                    mes.consec_a = pMeses.ElementAt(i).parm_consecutivo;
                    mes.mes_a = pMeses.ElementAt(i).parm_descripcion;

                    if (!param.parm_codigo.Equals("COP"))
                    {
                        trmA = ValidarTrm(mes.consec_a, Convert.ToInt32(cmbMoneda.Value.ToString()), inPeriodo);
                        
                        grid_meses.Columns[3].Visible = true;
                        grid_meses.Columns[7].Visible = true;

                        grid_meses.Columns[5].Visible = true;
                        grid_meses.Columns[9].Visible = true;
                    }
                    else
                    {
                        grid_meses.Columns[3].Visible = false;
                        grid_meses.Columns[7].Visible = false;

                        grid_meses.Columns[5].Visible = false;
                        grid_meses.Columns[9].Visible = false;
                    }

                    if (pMeses.ElementAt(i).parm_consecutivo >= Convert.ToInt32(cmbMesIni.Value.ToString()) && pMeses.ElementAt(i).parm_consecutivo <= Convert.ToInt32(cmbMesFin.Value.ToString()))
                    {
                        mes.valor_a = amortizacion * trmA;
                    }                        
                    else
                    {
                        mes.valor_a = 0;
                    }

                    mes.consec_b = pMeses.ElementAt(i + 6).parm_consecutivo;
                    mes.mes_b = pMeses.ElementAt(i + 6).parm_descripcion;

                    if (!param.parm_codigo.Equals("COP"))
                    {
                        trmB = ValidarTrm(mes.consec_a, Convert.ToInt32(cmbMoneda.Value.ToString()), inPeriodo);
                    }

                    if (pMeses.ElementAt(i + 6).parm_consecutivo >= Convert.ToInt32(cmbMesIni.Value.ToString()) && pMeses.ElementAt(i + 6).parm_consecutivo <= Convert.ToInt32(cmbMesFin.Value.ToString()))
                    {
                        mes.valor_b = amortizacion * trmB; 
                    }
                    else
                    {
                        mes.valor_b = 0;
                    }

                    mes.trm_a = trmA;
                    mes.equiv_trm_a = mes.valor_a / trmA;

                    mes.trm_b = trmB;
                    mes.equiv_trm_b = mes.valor_b / trmB;

                    res.Add(mes);
                }

                Session["DataSourceT"] = Session["DataSourceT"] != null ? Session["DataSourceT"] : res;

                grid_meses.DataSource = res;

                txtSuma["txtSuma"] = valor;
                Session["DataSourceT"] = res;

                grid_meses.DataSource = res;
                grid_meses.DataBind();
            
            }
            catch(Exception ex)
            {
                //chkAmortizar.Checked = false;
                //chkAmortEntre.Checked = false;
                return;
            }
             
            return;
        }

        protected void cmbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbProducto.Items.Clear();
            cmbCCosto.Items.Clear();
            cmbItem.Items.Clear();
            cmbProducto.Text = string.Empty;
            cmbCCosto.Text = string.Empty;
            cmbItem.Text = string.Empty;
            FillCCossto(Convert.ToInt32(cmbEmpresa.Value));
            cmbProducto.SelectedIndex = 0;
            cmbCCosto.SelectedIndex = 0;
            cmbItem.SelectedIndex = 0;

        }
        #endregion 
    }
}