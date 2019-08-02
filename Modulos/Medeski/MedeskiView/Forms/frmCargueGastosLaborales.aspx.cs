using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DevExpress.Web;
using MedeskiView.Controllers;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MedeskiView.Forms
{
    public partial class frmCargueGastosLaborales : System.Web.UI.Page
    {
        CtrCargueGastosLaborales cCargue = new CtrCargueGastosLaborales();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        CtrVlrsParamGrales valoresParams = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                CargarListas();

                if (!IsPostBack)
                {
                    Char delimiter = ';';
                    string[] strUsuario = null;
                    strUsuario = Session["usuario"].ToString().Split(delimiter);

                    Session["path"] = string.Empty;
                    Session["datos"] = null;
                    gvPpto.DataSource = cCargue.GetAll().Where(x => x.carl_usuario.ToUpper().Equals(strUsuario[0].ToString().ToUpper())).ToList<GE_TCARGUEARCHIVOSLABORAL>(); 
                    gvPpto.DataBind();
                    gvPpto.SettingsPager.PageSizeItemSettings.Visible = true;
                    Cutilidades.ConfigurarGrid(gvPpto);
                }
                else
                {
                    mostrar_datos();                    
                }
            }
            else
                Response.Redirect(strUrl);
        }

        protected void CargarListas()
        {
            try
            {
                cmbSubcategoria.Items.Clear();
                cmbSubcategoria.Items.Add("Cuentas Especiales", "CE");
                cmbSubcategoria.Items.Add("Gastos de Área", "GA");
                cmbSubcategoria.Items.Add("Otros Gastos de Área", "OT");
            }
            catch (Exception ex)
            {
                VentanaValidaciones1.mostrarError("No se puede cargar las listas. " + ex.Message);
            }
        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones1.validarComboObligatorio("Subcategoría", cmbSubcategoria.Value);
            }
            catch
            {
                return false;
            }
            return true;
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
                    }
                }
            }
            catch
            {
                VentanaValidaciones1.mostrarMensajePersonalizado("Error", "El archivo no es válido");
            }
        }

        protected void mostrar_datos()
        {
            try
            {   
                string FilePath = Session["path"] as string;
                
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                if (!String.IsNullOrEmpty(FilePath))
                {
                    IList<GE_TCARGUEARCHIVOSLABORAL> lstCarg = cCargue.LeerDatos(cmbSubcategoria.Value.ToString(), 0, FilePath, strUsuario[0].ToString()).OrderByDescending(x => x.carl_observaciones).ToList<GE_TCARGUEARCHIVOSLABORAL>();
                    Session["datos"] = lstCarg;
                    gvPpto.DataSource = lstCarg;
                    gvPpto.DataBind();
                    Cutilidades.ConfigurarGrid(gvPpto);                    
                }
                else
                {
                    IList<GE_TCARGUEARCHIVOSLABORAL> lstCarg = cCargue.GetAll().Where(x => x.carl_usuario.ToUpper().Equals(strUsuario[0].ToString().ToUpper())).ToList<GE_TCARGUEARCHIVOSLABORAL>();
                    gvPpto.DataSource = lstCarg;
                    gvPpto.DataBind();
                    Cutilidades.ConfigurarGrid(gvPpto);
                }
            }
            catch(Exception ex)
            {
                VentanaValidaciones1.mostrarMensajePersonalizado("Error", "El archivo no es válido." + ex.Message);                
            }
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);
                String usr = strUsuario[0].ToString();
                IList<GE_TCARGUEARCHIVOSLABORAL> lstPpto = (IList<GE_TCARGUEARCHIVOSLABORAL>)gvPpto.DataSource;
                bool anyObserv = lstPpto.Any(x => x.carl_observaciones != null);

                if(anyObserv){
                    VentanaValidaciones1.mostrarError("Existen observaciones en el documento. Por favor, corrija antes de continuar");
                }else{
                    cCargue.Guardar(lstPpto, usr);
                    VentanaValidaciones1.mostrarRegistroExitoso();
                }
                
            }
            catch(Exception ex)
            {
                VentanaValidaciones1.mostrarMensajePersonalizado("Error", "Se presento errores al guardar");
            }

        }

        protected void btnTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                GenerarPlantilla();

            }
            catch (Exception ex)
            {

            }
        }

        public void GenerarPlantilla()
        {
            try
            {
                //string path = @valoresParams.GetByClase("RUTA_PLANTILLA").vhpg_valor;
                string path = Server.MapPath("/Templates");
                string archivoFinal = path + "\\PlantillaCargueGastos.xls";
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
                cellTitle.SetCellValue("Empresa");

                // Descripción Centro Costos
                cellTitle = (XSSFCell)rowTitle.CreateCell(1);
                cellTitle.SetCellValue("Centro Costos");

                // Centro Operación
                cellTitle = (XSSFCell)rowTitle.CreateCell(2);
                cellTitle.SetCellValue("Producto");

                // Documento
                cellTitle = (XSSFCell)rowTitle.CreateCell(3);
                cellTitle.SetCellValue("Item");

                // Cargo
                cellTitle = (XSSFCell)rowTitle.CreateCell(4);
                cellTitle.SetCellValue("Moneda");

                // Empresa
                cellTitle = (XSSFCell)rowTitle.CreateCell(5);
                cellTitle.SetCellValue("Valor");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(6);
                cellTitle.SetCellValue("Cantidad");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(7);
                cellTitle.SetCellValue("Enero");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(8);
                cellTitle.SetCellValue("Febrero");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(9);
                cellTitle.SetCellValue("Marzo");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(10);
                cellTitle.SetCellValue("Abril");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(11);
                cellTitle.SetCellValue("Mayo");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(12);
                cellTitle.SetCellValue("Junio");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(13);
                cellTitle.SetCellValue("Julio");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(14);
                cellTitle.SetCellValue("Agosto");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(15);
                cellTitle.SetCellValue("Septiembre");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(16);
                cellTitle.SetCellValue("Octubre");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(17);
                cellTitle.SetCellValue("Noviembre");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(18);
                cellTitle.SetCellValue("Diciembre");

                // Valor
                cellTitle = (XSSFCell)rowTitle.CreateCell(19);
                cellTitle.SetCellValue("Usuario");

                FileStream file = File.Create(archivoFinal);
                workbook.Write(file);
                file.Close();

                //System.Diagnostics.Process.Start(archivoFinal);

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/octet-stream";
                response.AddHeader("Content-Disposition", "attachment; filename=" + "PlantillaCargueGastos.xls");
                response.TransmitFile(archivoFinal);
                response.Flush();
                response.End();

            }
            catch (Exception ex)
            {
                VentanaValidaciones1.mostrarError(ex.Message);
            }
        }
    }
}