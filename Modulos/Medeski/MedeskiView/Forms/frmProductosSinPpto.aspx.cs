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
    public partial class frmProductosSinPpto : System.Web.UI.Page
    {
        CtrUtilidades CUtilidades = new CtrUtilidades();
        CtrVWProductosSinPpto cProductos = new CtrVWProductosSinPpto();

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        protected void CargarDatos()
        {
            Char delimiter = ';';
            string[] strUsuario = null;
            strUsuario = Session["usuario"].ToString().Split(delimiter);

            IList<VW_PRODUCTOS_SIN_PPTO> miLista = cProductos.GetAll().Where(x => x.pers_usudom == strUsuario[0].ToString().ToUpper()).ToList();
            grid.DataSource = miLista;
            grid.DataBind();
            CUtilidades.ScrollGrid(grid);
        }


        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            gridExport.WriteCsvToResponse(new CsvExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }
    }
}