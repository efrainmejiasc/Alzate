using DevExpress.Export;
using DevExpress.XtraPrinting;
using MedeskiView.Controllers;
using MedeskiView.UserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedeskiView.Forms
{
    public partial class frmCostoGenteTecnica : System.Web.UI.Page
    {
        CtrGenteTecnica genteTecnica = new CtrGenteTecnica();
        CtrUtilidades utilidades = new CtrUtilidades();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    Session["grid"] = null;
                    cargarTecnicos();
                    grid.DataSource = null;
                    utilidades.ConfigurarGrid(grid);
                    grid.DataBind();
                }
                    
                else
                {
                    if (Session["grid"] != null)
                    {
                        grid.DataSource = Session["grid"];
                        grid.DataBind();
                    }
                    
                }
                     
            }
            else
                Response.Redirect(strUrl);
        }

        public void cargarTecnicos()
        {
            try
            {
                IList<String> lstTecnicos = new List<String>();
                lstTecnicos = genteTecnica.lstgetTecnicos();

                cmbTecnicos.Items.Clear();
                cmbTecnicos.Items.Add("Seleccionar Tecnico", null);
                int indice = 1;
                foreach (var item in lstTecnicos)
                {
                    cmbTecnicos.Items.Add(item,indice);
                    indice++;
                }

                if (cmbTecnicos.Items.Count == 2)
                {
                    cmbTecnicos.Items[1].Selected = true;
                    cargarTabla(cmbTecnicos.Items[1].Text.ToString());
                }

                cmbTecnicos.SelectedIndex = 0;
                cmbTecnicos.DataBind();
            }
            catch 
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error","Se presento error cargando los tecnicos");
                throw;
            }
        }

        public void cargarTabla(String p_nombreIngeniero)
        {
            try
            {
                IList<VW_GENTE_TECNICA> lstgenteTecnica = new List<VW_GENTE_TECNICA>();
                lstgenteTecnica = genteTecnica.lstgetAllFindName(p_nombreIngeniero).OrderByDescending(a => a.PORCENTAJE_DEDICACION).ToList();
                Session["grid"] = lstgenteTecnica;
                grid.DataSource = Session["grid"];
                grid.DataBind();

                txtCostoColaborador.Text = "";
                VW_GENTE_TECNICA first = lstgenteTecnica.FirstOrDefault();
                if (first != null)
                    txtCostoColaborador.Text = first.COSTO_COLABORADOR.ToString();
            }
            catch
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "Se presento error cargando la información del tecnico: " + p_nombreIngeniero);
                throw;
            }
        }

        protected void cmbTecnicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbTecnicos.Value) != 0)
            {
                cargarTabla(cmbTecnicos.Text.ToString());
            }
        }

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            gridExport.WriteCsvToResponse(new CsvExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }
    }
}