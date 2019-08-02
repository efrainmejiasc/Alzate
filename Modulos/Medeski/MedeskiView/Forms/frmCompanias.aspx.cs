using DevExpress.Web;
using MedeskiView.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedeskiView.Forms
{
    public partial class frmCompanias : System.Web.UI.Page
    {
        CtrCompanias CtrCompanias = new CtrCompanias();
        CtrUtilidades CUtilidades = new CtrUtilidades();
        Hashtable campoSeleccionado = null;
        string[] camposClaseparametro = new string[] { "comp_consecutivo", "comp_nombre", "comp_descripcion", "comp_usa_co", "comp_activo" };
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                Session["objeto"] = null;

                if (Session["mensaje"] != null)
                {
                    VentanaValidaciones.mostrarConfirmarAccion("Registro satisfactorio", "¿Desea Registrar Centros de Costos para la Compañia?");
                    Session["mensaje"] = null;
                } 
                
                CargarDatos();
            }
            else
                Response.Redirect(strUrl);
        }
        
        #region Metodos
        protected void CargarDatos()
        {
            try
            {
                var list = CtrCompanias.GetAll();
                foreach (var I in list)
                {
                    if (I.comp_activo != null)
                    {
                        if (I.comp_activo == 1)
                            I.comp_estadoStr = "Activo";
                        else
                            I.comp_estadoStr = "Inactivo";
                    }
                }
                //grid.DataSource = CtrCompanias.GetAll();
                grid.DataSource = list;
                grid.DataBind();
                CUtilidades.ConfigurarGrid(grid);
            }
            catch (Exception e)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede cargar los datos.  " + e.Message);
            }
        }
        #endregion

        #region Eventos
        protected void NuevoClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmCompanias_form.aspx");
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmCentroCostos_form.aspx");
        }

        protected void IdGrid_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("comp_activo"))
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
            else if (e.Column.FieldName.Equals("comp_usa_co"))
            {
                if (Convert.ToInt32(e.Value) == 1)
                {
                    e.DisplayText = "SI";
                }
                else
                {
                    e.DisplayText = "NO";
                }
            }
        }

        protected void grid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                campoSeleccionado = new Hashtable();
                foreach (string campo in camposClaseparametro)
                {
                    campoSeleccionado[campo] = grid.GetRowValues(e.VisibleIndex, campo);
                }

                GE_TCOMPANIAS objeto = new GE_TCOMPANIAS();

                objeto.comp_consecutivo = Convert.ToInt32(campoSeleccionado["comp_consecutivo"].ToString());
                objeto.comp_nombre = campoSeleccionado["comp_nombre"].ToString();
                objeto.comp_descripcion = campoSeleccionado["comp_descripcion"].ToString();

                int? nulo = null;
                objeto.comp_usa_co = (campoSeleccionado["comp_usa_co"].ToString() == null) ? nulo : Convert.ToInt32(campoSeleccionado["comp_usa_co"].ToString()); 
                objeto.comp_activo = Convert.ToInt32(campoSeleccionado["comp_activo"].ToString());

                Session["objeto"] = objeto;
                Session["compania"] = objeto;

                if (e.ButtonID == "btnConsultar")
                {
                    Response.Redirect("frmCompanias_form.aspx");
                }
                else
                {
                    Response.Redirect("frmCentroCostos.aspx");
                }
                
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede consultar el registro. " + ex.Message);
            }
        }
        #endregion
    }
}