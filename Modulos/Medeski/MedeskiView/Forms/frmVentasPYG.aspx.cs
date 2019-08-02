using DevExpress.Web;
using MedeskiView.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedeskiView.Forms
{
    public partial class frmVentasPYG : System.Web.UI.Page
    {
        CtrVWCuadroVentasPYG CtrVentas = new CtrVWCuadroVentasPYG();
        CtrHistorialPYG CtrHistorial = new CtrHistorialPYG();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        CtrUtilidades utilidades = new CtrUtilidades();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    Session["DataSourceT"] = null;
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


                IList<VW_CUADRO_VENTAS_PYG> lista = CtrVentas.GetAll();

                decimal sumaDirectos = 0;
                decimal sumaIndirectos = 0;

                foreach (var item in lista)
                {
                    sumaDirectos += Convert.ToDecimal(item.directos);
                    sumaIndirectos += Convert.ToDecimal(item.indirectos);
                }

                txtSumaDirectos["txtSumaDirectos"] = Convert.ToInt64(sumaDirectos);
                lblSumaDirectos["lblSumaDirectos"] = Convert.ToInt64(sumaDirectos);

                txtSumaIndirectos["txtSumaIndirectos"] = Convert.ToInt64(sumaIndirectos);
                lblSumaIndirectos["lblSumaIndirectos"] = Convert.ToInt64(sumaIndirectos);

                Session["DataSourceT"] = Session["DataSourceT"] != null ? Session["DataSourceT"] : lista;

                grid_Driver.DataSource = lista;
                grid_Driver.DataBind();
                grid_Driver.SettingsPager.Summary.Visible = true;

                utilidades.ScrollGrid(grid_Driver);
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarError("Error Cargando Datos. " + ex.Message);
            }
        }
        
        private void Limpiar()
        {
            Session["DataSourceT"] = null;
            grid_Driver.DataBind();
        }

        #endregion

        #region Eventos
        protected void grid_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = CustomDataSourceT;
        }

        protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            UpdateItem(e.Keys, e.NewValues);
            CancelEditing(e);
        }
        
        protected void CancelEditing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            grid_Driver.CancelEdit();        
        }

        private IList<VW_CUADRO_VENTAS_PYG> CustomDataSource
        {
            get
            {
                IList<VW_CUADRO_VENTAS_PYG> result = Session["DataSource"] as IList<VW_CUADRO_VENTAS_PYG>;
                Session["DataSource"] = result;
                return result;
            }
        }

        private IList<VW_CUADRO_VENTAS_PYG> CustomDataSourceT
        {
            get
            {
                IList<VW_CUADRO_VENTAS_PYG> result = Session["DataSourceT"] as IList<VW_CUADRO_VENTAS_PYG>;
                Session["DataSourceT"] = result;
                return result;
            }
        }

        protected void UpdateItem(OrderedDictionary keys, OrderedDictionary newValues)
        {
            var id = keys["ceop_codigo"];
            IList<VW_CUADRO_VENTAS_PYG> iList = Session["DataSourceT"] as IList<VW_CUADRO_VENTAS_PYG>;

            foreach (VW_CUADRO_VENTAS_PYG d in iList)
            {
                if (d.ceop_codigo.Equals(id))
                {
                    d.ceop_codigo = newValues["ceop_codigo"].ToString();

                    d.forecast_directos = Convert.ToDecimal(newValues["forecast_directos"]);
                    d.forecast_indirectos = Convert.ToDecimal(newValues["forecast_indirectos"]);
                    d.forecast_ventas = Convert.ToDecimal(newValues["forecast_ventas"]);

                    d.directos = Convert.ToDecimal(newValues["directos"]);
                    d.indirectos = Convert.ToDecimal(newValues["indirectos"]);
                    d.ventas = Convert.ToDecimal(newValues["ventas"]);

                    break;
                }
            }
            Session["DataSourceT"] = iList;
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                grid_Driver.UpdateEdit();

                long lblDirectos = Convert.ToInt64(lblSumaDirectos["lblSumaDirectos"].ToString());                   
                long valorDirectos = Convert.ToInt64(txtSumaDirectos["txtSumaDirectos"].ToString());

                long lblIndirectos = Convert.ToInt64(lblSumaIndirectos["lblSumaIndirectos"].ToString());
                long valorIndirectos = Convert.ToInt64(txtSumaIndirectos["txtSumaIndirectos"].ToString());

                if (lblDirectos != valorDirectos) {
                    VentanaValidaciones.mostrarError("Los valores Directos No coinciden con " + string.Format("{0:C0}", valorDirectos));
                    return;
                } else if (lblIndirectos != valorIndirectos) {
                    VentanaValidaciones.mostrarError("Los valores Indirectos No coinciden con " + string.Format("{0:C0}", valorIndirectos));
                    return;
                } else {

                    IList<VW_CUADRO_VENTAS_PYG> iList = (IList<VW_CUADRO_VENTAS_PYG>)grid_Driver.DataSource;

                    IList<GE_THISTORICOPYG> activos = new CtrHistorialPYG().GetAllActive();
                    
                    // Periodo Activo
                    CtrPeriodoPresupuesto periodo = new CtrPeriodoPresupuesto();
                    IList<GE_TPERIODOPRESUPUESTO> lstPeriodo = periodo.GetAllActive();
                    GE_TPERIODOPRESUPUESTO objPeriodo = lstPeriodo.Where(x => x.peri_activo == 1).First();

                    Char delimiter = ';';
                    string[] strUsuario = null;
                    strUsuario = Session["usuario"].ToString().Split(delimiter);
                    
                    foreach(VW_CUADRO_VENTAS_PYG d in iList)
                    {
                        // CeOp
                        CtrCentroOperacion ceop = new CtrCentroOperacion();

                        IEnumerable<GE_THISTORICOPYG> historicos = activos.Where(x => x.vent_periodo == objPeriodo.peri_consecutivo &&
                            x.vent_ceop == ceop.GetSingle(d.ceop_codigo).ceop_consecutivo
                        );

                        if (historicos.Count() > 0)
                        {
                            foreach (var item in historicos)
                            {
                                item.vent_fecha_act = DateTime.Now;
                                item.vent_usuario_act = strUsuario[0].ToString().ToUpper();
                                item.vent_activo = 0;
                                CtrHistorial.Update(item);
                            }
                        }

                        // PPTO
                        GE_THISTORICOPYG nuevo = new GE_THISTORICOPYG();
                                                            
                        nuevo.vent_ceop = ceop.GetSingle(d.ceop_codigo).ceop_consecutivo;
                        nuevo.vent_periodo = objPeriodo.peri_consecutivo;

                        nuevo.vent_fecha = DateTime.Now;
                        nuevo.vent_usuario = strUsuario[0].ToString().ToUpper();
                        nuevo.vent_fecha_act = DateTime.Now;
                        nuevo.vent_usuario_act = strUsuario[0].ToString().ToUpper();
                    
                        nuevo.vent_valor_directos = Convert.ToDecimal(d.directos);
                        nuevo.vent_valor_indirectos = Convert.ToDecimal(d.indirectos);
                        nuevo.vent_valor_ventas = Convert.ToDecimal(d.ventas);

                        nuevo.vent_tipo = "PPTO";
                        nuevo.vent_activo = 1;
                        CtrHistorial.Add(nuevo);

                        // FORECAST
                        GE_THISTORICOPYG forecast = new GE_THISTORICOPYG();

                        forecast.vent_ceop = ceop.GetSingle(d.ceop_codigo).ceop_consecutivo;
                        forecast.vent_periodo = objPeriodo.peri_consecutivo;

                        forecast.vent_fecha = DateTime.Now;
                        forecast.vent_usuario = strUsuario[0].ToString().ToUpper();
                        forecast.vent_fecha_act = DateTime.Now;
                        forecast.vent_usuario_act = strUsuario[0].ToString().ToUpper();

                        forecast.vent_valor_directos = Convert.ToDecimal(d.forecast_directos);
                        forecast.vent_valor_indirectos = Convert.ToDecimal(d.forecast_indirectos);
                        forecast.vent_valor_ventas = Convert.ToDecimal(d.forecast_ventas);

                        forecast.vent_tipo = "FORE";
                        forecast.vent_activo = 1;
                        CtrHistorial.Add(forecast);


                    }

                    Limpiar();
                    CargarDatos();
                    VentanaValidaciones.mostrarRegistroExitoso();
                }
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarError("Error al guardar. " + ex.Message);
            }
        }

        protected object GetTotalesXfila(string fila)
        {
            ASPxSummaryItem summaryItem = grid_Driver.TotalSummary.First(i => i.Tag == fila);
            return grid_Driver.GetTotalSummaryValue(summaryItem);
        }

        #endregion
    }
}