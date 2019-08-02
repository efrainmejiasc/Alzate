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
    public partial class frmMonedaItems : System.Web.UI.Page
    {
        CtrPeriodoPresupuesto Cperiodo = new CtrPeriodoPresupuesto();
        CtrVarEconomicas varEconomicas = new CtrVarEconomicas();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        
        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "vari_consecutivo", "vari_valor", "vari_ano", "vari_tipo_moneda", "GE_TPARAMETROS.parm_descripcion", "GE_TPARAMETROS1.parm_descripcion", "vari_activo" };

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                Session["items"] = null;

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
                GE_TPARAMETROS mObjeto = Session["objeto"] as GE_TPARAMETROS;
                grid.DataSource = varEconomicas.GetByMonedaAno(mObjeto.parm_consecutivo, Cperiodo.GetPeriodoActivo().peri_ano);
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
        
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmMonedaItems_form.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmMoneda.aspx");
        }

        protected void IdGrid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                camposSeleccionado = new Hashtable();
                foreach (string campo in camposClaseparametro)
                {
                    camposSeleccionado[campo] = grid.GetRowValues(e.VisibleIndex, campo);
                }

                GE_TPARAMETROS param2 = new GE_TPARAMETROS();
                param2.parm_descripcion = camposSeleccionado["GE_TPARAMETROS1.parm_descripcion"].ToString();
                
                GE_TVARECONOMICAS objeto = new GE_TVARECONOMICAS();
                objeto.GE_TPARAMETROS1 = param2;
                objeto.vari_consecutivo = Convert.ToInt32(camposSeleccionado["vari_consecutivo"].ToString());
                objeto.vari_valor = Convert.ToDecimal(camposSeleccionado["vari_valor"].ToString());
                objeto.vari_activo = Convert.ToInt32(camposSeleccionado["vari_activo"].ToString());
                objeto.vari_tipo_moneda = Convert.ToInt32(camposSeleccionado["vari_tipo_moneda"].ToString());
                objeto.vari_ano = Convert.ToInt32(camposSeleccionado["vari_ano"].ToString());

                Session["items"] = objeto;
                Response.Redirect("frmMonedaItems_form.aspx");
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede consultar el registro. " + ex.Message);
            }
        }


        protected void IdGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("vari_activo"))
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