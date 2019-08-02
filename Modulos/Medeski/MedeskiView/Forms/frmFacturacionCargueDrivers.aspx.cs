using DevExpress.Web;
using Medeski.BusinessLogic.Class;
using MedeskiView.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using DevExpress.XtraSpreadsheet.Model;

namespace MedeskiView.Forms
{
    public partial class frmFacturacionCargueDrivers : System.Web.UI.Page
    {
        CtrUtilidades Cutilidades = new CtrUtilidades();
        CtrCargueDrivers drivers = new CtrCargueDrivers();
        CtrVlrsParamGrales valoresParams = new CtrVlrsParamGrales();
        CtrProductos ctrProductos = new CtrProductos();
        CtrCompanias ctrCompanias = new CtrCompanias();
        CtrCentroCosto ctrCentroCostos = new CtrCentroCosto();
        CtrDrivers ctrDrivers = new CtrDrivers();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + valoresParams.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {

                    Session["path"] = string.Empty;
                    Char delimiter = ';';
                    string[] strUsuario = null;
                    strUsuario = Session["usuario"].ToString().Split(delimiter);

                    grid_Driver.DataSource = drivers.GetAllActive(strUsuario[0].ToString());
                    grid_Driver.DataBind();
                    Cutilidades.ConfigurarGrid(grid_Driver);
                    //cargarActivos();
                    Session["pantallaInicio"] = "0";
                }
                else
                {
                    mostrar_datos();                    
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
                        string strRuta = @Server.MapPath("/") + "Files\\";
                        Session["path"] = strRuta + file.FileName;
                        file.SaveAs(Session["path"].ToString(), true);
                        Session["pantallaInicio"] = "2";
                    }
                }
                // ValidarExcel(Session["path"].ToString());
                // mostrar_datos();
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
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                drivers.Guardar(Session["grvDriversOk"] as List<GE_TCARGUEDRIVERS>, strUsuario[0].ToString());
                Session["pantallaInicio"] = "1";

                VentanaValidaciones.mostrarRegistroExitoso();
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarError("Error al guardar. " + ex.Message);
            }
        }

        public void mostrar_datos()
        {
            try
            {
                string pantalla = Session["pantallaInicio"] as string;
                if (pantalla.Equals("1"))
                {
                    Char delimiter = ';';
                    string[] strUsuario = null;
                    strUsuario = Session["usuario"].ToString().Split(delimiter);

                    grid_Driver.DataSource = drivers.GetAllActive(strUsuario[0].ToString());
                    grid_Driver.DataBind();
                    Cutilidades.ConfigurarGrid(grid_Driver);
                }
                else
                {
                    string FilePath = Session["path"] as string;
                    if (!String.IsNullOrEmpty(FilePath))
                    {
                        Char delimiter = ';';
                        string[] strUsuario = null;
                        strUsuario = Session["usuario"].ToString().Split(delimiter);

                        Session["grvDrivers"] = null;
                        IList<DTOgenericoCargueArchivos> lstCarg = drivers.LeerExcel("Hoja1", FilePath, strUsuario[0].ToString()).ToList<DTOgenericoCargueArchivos>();
                        IList<DTOgenericoCargueArchivos> lstArbol = drivers.organizaGridview(lstCarg, strUsuario[0].ToString());
                        Boolean EstaOk = lstArbol.Any(a => a.dto_generic_observaciones == "OK");
                        IList<DTOgenericoCargueArchivos> lstPendientes = drivers.GetPendientes(lstCarg, strUsuario[0].ToString());

                        gridPendientes.DataSource = "";
                        gridPendientes.DataBind();
                        Cutilidades.ConfigurarGrid(gridPendientes);

                        if (lstPendientes.Count() != 0)
                        {
                            gridPendientes.Visible = true;
                            gridPendientes.DataSource = lstPendientes;
                            gridPendientes.DataBind();
                            Cutilidades.ConfigurarGrid(gridPendientes);
                        }

                        if (!EstaOk)
                        {
                            btnGuardar.Enabled = false;
                        }
                        else
                        {
                            Session["grvDriversOk"] = drivers.getDriversOk(lstArbol, Session["usuario"].ToString().Split(';')[0]);
                            btnGuardar.Enabled = true;
                        }

                        Session["grvDrivers"] = lstArbol;
                        grid_Driver.DataSource = Session["grvDrivers"];
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

        
        protected void btnTemplate_Click(object sender, EventArgs e)
        {
            GenerarPlantilla();
        }

        public void GenerarPlantilla()
        {
            try
            {
                //string path = @valoresParams.GetByClase("RUTA_PLANTILLA").vhpg_valor;
                string path = Server.MapPath("/Templates");
                string archivoFinal = path + "\\PlantillaCargueDrivers.xls";
                /*
                 * string plantilla = path + "plantilla.xls";
                 * File.Copy(plantilla, archivoFinal, true);
                 * if (!Directory.Exists(path))
                 * {
                 *    DirectoryInfo di = Directory.CreateDirectory(path);
                 * }
                */
                IWorkbook  workbook = new XSSFWorkbook();
                XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("Hoja1");
                XSSFRow rowTitle = (XSSFRow)sheet.CreateRow(0);

                // Producto
                XSSFCell cellTitle = (XSSFCell)rowTitle.CreateCell(0);
                cellTitle.SetCellValue("Producto");

                // Empresa
                cellTitle = (XSSFCell)rowTitle.CreateCell(1);
                cellTitle.SetCellValue("Empresa");

                // Sede
                cellTitle = (XSSFCell)rowTitle.CreateCell(2);
                cellTitle.SetCellValue("Sede");

                // Centro Costos
                cellTitle = (XSSFCell)rowTitle.CreateCell(3);
                cellTitle.SetCellValue("Centro Costos");

                // Cantidad
                cellTitle = (XSSFCell)rowTitle.CreateCell(4);
                cellTitle.SetCellValue("Cantidad");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(5);
                cellTitle.SetCellValue("Valor");

                // Proveedor
                cellTitle = (XSSFCell)rowTitle.CreateCell(6);
                cellTitle.SetCellValue("Proveedor");

                FileStream file = File.Create(archivoFinal);
                workbook.Write(file);
                file.Close();

                //System.Diagnostics.Process.Start(archivoFinal);

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/octet-stream";
                response.AddHeader("Content-Disposition", "attachment; filename=" + "PlantillaCargueDrivers.xls");
                response.TransmitFile(archivoFinal);
                response.Flush();
                response.End();

            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarError(ex.Message);
            }
        }

        protected void gridPendientes_PageIndexChanged(object sender, EventArgs e)
        {
            string a = "";
        }
    }
}