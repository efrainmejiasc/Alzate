using DevExpress.Web;
using DevExpress.Utils.Extensions;
using Medeski.BusinessLogic.Class;
using MedeskiView.Controllers;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedeskiView.Engine;

namespace MedeskiView.Forms
{
    public partial class frmCargueDistribucionMas : System.Web.UI.Page
    {
        CtrUtilidades Cutilidades;
        CtrPeriodoPresupuesto Cperiodo;
        CtrDistribMAS Cdist;
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    Session["path"] = string.Empty;
                    Session["pantallaInicio"] = "1";
                    Cperiodo = new CtrPeriodoPresupuesto();
                    Session["Periodo"] = 0;
                    IList<GE_TPERIODOPRESUPUESTO> per = Cperiodo.GetAllActive();

                    if (per.Count > 0)
                        Session["Periodo"] = per[0].peri_consecutivo;
                }
                else
                {
                    MostrarDatos();
                }
            }
            else
                Response.Redirect(strUrl);
        }


        protected void UploadControl_FilesUploadComplete(object sender, FilesUploadCompleteEventArgs e)
        {
            try
            {

                foreach (UploadedFile file in UploadControl.UploadedFiles)
                {
                    if (!string.IsNullOrEmpty(file.FileName) && file.IsValid)
                    {
                        if (file.FileName != "PlantillaDistribucionMas.xls")
                        {
                            return;
                        }

                        string strRuta = Server.MapPath("/") + "Files\\";
                        Session["path"] = strRuta + file.FileName;
                        file.SaveAs(Session["path"].ToString(), true);
                    }
                }
            }
            catch
            {
                VentanaValidaciones1.mostrarMensajePersonalizado("Error", "El archivo no es válido");
            }
        }

        protected void btnTemplate_Click(object sender, EventArgs e)
        {
            GenerarPlantilla();
        }

        private void GenerarPlantilla()
        {
            try
            {
                string path = Server.MapPath("/Templates");
                string archivoFinal = path + "PlantillaDistribucionMas.xls";

                IWorkbook workbook = new XSSFWorkbook();
                XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("Hoja1");
                XSSFRow rowTitle = (XSSFRow)sheet.CreateRow(0);

                XSSFCell cellTitle = (XSSFCell)rowTitle.CreateCell(0);
                cellTitle.SetCellValue("Producto Directo");

                cellTitle = (XSSFCell)rowTitle.CreateCell(1);
                cellTitle.SetCellValue("Cantidad");

                FileStream file = File.Create(archivoFinal);
                workbook.Write(file);
                file.Close();

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/octet-stream";
                response.AddHeader("Content-Disposition", "attachment; filename=" + "PlantillaDistribucionMas.xls");
                response.TransmitFile(archivoFinal);
                response.Flush();
                response.End();

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarError(ex.Message);
            }
        }

        private void MostrarDatos()
        {

            string[] n = Session["usuario"].ToString().Split(';');
            string usuario = n[0];
            EngineRead Funcion = new EngineRead();
            dt = new DataTable();
            dt = Funcion.ReadExcelDistribucionMas(Session["path"].ToString(), usuario);
            Session["DataCargue"] = dt;
            gvPpto.DataSource = dt;
            gvPpto.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["DataCargue"] == null)
                {
                    VentanaValidaciones1.mostrarMensajePersonalizado("Error", "Debe cargar archivo");
                    return;
                }
                EngineDb Metodo = new EngineDb();
                DataTable dt = new DataTable();
                dt = (DataTable)Session["DataCargue"];
                EngineProyect Funcion = new EngineProyect();

                bool resultado = Metodo.InsertDistribucionMas(dt);
                if (resultado)
                    VentanaValidaciones1.mostrarRegistroExitoso();
                else
                    VentanaValidaciones1.mostrarMensajePersonalizado("Error", "");
            }
            catch (Exception ex)
            {
                VentanaValidaciones1.mostrarMensajePersonalizado("Error", "Se presento errores al guardar");
            }

        }

        public string GetSumatoriaMas()
        {
            if (HttpContext.Current.Session["SumatoriaMas"] != null)
                return Convert.ToDecimal(HttpContext.Current.Session["SumatoriaMas"]).ToString("N3");
            else
                return "0.00";
        }

    }
}