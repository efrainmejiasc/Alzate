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
    public partial class frmDistribucionDirectos : System.Web.UI.Page
    {
        CtrUtilidades Cutilidades;
        CtrPeriodoPresupuesto Cperiodo;
        CtrDistribucionInfraest Cdist;
        CtrServidores Cserv;
        string strTipo = "DIRECTO";
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                CargarDatos();
                CargarListas();

                // grid.Settings.VerticalScrollableHeight = gridServidores.Settings.VerticalScrollableHeight;
                    
                Cperiodo = new CtrPeriodoPresupuesto();
                Session["Periodo"] = 0;
                IList<GE_TPERIODOPRESUPUESTO> per = Cperiodo.GetAllActive();

                if (per.Count > 0)
                    Session["Periodo"] = per[0].peri_consecutivo;
            }
            else
                Response.Redirect(strUrl);
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

        #region Eventos
        protected void CancelEditing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            grid.CancelEdit();
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

                foreach (GE_TDISTRIBUCIONINFRAESTRUCTURA d in iList)
                {
                    GE_TDISTRIBUCIONINFRAESTRUCTURA dist = new GE_TDISTRIBUCIONINFRAESTRUCTURA();
                    dist.dinf_estado = 1;
                    dist.dinf_fecha = dtFecha;
                    dist.dinf_periodo = Convert.ToInt32(Session["periodo"].ToString());
                    dist.dinf_producto = d.dinf_producto;
                    dist.dinf_producto_item = null;
                    dist.dinf_servidor = d.dinf_servidor;
                    dist.dinf_tipo = strTipo;
                    dist.dinf_usuario = strUsuario[0].ToString();
                    dist.dinf_valor = d.dinf_valor;

                    if ((d.dinf_consecutivo <= 0) && (d.dinf_valor > 0))
                    {
                        Cdist.Add(dist);
                    }
                    
                    if (d.dinf_consecutivo > 0)
                    {
                        dist.dinf_consecutivo = d.dinf_consecutivo;
                        Cdist.Update(dist);
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

        #endregion

        #region Metodos
        private void Limpiar()
        {
            Session["DataSource"] = null;
            grid.DataBind();
            CargarDatos();
            CargarListas();
            btnGuardar.Enabled = false;
        }
        
        public void CargarListas()
        {
            try
            {
                Cserv = new CtrServidores();
                IList<GE_TSERVIDORES> serv = Cserv.GetAllActive();

                foreach (var I in serv)
                {
                    if (I.serv_activo == 1)
                        I.serv_estadoStr = "Activo";
                    else
                        I.serv_estadoStr = "Inactivo";
                }

                gridServidores.DataSource = serv;
                gridServidores.DataBind();


                Cutilidades = new CtrUtilidades();
                Cutilidades.ConfigurarGrid(gridServidores);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar las listas. " + ex.Message);
            }
        }
        
        protected object GetTotalSummaryValue()
        {
            ASPxSummaryItem summaryItem = grid.TotalSummary.First(i => i.Tag == "TPorcentaje");
            return grid.GetTotalSummaryValue(summaryItem);
        }

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

        private IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> CustomDataSource
        {
            get
            {
                IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> result = Session["DataSource"] as IList<GE_TDISTRIBUCIONINFRAESTRUCTURA>;
                Session["DataSource"] = result;
                return result;
            }
        }

        #endregion

        protected void gridServidores_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                int serv = Convert.ToInt32(gridServidores.GetRowValues(e.VisibleIndex, "serv_consecutivo").ToString());
                
                Cdist = new CtrDistribucionInfraest();
                IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> iList = Cdist.GetAllProductosDistribuidosServ(Convert.ToInt32(Session["periodo"].ToString()), serv, strTipo);
                grid.DataSource = iList;
                Session["DataSource"] = iList;
                grid.DataBind();
                btnGuardar.Enabled = true;                
                
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los items. " + ex.Message);
            }
        }

        protected void gridServidores_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("serv_activo"))
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