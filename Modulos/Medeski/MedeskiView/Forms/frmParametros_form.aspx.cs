using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedeskiView.Controllers;
using DevExpress.Web;
using System.Collections;

namespace MedeskiView.Forms
{
    public partial class frmParametros_form : System.Web.UI.Page
    {
        CtrParametros ctrParam = new CtrParametros();
        CtrVlrsParamGrales parametros = new CtrVlrsParamGrales();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] {"clap_clase", "clap_nombre", "clap_descripcion", "clap_estado", "clap_fechaini", "clap_fechafin" };

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + parametros.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                if (!IsPostBack)
                {
                    if (Session["objeto"] != null)
                    {
                        GE_TCLASESPARAMETROS mObjeto = Session["objeto"] as GE_TCLASESPARAMETROS;

                        txtClapClase["txtClapClase"] = mObjeto.clap_clase.ToString();
                        txtNombre.Value = mObjeto.clap_nombre.ToString();
                        txtDescripcion.Value = mObjeto.clap_descripcion.ToString();
                        dateEdit.Date = DateTime.Parse(mObjeto.clap_fechaini.ToString());
                        ASPxDateEdit1.Value = string.IsNullOrEmpty(mObjeto.clap_fechafin.ToString()) ? "" : mObjeto.clap_fechafin.ToString();
                        lbxEstado.Value = mObjeto.clap_estado.ToString();
                    }
                }
            }
            else
                Response.Redirect(strUrl);
        }

        private void limpiar()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            lbxEstado.Value = "";
            dateEdit.Value = "";
            ASPxDateEdit1.Value = "";
            txtClapClase["txtClapClase"] = string.Empty;
        }

        private bool validar()
        {
            try
            {
                if (ASPxDateEdit1.Date < dateEdit.Date || Convert.ToDateTime(ASPxDateEdit1.Text) < dateEdit.Date)
                {
                    VentanaValidaciones.mostrarMensajePersonalizado("Error", "La fecha final no puede ser menor que la fecha inicial");
                    return false;
                }
                VentanaValidaciones.validarTxtObligatorio("Codigo", txtNombre.Text, 100);
                VentanaValidaciones.validarTxtObligatorio("Descripción", txtDescripcion.Text, 200);
                VentanaValidaciones.validarTxtNumericoObligatorio("Estado", lbxEstado.Value, 1);
                VentanaValidaciones.validarFechaObligatoria("Fecha Inicial", dateEdit.Date.ToShortDateString(), 20);
            }
            catch
            {
                return false;
            }
            return true;
        }


        protected void RegresarClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmParametros.aspx");
        }
        
        protected void GuardarClicked(object sender, EventArgs e)
        {
            try
            {
                if (!validar())
                {
                    return;
                }

                GE_TCLASESPARAMETROS cParam = new GE_TCLASESPARAMETROS();
                cParam.clap_nombre = txtNombre.Text;
                cParam.clap_descripcion = txtDescripcion.Text;
                cParam.clap_fechaini = dateEdit.Date;
                cParam.clap_fechafin = ASPxDateEdit1.Date;
                cParam.clap_estado = Convert.ToInt32(lbxEstado.Value.ToString());

                if ( txtClapClase.Contains("txtClapClase") && !string.IsNullOrEmpty(txtClapClase["txtClapClase"].ToString()))
                {
                    cParam.clap_clase = Convert.ToInt32(txtClapClase["txtClapClase"].ToString());
                    ctrParam.Update(cParam);

                }
                else
                {
                    ctrParam.Add(cParam);
                }

                Session["clase"] = cParam;
                Session["mensaje"] = "OK";
                Response.Redirect("frmParametros.aspx", false);
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
    }
}