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
    public partial class frmCentroOperaciones_form : System.Web.UI.Page
    {
        CtrCentroOperacion ctrCentroOperaciones = new CtrCentroOperacion();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "ceop_consecutivo", "ceop_codigo", "ceop_descripcion", "ceop_vicepresidencia", "ceop_activo" };
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    if (Session["objeto"] != null)
                    {
                        GE_TCENTROSOPERACION mObjeto = Session["objeto"] as GE_TCENTROSOPERACION;

                        txtConsecutivo["txtConsecutivo"] = mObjeto.ceop_consecutivo.ToString();
                        txtCodigo.Value = mObjeto.ceop_codigo.ToString();
                        txtDescripcion.Value = mObjeto.ceop_descripcion.ToString();

                        if (mObjeto.ceop_activo != null)
                        {
                            ListEditItem liEstado = cmbEstado.Items.FindByValue(mObjeto.ceop_activo.ToString());
                            liEstado.Selected = true;
                        }

                        if (mObjeto.ceop_vicepresidencia != "")
                        {
                            ListEditItem liVicepre = cmbVicepresidencia.Items.FindByValue(mObjeto.ceop_vicepresidencia.ToString());
                            liVicepre.Selected = true;
                        }
                    }
                }
            }
            else
                Response.Redirect(strUrl);
        }
        
        #region Metodos

        protected void limpiar()
        {
            txtConsecutivo["txtConsecutivo"] = string.Empty;
            txtCodigo.Value = "";
            txtDescripcion.Value = "";
            cmbEstado.Value = "";
            cmbVicepresidencia.Value = "";


        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarTxtObligatorio("Codigo", txtCodigo.Text, 50);
                VentanaValidaciones.validarTxtObligatorio("Descripción", txtDescripcion.Text, 500);

                VentanaValidaciones.validarComboObligatorio("Es Vicepresidencia", cmbVicepresidencia.Value);
                VentanaValidaciones.validarComboObligatorio("Activo", cmbEstado.Value);
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
            Response.Redirect("frmCentroOperaciones.aspx");
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

                GE_TCENTROSOPERACION centroOperaciones = new GE_TCENTROSOPERACION();
                centroOperaciones.ceop_codigo = txtCodigo.Text;
                centroOperaciones.ceop_descripcion = txtDescripcion.Text;

                if (cmbVicepresidencia.Value.ToString().Equals("SI"))
                {
                    centroOperaciones.ceop_vicepresidencia = "S";
                }
                else if (cmbVicepresidencia.Value.ToString().Equals("NO"))
                {
                    centroOperaciones.ceop_vicepresidencia = "N";
                }
                else
                {
                    centroOperaciones.ceop_vicepresidencia = cmbVicepresidencia.Value.ToString();
                }

                centroOperaciones.ceop_activo = Convert.ToInt32(cmbEstado.Value);
                centroOperaciones.ceop_usuario = strUsuario[0].ToString();
                centroOperaciones.ceop_fecha = DateTime.Now;

                if (txtConsecutivo.Contains("txtConsecutivo") && !string.IsNullOrEmpty(txtConsecutivo["txtConsecutivo"].ToString()))
                {
                    centroOperaciones.ceop_consecutivo = Convert.ToInt32(txtConsecutivo["txtConsecutivo"].ToString());
                    ctrCentroOperaciones.Update(centroOperaciones);

                }
                else
                {
                    ctrCentroOperaciones.Add(centroOperaciones);
                }

                Session["mensaje"] = "OK";
                Response.Redirect("frmCentroOperaciones.aspx", false);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede guardar el registro. " + ex.Message);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e) 
        {
            limpiar();
        }
        #endregion 
    }
}