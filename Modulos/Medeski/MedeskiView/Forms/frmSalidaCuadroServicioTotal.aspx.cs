using System;
using MedeskiView.Controllers;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.Export;

namespace MedeskiView.Forms
{
    public partial class frmSalidaCuadroServicioTotal : System.Web.UI.Page
    {
        CtrVwCuadroServicioTotal Cvwsalida;
        CtrUtilidades Cutilidades;
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                CargarDatos();
            }
            else
                Response.Redirect(strUrl);
        }

        public void CargarDatos()
        {
            try
            {
                Cvwsalida = new CtrVwCuadroServicioTotal();
                Cutilidades = new CtrUtilidades();
                grid.DataSource = Cvwsalida.GetAll();
                grid.DataBind();
                // Cutilidades.ConfigurarGrid(grid);
                grid.SettingsPager.Summary.Visible = true;
                grid.Settings.VerticalScrollBarMode = (ScrollBarMode)Enum.Parse(typeof(ScrollBarMode), "Auto");

                grid.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.HideDataCells;
                grid.SettingsAdaptivity.AllowOnlyOneAdaptiveDetailExpanded = true;
                grid.SettingsBehavior.AllowEllipsisInText = true;
                grid.EditFormLayoutProperties.SettingsAdaptivity.AdaptivityMode = FormLayoutAdaptivityMode.SingleColumnWindowLimit;
                grid.EditFormLayoutProperties.SettingsAdaptivity.SwitchToSingleColumnAtWindowInnerWidth = 600;
                grid.SettingsEditing.Mode = GridViewEditingMode.EditForm;
                grid.Styles.Cell.Wrap = DevExpress.Utils.DefaultBoolean.True;
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se pueden cargar los registros. ", ex);
            }
        }

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            gridExport.WriteCsvToResponse(new CsvExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }
    }
}