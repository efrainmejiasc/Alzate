using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using MedeskiView.Controllers;

namespace MedeskiView.Forms
{
    public partial class frmRedistribucion : System.Web.UI.Page
    {
        CtrProductos Cproducto;
        CtrUtilidades Cutilidades;
        CtrPeriodoPresupuesto Cperiodo;
        CtrVwVlrRedistribucion CVwVlrRedist;
        CtrRedistribucion Credist;        
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                Cperiodo = new CtrPeriodoPresupuesto();
                Session["Periodo"] = 0;
                IList<GE_TPERIODOPRESUPUESTO> per = Cperiodo.GetAllActive();

                if (per.Count > 0)
                    Session["Periodo"] = per[0].peri_consecutivo;

                CargarDatos();
                CargarListas();
            }
            else
                Response.Redirect(strUrl);
        }

        #region Metodos
        public void CargarDatos()
        {
            try
            {
                Cutilidades = new CtrUtilidades();
                Cutilidades.ConfigurarGrid(gridenc);
                Cutilidades.ScrollGrid(grid);
                gridenc.Settings.VerticalScrollBarMode = (ScrollBarMode)Enum.Parse(typeof(ScrollBarMode), "Auto");
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los datos. " + ex.Message);
            }
        }

        public void CargarListas()
        {
            try
            {
                Cproducto = new CtrProductos();
                cmbProducto.Items.Clear();
                cmbProducto.Items.Add("Seleccionar Producto a Redistribuir", null);
                IEnumerable<GE_TPRODUCTOS> prod = Cproducto.GetAllRedistribuir();
                foreach (GE_TPRODUCTOS p in prod)
                {
                    cmbProducto.Items.Add(p.prod_descripcion, p.prod_consecutivo);
                }

                if (cmbProducto.Items.Count == 2)
                {
                    cmbProducto.Items[1].Selected = true;
                    cmbProductoChanged();
                }

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar las listas. " + ex.Message);
            }
        }

        private IList<VW_VLR_REDISTRIBUCION> CustomDataSource
        {
            get
            {
                IList<VW_VLR_REDISTRIBUCION> result = Session["DataSource"] as IList<VW_VLR_REDISTRIBUCION>;
                Session["DataSource"] = result;
                return result;
            }
        }

        private IList<GE_TREDISTRIBUCION> CustomDataSourceT
        {
            get
            {
                IList<GE_TREDISTRIBUCION> result = Session["DataSourceT"] as IList<GE_TREDISTRIBUCION>;
                Session["DataSourceT"] = result;
                return result;
            }
        }

        protected void UpdateItem(OrderedDictionary keys, OrderedDictionary newValues)
        {
            var id = Convert.ToInt32(keys["redi_producto_dist"]);
            IList<GE_TREDISTRIBUCION> iList = Session["DataSourceT"] as IList<GE_TREDISTRIBUCION>;

            foreach (GE_TREDISTRIBUCION d in iList)
            {
                if (Convert.ToInt32(d.redi_producto_dist) == id)
                {
                    d.redi_valor = Convert.ToDecimal(newValues["redi_valor"]);
                    break;
                }
            }
            Session["DataSourceT"] = iList;
        }

        protected object GetTotalSummaryValue()
        {
            ASPxSummaryItem summaryItem = grid.TotalSummary.First(i => i.Tag == "TPorcentaje");
            return grid.GetTotalSummaryValue(summaryItem);
        }

        private void Limpiar()
        {
            valorItem.Value = null;
            Session["DataSourceT"] = null;
            grid.DataBind();
            gridenc.DataSource = null;
            gridenc.DataBind();
            cmbProducto.Value = null;

            gridenc.Visible = false;
            grid.Visible = false;
            btnGuardar.Enabled = false;
        }

        private void cmbProductoChanged()
        {
            try
            {
                if (cmbProducto.Value != null)
                {
                    gridenc.Visible = true;
                    grid.Visible = true;
                    btnGuardar.Enabled = true;

                    CVwVlrRedist = new CtrVwVlrRedistribucion();
                    gridenc.DataSource = CVwVlrRedist.GetByIdProducto(Convert.ToInt32(cmbProducto.Value));
                    gridenc.DataBind();
                    gridenc.SettingsPager.PageSizeItemSettings.Visible = false;

                    Credist = new CtrRedistribucion();
                    IList<GE_TREDISTRIBUCION> r = Credist.GetByIdProducto(Convert.ToInt32(cmbProducto.Value), Convert.ToInt32(Session["periodo"].ToString()));
                    Session["DataSourceT"] = r;
                    grid.DataBind();

                    valorItem.Value = r.First().redi_valor_producto;
                }
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede cargar la distribución. " + ex.Message);
            }
        }
        #endregion

        #region Eventos
        protected void CancelEditing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            grid.CancelEdit();
        }
        

        protected void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbProductoChanged();
        }

        protected void gridenc_DataBinding(object sender, EventArgs e)
        {
        }

        protected void GuardarClicked(object sender, EventArgs e)
        {
            try
            {
                grid.UpdateEdit();
                IList<GE_TREDISTRIBUCION> iList = (IList<GE_TREDISTRIBUCION>)grid.DataSource;
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);
                DateTime dtFecha = DateTime.Now;
                Credist = new CtrRedistribucion();

                foreach (GE_TREDISTRIBUCION d in iList)
                {
                    GE_TREDISTRIBUCION redi = new GE_TREDISTRIBUCION();
                    redi.redi_periodo = Convert.ToInt32(Session["periodo"].ToString());
                    redi.redi_fecha = dtFecha;
                    redi.redi_producto_dist = d.redi_producto_dist;
                    redi.redi_producto_orig = d.redi_producto_orig;
                    redi.redi_usuario = strUsuario[0].ToString();
                    redi.redi_valor = d.redi_valor;
                    redi.redi_valor_asignado = null;
                    redi.redi_valor_producto = null;


                    if ((d.redi_consecutivo <= 0) && (d.redi_valor > 0))
                    {
                        Credist.Add(redi);
                    }

                    if (d.redi_consecutivo > 0)
                    {
                        redi.redi_consecutivo = d.redi_consecutivo;
                        Credist.Update(redi);
                    }
                }

                Limpiar();
                CargarDatos();
                CargarListas();
                VentanaValidaciones.mostrarRegistroExitoso();

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede guardar el registro. " + ex.Message);
            }
        }

        protected void grid_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {

        }

        protected void grid_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = CustomDataSourceT;
        }

        protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            UpdateItem(e.Keys, e.NewValues);
            CancelEditing(e);
        }
        
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        #endregion
    }
}