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
    public partial class frmPorcentajesPYG : System.Web.UI.Page
    {
        CtrUtilidades CUtilidades = new CtrUtilidades();
        CtrPorcentajesPYG CtrPorcentajes = new CtrPorcentajesPYG();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                if (IsPostBack)
                {
                    Recalcular();
                }

                CargarDatos();
            }
            else
                Response.Redirect(strUrl);            
        }

        #region Metodos
        public void CargarDatos()
        {
            if (Session["DataSourceTbl"] != null) {
                var lista = Session["DataSourceTbl"];
            }
            else
            {
                var lista = CtrPorcentajes.GetAllActive();                
            }
            grid.DataSource = Session["DataSourceTbl"];
            grid.DataBind();
            CUtilidades.ConfigurarGrid(grid);
        }

        public void Recalcular()
        {
            Char delimiter = ';';
            string[] strUsuario = null;
            strUsuario = Session["usuario"].ToString().Split(delimiter);

            Session["DataSourceTbl"] = CtrPorcentajes.Recalcular(strUsuario[0].ToString());
        }

        #endregion

        #region Eventos
        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            gridExport.WriteCsvToResponse(new CsvExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }

        protected void CalcularClicked(object sender, EventArgs e)
        {
            Recalcular();
        }
        #endregion
    }
}