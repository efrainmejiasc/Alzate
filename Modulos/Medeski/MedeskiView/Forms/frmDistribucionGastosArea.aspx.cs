using DevExpress.Web;
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
    public partial class frmDistribucionGastosArea : System.Web.UI.Page
    {
        CtrParametros param = new CtrParametros();
        CtrCargueGastosArea gtos = new CtrCargueGastosArea();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        CtrPeriodoPresupuesto periodo = new CtrPeriodoPresupuesto();
        CtrCentroCosto ccostos = new CtrCentroCosto();
        CtrProductos productos = new CtrProductos();
        CVwSalidaPresupuesto presupuesto = new CVwSalidaPresupuesto();

        bool estaIgual = true;
        bool exitenErrores = false;
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    Session["path"] = string.Empty;
                    gvGastosArea.DataSource = null;
                    gvGastosArea.DataBind();
                    gvGastosArea.SettingsPager.PageSizeItemSettings.Visible = true;
                    Cutilidades.ConfigurarGrid(gvGastosArea);
                    cargarActivos();
                    Session["pantallaInicio"] = "1";
                }
                else
                {
                    string pantalla = Session["pantallaInicio"] as string;
                    if (pantalla.Equals("1"))
                    {
                        gvGastosArea.DataSource = Session["grvGastoArea"];
                        gvGastosArea.DataBind();
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

        protected void cargarActivos()
        {
            Session["grvGastoArea"] = null;
            Session["grvGastoArea"] = gtos.cargarActuales();
            gvGastosArea.DataSource = Session["grvGastoArea"];
            gvGastosArea.DataBind();
            btnGuardar.Enabled = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            IList<DTOgenericoCargueArchivos> lstCarg = (List<DTOgenericoCargueArchivos>)Session["grvGastoArea"];
            IList<GE_TDISTRIBUCIONCARGUEGA> lstGastosArea = new List<GE_TDISTRIBUCIONCARGUEGA>();

            IList<GE_TPRODUCTOS> lstProductos = new List<GE_TPRODUCTOS>();
            lstProductos = productos.GetAll();
            
            IList<GE_TCENTROSCOSTOS> lstCentroCostos = new List<GE_TCENTROSCOSTOS>();
            lstCentroCostos = ccostos.GetAll();

            GE_TPERIODOPRESUPUESTO objPeriodo = new GE_TPERIODOPRESUPUESTO();
            objPeriodo = periodo.GetAllActive()[0];

                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);
                String usr = strUsuario[0].ToString();

            try
            {
                foreach (var item in lstCarg)
                {
                    int idProducto = lstProductos.Where(x => x.prod_codigo == item.dto_generic_productos)
                                                .Select(y => y.prod_consecutivo)
                                                .FirstOrDefault();

                    int idCCosto = lstCentroCostos.Where(a => a.cost_codigo == item.dto_generic_ccostos)
                                                  .Select(b => b.cost_consecutivo)
                                                  .FirstOrDefault();

                    GE_TDISTRIBUCIONCARGUEGA objGastosArea = new GE_TDISTRIBUCIONCARGUEGA();

                    objGastosArea.card_periodo = objPeriodo.peri_consecutivo;
                    objGastosArea.card_producto = idProducto;
                    objGastosArea.card_ccosto = idCCosto;
                    objGastosArea.card_valor = Convert.ToInt32(item.dto_generic_valor);
                    objGastosArea.card_usuario = usr;
                    objGastosArea.card_fecha = DateTime.Now;
                    lstGastosArea.Add(objGastosArea);
                }
                gtos.Guardar(lstGastosArea);
                VentanaValidaciones.mostrarRegistroExitoso();
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se completo la transacción de forma exitosa " + ex.Message);
            }
            
        }

        public void mostrar_datos()
        {
            try
            {
                string FilePath = Session["path"] as string;
                if (!String.IsNullOrEmpty(FilePath))
                {
                    Session["grvGastoArea"] = null;
                    IList<DTOgenericoCargueArchivos> lstCarg = gtos.LeerExcel("Hoja1", FilePath).ToList<DTOgenericoCargueArchivos>();
                    sumaPresupuesto(lstCarg);
                    Session["grvGastoArea"] = lstCarg.OrderByDescending(x => x.dto_generic_observaciones).ToList();
                    gvGastosArea.DataSource = Session["grvGastoArea"];
                    gvGastosArea.DataBind();
                    Cutilidades.ConfigurarGrid(gvGastosArea);
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

        public bool sumaPresupuesto(IList<DTOgenericoCargueArchivos> p_lstCarg)
        {
            
            double sumaSalidaPresupuesto = presupuesto.GetSumSalidaGastosArea();
            double sumaArchivo = Convert.ToDouble(p_lstCarg.Sum(b => Convert.ToInt32(b.dto_generic_valor)));

            estaIgual = sumaSalidaPresupuesto == sumaArchivo ? true : false;

            return estaIgual;

        }

        public void mensajes()
        {
            string msn = "";
            string aux = "";

            msn = estaIgual == false ? "La suma de los valores del archivo no coincide con el presupuesto" : "";
            aux = msn;
            msn = exitenErrores == true ? " El archivo presenta inconsistencias no existen algunos items revise observaciones" : "";
            aux = aux + msn;

            if (!aux.Equals(""))
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Advertencia", aux);
            }
        }

        //Valido que no existan obsercaiones en el cargue
        public void buscaErrores(IList<DTOgenericoCargueArchivos> p_lstCarg)
        {
            int cantObservaiones;
            try
            {
                cantObservaiones = p_lstCarg.Where(x => x.dto_generic_observaciones != "" && x.dto_generic_observaciones != null).Count();

                if (cantObservaiones > 0)
                {
                    exitenErrores = true;
                }
            }
            catch
            {
                exitenErrores = false;
                btnGuardar.Enabled = false;
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "Revisar la estructura del archivo");
            }
        }

        protected void cbpProgreso_Callback(object sender, CallbackEventArgsBase e)
        {
            IList<DTOgenericoCargueArchivos> lstCarg =(List<DTOgenericoCargueArchivos>) Session["grvGastoArea"];
            buscaErrores(lstCarg);

            if (exitenErrores || !estaIgual)
            {
                btnGuardar.ClientEnabled = false;
                mensajes();
            }
            else
            {
                btnGuardar.Enabled = true;
            }
            
        }
    }
}