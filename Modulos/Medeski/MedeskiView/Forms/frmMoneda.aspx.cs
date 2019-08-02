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
    public partial class frmMoneda : System.Web.UI.Page
    {
        CtrPeriodoPresupuesto Cperiodo = new CtrPeriodoPresupuesto();
        CtrCParametros Cparametros = new CtrCParametros();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        
        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "parm_consecutivo", "parm_descripcion", "parm_codigo", "parm_estado" };

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
            {
                Response.Redirect(strUrl);
            }
        }

        #region Metodos
        public void CargarDatos()
        {
            try
            {
                var list = Cparametros.GetListbyClase("MONEDA").Where(x => x.parm_codigo != "COP");

                foreach (var I in list)
                {
                    if (I.parm_estado == 1)
                        I.parm_estadoStr = "Activo";
                    else
                        I.parm_estadoStr = "Inactivo";
                }
                grid.DataSource = list;
                // grid.DataSource = Cparametros.GetListbyClase("MONEDA").Where(x => x.parm_codigo != "COP");
                grid.DataBind();
                Cutilidades.ConfigurarGrid(grid);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los datos. " + ex.Message);
            }
        }
        #endregion

        #region Eventos

        protected void IdGrid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                camposSeleccionado = new Hashtable();
                foreach (string campo in camposClaseparametro)
                {
                    camposSeleccionado[campo] = grid.GetRowValues(e.VisibleIndex, campo);
                }

                GE_TPARAMETROS objeto = new GE_TPARAMETROS();

                objeto.parm_consecutivo = Convert.ToInt32(camposSeleccionado["parm_consecutivo"].ToString());
                objeto.parm_descripcion = camposSeleccionado["parm_descripcion"].ToString();
                objeto.parm_estado = Convert.ToInt32(camposSeleccionado["parm_estado"].ToString());
                objeto.parm_codigo = camposSeleccionado["parm_codigo"].ToString();

                Session["objeto"] = objeto;
                Response.Redirect("frmMonedaItems.aspx");
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede consultar el registro. " + ex.Message);
            }
        }


        protected void IdGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("parm_estado"))
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
        #endregion
    }
}