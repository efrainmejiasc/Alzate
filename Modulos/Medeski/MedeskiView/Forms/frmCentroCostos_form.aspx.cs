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
    public partial class frmCentroCostos_form : System.Web.UI.Page
    {
        CtrCentroCosto ctrCentrocostos = new CtrCentroCosto();
        CtrCompanias ctrCompanias = new CtrCompanias();
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
                CargarListas();
                if (!IsPostBack)
                {
                    if (Session["objeto"] != null)
                    {
                        GE_TCENTROSCOSTOS mObjeto = Session["objeto"] as GE_TCENTROSCOSTOS;

                        txtConsecutivo["txtConsecutivo"] = mObjeto.cost_consecutivo.ToString();
                        txtCcosto.Value = mObjeto.cost_codigo.ToString();
                        txtDescripcion.Value = mObjeto.cost_descripcion.ToString();
                        txtResponsable.Value = mObjeto.cost_responsable != null ? mObjeto.cost_responsable.ToString() : null;

                        if (mObjeto.cost_centro_operacion != "")
                        {
                            ListEditItem liCentroOperaciones = cmbCentroOperaciones.Items.FindByText(mObjeto.cost_centro_operacion.ToString());
                            liCentroOperaciones.Selected = true;
                        }

                        if (mObjeto.GE_TPARAMETROS.parm_descripcion != "")
                        {
                            ListEditItem liTipoCliente = cmbTipoCliente.Items.FindByText(mObjeto.GE_TPARAMETROS.parm_descripcion.ToString());
                            liTipoCliente.Selected = true;
                        }

                        if (mObjeto.cost_activo != null)
                        {
                            ListEditItem liEstado = cmbEstado.Items.FindByValue(mObjeto.cost_activo.ToString());
                            liEstado.Selected = true;
                        }

                        if (mObjeto.GE_TPARAMETROS2.parm_descripcion != "")
                        {
                            ListEditItem liTipoDistri = cmbTipoDistri.Items.FindByText(mObjeto.GE_TPARAMETROS2.parm_descripcion.ToString());
                            liTipoDistri.Selected = true;
                        }

                        if (mObjeto.GE_TCOMPANIAS.comp_nombre != null)
                        {
                            ListEditItem liCompanias = cmbCompania.Items.FindByText(mObjeto.GE_TCOMPANIAS.comp_nombre.ToString());
                            liCompanias.Selected = true;
                        }
                    }
                }
            }
            else
                Response.Redirect(strUrl);
        }

        #region Metodos
        protected void CargarListas()
        {
            try 
            {
                cmbCompania.Items.Clear();
                cmbCompania.Items.Add("Seleccionar Compañia", null);
                GE_TCOMPANIAS compania = Session["compania"] as GE_TCOMPANIAS;
                IList<GE_TCOMPANIAS> lstCompanias = ctrCompanias.GetAllActive().Where(x => x.comp_consecutivo == compania.comp_consecutivo).ToList();

                foreach (GE_TCOMPANIAS cop in lstCompanias)
                {
                    cmbCompania.Items.Add(cop.comp_nombre, cop.comp_consecutivo);
                }

                if (cmbCompania.Items.Count == 2)
                {
                    cmbCompania.Items[1].Selected = true;
                }


                cmbCentroOperaciones.Items.Clear();
                cmbCentroOperaciones.Items.Add("Seleccionar Centro de Operaciones", null);
                IList<GE_TCENTROSOPERACION> lstCentroOp = ctrCentroOperacion.GetAll();
                foreach (GE_TCENTROSOPERACION cop in lstCentroOp)
                {
                    cmbCentroOperaciones.Items.Add(cop.ceop_codigo, cop.ceop_consecutivo);
                }

                if (cmbCentroOperaciones.Items.Count == 2)
                {
                    cmbCentroOperaciones.Items[0].Selected = true;
                }


                cmbTipoCliente.Items.Clear();
                cmbTipoCliente.Items.Add("Seleccionar Tipo de Cliente", null);
                IList<GE_TPARAMETROS> lstTipoCliente = ctrParametros.GetListbyClase("TIPO_CLIENTE");
                foreach (GE_TPARAMETROS tipo in lstTipoCliente)
                {
                    cmbTipoCliente.Items.Add(tipo.parm_descripcion, tipo.parm_consecutivo);
                }

                if (cmbTipoCliente.Items.Count == 2)
                {
                    cmbTipoCliente.Items[0].Selected = true;
                }


                cmbTipoDistri.Items.Clear();
                cmbTipoDistri.Items.Add("Seleccionar Tipo de Distribucion de Facturacion", null);
                IList<GE_TPARAMETROS> lstTipoDistri = ctrParametros.GetListbyClase("TIPO_DISTR_FAC");
                foreach (GE_TPARAMETROS tipo2 in lstTipoDistri)
                {
                    cmbTipoDistri.Items.Add(tipo2.parm_descripcion,tipo2.parm_consecutivo);
                }

                if (cmbTipoDistri.Items.Count == 2)
                {
                    cmbTipoDistri.Items[0].Selected = true;
                }
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar las listas. " + ex.Message);
            }
        }

        protected void limpiar()
        {
            txtConsecutivo["txtConsecutivo"] = string.Empty;
            txtCcosto.Value = "";
            txtDescripcion.Value = "";
            txtResponsable.Value = "";

            cmbCentroOperaciones.Value = "";
            cmbTipoCliente.Value = "";
            cmbEstado.Value = "";
            cmbTipoDistri.Value = "";

        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarTxtNumericoObligatorio("Codigo", txtCcosto.Text, 50);
                VentanaValidaciones.validarTxtObligatorio("Descripción", txtDescripcion.Text, 500);
                VentanaValidaciones.validarTxtObligatorio("Responsable", txtResponsable.Text, 100);

                VentanaValidaciones.validarComboObligatorio("Tipo de Distribucion", cmbTipoDistri.Value);
                VentanaValidaciones.validarComboObligatorio("Tipo de Cliente", cmbTipoCliente.Value);
                VentanaValidaciones.validarComboObligatorio("Centro de Operaciones", cmbCentroOperaciones.Value);
                VentanaValidaciones.validarComboObligatorio("Compañia", cmbCompania.Value);
                VentanaValidaciones.validarComboObligatorio("Activo", cmbEstado.Value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        #endregion


        #region Eventos

        protected void RegresarClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmCentroCostos.aspx");
        }
        
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
        }

        protected void GuardarClicked(object sender, EventArgs e) {
            try 
            {
                if (!validar())
                {
                    return;
                }

                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                GE_TCENTROSCOSTOS centroCostos = new GE_TCENTROSCOSTOS();
                centroCostos.cost_codigo = txtCcosto.Text;
                centroCostos.cost_descripcion = txtDescripcion.Text;
                centroCostos.cost_centro_operacion = cmbCentroOperaciones.Text;
                centroCostos.cost_responsable = txtResponsable.Text;
                centroCostos.cost_tipo_cliente = Convert.ToInt32(cmbTipoCliente.Value);
                centroCostos.cost_tipo_distribucion = Convert.ToInt32(cmbTipoDistri.Value);
                centroCostos.cost_empresa = Convert.ToInt32(cmbCompania.Value);

                centroCostos.cost_consec_responsable = 1;
                centroCostos.cost_ppto_interno = 1;
                centroCostos.cost_cuenta_especial = 1;
                centroCostos.cost_consec_resp_ppto = 1;
                centroCostos.cost_consec_categoria = 3;

                centroCostos.cost_usuario_act = strUsuario[0].ToString();
                centroCostos.cost_fecha_act = DateTime.Now;
                centroCostos.cost_usuario = strUsuario[0].ToString();
                centroCostos.cost_fecha = DateTime.Now;
                
                centroCostos.cost_activo = Convert.ToInt32(cmbEstado.Value);

                if (txtConsecutivo.Contains("txtConsecutivo") && !string.IsNullOrEmpty(txtConsecutivo["txtConsecutivo"].ToString()))
                {
                    centroCostos.cost_consecutivo = Convert.ToInt32(txtConsecutivo["txtConsecutivo"].ToString());
                    ctrCentrocostos.Update(centroCostos);

                }
                else
                {
                    
                    ctrCentrocostos.Add(centroCostos);
                }

                Session["mensaje"] = "OK";
                Response.Redirect("frmCentroCostos.aspx", false);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede guardar el registro. " + ex.Message);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        #endregion


    }
}