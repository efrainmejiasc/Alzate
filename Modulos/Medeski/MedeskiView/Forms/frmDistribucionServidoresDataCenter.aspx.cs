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
using DevExpress.XtraPrinting;
using DevExpress.Export;
namespace MedeskiView.Forms
{
    public partial class frmDistribucionServidoresDataCenter : System.Web.UI.Page
    {
        CtrUtilidades Cutilidades;
        CtrVwVlrItemsDataCenter CVwVlrItData;
        CtrServidores Cserv;
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                CargarDatos();
                CargarListas();
            }
            else
                Response.Redirect(strUrl);
        }

        #region Eventos
        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            gridExport.WriteCsvToResponse(new CsvExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }

        protected void cmbServidor_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbServidorChanged();
        }

        protected void grid_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = CustomDataSource;
        }

        #endregion

        #region Metodos

        private void cmbServidorChanged()
        {
            try
            {
                if (cmbServidor.Value != null)
                {
                    CVwVlrItData = new CtrVwVlrItemsDataCenter();
                    IList<VW_VLR_ITEMS_DATACENTER> iList = CVwVlrItData.GetAllxServ(Convert.ToInt32(cmbServidor.Value));
                    grid.DataSource = iList;
                    Session["DataSource"] = iList;
                    grid.DataBind();
                }

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los productos. " + ex.Message);
            }
        }

        public void CargarListas()
        {
            try
            {
                Cserv = new CtrServidores();
                cmbServidor.Items.Clear();
                cmbServidor.Items.Add("Seleccionar Servidor", null);
                IList<GE_TSERVIDORES> serv = Cserv.GetAllActive();
                foreach (GE_TSERVIDORES s in serv)
                {
                    cmbServidor.Items.Add(s.serv_nombre, s.serv_consecutivo);
                }

                if (cmbServidor.Items.Count == 2)
                {
                    cmbServidor.Items[1].Selected = true;
                    cmbServidorChanged();
                }

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar las listas. " + ex.Message);
            }
        }

        public void CargarDatos()
        {
            try
            {
                Cutilidades = new CtrUtilidades();
                Cutilidades.ConfigurarGrid(grid);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los datos. " + ex.Message);
            }
        }

        private IList<VW_VLR_ITEMS_DATACENTER> CustomDataSource
        {
            get
            {
                IList<VW_VLR_ITEMS_DATACENTER> result = Session["DataSource"] as IList<VW_VLR_ITEMS_DATACENTER>;
                Session["DataSource"] = result;
                return result;
            }
        }


        #endregion
    }
}