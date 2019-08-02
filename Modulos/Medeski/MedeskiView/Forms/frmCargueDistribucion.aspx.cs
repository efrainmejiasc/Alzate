using DevExpress.Web;
using MedeskiView.Controllers;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Medeski.BusinessLogic.Class;

namespace MedeskiView.Forms
{
    public partial class frmCargueDistribucion : System.Web.UI.Page
    {
        CtrUtilidades Cutilidades = new CtrUtilidades();
        CtrCargueDistribucion cargue = new CtrCargueDistribucion();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    Session["path"] = string.Empty;
                    grid_Driver.DataSource = cargue.GetAllActive();
                    grid_Driver.DataBind();
                    Cutilidades.ConfigurarGrid(grid_Driver);
                    Session["pantallaInicio"] = "0";
                }
                else
                {
                    string pantalla = Session["pantallaInicio"] as string;
                    if (pantalla.Equals("1"))
                    {
                        grid_Driver.DataSource = Session["grvDsitribucion"];
                        grid_Driver.DataBind();
                        Cutilidades.ConfigurarGrid(grid_Driver);
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

    #region Metodos
        public void mostrar_datos()
        {
            try
            {
                string pantalla = Session["pantallaInicio"] as string;
                if (pantalla.Equals("0"))
                {
                    grid_Driver.DataSource = cargue.GetAllActive();
                    grid_Driver.DataBind();
                    Cutilidades.ConfigurarGrid(grid_Driver);                    
                }
                else
                {                
                    string FilePath = Session["path"] as string;
                    if (!String.IsNullOrEmpty(FilePath))
                    {
                        Session["grvDsitribucion"] = null;
                        IList<DTOgenericoCargueArchivos> lstCarg = cargue.LeerExcel("Hoja1", FilePath).ToList<DTOgenericoCargueArchivos>();
                        IList<DTOgenericoCargueArchivos> cargueOk = cargue.OrganizarGrid(lstCarg);
                        Boolean EstaMal = cargueOk.Any(a => a.dto_generic_observaciones != "OK");
                    
                        if (EstaMal)
                        {
                            btnGuardar.Enabled = false;           
                        }
                        else
                        {
                            btnGuardar.Enabled = true;
                        }

                        Session["grvDsitribucion"] = cargueOk;
                        IList<DTOgenericoCargueArchivos>  listaOk = cargueOk.Where(x => x.dto_generic_observaciones.Equals("OK")).ToList<DTOgenericoCargueArchivos>();
                        Session["grvDsitribucionOK"] = listaOk;
                        grid_Driver.DataSource = Session["grvDsitribucion"];
                        grid_Driver.DataBind();
                        Cutilidades.ConfigurarGrid(grid_Driver);                    
                    }
                    else
                    {
                        VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se encontro el archivo cargado");
                    }
                }
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se carga gridview. " + ex.Message);
            }
        }

    #endregion

        protected void UploadControl_FilesUploadComplete(object sender, DevExpress.Web.FilesUploadCompleteEventArgs e)
        {
            try
            {
                foreach (UploadedFile file in UploadControl.UploadedFiles)
                {
                    if (!string.IsNullOrEmpty(file.FileName) && file.IsValid)
                    {
                        string strRuta = @Server.MapPath("/") + "Files\\";
                        Session["path"] = strRuta + file.FileName;
                        File.Delete(Session["path"].ToString());

                        file.SaveAs(Session["path"].ToString(), true);
                        Session["pantallaInicio"] = "1";
                    }
                }
                // ValidarExcel(Session["path"].ToString());
                mostrar_datos();
            }
            catch
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "El archivo no es válido");
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
                //string path = @ctrParam.GetByClase("RUTA_PLANTILLA").vhpg_valor;
                string path = Server.MapPath("/Templates");
                string archivoFinal = path + "\\PlantillaCargueDistribucion.xls";
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

                // CO Origen
                XSSFCell cellTitle = (XSSFCell)rowTitle.CreateCell(0);
                cellTitle.SetCellValue("CO Origen");

                // Porcentaje
                cellTitle = (XSSFCell)rowTitle.CreateCell(1);
                cellTitle.SetCellValue("Porcentaje");

                // CO Destino
                cellTitle = (XSSFCell)rowTitle.CreateCell(2);
                cellTitle.SetCellValue("CO Destino");

                FileStream file = File.Create(archivoFinal);
                workbook.Write(file);
                file.Close();

                //System.Diagnostics.Process.Start(archivoFinal);

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/octet-stream";
                response.AddHeader("Content-Disposition", "attachment; filename=" + "PlantillaCargueDistribucion.xls");
                response.TransmitFile(archivoFinal);
                response.Flush();
                response.End();

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarError(ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                cargue.Guardar(Session["grvDsitribucionOK"] as List<DTOgenericoCargueArchivos>, strUsuario[0].ToString());
                VentanaValidaciones.mostrarRegistroExitoso();
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarError("Error al guardar. " + ex.Message);
            }
        }
    }
}