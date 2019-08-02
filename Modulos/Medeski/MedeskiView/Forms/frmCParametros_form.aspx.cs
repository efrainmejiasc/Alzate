using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedeskiView.Controllers;

namespace MedeskiView.Forms
{
    public partial class frmCParametros_form : System.Web.UI.Page
    {
        CtrCParametros ctrParam = new CtrCParametros();
        CtrParametros ctrCparam = new CtrParametros();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "parm_consecutivo", "clap_clase", "parm_descripcion", "parm_codigo", "parm_estado", "parm_fechadesde", "parm_fechahasta" };
        CtrVlrsParamGrales parametros = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + parametros.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                listas();
                if (!IsPostBack)
                {
                    if (Session["objeto"] != null)
                    {
                        GE_TPARAMETROS mObjeto = Session["objeto"] as GE_TPARAMETROS;

                        txtParametro["txtParametro"] = mObjeto.parm_consecutivo;
                        txtDescripcion.Text = mObjeto.parm_descripcion.ToString();
                        txtCodigo.Text = mObjeto.parm_codigo.ToString();
                        cmbClase.Value = mObjeto.clap_clase.ToString();
                        cmbEstado.Value = mObjeto.parm_estado.ToString();
                        DateTime dtFecha = DateTime.Parse(mObjeto.parm_fechadesde.ToString());
                        dtFechaini.Date = dtFecha;
                        dtFechafin.Value = string.IsNullOrEmpty(mObjeto.parm_fechahasta.ToString()) ? "" : mObjeto.parm_fechahasta.ToString();
                    }
                }
                
            }
            else
                Response.Redirect(strUrl);
        }

        #region Metodos
        public void limpiar()
        {
            txtDescripcion.Text = "";
            txtCodigo.Text = "";
            dtFechaini.Value = "";
            dtFechafin.Value = "";
            txtParametro["txtParametro"] = string.Empty;
            cmbClase.Value = null;
            cmbEstado.Value = null;
        }

        public void listas()
        {
            try
            {
                cmbClase.Items.Clear();
                cmbClase.Items.Add("Seleccionar la clase", null);


                GE_TCLASESPARAMETROS clase = Session["clase"] as GE_TCLASESPARAMETROS;
                IList<GE_TCLASESPARAMETROS> lstClase = ctrCparam.GetAll().Where(x => x.clap_clase == clase.clap_clase).ToList();

                foreach (GE_TCLASESPARAMETROS item in lstClase)
                {
                    cmbClase.Items.Add(item.clap_descripcion,item.clap_clase);
                }

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar los registros. " + ex.Message);
            }
        }

        private bool validar()
        {
            try
            {
                if (dtFechafin.Date < dtFechaini.Date || Convert.ToDateTime(dtFechafin.Text) < dtFechaini.Date)
                {
                    VentanaValidaciones.mostrarMensajePersonalizado("Error", "La fecha final no puede ser menor que la fecha inicial");
                    return false;
                }


                VentanaValidaciones.validarTxtObligatorio("Descripción", txtDescripcion.Text, 100);
                VentanaValidaciones.validarTxtNumericoObligatorio("Estado", cmbEstado.Value, 1);
                VentanaValidaciones.validarTxtNumericoObligatorio("Clase", cmbClase.Value, 6);
                VentanaValidaciones.validarFechaObligatoria("Fecha Inicial", dtFechaini.Date.ToShortDateString(), 20);
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
            Response.Redirect("frmCParametros.aspx");
        }
        
        protected void btnNuevo_Click(object sender, EventArgs e)
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

                GE_TPARAMETROS Param = new GE_TPARAMETROS();
                Param.parm_descripcion = txtDescripcion.Text;
                Param.parm_codigo = txtCodigo.Text;
                Param.parm_estado = Convert.ToInt32(cmbEstado.Value.ToString());
                Param.parm_fechadesde = dtFechaini.Date;
                Param.parm_fechahasta = dtFechafin.Text == "" ? (DateTime?)null : dtFechafin.Date;
                Param.clap_clase = Convert.ToInt32(cmbClase.Value.ToString());

                if (txtParametro.Contains("txtParametro") && !string.IsNullOrEmpty(txtParametro["txtParametro"].ToString()))
                {
                    Param.parm_consecutivo = Convert.ToInt32(txtParametro["txtParametro"].ToString());
                    ctrParam.Update(Param);

                }
                else
                {
                    ctrParam.Add(Param);
                }

                Session["mensaje"] = "OK";
                Response.Redirect("frmCParametros.aspx", false);

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede guardar el registro. " + ex.Message);
            }
        }
        #endregion

    }
}