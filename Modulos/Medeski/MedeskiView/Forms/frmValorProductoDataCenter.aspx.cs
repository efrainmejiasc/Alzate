using DevExpress.Export;
using DevExpress.XtraPrinting;
using MedeskiView.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedeskiView.Forms
{
    public partial class frmValorProductoDataCenter : System.Web.UI.Page
    {
        CtrVwVlrGenteTecnicaProd genteTecnicaProducto = new CtrVwVlrGenteTecnicaProd();
        CtrUtilidades utilidades = new CtrUtilidades();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    Session["Servidores"] = null;
                    obtenerServidores();
                    utilidades.ConfigurarGrid(grid);
                }
                else
                {
                    grid.DataSource = Session["Servidores"];
                    grid.DataBind();
                }
            }
            else
                Response.Redirect(strUrl);
        }

        public void obtenerServidores()
        {
            try
            {
                IList<String> lstTecnicos = new List<String>();
                lstTecnicos = genteTecnicaProducto.getServer();

                cmbServidores.Items.Clear();
                cmbServidores.Items.Add("Seleccionar Servidor", null);
                int indice = 1;
                foreach (var item in lstTecnicos)
                {
                    cmbServidores.Items.Add(item, indice);
                    indice++;
                }

                if (cmbServidores.Items.Count == 2)
                {
                    cmbServidores.Items[1].Selected = true;
                    obtenerProductosPorServidor(cmbServidores.Items[1].Text.ToString());
                }

                cmbServidores.SelectedIndex = 0;
                cmbServidores.DataBind();
            }
            catch (Exception e)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "Se presento error cargando los Servidores " + e.Message);
            }
        }


        public void obtenerProductosPorServidor(String p_nombreServidor)
        {
            try
            {
                grid.DataSource = null;
                Session["Servidores"] = genteTecnicaProducto.getAll(p_nombreServidor);
                grid.DataSource = Session["Servidores"];
                grid.DataBind();
            }
            catch (Exception e)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "Se presento error cargando los productos por Servidor" + e.Message);
            }
        }

        protected void cmbServidores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbServidores.Value) != 0)
            {
                obtenerProductosPorServidor(cmbServidores.Text.ToString());
            }
            else
            {
                grid.DataSource = null;
                grid.DataBind();
            }
        }

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            gridExport.WriteCsvToResponse(new CsvExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }
    }
}
