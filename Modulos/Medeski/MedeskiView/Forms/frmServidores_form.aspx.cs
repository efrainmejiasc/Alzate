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
    public partial class frmServidores_form : System.Web.UI.Page
    {
        CtrUtilidades Cutilidades = new CtrUtilidades();
        CtrServidores CServidores = new CtrServidores();
        Hashtable servSeleccionado = null;

        string[] camposServidor = new string[]
        { 
            "serv_consecutivo",
            "serv_nombre",
            "ser_marca",
            "ser_modelo",
            "serv_funcion",
            "serv_estado",
            "serv_ip",
            "ser_sistema_operativo",
            "serv_numero_bits",
            "serv_memoria",
            "serv_procesadores",
            "serv_core",
            "serv_disco_duro",
            "serv_descripcion_disco_duro",
            "serv_aplicaciones_instaladas",
            "serv_virtualizado",
            "serv_software_virtualizacion",
            "serv_granja_virtual",
            "serv_ubicacion_fisica",
            "serv_depreciable", 
            "serv_activo_fijo", 
            "serv_activo"
        };

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
                        GE_TSERVIDORES objeto = Session["objeto"] as GE_TSERVIDORES;
                
                        txtId["txtId"] = objeto.serv_consecutivo.ToString();
                        txtMarca.Text = objeto.ser_marca.ToString();
                        txtModelo.Text = objeto.ser_modelo.ToString();
                        txtFuncion.Text = objeto.serv_funcion.ToString();
                        txtEstado.Text = objeto.serv_estado.ToString();
                        txtDireccionIP.Text = objeto.serv_ip.ToString();
                        txtSistemaOperativo.Text = objeto.ser_sistema_operativo.ToString();
                        txtNumeroBits.Text = objeto.serv_numero_bits.ToString();
                        txtProcesadores.Text = objeto.serv_procesadores.ToString();
                        txtDescripcionDD.Text = objeto.serv_descripcion_disco_duro.ToString();
                        txtAplicaciones.Text = objeto.serv_aplicaciones_instaladas.ToString();
                        cmbVirtualizado.Value = objeto.serv_virtualizado.ToString();
                        txtSoftwareVirtualizacion.Text = objeto.serv_software_virtualizacion.ToString();
                        txtGranja.Text = objeto.serv_granja_virtual.ToString();
                        txtUbicacionFisica.Text = objeto.serv_ubicacion_fisica.ToString();
                        cmbActivo.Value = objeto.serv_activo.ToString();
                        txtServidor.Text = objeto.serv_nombre.ToString();
                        txtCore.Text = objeto.serv_memoria.ToString();
                        txtDiscoDuro.Text = objeto.serv_disco_duro.ToString();
                        txtMemoria.Text = objeto.serv_memoria.ToString();
                        txtActivoFijo.Text = objeto.serv_activo_fijo.ToString();
                        cmbActivo.Value = objeto.serv_activo.ToString();
                        cmbDepreciable.Value = objeto.serv_depreciable.ToString();
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
                cmbDepreciable.Items.Clear();
                cmbDepreciable.Items.Add("Depreciable?", null);
                cmbDepreciable.Items.Add("Si", 1);
                cmbDepreciable.Items.Add("No", 0);

                cmbActivo.Items.Clear();
                cmbActivo.Items.Add("Activo?", null);
                cmbActivo.Items.Add("Si", 1);
                cmbActivo.Items.Add("No", 0);

                cmbVirtualizado.Items.Clear();
                cmbVirtualizado.Items.Add("Servidor Virtualizado?", null);
                cmbVirtualizado.Items.Add("Si", 1);
                cmbVirtualizado.Items.Add("No", 0);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar las listas. " + ex.Message);
            }
        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarTxtObligatorio("Servidor", txtServidor.Text, 100);
                VentanaValidaciones.validarTxtObligatorio("Marca", txtMarca.Text, 50);
                VentanaValidaciones.validarTxtObligatorio("Modelo", txtModelo.Text, 80);
                VentanaValidaciones.validarTxtObligatorio("Dirección IP", txtDireccionIP.Text, 20);
                VentanaValidaciones.validarTxtObligatorio("Sistema Operativo", txtSistemaOperativo.Text, 100);
                VentanaValidaciones.validarTxtNumericoObligatorio("Bits de Sistema Operativo", txtNumeroBits.Text, 5);
                VentanaValidaciones.validarTxtMonedaObligatorio("Memoria", txtMemoria.Text, 50);
                if (cmbVirtualizado.Text.Equals("Si"))
                {
                    VentanaValidaciones.validarTxtObligatorio("Software de Virtualización", txtSoftwareVirtualizacion.Text, 80);
                }
                VentanaValidaciones.validarTxtObligatorio("Granja", txtGranja.Text, 80);
                VentanaValidaciones.validarTxtObligatorio("Ubicación Fisica", txtUbicacionFisica.Text, 80);
                VentanaValidaciones.validarTxtNumericoObligatorio("Activo", cmbActivo.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Depreciable", cmbDepreciable.Value, 6);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void Limpiar()
        {
            txtId["txtId"] = "";
            cmbActivo.Value = null;
            cmbDepreciable.Value = null;
            txtActivoFijo.Text = "";
            txtCore.Text = "";
            txtDiscoDuro.Text = "";
            txtMemoria.Text = "";
            txtServidor.Text = "";
            txtModelo.Text = "";
            txtMarca.Text = "";
            txtFuncion.Text = "";
            txtEstado.Text = "";
            txtDireccionIP.Text = "";
            txtSistemaOperativo.Text = "";
            txtNumeroBits.Text = "";
            txtDescripcionDD.Text = "";
            txtAplicaciones.Text = "";
            cmbVirtualizado.Value = null;
            txtSoftwareVirtualizacion.Text = "";
            txtGranja.Text = "";
            txtUbicacionFisica.Text = "";

        }


        #endregion

        #region Eventos
        protected void RegresarClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmServidores.aspx");
        }

        protected void NuevoClicked(object sender, EventArgs e)
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

                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);
                DateTime dtFecha = DateTime.Now;

                GE_TSERVIDORES serv = new GE_TSERVIDORES();
                serv.serv_nombre = txtServidor.Text.ToUpper();
                serv.ser_marca = txtMarca.Text.ToUpper();
                serv.ser_modelo = txtModelo.Text.ToUpper();
                serv.serv_funcion = txtFuncion.Text.ToUpper();
                serv.serv_estado = txtEstado.Text.ToUpper();
                serv.serv_ip = txtDireccionIP.Text.ToUpper();
                serv.ser_sistema_operativo = txtSistemaOperativo.Text.ToUpper();
                serv.serv_numero_bits = txtNumeroBits.Text;
                serv.serv_memoria = String.IsNullOrEmpty(txtMemoria.Text) ? (decimal?)null : Convert.ToDecimal(txtMemoria.Text);
                serv.serv_procesadores = txtProcesadores.Text.ToUpper();
                serv.serv_core = String.IsNullOrEmpty(txtCore.Text) ? (decimal?)null : Convert.ToDecimal(txtCore.Text);
                serv.serv_disco_duro = String.IsNullOrEmpty(txtDiscoDuro.Text) ? (decimal?)null : Convert.ToDecimal(txtDiscoDuro.Text);
                serv.serv_descripcion_disco_duro = txtDescripcionDD.Text.ToUpper();
                serv.serv_aplicaciones_instaladas = txtAplicaciones.Text.ToUpper();
                serv.serv_virtualizado = Convert.ToInt32(cmbVirtualizado.Value.ToString());
                serv.serv_software_virtualizacion = txtSoftwareVirtualizacion.Text.ToUpper();
                serv.serv_granja_virtual = txtGranja.Text.ToUpper();
                serv.serv_ubicacion_fisica = txtUbicacionFisica.Text.ToUpper();
                serv.serv_activo_fijo = txtActivoFijo.Text;
                serv.serv_activo = Convert.ToInt32(cmbActivo.Value.ToString());
                serv.serv_depreciable = (cmbDepreciable.Value == null) ? (int?)null : Convert.ToInt32(cmbDepreciable.Value.ToString());
                serv.serv_usuario = strUsuario[0].ToString();
                serv.serv_fecha = dtFecha;

                if (!String.IsNullOrEmpty(txtId["txtId"].ToString()))
                {
                    serv.serv_consecutivo = Convert.ToInt32(txtId["txtId"].ToString());
                    CServidores.Update(serv);

                }
                else
                {
                    CServidores.Add(serv);

                }

                Session["mensaje"] = "OK";
                Response.Redirect("frmServidores.aspx", false);

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede guardar el registro" + ex.Message);
            }
        }
        #endregion

        protected void cmbVirtualizado_ValueChanged(object sender, EventArgs e)
        {
            if (cmbVirtualizado.Text.Equals("Si"))
            {
                txtSoftwareVirtualizacion.Enabled = true;
            }
            else
            {
                txtSoftwareVirtualizacion.Enabled = false;
            }
        }

    }
}