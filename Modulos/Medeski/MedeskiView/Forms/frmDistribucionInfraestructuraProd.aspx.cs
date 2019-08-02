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
    public partial class frmDistribucionInfraestructuraProd : System.Web.UI.Page
    {
        CtrUtilidades Cutilidades;
        CtrVwVlrProdInfr CVwVlrProdInfr;
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
        protected void cmbServidor_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbServidorChanged();
        }

        protected void grid_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = CustomDataSource;
        }

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            gridExport.WriteCsvToResponse(new CsvExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }
        #endregion

        #region Metodos

        private void cmbServidorChanged()
        {
            try
            {
                if (cmbServidor.Value != null)
                {
                    CVwVlrProdInfr = new CtrVwVlrProdInfr();
                    IList<VW_VLR_PROD_INFRAESTRUCTURA> iList = CVwVlrProdInfr.GetAllxServ(Convert.ToInt32(cmbServidor.Value));
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

        private IList<VW_VLR_PROD_INFRAESTRUCTURA> CustomDataSource
        {
            get
            {
                IList<VW_VLR_PROD_INFRAESTRUCTURA> result = Session["DataSource"] as IList<VW_VLR_PROD_INFRAESTRUCTURA>;
                Session["DataSource"] = result;
                return result;
            }
        }


        #endregion
    }
}