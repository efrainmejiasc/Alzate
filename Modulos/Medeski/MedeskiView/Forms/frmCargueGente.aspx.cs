using DevExpress.Web;
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

namespace MedeskiView.Forms
{
    public partial class frmCargueGente : System.Web.UI.Page
    {
        CtrUtilidades Cutilidades = new CtrUtilidades();
        CtrCargueGente gente = new CtrCargueGente();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        CtrVlrsParamGrales valoresParams = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    Session["path"] = string.Empty;
                    grid_gente.DataSource = null;
                    grid_gente.DataBind();
                    Cutilidades.ConfigurarGrid(grid_gente);
                    Session["pantallaInicio"] = "1";
                }
                else
                {
                    string pantalla = Session["pantallaInicio"] as string;
                    if (pantalla.Equals("1"))
                    {
                        grid_gente.DataSource = Session["grvGente"];
                        grid_gente.DataBind();
                        Cutilidades.ConfigurarGrid(grid_gente);
                    }
                    else
                    {
                        mostrar_datos();
                    }
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
                        string strRuta = Server.MapPath("/") + "Files\\";
                        Session["path"] = strRuta + file.FileName;
                        file.SaveAs(Session["path"].ToString(), true);
                        Session["pantallaInicio"] = "0";
                    }
                }
            }
            catch
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "El archivo no es válido");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                IList<GE_TGENTE> lstGenteOk = new List<GE_TGENTE>();
                lstGenteOk = (IList<GE_TGENTE>)Session["grvGentesOk"];
                gente.Guardar(lstGenteOk);
                VentanaValidaciones.mostrarRegistroExitoso();
            }
            catch (Exception)
            {
                VentanaValidaciones.mostrarError("Se presento error guardando");
            }
            
        }

        public void mostrar_datos()
        {
            try
            {
                string FilePath = Session["path"] as string;
                if (!String.IsNullOrEmpty(FilePath))
                {
                    Session["grvGente"] = null;

                    //Aqui quede
                    DataTable dtGente  = Cutilidades.ExceltoDataTable(0, FilePath);
                    IList<DTOgenericoCargueArchivos> lstGente = gente.organizaGridView(dtGente);
                    Boolean estaOk = lstGente.Any(a => a.dto_generic_observaciones != null && a.dto_generic_observaciones != "");

                    if (estaOk)
                    {
                        btnGuardar.Enabled = false;
                        VentanaValidaciones.mostrarMensajePersonalizado("Advertencía","El archivo contiene observaciones");
                    }
                    else
                    {
                        Session["grvGentesOk"] = gente.genteOk(lstGente, Session["usuario"].ToString().Split(';')[0]);
                        btnGuardar.Enabled = true;
                    }

                    Session["grvGente"] = lstGente.OrderByDescending(a => a.dto_generic_observaciones);
                    grid_gente.DataSource = Session["grvGente"];
                    grid_gente.DataBind();
                    Cutilidades.ConfigurarGrid(grid_gente);
                }
                else
                {
                    VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se encontro el archivo cargado");
                }
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se carga gridview. " + ex.Message);
            }
        }

        protected void btnTemplate_Click(object sender, EventArgs e)
        {
            GenerarPlantilla();
        }

        public void GenerarPlantilla()
        {
            try
            {
                string path = Server.MapPath("/Templates");
                string archivoFinal = path + "\\PlantillaCargueGente.xls";
                /*
                 * string plantilla = path + "plantilla.xls";
                 * File.Copy(plantilla, archivoFinal, true);
                 * if (!Directory.Exists(path))
                 * {
                 *    DirectoryInfo di = Directory.CreateDirectory(path);
                 * }
                */
                IWorkbook workbook = new XSSFWorkbook();
                XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("Hoja1");
                XSSFRow rowTitle = (XSSFRow)sheet.CreateRow(0);

                // Centro Costos
                XSSFCell cellTitle = (XSSFCell)rowTitle.CreateCell(0);
                cellTitle.SetCellValue("Centro Costos");

                // Descripción Centro Costos
                cellTitle = (XSSFCell)rowTitle.CreateCell(1);
                cellTitle.SetCellValue("Descripcion Centro de Costos");

                // Centro Operación
                cellTitle = (XSSFCell)rowTitle.CreateCell(2);
                cellTitle.SetCellValue("Centro de Operacion");
                
                // Documento
                cellTitle = (XSSFCell)rowTitle.CreateCell(3);
                cellTitle.SetCellValue("Numero Cedula");

                // Cargo
                cellTitle = (XSSFCell)rowTitle.CreateCell(4);
                cellTitle.SetCellValue("Cargo");

                // Empresa
                cellTitle = (XSSFCell)rowTitle.CreateCell(5);
                cellTitle.SetCellValue("Empresa");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(6);
                cellTitle.SetCellValue("Valor Colaborador");

                // Porcentaje
                cellTitle = (XSSFCell)rowTitle.CreateCell(7);
                cellTitle.SetCellValue("Porcentaje Dedicacion");

                FileStream file = File.Create(archivoFinal);
                workbook.Write(file);
                file.Close();

                //System.Diagnostics.Process.Start(archivoFinal);

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/octet-stream";
                response.AddHeader("Content-Disposition", "attachment; filename=" + "PlantillaCargueGente.xls");
                response.TransmitFile(archivoFinal);
                response.Flush();
                response.End();

            }
            catch (Exception ex)
            {
               VentanaValidaciones.mostrarError(ex.Message);
            }
        }
    }
}