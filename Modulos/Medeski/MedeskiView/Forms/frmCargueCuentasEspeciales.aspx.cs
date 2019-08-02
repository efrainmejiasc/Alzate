using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using MedeskiView.Controllers;


namespace MedeskiView.Forms
{
    public partial class frmCargueCuentasEspeciales : System.Web.UI.Page
    {
        CtrParametros param = new CtrParametros();
        CtrCargueCuentasEspeciales ctas = new CtrCargueCuentasEspeciales();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {                
                if (!IsPostBack)
                {
                    lista();
                    Session["path"] = string.Empty;
                    gvCuentas.DataSource = null;
                    gvCuentas.DataBind();
                    gvCuentas.SettingsPager.PageSizeItemSettings.Visible = true;
                    Cutilidades.ConfigurarGrid(gvCuentas);
                }
                else
                {
                    mostrar_datos();
                }
            }
            else
                Response.Redirect(strUrl);
        }

        #region Eventos

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
                String usr = strUsuario[0].ToString();
                string strProducto = cmbProducto.Value.ToString();
                IList<GE_TCARGUEARCHIVOS> lstPpto = (IList<GE_TCARGUEARCHIVOS>)gvCuentas.DataSource;
                ctas.Guardar(lstPpto, usr, strProducto);
                VentanaValidaciones.mostrarRegistroExitoso();
            }
            catch
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "Se presento errores al guardar");
            }
        }

        protected void gvCuentas_Init(object sender, EventArgs e)
        {
            try
            {
                /*string FilePath = Session["path"] as string;
                if (!String.IsNullOrEmpty(FilePath))
                {
                    IList<GE_TCARGUEARCHIVOS> lstCarg = ctas.LeerDatos(0, FilePath).ToList<GE_TCARGUEARCHIVOS>();
                    gvCuentas.DataSource = lstCarg;
                    gvCuentas.DataBind();
                    Cutilidades.ConfigurarGrid(gvCuentas);
                }
                else
                {
                    if (cmbProducto.Value != null)
                    {
                        IList<GE_TCARGUEARCHIVOS> lstCarg = ctas.GetAllProd(cmbProducto.Value.ToString()).ToList<GE_TCARGUEARCHIVOS>();
                        gvCuentas.DataSource = lstCarg;
                        gvCuentas.DataBind();
                        Cutilidades.ConfigurarGrid(gvCuentas);
                    }
                }*/
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se carga gridview. " + ex.Message);
            }
        }

        protected void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASPxComboBox cmb = (ASPxComboBox)sender;
            gvCuentas.DataSource = ctas.GetAllProd(cmb.Value.ToString());
            gvCuentas.DataBind();
            Cutilidades.ConfigurarGrid(gvCuentas);
            gvCuentas.Settings.ShowFilterRow = false;
        }


        protected void gvCuentas_DataBound(object sender, EventArgs e)
        {
            try
            {
                if (gvCuentas.DataSource != null)
                {
                    ASPxGridView gridView = sender as ASPxGridView;
                    GridViewDataTextColumn c = gridView.Columns["VALOR"] as GridViewDataTextColumn;
                    c.PropertiesTextEdit.DisplayFormatString = "{0: c0}";
                }

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden enlazar los datos al gridview. " + ex.Message);
            }
        }

        #endregion

        #region Metodos
        
        public void mostrar_datos()
        {
            try
            {
                string FilePath = Session["path"] as string;
                if (!String.IsNullOrEmpty(FilePath))
                {
                    IList<GE_TCARGUEARCHIVOS> lstCarg = ctas.LeerDatos(0, FilePath).ToList<GE_TCARGUEARCHIVOS>();
                    gvCuentas.DataSource = lstCarg;
                    gvCuentas.DataBind();
                    Cutilidades.ConfigurarGrid(gvCuentas);
                }
                else
                {
                    if (cmbProducto.Value != null)
                    {
                        IList<GE_TCARGUEARCHIVOS> lstCarg = ctas.GetAllProd(cmbProducto.Value.ToString()).ToList<GE_TCARGUEARCHIVOS>();
                        gvCuentas.DataSource = lstCarg;
                        gvCuentas.DataBind();
                        Cutilidades.ConfigurarGrid(gvCuentas);
                    }
                }
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se carga gridview. " + ex.Message);
            }

        }
        
        public void lista()
        {
            try
            {
                cmbProducto.Items.Clear();
                cmbProducto.Items.Add("Seleccionar la clase", null);
                IList<GE_TPARAMETROS> lstClase = param.GetidList(26).ToList<GE_TPARAMETROS>();

                foreach (GE_TPARAMETROS item in lstClase)
                {
                    cmbProducto.Items.Add(item.parm_descripcion, item.parm_codigo);
                }

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se pueden cargar los registros. ", ex);
            }
        }

        #endregion

    }
}