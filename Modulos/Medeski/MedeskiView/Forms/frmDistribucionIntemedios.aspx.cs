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
    public partial class frmDistribucionIntemedios : System.Web.UI.Page
    {
        CtrProductos Cproducto;
        CtrUtilidades Cutilidades;
        CtrPeriodoPresupuesto Cperiodo;
        CtrDistribucionIntermedios Cdist;
        CtrProductosItems Citems;
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                CargarDatos();
                CargarListas();

                Cperiodo = new CtrPeriodoPresupuesto();
                Session["Periodo"] = 0;
                IList<GE_TPERIODOPRESUPUESTO> per = Cperiodo.GetAllActive();

                if (per.Count > 0)
                    Session["Periodo"] = per[0].peri_consecutivo;
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
                Cutilidades = new CtrUtilidades();
                Cutilidades.ScrollGrid(grid);
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
                cmbProducto.Items.Add("Seleccionar Producto Intermedio", null);
                IEnumerable<GE_TPRODUCTOS> prod = Cproducto.GetAllIntermedios();
                foreach (GE_TPRODUCTOS p in prod)
                {
                    cmbProducto.Items.Add(p.prod_descripcion, p.prod_consecutivo);
                }

                if (cmbProducto.Items.Count == 2)
                {
                    cmbProducto.Items[1].Selected = true;
                    CargarItems();
                }

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar las listas. " + ex.Message);
            }
        }

        private IList<GE_TDISTRIBUCIONINTERMEDIOS> CustomDataSource
        {
            get
            {
                IList<GE_TDISTRIBUCIONINTERMEDIOS> result = Session["DataSource"] as IList<GE_TDISTRIBUCIONINTERMEDIOS>;
                Session["DataSource"] = result;
                return result;
            }
        }

        protected void UpdateItem(OrderedDictionary keys, OrderedDictionary newValues)
        {
            var id = Convert.ToInt32(keys["dint_producto_directo"]);
            IList<GE_TDISTRIBUCIONINTERMEDIOS> iList = Session["DataSource"] as IList<GE_TDISTRIBUCIONINTERMEDIOS>;

            foreach (GE_TDISTRIBUCIONINTERMEDIOS d in iList)
            {
                if (Convert.ToInt32(d.dint_producto_directo) == id)
                {
                    d.dint_valor = Convert.ToDecimal(newValues["dint_valor"]);
                    break;
                }
            }
            Session["DataSource"] = iList;
        }

        private void Limpiar()
        {
            cmbProducto.Items.Clear();
            Session["DataSource"] = null;
            grid.DataBind();
            CargarDatos();
            CargarListas();
            cmbProducto.Value = null;
            btnGuardar.Enabled = false;
            cmbItem.Items.Clear();
            cmbItem.Value = null;
            valorItem.Value = null;
        }

        private void CargarItems()
        {
            try
            {
                Citems = new CtrProductosItems();
                cmbItem.Items.Clear();
                cmbItem.Items.Add("Seleccionar Item", null);
                IEnumerable<GE_TPRODUCTOSITEMS> item = Citems.GetByProducto(Convert.ToInt32(cmbProducto.Value));
                foreach (GE_TPRODUCTOSITEMS i in item)
                {
                    cmbItem.Items.Add(i.prit_item, i.prit_consecutivo);
                }

                if (cmbItem.Items.Count == 2)
                {
                    cmbItem.Items[1].Selected = true;
                    cmbItemsChanged();
                }
                else
                {
                    cmbItem.Value = null;
                }
                
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar las listas. " + ex.Message);
            }

        }

        protected object GetTotalSummaryValue()
        {
            ASPxSummaryItem summaryItem = grid.TotalSummary.First(i => i.Tag == "TPorcentaje");
            object obj = grid.GetTotalSummaryValue(summaryItem);
            return obj;
        }

        private void cmbItemsChanged()
        {
            try
            {
                if (cmbItem.Value != null)
                {
                    Cdist = new CtrDistribucionIntermedios();
                    IList<GE_TDISTRIBUCIONINTERMEDIOS> iList = Cdist.GetAllDistribucion(Convert.ToInt32(Session["periodo"].ToString()), Convert.ToInt32(cmbProducto.Value), Convert.ToInt32(cmbItem.Value));
                    grid.DataSource = iList;
                    Session["DataSource"] = iList;
                    grid.DataBind();
                    btnGuardar.Enabled = true;

                    valorItem.Value = iList.First().dint_valor_item;
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
            try
            {
                if (cmbProducto.Value != null)
                {

                    Session["DataSource"] = null;
                    CargarItems();                    
                    grid.DataBind();
                }
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede cargar la distribución. " + ex.Message);
            }
        }

        protected void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbItemsChanged();
        }

        protected void GuardarClicked(object sender, EventArgs e)
        {
            try
            {
               
                grid.UpdateEdit();
                IList<GE_TDISTRIBUCIONINTERMEDIOS> iList = (IList<GE_TDISTRIBUCIONINTERMEDIOS>)grid.DataSource;
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);
                DateTime dtFecha = DateTime.Now;
                Cdist = new CtrDistribucionIntermedios();

                int periodo = Convert.ToInt32(Session["periodo"].ToString());
                int prod = Convert.ToInt32(cmbProducto.Value.ToString());
                int item = Convert.ToInt32(cmbItem.Value.ToString());
                IList<GE_TDISTRIBUCIONINTERMEDIOS> listaActual = Cdist.GetAllProductoItem(periodo, prod, item);

                foreach (GE_TDISTRIBUCIONINTERMEDIOS d in iList)
                {
                    bool inLista = listaActual.Any(x => x.dint_consecutivo == d.dint_consecutivo);

                    if(inLista)
                    {
                        GE_TDISTRIBUCIONINTERMEDIOS dist = new GE_TDISTRIBUCIONINTERMEDIOS();
                        dist.dint_estado = 1;
                        dist.dint_fecha = dtFecha;
                        dist.dint_periodo = Convert.ToInt32(Session["periodo"].ToString());
                        dist.dint_producto_directo = d.dint_producto_directo;
                        dist.dint_producto_intermedio = d.dint_producto_intermedio;
                        dist.dint_item_intermedio = Convert.ToInt32(cmbItem.Value);
                        dist.dint_usuario = strUsuario[0].ToString();
                        dist.dint_valor = d.dint_valor;

                        dist.dint_consecutivo = d.dint_consecutivo;
                        Cdist.Update(dist);
                    }
                    else if (d.dint_valor > 0)
                    {
                        GE_TDISTRIBUCIONINTERMEDIOS dist = new GE_TDISTRIBUCIONINTERMEDIOS();
                        dist.dint_estado = 1;
                        dist.dint_fecha = dtFecha;
                        dist.dint_periodo = Convert.ToInt32(Session["periodo"].ToString());
                        dist.dint_producto_directo = d.dint_producto_directo;
                        dist.dint_producto_intermedio = d.dint_producto_intermedio;
                        dist.dint_item_intermedio = Convert.ToInt32(cmbItem.Value);
                        dist.dint_usuario = strUsuario[0].ToString();
                        dist.dint_valor = d.dint_valor;

                        if (d.dint_consecutivo == 0)
                        {
                            Cdist.Add(dist);
                        }
                        else
                        {
                            dist.dint_consecutivo = d.dint_consecutivo;
                            Cdist.Update(dist);
                        }
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
            (sender as ASPxGridView).DataSource = CustomDataSource;
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