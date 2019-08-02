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
    public partial class frmDistribucionInfraestructura : System.Web.UI.Page
    {
        CtrProductos Cproducto = new CtrProductos();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        CtrPeriodoPresupuesto Cperiodo = new CtrPeriodoPresupuesto();
        CtrProductosItems Citem = new CtrProductosItems();
        CtrDistribucionInfraest Cdist = new CtrDistribucionInfraest();

        string strTipo = "INFRAESTRUCTURA";        
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "prit_consecutivo", "prit_item" };

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    Session["items"] = null;
                    Session["DataSource"] = null;
                    CargarListas();                    
                }
                else
                {
                    gridItems.DataSource = Session["items"];
                    gridItems.DataBind();
                }
                

                Cperiodo = new CtrPeriodoPresupuesto();
                Session["Periodo"] = 0;
                IList<GE_TPERIODOPRESUPUESTO> per = Cperiodo.GetAllActive();

                if (per.Count > 0)
                    Session["Periodo"] = per[0].peri_consecutivo;
            }
            else
                Response.Redirect(strUrl);
        }

        #region Metodos
       
        public void CargarListas()
        {
            try
            {
                cmbProducto.Items.Clear();
                cmbProducto.Items.Add("Seleccionar Producto Infraestructura", null);
                IEnumerable<GE_TPRODUCTOS> prod = Cproducto.GetAllTipoComponente(strTipo);
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

        private IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> CustomDataSource
        {
            get
            {
                IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> result = Session["DataSource"] as IList<GE_TDISTRIBUCIONINFRAESTRUCTURA>;
                Session["DataSource"] = result;
                return result;
            }
        }

        protected void UpdateItem(OrderedDictionary keys, OrderedDictionary newValues)
        {
            var id = Convert.ToInt32(keys["dinf_consecutivo"]);
            IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> iList = Session["DataSource"] as IList<GE_TDISTRIBUCIONINFRAESTRUCTURA>;

            foreach (GE_TDISTRIBUCIONINFRAESTRUCTURA d in iList)
            {
                if (Convert.ToInt32(d.dinf_consecutivo) == id)
                {
                    d.dinf_valor = Convert.ToDecimal(newValues["dinf_valor"]);
                    break;
                }
            }
            Session["DataSource"] = iList;
          
        }

        private void Limpiar()
        {
            Session["DataSource"] = null;
            Session["items"] = null;
            Session["item"] = null;

            gridItems.DataSource = null;
            gridItems.DataBind();

            grid.DataSource = null; 
            grid.DataBind();

            CargarListas();
            cmbProducto.Value = null;
            btnGuardar.Enabled = false;
        }

        private void cmbProductoChanged()
        {
            try
            {
              
                if (cmbProducto.Value != null)
                {
                    IList<GE_TPRODUCTOSITEMS> pr = Citem.GetByProducto(Convert.ToInt32(cmbProducto.Value));
                    Session["items"] = pr;
                    gridItems.DataSource = pr;
                    gridItems.DataBind();
                    Cutilidades = new CtrUtilidades();
                    Cutilidades.ConfigurarGrid(gridItems);
                }
                else
                {
                    Limpiar();
                }

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los items. " + ex.Message);
            }
        }

        #endregion
        #region Eventos

        protected void CancelEditing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            grid.CancelEdit();
        }

        protected void GuardarClicked(object sender, EventArgs e)
        {
            try
            {

                grid.UpdateEdit();
                IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> iList = (IList<GE_TDISTRIBUCIONINFRAESTRUCTURA>)grid.DataSource;
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);
                DateTime dtFecha = DateTime.Now;
                Cdist = new CtrDistribucionInfraest();

                int periodo = Convert.ToInt32(Session["periodo"].ToString());
                int prod = Convert.ToInt32(cmbProducto.Value.ToString());
                int item = Convert.ToInt32(Session["item"].ToString());
                IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> listaActual = Cdist.GetAllProductoItem(periodo, prod, item);

                foreach (GE_TDISTRIBUCIONINFRAESTRUCTURA d in iList)
                {
                    bool inLista = listaActual.Any(x => x.dinf_consecutivo == d.dinf_consecutivo);

                    if (inLista)
                    {
                        GE_TDISTRIBUCIONINFRAESTRUCTURA dist = new GE_TDISTRIBUCIONINFRAESTRUCTURA(); 
                        dist.dinf_estado = 1;
                        dist.dinf_fecha = dtFecha;
                        dist.dinf_periodo = Convert.ToInt32(Session["periodo"].ToString());
                        dist.dinf_producto = d.dinf_producto;
                        dist.dinf_producto_item = d.dinf_producto_item;
                        dist.dinf_servidor = d.dinf_servidor;
                        dist.dinf_tipo = strTipo;
                        dist.dinf_usuario = strUsuario[0].ToString();
                        dist.dinf_valor = d.dinf_valor;

                        dist.dinf_consecutivo = d.dinf_consecutivo;
                        Cdist.Update(dist);
                    }
                    else if (d.dinf_valor > 0)
                    {
                        GE_TDISTRIBUCIONINFRAESTRUCTURA dist = new GE_TDISTRIBUCIONINFRAESTRUCTURA();
                        dist.dinf_estado = 1;
                        dist.dinf_fecha = dtFecha;
                        dist.dinf_periodo = Convert.ToInt32(Session["periodo"].ToString());
                        dist.dinf_producto = d.dinf_producto;
                        dist.dinf_producto_item = d.dinf_producto_item;
                        dist.dinf_servidor = d.dinf_servidor;
                        dist.dinf_tipo = strTipo;
                        dist.dinf_usuario = strUsuario[0].ToString();
                        dist.dinf_valor = d.dinf_valor;

                        if (d.dinf_consecutivo <= 0)
                        {
                            Cdist.Add(dist);
                        }
                        else
                        {
                            dist.dinf_consecutivo = d.dinf_consecutivo;
                            Cdist.Update(dist);
                        }

                    }
                }

                Limpiar();
                CargarListas();
                VentanaValidaciones.mostrarRegistroExitoso();

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede guardar el registro. " + ex.Message);
            }
        }

        protected void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbProductoChanged();            
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

        #endregion

        protected void gridItems_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                int item = Convert.ToInt32(gridItems.GetRowValues(e.VisibleIndex, "prit_consecutivo"));
                Session["item"] = item;
                IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> iList = Cdist.GetAllProductosDistribuidos(Convert.ToInt32(Session["periodo"].ToString()), Convert.ToInt32(cmbProducto.Value.ToString()), item, strTipo);
                Session["DataSource"] = iList; 
                grid.DataSource = iList;
                grid.DataBind();

                Cutilidades.ScrollGrid(grid);
                btnGuardar.Enabled = true;

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los items. " + ex.Message);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void gridItems_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("prit_activo"))
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
    }
}