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
    public partial class frmRedistribucionDriver : System.Web.UI.Page
    {
        CtrUtilidades Cutilidades = new CtrUtilidades();
        CtrRedistribucionDrivers ctrRedistribucion = new CtrRedistribucionDrivers();

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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                ctrRedistribucion.Guardar(Session["grvDriversOk"] as List<DTOgenericoCargueArchivos>, strUsuario[0].ToString());
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
                /*
                if(!IsPostBack)
                    Session["grvDriversOk"] = ctrRedistribucion.GetAllActive();
                else
                 * */
                Session["grvDriversOk"] = ctrRedistribucion.GetAllActive();

                grid_Driver.DataSource = Session["grvDriversOk"];
                grid_Driver.DataBind();
                Cutilidades.ConfigurarGrid(grid_Driver);       
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se carga gridview. " + ex.Message);
            }
        }
    }
}