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
    public partial class frmDistribucionMAS : System.Web.UI.Page
    {
        CtrUtilidades Cutilidades;
        CtrPeriodoPresupuesto Cperiodo;
        CtrDistribMAS Cdist;
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
            }
            else
                Response.Redirect(strUrl);
        }

        #region Metodos
        private void Limpiar()
        {
            Session["DataSource"] = null;
            grid.DataBind();
            CargarDatos();
            btnGuardar.Enabled = false;
        }

        public void CargarDatos()
        {
            try
            {
                Cutilidades = new CtrUtilidades();
                Cutilidades.ScrollGrid(grid);
                Cdist = new CtrDistribMAS();
                IList<GE_TDISTRIBUCIONMASPROCESOS> iList = Cdist.GetAllProductosDistrib(Convert.ToInt32(Session["periodo"].ToString()));
                grid.DataSource = iList;
                Session["DataSource"] = iList;
                grid.DataBind();
                btnGuardar.Enabled = true;
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los datos. " + ex.Message);
            }
        }

        private IList<GE_TDISTRIBUCIONMASPROCESOS> CustomDataSource
        {
            get
            {
                IList<GE_TDISTRIBUCIONMASPROCESOS> result = Session["DataSource"] as IList<GE_TDISTRIBUCIONMASPROCESOS>;
                Session["DataSource"] = result;
                return result;
            }
        }


        protected void UpdateItem(OrderedDictionary keys, OrderedDictionary newValues)
        {
            var id = Convert.ToInt32(keys["dmas_consecutivo"]);
            IList<GE_TDISTRIBUCIONMASPROCESOS> iList = Session["DataSource"] as IList<GE_TDISTRIBUCIONMASPROCESOS>;

            foreach (GE_TDISTRIBUCIONMASPROCESOS d in iList)
            {
                if (Convert.ToInt32(d.dmas_consecutivo) == id)
                {
                    d.dmas_valor = Convert.ToDecimal(newValues["dmas_valor"]);
                    break;
                }
            }
            Session["DataSource"] = iList;

        }

        protected object GetTotalSummaryValue()
        {
            ASPxSummaryItem summaryItem = grid.TotalSummary.First(i => i.Tag == "TTotal");
            return grid.GetTotalSummaryValue(summaryItem);
        }
        #endregion

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
                IList<GE_TDISTRIBUCIONMASPROCESOS> iList = (IList<GE_TDISTRIBUCIONMASPROCESOS>)grid.DataSource;
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);
                DateTime dtFecha = DateTime.Now;
                Cdist = new CtrDistribMAS();

                foreach (GE_TDISTRIBUCIONMASPROCESOS d in iList)
                {
                    GE_TDISTRIBUCIONMASPROCESOS dist = new GE_TDISTRIBUCIONMASPROCESOS();
                    dist.dmas_fecha = dtFecha;
                    dist.dmas_periodo = Convert.ToInt32(Session["periodo"].ToString());
                    dist.dmas_producto = d.dmas_producto;
                    dist.dmas_usuario = strUsuario[0].ToString();
                    dist.dmas_valor = d.dmas_valor;

                    if ((d.dmas_consecutivo <= 0) && (d.dmas_valor > 0))
                    {
                        Cdist.Add(dist);
                    }

                    if (d.dmas_consecutivo > 0)
                    {
                        dist.dmas_consecutivo = d.dmas_consecutivo;
                        Cdist.Update(dist);
                    }
                }

                Limpiar();
                CargarDatos();
                VentanaValidaciones.mostrarRegistroExitoso();

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede guardar el registro. " + ex.Message);
            }
        }

        #endregion
    }
}