using DevExpress.Web;
using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
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
    public partial class frmAnoPresupuesto : System.Web.UI.Page
    {
        CtrPeriodoPresupuesto Cperiodo = new CtrPeriodoPresupuesto();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "peri_consecutivo", "peri_ano", "peri_paso", "peri_activo" };

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
                var list = Cperiodo.GetAll().OrderBy(x => x.peri_ano).ThenBy(x => x.peri_paso);
                foreach (var I in list)
                {
                    if (I.peri_activo == 1)
                        I.peri_estadoStr = "Activo";
                    else
                        I.peri_estadoStr = "Inactivo";
                }
                //grid.DataSource = Cperiodo.GetAll().OrderBy(x => x.peri_ano).ThenBy(x => x.peri_paso);
                grid.DataSource = list;
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
            Response.Redirect("frmAnoPresupuesto_form.aspx");
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

                GE_TPERIODOPRESUPUESTO objeto = new GE_TPERIODOPRESUPUESTO();

                objeto.peri_consecutivo = Convert.ToInt32(camposSeleccionado["peri_consecutivo"].ToString());
                objeto.peri_paso = Convert.ToInt32(camposSeleccionado["peri_paso"].ToString());
                objeto.peri_ano = Convert.ToInt32(camposSeleccionado["peri_ano"].ToString());
                objeto.peri_activo = Convert.ToInt32(camposSeleccionado["peri_activo"].ToString());

                Session["objeto"] = objeto;
                Response.Redirect("frmAnoPresupuesto_form.aspx");
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede consultar el registro. " + ex.Message);
            }
        }


        protected void IdGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("peri_activo"))
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