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
    public partial class frmServidores : System.Web.UI.Page
    {
        CtrUtilidades Cutilidades;
        CtrServidores CServidores;
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
                Cutilidades = new CtrUtilidades();
                CServidores = new CtrServidores();
                gvServidores.DataSource = CServidores.GetAllGridView();
                gvServidores.DataBind();
                Cutilidades.ConfigurarGrid(gvServidores);
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
            Response.Redirect("frmServidores_form.aspx");
        }

        protected void grid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                if (e.ButtonID != "btnConsultar" && e.ButtonID != "btnEliminar") return;

                servSeleccionado = new Hashtable();
                foreach (string campo in camposServidor)
                {
                    servSeleccionado[campo] = gvServidores.GetRowValues(e.VisibleIndex, campo);
                }

                GE_TSERVIDORES objeto = new GE_TSERVIDORES();

                int? nulo = null;
                decimal? nuloDec = null;

                objeto.serv_consecutivo = Convert.ToInt32(servSeleccionado["serv_consecutivo"].ToString());
                objeto.ser_marca = (servSeleccionado["ser_marca"] == null) ? "" : servSeleccionado["ser_marca"].ToString();
                objeto.ser_modelo = (servSeleccionado["ser_modelo"] == null) ? "" : servSeleccionado["ser_modelo"].ToString();
                objeto.serv_funcion = (servSeleccionado["serv_funcion"] == null) ? "" : servSeleccionado["serv_funcion"].ToString();
                objeto.serv_estado = (servSeleccionado["serv_estado"] == null) ? "" : servSeleccionado["serv_estado"].ToString();
                objeto.serv_ip = (servSeleccionado["serv_ip"] == null) ? "" : servSeleccionado["serv_ip"].ToString();
                objeto.ser_sistema_operativo = (servSeleccionado["ser_sistema_operativo"] == null) ? "" : servSeleccionado["ser_sistema_operativo"].ToString();
                objeto.serv_numero_bits = (servSeleccionado["serv_numero_bits"] == null) ? "" : servSeleccionado["serv_numero_bits"].ToString();
                objeto.serv_procesadores = (servSeleccionado["serv_procesadores"] == null) ? "" : servSeleccionado["serv_procesadores"].ToString();
                objeto.serv_descripcion_disco_duro = (servSeleccionado["serv_descripcion_disco_duro"] == null) ? "" : servSeleccionado["serv_descripcion_disco_duro"].ToString();
                objeto.serv_aplicaciones_instaladas = (servSeleccionado["serv_aplicaciones_instaladas"] == null) ? "" : servSeleccionado["serv_aplicaciones_instaladas"].ToString();
                objeto.serv_virtualizado = (servSeleccionado["serv_virtualizado"] == null) ? nulo : Convert.ToInt32(servSeleccionado["serv_virtualizado"].ToString());
                objeto.serv_software_virtualizacion = (servSeleccionado["serv_software_virtualizacion"] == null) ? "" : servSeleccionado["serv_software_virtualizacion"].ToString();
                objeto.serv_granja_virtual = (servSeleccionado["serv_granja_virtual"] == null) ? "" : servSeleccionado["serv_granja_virtual"].ToString();
                objeto.serv_ubicacion_fisica = (servSeleccionado["serv_ubicacion_fisica"] == null) ? "" : servSeleccionado["serv_ubicacion_fisica"].ToString();
                objeto.serv_activo = Convert.ToInt32(servSeleccionado["serv_activo"].ToString());
                objeto.serv_nombre = (servSeleccionado["serv_nombre"] == null) ? "" : servSeleccionado["serv_nombre"].ToString();
                objeto.serv_memoria = (servSeleccionado["serv_memoria"] == null) ? nuloDec : Convert.ToDecimal(servSeleccionado["serv_memoria"].ToString());
                objeto.serv_disco_duro = (servSeleccionado["serv_disco_duro"] == null) ? nuloDec : Convert.ToDecimal(servSeleccionado["serv_disco_duro"].ToString());
                objeto.serv_memoria = (servSeleccionado["serv_memoria"] == null) ? nuloDec : Convert.ToDecimal(servSeleccionado["serv_memoria"].ToString());
                objeto.serv_activo_fijo = (servSeleccionado["serv_activo_fijo"] == null) ? "" : servSeleccionado["serv_activo_fijo"].ToString();
                objeto.serv_depreciable = (servSeleccionado["serv_depreciable"].ToString() == null) ? nulo : Convert.ToInt32(servSeleccionado["serv_virtualizado"].ToString());
                
                Session["objeto"] = objeto;
                Response.Redirect("frmServidores_form.aspx");
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede consultar el registro. " + ex.Message);
            }
        }

        #endregion
    }
}