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
    public partial class frmCentroOperaciones : System.Web.UI.Page
    {
        CtrCentroOperacion ctrCentroOperaciones = new CtrCentroOperacion();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "ceop_consecutivo", "ceop_codigo", "ceop_descripcion", "ceop_vicepresidencia", "ceop_activo" };
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
        protected void CargarDatos()
        {
            try { 
            
                var list = ctrCentroOperaciones.GetAll();
                foreach (var I in list)
                {
                    if (I.ceop_activo == 1)
                        I.ceop_estadoStr = "Activo";
                    else
                        I.ceop_estadoStr = "Inactivo";
                }
                grid.DataSource = list;
                // grid.DataSource = ctrCentroOperaciones.GetAll();
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
        protected void IdGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("ceop_activo"))
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

            if (e.Column.FieldName.Equals("ceop_vicepresidencia"))
            {
                if (e.Value == null || e.Value.ToString().Equals("N"))
                {
                    e.DisplayText = "NO";
                }
                else
                {
                    e.DisplayText = "SI";
                }
            }  
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmCentroOperaciones_form.aspx");
        }

        protected void IdGrid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            camposSeleccionado = new Hashtable();
            foreach (string campo in camposClaseparametro)
            {
                camposSeleccionado[campo] = grid.GetRowValues(e.VisibleIndex, campo);
            }

            GE_TCENTROSOPERACION objeto = new GE_TCENTROSOPERACION();

            objeto.ceop_consecutivo = Convert.ToInt32(camposSeleccionado["ceop_consecutivo"].ToString());
            objeto.ceop_codigo = camposSeleccionado["ceop_codigo"].ToString();
            objeto.ceop_descripcion = camposSeleccionado["ceop_descripcion"].ToString();

            objeto.ceop_activo = Convert.ToInt32(camposSeleccionado["ceop_activo"].ToString());
            objeto.ceop_vicepresidencia = camposSeleccionado["ceop_vicepresidencia"].ToString();

            Session["objeto"] = objeto;
            Response.Redirect("frmCentroOperaciones_form.aspx");
        }

        #endregion 
    }
}