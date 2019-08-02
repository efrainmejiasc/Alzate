using DevExpress.Web;
using MedeskiView.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedeskiView.Forms
{
    public partial class frmCompanias_form : System.Web.UI.Page
    {
        CtrCompanias CtrCompanias = new CtrCompanias();
        CtrUtilidades CUtilidades = new CtrUtilidades();
        Hashtable campoSeleccionado = null;
        string[] camposClaseparametro = new string[] { "comp_consecutivo", "comp_nombre", "comp_descripcion", "comp_usa_co", "comp_activo" };
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {               
                CargarListas();

                if (!IsPostBack)
                {
                    if (Session["objeto"] != null)
                    {
                        GE_TCOMPANIAS mObjeto = Session["objeto"] as GE_TCOMPANIAS;

                        txtConsecutivo["txtConsecutivo"] = mObjeto.comp_consecutivo.ToString();
                        txtNombre.Value = mObjeto.comp_nombre.ToString();
                        txtDescripcion.Value = mObjeto.comp_descripcion.ToString();

                        if (mObjeto.comp_usa_co != null)
                        {
                            ListEditItem liUsaCO = cmbUsaCO.Items.FindByValue(mObjeto.comp_usa_co.ToString());
                            liUsaCO.Selected = true;
                        }

                        if (mObjeto.comp_activo != null)
                        {
                            ListEditItem liEstado = cmbActivo.Items.FindByValue(mObjeto.comp_activo.ToString());
                            liEstado.Selected = true;
                        }

                    }
                }
            }
            else
                Response.Redirect(strUrl);
        }
        
        #region Metodos
        protected void CargarListas()
        {
            try
            {

                if (!txtConsecutivo.Contains("txtConsecutivo"))
                {
                    txtConsecutivo["txtConsecutivo"] = null;
                }

                cmbUsaCO.Items.Clear();
                cmbUsaCO.Items.Add("Utiliza Centro de Costos?", null);
                cmbUsaCO.Items.Add("SI", "1");
                cmbUsaCO.Items.Add("NO", "0");

                cmbActivo.Items.Clear();
                cmbActivo.Items.Add("Seleccionar Activo", null);
                cmbActivo.Items.Add("ACTIVO", 1);
                cmbActivo.Items.Add("INACTIVO", 0);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar las listas. " + ex.Message);
            }
        }

        protected void limpiar()
        {
            txtConsecutivo["txtConsecutivo"] = null;
            txtNombre.Value = "";
            txtDescripcion.Value = "";
            cmbUsaCO.Value = "";
            cmbActivo.Value = "";
        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarTxtObligatorio("Nombre", txtNombre.Text, 50);
                VentanaValidaciones.validarTxtObligatorio("Descripcion", txtDescripcion.Text, 500);

                VentanaValidaciones.validarComboObligatorio("Usa Centro de Costos", cmbUsaCO.Value);
                VentanaValidaciones.validarComboObligatorio("Activo", cmbActivo.Value);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Eventos

        protected void RegresarClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmCompanias.aspx");
        }

        protected void NuevoClicked(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void GuardarClicked(object sender, EventArgs e)
        {
            try
            {
                if (!validar())
                {
                    return;
                }

                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                GE_TCOMPANIAS compania = new GE_TCOMPANIAS();

                compania.comp_nombre = txtNombre.Text.ToString();
                compania.comp_descripcion = txtDescripcion.Text.ToString();
                compania.comp_usa_co = Convert.ToInt32(cmbUsaCO.Value);
                compania.comp_activo = Convert.ToInt32(cmbActivo.Value);
                compania.comp_fecha = DateTime.Now;
                compania.comp_usuario = strUsuario[0].ToString();

                if (txtConsecutivo["txtConsecutivo"] != null)
                {
                    compania.comp_consecutivo = Convert.ToInt32(txtConsecutivo["txtConsecutivo"].ToString());
                    CtrCompanias.Update(compania);
                }
                else
                {
                    CtrCompanias.Add(compania);
                }

                Session["mensaje"] = "OK";
                Session["compania"] = compania;
                Response.Redirect("frmCompanias.aspx", false);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede guardar el registro. " + ex.Message);
            }
        }
        #endregion
    }
}