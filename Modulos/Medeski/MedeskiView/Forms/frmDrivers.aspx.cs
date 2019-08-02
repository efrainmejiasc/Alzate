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
    public partial class frmDrivers : System.Web.UI.Page
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

        protected void CargarDatos() 
        {
            try
            {
                var list = ctrDrivers.GetAll();
                foreach (var I in list)
                {
                    if (I.driv_activo == "1")
                        I.driv_estadoStr = "Activo";
                    else
                        I.driv_estadoStr = "Inactivo";
                }
                //grid.DataSource = ctrDrivers.GetAll();
                grid.DataSource = list;
                grid.DataBind();
                CUtilidades.ConfigurarGrid(grid);
            }
            catch(Exception e)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede cargar los datos.  " + e.Message);
            }
        }
        #endregion

        #region Eventos
        protected void NuevoClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmDrivers_form.aspx");
        }

        protected void IdGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("driv_activo"))
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

            else if (e.Column.FieldName.Equals("driv_tipo_cobro"))
            {
                if (e.Value.Equals("T"))
                {
                    e.DisplayText = "TOTAL";
                }
                else
                {
                    e.DisplayText = "UNO A UNO";
                }
            }

            else if (e.Column.FieldName.Equals("driv_aplica_valor"))
            {
                if (e.Value.Equals("S"))
                {
                    e.DisplayText = "SI";
                }
                else
                {
                    e.DisplayText = "NO";
                }
            }

            else if (e.Column.FieldName.Equals("driv_aplica_proveedor"))
            {
                if (e.Value.Equals("S"))
                {
                    e.DisplayText = "SI";
                }
                else
                {
                    e.DisplayText = "NO";
                }
            }

            else if (e.Column.FieldName.Equals("driv_aplica_sede"))
            {
                if (e.Value.Equals("S"))
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
            campoSeleccionado = new Hashtable();
            foreach (string campo in camposClaseparametro)
            {
                campoSeleccionado[campo] = grid.GetRowValues(e.VisibleIndex, campo);
            }

            GE_TDRIVERS objeto = new GE_TDRIVERS();

            objeto.driv_consecutivo = Convert.ToInt32(campoSeleccionado["driv_consecutivo"].ToString());
            objeto.driv_nombre = campoSeleccionado["driv_nombre"].ToString();
            objeto.driv_descripcion = campoSeleccionado["driv_descripcion"].ToString();            
            objeto.driv_tipo_cobro = (campoSeleccionado["driv_tipo_cobro"].ToString() == null) ? null : campoSeleccionado["driv_tipo_cobro"].ToString();
            objeto.driv_aplica_sede = (campoSeleccionado["driv_aplica_sede"].ToString() == null) ? null : campoSeleccionado["driv_aplica_sede"].ToString();
            objeto.driv_aplica_valor = (campoSeleccionado["driv_aplica_valor"].ToString() == null) ? null : campoSeleccionado["driv_aplica_valor"].ToString();
            objeto.driv_aplica_proveedor = (campoSeleccionado["driv_aplica_proveedor"].ToString() == null) ? null : campoSeleccionado["driv_aplica_proveedor"].ToString();
            objeto.driv_activo = campoSeleccionado["driv_activo"].ToString();
            
            Session["objeto"] = objeto;
            Response.Redirect("frmDrivers_form.aspx");
        }
        #endregion
    }
}