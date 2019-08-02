using DevExpress.Web;
using Medeski.BusinessLogic.Class;
using MedeskiView.Controllers;
using MedeskiView.Engine;
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
    public partial class frmCargueDistribucionPersonas : System.Web.UI.Page
    {
        CtrCargueGastosLaborales cCargue = new CtrCargueGastosLaborales();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        CtrVlrsParamGrales valoresParams = new CtrVlrsParamGrales();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                string n = Session["usuario"].ToString();
                if (!IsPostBack)
                {
                    Session["path"] = string.Empty;
                    Session["pantallaInicio"] = "1";
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
                        if (file.FileName != "PlantillaDistribucionPersona.xls")
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
                string archivoFinal = path + "PlantillaDistribucionPersona.xls";

                IWorkbook workbook = new XSSFWorkbook();
                XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("Hoja1");
                XSSFRow rowTitle = (XSSFRow)sheet.CreateRow(0);

                XSSFCell cellTitle = (XSSFCell)rowTitle.CreateCell(0);
                cellTitle.SetCellValue("Colaborador");

                cellTitle = (XSSFCell)rowTitle.CreateCell(1);
                cellTitle.SetCellValue("Cedula");

                cellTitle = (XSSFCell)rowTitle.CreateCell(2);
                cellTitle.SetCellValue("Criterio de Distribucion"); 

                cellTitle = (XSSFCell)rowTitle.CreateCell(3);
                cellTitle.SetCellValue("Codigo de Area");

                cellTitle = (XSSFCell)rowTitle.CreateCell(4);
                cellTitle.SetCellValue("Area");

                cellTitle = (XSSFCell)rowTitle.CreateCell(5); 
                cellTitle.SetCellValue("Descripcion");

                cellTitle = (XSSFCell)rowTitle.CreateCell(6);
                cellTitle.SetCellValue("Codigo Descripcion");

                cellTitle = (XSSFCell)rowTitle.CreateCell(7); 
                cellTitle.SetCellValue("Porcentaje Distribucion");

                FileStream file = File.Create(archivoFinal);
                workbook.Write(file);
                file.Close();

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/octet-stream";
                response.AddHeader("Content-Disposition", "attachment; filename=" + "PlantillaDistribucionPersona.xls");
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
            dt = Funcion.ReadExcelDistribucionPersonas(Session["path"].ToString(),usuario);
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
                Dictionary<string, Decimal> Sumatoria = Funcion.ValidarDistribucion(dt);
                bool flag = false;
                foreach (KeyValuePair<string, Decimal> entry in Sumatoria)
                {
                   if(entry.Value > 100)
                    {
                        flag = true;
                        VentanaValidaciones1.mostrarMensajePersonalizado(entry.Key, " La sumatoria no puede ser mayor a 100,modifique el archivo e intentelo de nuevo");
                    }
                }

                if (flag)
                    return;

                bool resultado = Metodo.InsertDistribucionPersona(dt); 
                if (resultado)
                    VentanaValidaciones1.mostrarRegistroExitoso();
                else
                    VentanaValidaciones1.mostrarMensajePersonalizado("Error","");
            }
            catch (Exception ex)
            {
                VentanaValidaciones1.mostrarMensajePersonalizado("Error", "Se presento errores al guardar");
            }

        }

    }
}