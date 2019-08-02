using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using Medeski.BusinessLogic.Class;
using MedeskiView.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedeskiView.Forms
{
    public partial class frmFacturacionCargueDrivers_old : System.Web.UI.Page
    {

        CtrUtilidades Cutilidades = new CtrUtilidades();
        CtrCargueDrivers drivers = new CtrCargueDrivers();

        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    Session["path"] = string.Empty;
                    TreeList_Drive.DataSource = null;
                    TreeList_Drive.DataBind();
                    Cutilidades.ConfigurarTreeList(TreeList_Drive);
                    cargarActivos();
                    Session["pantallaInicio"] = "1";
                }
                else
                {
                    string pantalla = Session["pantallaInicio"] as string;
                    if (pantalla.Equals("1"))
                    {
                        TreeList_Drive.DataSource = Session["grvDrivers"];
                        TreeList_Drive.DataBind();
                        Cutilidades.ConfigurarTreeList(TreeList_Drive);
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

        public void cargarActivos()
        {
            Session["grvDrivers"] = null;
            Session["grvDrivers"] = drivers.cargarDriversActivos();
            TreeList_Drive.DataSource = Session["grvDrivers"];
            TreeList_Drive.DataBind();
            Cutilidades.ConfigurarTreeList(TreeList_Drive);
            btnGuardar.Enabled = false;
        }

        public void mostrar_datos()
        {
            try
            {
                string FilePath = Session["path"] as string;
                if (!String.IsNullOrEmpty(FilePath))
                {
                    Session["grvDrivers"] = null;
                    IList<DTOgenericoCargueArchivos> lstCarg = drivers.LeerExcel("Hoja1", FilePath).ToList<DTOgenericoCargueArchivos>();
                    IList<DTOgenericoCargueArchivos> lstArbol = drivers.organizaTreeList(lstCarg);
                    Boolean estaOk = lstArbol.Any(a => a.dto_generic_observaciones != null && a.dto_generic_observaciones != "");
                    
                    if (estaOk)
                    {
                        btnGuardar.Enabled = false;
                    }
                    else
                    {
                        Session["grvDriversOk"] = drivers.getDriversOk(lstCarg, Session["usuario"].ToString().Split(';')[0]);
                        btnGuardar.Enabled = true;
                    }

                    Session["grvDrivers"] = lstArbol;
                    TreeList_Drive.DataSource = Session["grvDrivers"];
                    TreeList_Drive.DataBind();
                    Cutilidades.ConfigurarTreeList(TreeList_Drive);
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                drivers.Guardar(Session["grvDriversOk"] as List<GE_TCARGUEDRIVERS>);
                VentanaValidaciones.mostrarRegistroExitoso();
            }
            catch
            {
                VentanaValidaciones.mostrarErrorEliminar();
            }
        }
    }
}