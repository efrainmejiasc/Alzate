using DevExpress.Web;
using DevExpress.Web.Data;
using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using MedeskiView.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedeskiView.Forms
{
    public partial class frmDistribucionPersonas : System.Web.UI.Page
    {

        CtrDistribucionPersonas ctrPersonas = new CtrDistribucionPersonas();
        CtrProductos ctrProductos = new CtrProductos();
        CtrServidores ctrServiores = new CtrServidores();
        CtrDistribucionPersonas ctrDistribucionPersonas = new CtrDistribucionPersonas();
        CtrUtilidades utilidades = new CtrUtilidades();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        CtrParametros CParametros = new CtrParametros();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    Session["LISTA_TEMPORAL"] = null;
                    Session["grid"] = null;
                    Session["personas"] = null;
                    cargarListas();
                    utilidades.ConfigurarGrid(grid);
                }
                else
                {
                    gridPersonas.DataSource = Session["personas"];
                    gridPersonas.DataBind();

                    if (Session["grid"] != null)
                    {
                        grid.Visible = true;
                        grid.DataSource = Session["grid"];
                        grid.DataBind();                        
                    }
                }
            }
            else
                Response.Redirect(strUrl);
        }

        //Cargo las listas para iniciar la Pantalla
        public void cargarListas()
        {
            // cargarPersonas();
            cmbCriterioDistribucion.Items.Clear();
            cmbCriterioDistribucion.Items.Add("Seleccionar Criterio de Distribución", null);
            cmbCriterioDistribucion.Items.Add("Productos", 1);
            cmbCriterioDistribucion.Items.Add("Servidores", 2);

            if (cmbCriterioDistribucion.Items.Count == 2)
            {
                cmbCriterioDistribucion.Items[1].Selected = true;
                cargarPersonas();
            }

            // cargarPersonas();
            cmbAreas.Items.Clear();
            IList<GE_TPARAMETROS>  lstParams = CParametros.GetListbyClase("AREAS_PERSONAS");
            cmbAreas.Items.Clear();
            cmbAreas.Items.Add("Seleccionar Area", null);
            foreach (GE_TPARAMETROS param in lstParams)
            {
                cmbAreas.Items.Add(param.parm_descripcion, param.parm_consecutivo);
            }
            cmbCriterioDistribucion.SelectedIndex = 0;
            cmbAreas.SelectedIndex = 0;

        }

        public void limpiar()
        {
            Session["Ok"] = "0";
            Session["LISTA_TEMPORAL"] = null;
            Session["grid"] = null;
            grid.DataSource = null;
            grid.DataBind();

            Session["item"] = null;
            Session["personas"] = null;
            gridPersonas.DataSource = null;
            gridPersonas.DataBind();

            cmbCriterioDistribucion.Items.Clear();
            cmbAreas.Items.Clear();

            cargarListas();

            btnGuardar.Enabled = false;
        }


        //Cargo la gente que se cargo desde el formulario de
        //cargue de Gente.
        public void cargarPersonas()
        {
            IList<GE_TGENTE> lstPersonas = new List<GE_TGENTE>();
            lstPersonas = ctrPersonas.cargarPersonas();
            Session["personas"] = lstPersonas;
            gridPersonas.DataSource = lstPersonas;
            gridPersonas.DataBind();

            utilidades.ConfigurarGrid(gridPersonas);
        }

        public void filtrarPersonas()
        {
            IList<GE_TGENTE> lstPersonas = new List<GE_TGENTE>();
            lstPersonas = ctrPersonas.cargarPersonas().Where(x => x.GE_TPERSONAS.pers_nombre_area == Convert.ToInt32(cmbAreas.Value)).ToList();
            Session["personas"] = lstPersonas;
            gridPersonas.DataSource = lstPersonas;
            gridPersonas.DataBind();

            utilidades.ConfigurarGrid(gridPersonas);

            grid.DataSource = null;
            grid.DataBind();
        }

        protected void gridPersonas_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)        
        {
            try
            {
                int item = Convert.ToInt32(gridPersonas.GetRowValues(e.VisibleIndex, "gent_persona"));
                Session["item"] = item;
                
                if (cmbCriterioDistribucion.Value != null)
                {
                    grid.Visible = true;
                    cargarGrid(Convert.ToInt32(cmbCriterioDistribucion.Value), item);
                }
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los registros. " + ex.Message);
            }                        
        }

        public void cargarGrid(Int32 p_criterio, Int32 p_consecutivoPersona)
        {
            if (p_criterio == 1)
            {
                Session["TIPO_DISTRIBUCION"] = "PRODUCTOS";
                Session["LISTA_TEMPORAL"] = null;
                Session["grid"] = ctrDistribucionPersonas.organizaGridProductos(p_consecutivoPersona);
                grid.DataSource = Session["grid"];                
                grid.DataBind();
                btnGuardar.Enabled = true;
                utilidades.ScrollGrid(grid);
            }
            else if (p_criterio == 2)
            {
                Session["TIPO_DISTRIBUCION"] = "INFRAESTRUCTURA";
                Session["LISTA_TEMPORAL"] = null;
                Session["grid"] = ctrDistribucionPersonas.organizaGridInfraestructura(p_consecutivoPersona);
                grid.DataSource = Session["grid"];               
                grid.DataBind();
                btnGuardar.Enabled = true;
                utilidades.ScrollGrid(grid);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                grid.UpdateEdit();
                grid.DataBind();
                IList<DTOdistribucionPersonas> lstTemporal = Session["LISTA_TEMPORAL"] as List<DTOdistribucionPersonas>;

                if (lstTemporal != null)
                {
                    validaTotalPorcentaje(lstTemporal);
                    ctrDistribucionPersonas.organizaDistribucion(lstTemporal, Convert.ToInt32(Session["item"]), Session["TIPO_DISTRIBUCION"].ToString());
                    limpiar();
                    grid.FilterExpression = "";
                    VentanaValidaciones.mostrarRegistroExitoso();
                    limpiar();
                    cargarListas();
                }
            }
            catch (Exception ex)
            {
                Session["Ok"] = "0";
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pudo guardar el registro " + ex.Message);
            }
        }
        protected object GetTotalSummaryValue()
        {
            ASPxSummaryItem summaryItem = grid.TotalSummary.First(i => i.Tag == "TPorcentaje");
            return grid.GetTotalSummaryValue(summaryItem);
        }
        protected void grid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            CancelEditing(e);
        }

        protected void CancelEditing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void grid_RowUpdated(object sender, ASPxDataUpdatedEventArgs e)
        {
            grid.CancelEdit();
            grid.EndUpdate();
        }

        protected void grid_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            string v_actualizados = Session["Ok"] == null ? "0" : Session["Ok"].ToString();
            string v_tipoDistribucion = Session["TIPO_DISTRIBUCION"].ToString();
            string v_usuario = Session["Usuario"].ToString().Split(';')[0];

            try
            {
                IList<DTOdistribucionPersonas> lstCargada = Session["grid"] as List<DTOdistribucionPersonas>;
                if (v_tipoDistribucion.Equals("INFRAESTRUCTURA") && v_actualizados.Equals("0"))
                {

                    IList<DTOdistribucionPersonas> lstDistribuionInfraestructura = new List<DTOdistribucionPersonas>();
                    for (int i = 0; i < e.UpdateValues.Count; i++)
                    {
                        DTOdistribucionPersonas objServidoresActualizados = lstCargada.Where(b => b.consecutivo.ToString() == e.UpdateValues[i].Keys["consecutivo"].ToString()).FirstOrDefault();
                        objServidoresActualizados.vlrDistribucion = Convert.ToDecimal(e.UpdateValues[i].NewValues[0]);
                        objServidoresActualizados.tipo_distribucion = lstCargada.Where(a => a.descripcion == objServidoresActualizados.descripcion).Select(a => a.tipo_distribucion).SingleOrDefault().ToString();
                        objServidoresActualizados.usuarioSesion = v_usuario;

                        lstDistribuionInfraestructura.Add(objServidoresActualizados);
                    }
                    Session["LISTA_TEMPORAL"] = lstDistribuionInfraestructura;
                    Session["Ok"] = "1";
                }
                else if (v_tipoDistribucion.Equals("PRODUCTOS") && v_actualizados.Equals("0"))
                {
                    IList<DTOdistribucionPersonas> lstProductos = new List<DTOdistribucionPersonas>();
                    for (int i = 0; i < e.UpdateValues.Count; i++)
                    {
                        DTOdistribucionPersonas objProductosActualizados = lstCargada.Where(b => b.consecutivo.ToString() == e.UpdateValues[i].Keys["consecutivo"].ToString()).FirstOrDefault();
                        objProductosActualizados.vlrDistribucion = Convert.ToDecimal(e.UpdateValues[i].NewValues[0]); 
                        objProductosActualizados.tipo_distribucion = "Productos";
                        objProductosActualizados.usuarioSesion = v_usuario;

                        lstProductos.Add(objProductosActualizados);
                    }
                    Session["LISTA_TEMPORAL"] = lstProductos;
                    Session["Ok"] = "1";
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public void validaTotalPorcentaje(IList<DTOdistribucionPersonas> p_lstPantalla)
        {
            try
            {
                decimal valorTotalBaseDatos = 0;
                decimal valorTotalPantalla = 0;

                string v_tipoDistribucion = Session["TIPO_DISTRIBUCION"].ToString();
                IList<DTOdistribucionPersonas> lstCargada = Session["grid"] as List<DTOdistribucionPersonas>;

                foreach (var item in p_lstPantalla)
                {
                    DTOdistribucionPersonas objEliminar;
                    objEliminar = lstCargada.Where(a => a.consecutivoItem == item.consecutivoItem).Where(b => b.tipo_distribucion == item.tipo_distribucion).FirstOrDefault();
                    lstCargada.Remove(objEliminar);
                }

                if (v_tipoDistribucion.Equals("PRODUCTOS"))
                {
                    valorTotalBaseDatos = Convert.ToDecimal(lstCargada.Where(a => a.tipo_distribucion == "Productos").Sum(a => a.vlrDistribucion));
                    valorTotalPantalla = Convert.ToDecimal(p_lstPantalla.Where(a => a.tipo_distribucion == "Productos").Sum(a => a.vlrDistribucion));
                }
                else if (v_tipoDistribucion.Equals("INFRAESTRUCTURA"))
                {
                    valorTotalBaseDatos = Convert.ToDecimal(lstCargada.Where(a => (new string[] { "Producto-Infraestructura", "Infraestructura" }).Contains(a.tipo_distribucion)).Sum(a => a.vlrDistribucion));
                    valorTotalPantalla = Convert.ToDecimal(p_lstPantalla.Where(a => (new string[] { "Producto-Infraestructura", "Infraestructura" }).Contains(a.tipo_distribucion)).Sum(a => a.vlrDistribucion));
                }

                if ((valorTotalBaseDatos + valorTotalPantalla) > 100)
                    throw new Exception("La suma de los valores no puede pasar el 100 % del total de la Distribución");

            }
            catch
            {
                throw;
            }
        }

        protected void cmbCriterioDistribucion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarPersonas();
            cmbAreas.Value = null;

            Session["personas"] = null;

            gridPersonas.DataSource = null;
            gridPersonas.DataBind();

            grid.DataSource = null;
            grid.DataBind();

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void cmbAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAreas.Value == null)
            {
                cargarPersonas();
            }
            else
            {
                filtrarPersonas();
            }            
        }

    }
}