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
    public partial class frmDrivers_form : System.Web.UI.Page
    {
        CtrDrivers ctrDrivers = new CtrDrivers();
        CtrUtilidades CUtilidades = new CtrUtilidades();
        Hashtable campoSeleccionado = null; 
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        string[] camposClaseparametro = new string[] { "driv_consecutivo", "driv_nombre", "driv_descripcion", "driv_tipo_cobro", "driv_aplica_sede", "driv_aplica_valor", "driv_aplica_proveedor", "driv_activo" };

        #region Metodos
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
                        GE_TDRIVERS mObjeto = Session["objeto"] as GE_TDRIVERS;

                        txtConsecutivo["txtConsecutivo"] = mObjeto.driv_consecutivo.ToString();
                        txtNombre.Value = mObjeto.driv_nombre.ToString();
                        txtDescripcion.Value = mObjeto.driv_descripcion.ToString();

                        if (mObjeto.driv_tipo_cobro != "")
                        {
                            ListEditItem liTipoCobro = cmbTipoCobro.Items.FindByValue(mObjeto.driv_tipo_cobro.ToString());
                            liTipoCobro.Selected = true;
                        }

                        if (mObjeto.driv_aplica_sede != "")
                        {
                            ListEditItem liAplicaSede = cmbAplicaSede.Items.FindByValue(mObjeto.driv_aplica_sede.ToString());
                            liAplicaSede.Selected = true;
                        }

                        if (mObjeto.driv_aplica_valor != "")
                        {
                            ListEditItem liAplicaValor = cmbAplicaValor.Items.FindByValue(mObjeto.driv_aplica_valor.ToString());
                            liAplicaValor.Selected = true;
                        }

                        if (mObjeto.driv_aplica_proveedor != "")
                        {
                            ListEditItem liAplicaProveedor = cmbAplicaProv.Items.FindByValue(mObjeto.driv_aplica_proveedor.ToString());
                            liAplicaProveedor.Selected = true;
                        }

                        ListEditItem liEstado = cmbActivo.Items.FindByValue(mObjeto.driv_activo.ToString());
                        liEstado.Selected = true;
                    }
                }
            }
            else
                Response.Redirect(strUrl);    
        }

        protected void CargarListas()
        {
            try 
            {
                
                if (!txtConsecutivo.Contains("txtConsecutivo"))
                {
                    txtConsecutivo["txtConsecutivo"] = null;
                }

                cmbTipoCobro.Items.Clear();
                cmbTipoCobro.Items.Add("Seleccionar Forma de Cobro", null);
                cmbTipoCobro.Items.Add("TOTAL", "T");
                cmbTipoCobro.Items.Add("UNO A UNO", "U");
                cmbTipoCobro.Items.Add("EQUIPOS", "E");

                cmbAplicaSede.Items.Clear();
                cmbAplicaSede.Items.Add("Aplica Sede?", null);
                cmbAplicaSede.Items.Add("SI", "S");
                cmbAplicaSede.Items.Add("NO", "N");

                cmbAplicaValor.Items.Clear();
                cmbAplicaValor.Items.Add("Aplica Valor?", null);
                cmbAplicaValor.Items.Add("SI", "S");
                cmbAplicaValor.Items.Add("NO", "N");

                cmbAplicaProv.Items.Clear();
                cmbAplicaProv.Items.Add("Aplica Proveedor?", null);
                cmbAplicaProv.Items.Add("SI", "S");
                cmbAplicaProv.Items.Add("NO", "N");

                cmbActivo.Items.Clear();
                cmbActivo.Items.Add("Seleccionar Activo", null);
                cmbActivo.Items.Add("ACTIVO", "1");
                cmbActivo.Items.Add("INACTIVO", "0");
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar las listas. " + ex.Message);
            }
        }

        protected void limpiar()
        {
            txtConsecutivo["txtConsecutivo"] = null;
            txtNombre.Value = "";
            txtDescripcion.Value = "";

            cmbTipoCobro.Value = "";
            cmbAplicaSede.Value = "";
            cmbAplicaValor.Value = "";
            cmbAplicaProv.Value = "";
            cmbActivo.Value = "";
        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarTxtObligatorio("Nombre", txtNombre.Text, 50);
                VentanaValidaciones.validarTxtObligatorio("Descripcion", txtDescripcion.Text, 500);

                VentanaValidaciones.validarComboObligatorio("Aplica Sede", cmbAplicaSede.Value);
                VentanaValidaciones.validarComboObligatorio("Aplica Valor", cmbAplicaValor.Value);
                VentanaValidaciones.validarComboObligatorio("Aplica Proveedor", cmbAplicaProv.Value);
                VentanaValidaciones.validarComboObligatorio("Tipo de Cobro", cmbTipoCobro.Value);
                VentanaValidaciones.validarComboObligatorio("Activo", cmbActivo.Value);                
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
            Response.Redirect("frmDrivers.aspx");
        }

        protected void NuevoClicked(object sender, EventArgs e)
        {
            limpiar();
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

                GE_TDRIVERS drivers = new GE_TDRIVERS();

                drivers.driv_nombre = txtNombre.Text.ToString();
                drivers.driv_descripcion = txtDescripcion.Text.ToString();
                drivers.driv_tipo_cobro = cmbTipoCobro.Value.ToString();
                drivers.driv_aplica_sede = cmbAplicaSede.Value.ToString();
                drivers.driv_aplica_valor = cmbAplicaValor.Value.ToString();
                drivers.driv_aplica_proveedor = cmbAplicaProv.Value.ToString();
                drivers.driv_activo = cmbActivo.Value.ToString();
                drivers.driv_fecha = DateTime.Now;
                drivers.driv_usuario = strUsuario[0].ToString();

                if(txtConsecutivo["txtConsecutivo"] != null)
                {
                    drivers.driv_consecutivo = Convert.ToInt32(txtConsecutivo["txtConsecutivo"].ToString());
                    ctrDrivers.Update(drivers);
                }else
                {
                    ctrDrivers.Add(drivers);
                }

                Session["mensaje"] = "OK";
                Response.Redirect("frmDrivers.aspx", false);
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede guardar el registro. " + ex.Message);
            }
        }
        #endregion
    }
}