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
    public partial class frmGente : System.Web.UI.Page
    {
        CtrUtilidades CUtilidades = new CtrUtilidades();
        CtrPeriodoPresupuesto CPeriodo = new CtrPeriodoPresupuesto();
        CtrGente CGente = new CtrGente();

        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "gent_consecutivo", "GE_TPERIODOPRESUPUESTO.peri_consecutivo", "GE_TPERSONAS.pers_consecutivo", "GE_TPERSONAS.pers_identificacion", 
                                                        "GE_TCENTROSCOSTOS.cost_consecutivo", "GE_TCENTROSCOSTOS.cost_descripcion", "GE_TPERSONAS.GE_TPARAMETROS1.parm_descripcion", 
                                                        "GE_TPERSONAS.GE_TPARAMETROS.parm_descripcion", "GE_TPERSONAS.pers_nombre", "GE_TPERSONAS.pers_apellido",
                                                        "GE_TPERIODOPRESUPUESTO.peri_ano", "GE_TCENTROSCOSTOS.cost_codigo",
                                                        "gent_costo_colaborador", "gent_porcentaje_manual_dedicacion", "gent_estado" };
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

        private void CargarDatos()
        {
            try
            {

                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                IList<GE_TGENTE> gente = CGente.GetAllInfo(strUsuario[0].ToString());
                grid.DataSource = gente;
                grid.DataBind();
                CUtilidades.ConfigurarGrid(grid);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarError("No se pudo cargar los datos. " + ex.Message);
            }
            
        }

        #endregion

        #region Eventos

        protected void IdGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("gent_estado"))
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

        protected void grid_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            camposSeleccionado = new Hashtable();
            foreach (string campo in camposClaseparametro)
            {
                camposSeleccionado[campo] = grid.GetRowValues(e.VisibleIndex, campo);
            }

            GE_TGENTE objeto = new GE_TGENTE();

            objeto.gent_consecutivo = Convert.ToInt32(camposSeleccionado["gent_consecutivo"].ToString());
            
            GE_TPERIODOPRESUPUESTO periodo = new GE_TPERIODOPRESUPUESTO();
            periodo.peri_consecutivo = Convert.ToInt32(camposSeleccionado["GE_TPERIODOPRESUPUESTO.peri_consecutivo"].ToString());
            objeto.GE_TPERIODOPRESUPUESTO = periodo;

            GE_TPERSONAS personas = new GE_TPERSONAS();
            personas.pers_consecutivo = Convert.ToInt32(camposSeleccionado["GE_TPERSONAS.pers_consecutivo"].ToString());
            personas.pers_identificacion = camposSeleccionado["GE_TPERSONAS.pers_identificacion"].ToString();
            personas.pers_nombre = camposSeleccionado["GE_TPERSONAS.pers_nombre"].ToString();
            personas.pers_apellido = camposSeleccionado["GE_TPERSONAS.pers_apellido"].ToString();            
            objeto.GE_TPERSONAS = personas;

            GE_TCENTROSCOSTOS ccostos = new GE_TCENTROSCOSTOS();
            ccostos.cost_consecutivo = Convert.ToInt32(camposSeleccionado["GE_TCENTROSCOSTOS.cost_consecutivo"].ToString());
            ccostos.cost_descripcion = camposSeleccionado["GE_TCENTROSCOSTOS.cost_descripcion"].ToString();
            ccostos.cost_codigo = camposSeleccionado["GE_TCENTROSCOSTOS.cost_codigo"].ToString();
            
            GE_TPARAMETROS param = new GE_TPARAMETROS();
            param.parm_descripcion = camposSeleccionado["GE_TPERSONAS.GE_TPARAMETROS.parm_descripcion"].ToString();
            personas.GE_TPARAMETROS = param;

            GE_TPARAMETROS param1 = new GE_TPARAMETROS();
            param1.parm_descripcion = camposSeleccionado["GE_TPERSONAS.GE_TPARAMETROS1.parm_descripcion"].ToString();
            personas.GE_TPARAMETROS1 = param1;

            objeto.GE_TPERSONAS = personas;
            
            objeto.gent_porcentaje_manual_dedicacion = Convert.ToDecimal(camposSeleccionado["gent_porcentaje_manual_dedicacion"].ToString());
            objeto.gent_costo_colaborador = Convert.ToDecimal(camposSeleccionado["gent_costo_colaborador"].ToString());

            objeto.gent_estado = Convert.ToInt32(camposSeleccionado["gent_estado"].ToString());

            Session["objeto"] = objeto;
            Response.Redirect("frmGente_form.aspx");
        }

        protected void NuevoClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmGente_form.aspx");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmPersonas.aspx");
        }

        #endregion
    }
}