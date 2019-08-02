using DevExpress.Export;
using DevExpress.Web;
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
    public partial class frmReporteRedistribucion : System.Web.UI.Page
    {
        CtrUtilidades Cutilidades = new CtrUtilidades();
        CtrReporteRedistribucion CReporte = new CtrReporteRedistribucion();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                mostrar_datos();
            }
            else
                Response.Redirect(strUrl);
        }

        public void mostrar_datos()
        {
            try
            {
                grid_Driver.DataSource = CReporte.GetAll();
                grid_Driver.DataBind();
                Cutilidades.ConfigurarGrid(grid_Driver);
                grid_Driver.SettingsPager.Summary.Visible = true;
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se carga gridview. " + ex.Message);
            }
        }

        #region Eventos

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            gridExport.WriteCsvToResponse(new CsvExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }

        protected void grid_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "dto_generic_valor_total")
            {
                decimal price = (decimal)e.GetListSourceFieldValue("dto_generic_valor_distribuidos");
                decimal quantity = (decimal)e.GetListSourceFieldValue("dto_generic_valor_adicional");
                e.Value = price + quantity;
            }
        }

        protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ApplyLayout(Int32.Parse(e.Parameters));
        }
        #endregion

        void ApplyLayout(int layoutIndex)
        {
            grid_Driver.BeginUpdate();
            try
            {
                grid_Driver.ClearSort();
                switch (layoutIndex)
                {
                    case 0:
                        grid_Driver.GroupBy(grid_Driver.Columns["ceop_codigo"]);
                        break;
                    case 1:
                        grid_Driver.GroupBy(grid_Driver.Columns["cost_centro_operacion"]);
                        break;                    
                }
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarError("No se pueden agrupar los valores. " + ex.Message);
            }
            finally
            {
                grid_Driver.EndUpdate();
            }
            grid_Driver.ExpandAll();
        }
    }
}