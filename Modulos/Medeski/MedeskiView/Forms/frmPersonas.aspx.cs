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
    public partial class frmPersonas : System.Web.UI.Page
    {
        IList<GE_TPARAMETROS> lstParams;
        IList<GE_TCENTROSCOSTOS> lstCCostos;
        IList<GE_TPERSONAS> lstPersonas;

        CtrParametros CParametros = new CtrParametros();
        CtrCentroCosto CCostos = new CtrCentroCosto();
        CtrPersonas CPersonas = new CtrPersonas();
        CtrCompanias CCompania = new CtrCompanias();
        CtrGente CGente = new CtrGente();
        CtrPeriodoPresupuesto CPeriodo = new CtrPeriodoPresupuesto();
        CtrUtilidades CUtilidades = new CtrUtilidades();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "pers_consecutivo", "pers_tipodoc", "pers_identificacion", 
                                                        "pers_nombre","pers_apellido","pers_nombres", "GE_TPERSONAS2.pers_nombres", 
                                                        "GE_TPARAMETROS.parm_descripcion", "GE_TPARAMETROS1.parm_descripcion",
                                                        "GE_TPARAMETROS2.parm_descripcion", "GE_TPARAMETROS3.parm_descripcion",
                                                        "GE_TPARAMETROS4.parm_descripcion", "GE_TCENTROSCOSTOS2.cost_codigo", 
                                                        "pers_usudom", "GE_TPARAMETROS5.parm_descripcion", "pers_nombre_busq", "pers_activo"
                                                        };

        #region Metodos
        

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                Session["objeto"] = null;
                    
                if (Session["mensaje"] != null)
                {
                    VentanaValidaciones.mostrarConfirmarAccion("Registro satisfactorio", "¿Desea asignar un costo a esta persona para el periodo activo?");
                    Session["mensaje"] = null;
                }
                else if (Session["mensaje2"] != null)
                {
                    VentanaValidaciones.mostrarRegistroExitoso();
                    Session["mensaje2"] = null;
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
                grid.DataSource = CPersonas.GetAllInfo();
                grid.DataBind();
                CUtilidades.ConfigurarGrid(grid);                
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarError("No se pudieron cargar los datos. " + ex.Message);
            }
        }

        #endregion


        #region Eventos
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmPersonas_form.aspx");
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmGente_form.aspx");
        }

        protected void IdGrid_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try{
                camposSeleccionado = new Hashtable();
                foreach (string campo in camposClaseparametro)
                {
                    camposSeleccionado[campo] = grid.GetRowValues(e.VisibleIndex, campo);
                }

                GE_TPERSONAS objeto = new GE_TPERSONAS();

                objeto.pers_consecutivo = Convert.ToInt32(camposSeleccionado["pers_consecutivo"].ToString());
                objeto.pers_tipodoc = camposSeleccionado["pers_tipodoc"].ToString();
                objeto.pers_identificacion = camposSeleccionado["pers_identificacion"].ToString();
                objeto.pers_nombre = camposSeleccionado["pers_nombre"].ToString();
                objeto.pers_apellido = camposSeleccionado["pers_apellido"].ToString();

                objeto.pers_usudom = camposSeleccionado["pers_usudom"] != null ? camposSeleccionado["pers_usudom"].ToString() : "";
                objeto.pers_nombre_busq = camposSeleccionado["pers_nombre_busq"] != null ? camposSeleccionado["pers_nombre_busq"].ToString() : "";

                GE_TPARAMETROS param = new GE_TPARAMETROS();
                param.parm_descripcion = camposSeleccionado["GE_TPARAMETROS.parm_descripcion"] != null ? camposSeleccionado["GE_TPARAMETROS.parm_descripcion"].ToString() : "";
                objeto.GE_TPARAMETROS = param; 
                
                GE_TPARAMETROS param1 = new GE_TPARAMETROS();
                param1.parm_descripcion = camposSeleccionado["GE_TPARAMETROS1.parm_descripcion"] != null ? camposSeleccionado["GE_TPARAMETROS1.parm_descripcion"].ToString() : "";
                objeto.GE_TPARAMETROS1 = param1;
                
                GE_TPARAMETROS param2 = new GE_TPARAMETROS();
                param2.parm_descripcion = camposSeleccionado["GE_TPARAMETROS2.parm_descripcion"] != null ? camposSeleccionado["GE_TPARAMETROS2.parm_descripcion"].ToString() : "";
                objeto.GE_TPARAMETROS2 = param2;

                GE_TPARAMETROS param3 = new GE_TPARAMETROS();
                param3.parm_descripcion = camposSeleccionado["GE_TPARAMETROS3.parm_descripcion"] != null ? camposSeleccionado["GE_TPARAMETROS3.parm_descripcion"].ToString() : "";
                objeto.GE_TPARAMETROS3 = param3;

                GE_TPARAMETROS param4 = new GE_TPARAMETROS();
                param4.parm_descripcion = camposSeleccionado["GE_TPARAMETROS4.parm_descripcion"] != null ? camposSeleccionado["GE_TPARAMETROS4.parm_descripcion"].ToString() : "";
                objeto.GE_TPARAMETROS4 = param4;
                
                GE_TPARAMETROS param5 = new GE_TPARAMETROS();
                param5.parm_descripcion = camposSeleccionado["GE_TPARAMETROS5.parm_descripcion"] != null ? camposSeleccionado["GE_TPARAMETROS5.parm_descripcion"].ToString() : "";
                objeto.GE_TPARAMETROS5 = param5;
                
                GE_TCENTROSCOSTOS ceop2 = new GE_TCENTROSCOSTOS();
                ceop2.cost_codigo = camposSeleccionado["GE_TCENTROSCOSTOS2.cost_codigo"].ToString();
                objeto.GE_TCENTROSCOSTOS2 = ceop2;

                GE_TPERSONAS pers2 = new GE_TPERSONAS();
                pers2.pers_nombres = camposSeleccionado["GE_TPERSONAS2.pers_nombres"].ToString();
                objeto.GE_TPERSONAS2 = pers2;

                objeto.pers_activo = Convert.ToInt32(camposSeleccionado["pers_activo"].ToString());

                Session["objeto"] = objeto;
                Session["persona"] = objeto;

                if (e.ButtonID == "btnConsultar")
                {
                    Response.Redirect("frmPersonas_form.aspx");
                }
                else
                {
                    Response.Redirect("frmGente_form.aspx");
                }

            }
            catch( Exception ex){
                VentanaValidaciones.mostrarError("No se pueden cargar los datos en los campos. " + ex.Message);
            }
        }

        protected void IdGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("pers_activo"))
            {
                if (Convert.ToInt32(e.Value) == 1 ) 
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