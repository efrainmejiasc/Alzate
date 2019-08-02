using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedeskiView.Controllers;
using DevExpress.Web;
using System.Collections;
using System.Data;

namespace MedeskiView.Forms
{
    public partial class frmParametros : System.Web.UI.Page
    {
        CtrParametros ctrParam = new CtrParametros();
        CtrVlrsParamGrales parametros = new CtrVlrsParamGrales();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] {"clap_clase", "clap_nombre", "clap_descripcion", "clap_estado", "clap_fechaini", "clap_fechafin" };

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + parametros.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                Session["objeto"] = null;

                if (Session["mensaje"] != null)
                {
                    VentanaValidaciones.mostrarConfirmarAccion("Registro satisfactorio", "¿Desea Registrar Parámetros para la Clase?");
                    Session["mensaje"] = null;
                }
                
                cargarDatos();
            }
            else
                Response.Redirect(strUrl);
        }

        public void cargarDatos()
        {
            try
            {
                IList<GE_TCLASESPARAMETROS> list = ctrParam.GetAll();
               foreach(var I in list)
              {
                    if (I.clap_estado == 1)
                        I.clap_estadoStr = "Activo";
                    else
                        I.clap_estadoStr = "Inactivo";
                }
                IdGrid.DataSource = list;
                IdGrid.DataBind();
                Cutilidades.ConfigurarGrid(IdGrid);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los registros. " + ex.Message);
            }
        }

        #region Eventos

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmParametros_form.aspx");
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmCParametros_form.aspx");
        }
        
        protected void IdGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("clap_estado"))
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

        protected void IdGrid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                camposSeleccionado = new Hashtable();
                foreach (string campo in camposClaseparametro)
                {
                    camposSeleccionado[campo] = IdGrid.GetRowValues(e.VisibleIndex, campo);
                }

                GE_TCLASESPARAMETROS objeto = new GE_TCLASESPARAMETROS();

                objeto.clap_clase = Convert.ToInt32(camposSeleccionado["clap_clase"].ToString());
                objeto.clap_nombre = camposSeleccionado["clap_nombre"].ToString();
                objeto.clap_descripcion = camposSeleccionado["clap_descripcion"].ToString();
                objeto.clap_fechaini = DateTime.Parse(camposSeleccionado["clap_fechaini"].ToString());
                
                objeto.clap_fechafin = (camposSeleccionado["clap_fechafin"] == null) ? (DateTime?) null : DateTime.Parse(camposSeleccionado["clap_fechafin"].ToString());
                objeto.clap_estado = Convert.ToInt32(camposSeleccionado["clap_estado"].ToString());


                Session["objeto"] = objeto;
                Session["clase"] = objeto;

                if (e.ButtonID == "btnConsultar")
                {
                    Response.Redirect("frmParametros_form.aspx");
                }
                else
                {
                    Response.Redirect("frmCParametros.aspx");
                }
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede consultar el registro. " + ex.Message);
            }            
        }

        #endregion
    }
}