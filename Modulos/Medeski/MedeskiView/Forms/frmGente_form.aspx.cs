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
    public partial class frmGente_form : System.Web.UI.Page
    {
        CtrUtilidades CUtilidades = new CtrUtilidades();
        CtrPeriodoPresupuesto CPeriodo = new CtrPeriodoPresupuesto();
        CtrGente CGente = new CtrGente();

        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "gent_consecutivo", "GE_TPERIODOPRESUPUESTO.peri_consecutivo", "GE_TPERSONAS.pers_consecutivo", "GE_TPERSONAS.pers_identificacion", 
                                                        "GE_TCENTROSCOSTOS.cost_consecutivo", "GE_TCENTROSCOSTOS.cost_descripcion", "GE_TPERSONAS.GE_TPARAMETROS1.parm_descripcion", 
                                                        "GE_TPERSONAS.GE_TPARAMETROS.parm_descripcion", "GE_TPERSONAS.pers_nombre", "GE_TPERSONAS.pers_apellido",
                                                        "GE_TPERIODOPRESUPUESTO.peri_ano", "GE_TCENTROSCOSTOS.cost_codigo",
                                                        "gent_costo_colaborador", "gent_porcentaje_manual_dedicacion", "gent_estado" };
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                CargarListas();
                if (!IsPostBack)
                {
                    if (Session["persona"] != null)
                    {
                        GE_TPERSONAS mPersona = Session["persona"] as GE_TPERSONAS;
                        GE_TGENTE mObjeto = CGente.getByPersonaId(mPersona.pers_consecutivo);
                        if (mObjeto != null)
                        {
                            txtConsecutivo["txtConsecutivo"] = mObjeto.gent_consecutivo.ToString();
                            txtPeriodo["txtPeriodo"] = mObjeto.GE_TPERIODOPRESUPUESTO.peri_consecutivo.ToString();
                            txtIdPersona["txtIdPersona"] = mObjeto.GE_TPERSONAS.pers_consecutivo.ToString();
                            txtIdentificacion["txtIdentificacion"] = mObjeto.GE_TPERSONAS.pers_identificacion.ToString();
                            txtIdCcostos["txtIdCcostos"] = mObjeto.GE_TCENTROSCOSTOS != null ? mObjeto.GE_TCENTROSCOSTOS.cost_consecutivo.ToString() : "";
                            txtDescCcostos["txtDescCcostos"] = mObjeto.GE_TCENTROSCOSTOS != null ? mObjeto.GE_TCENTROSCOSTOS.cost_descripcion.ToString() : "";
                            txtCargo["txtCargo"] = mObjeto.GE_TPERSONAS.GE_TPARAMETROS1.parm_descripcion.ToString();
                            txtEmpresa["txtEmpresa"] = mObjeto.GE_TPERSONAS.GE_TPARAMETROS.parm_descripcion.ToString();

                            txtNombre.Value = mObjeto.GE_TPERSONAS.pers_nombre.ToString();
                            txtApellido.Value = mObjeto.GE_TPERSONAS.pers_apellido.ToString();
                            txtCentroCostos.Value = mObjeto.GE_TCENTROSCOSTOS != null ? mObjeto.GE_TCENTROSCOSTOS.cost_codigo.ToString() : "";
                            txtPorcentaje.Value = mObjeto.gent_porcentaje_manual_dedicacion.ToString();
                            txtCostoColaborador.Value = mObjeto.gent_costo_colaborador.ToString();

                            ListEditItem liEstado = cmbActivo.Items.FindByValue(mObjeto.gent_estado.ToString());
                            liEstado.Selected = true;
                        }
                    }
                }
            }
            else
                Response.Redirect(strUrl);
        }

        #region Metodos

        private void CargarListas()
        {
            try
            {
                cmbActivo.Items.Clear();
                cmbActivo.Items.Add("Seleccione Estado", null);
                cmbActivo.Items.Add("Activo", 1);
                cmbActivo.Items.Add("Inactivo", 0);
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarError("Error cargando listas. " + ex.Message);
            }
        }

        private void limpiar()
        {
            txtConsecutivo["txtConsecutivo"] = string.Empty;
            txtPeriodo["txtPeriodo"] = string.Empty;
            txtIdPersona["txtIdPersona"] = string.Empty;
            txtIdentificacion["txtIdentificacion"] = string.Empty;
            txtIdCcostos["txtIdCcostos"] = string.Empty;
            txtDescCcostos["txtDescCcostos"] = string.Empty;
            txtCargo["txtCargo"] = string.Empty;
            txtEmpresa["txtEmpresa"] = string.Empty;
            
            txtNombre.Value = "";
            txtCentroCostos.Value = "";
            txtPorcentaje.Value = "";
            txtCostoColaborador.Value = "";
            cmbActivo.Value = "";
        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarTxtNumericoObligatorio("Porcentaje", txtPorcentaje.Text, 50);
                VentanaValidaciones.validarTxtNumericoObligatorio("Costo Colaborador", txtCostoColaborador.Text, 50);
                VentanaValidaciones.validarComboObligatorio("Estado", cmbActivo.Value);
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
            Response.Redirect("frmPersonas.aspx");
        }

        protected void IdGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.Equals("gent_estado"))
            {
                if (Convert.ToInt32(e.Value) == 1)
                {
                    e.DisplayText = "Activo";
                }
                else
                {
                    e.DisplayText = "Inactivo";
                }
            }
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

                GE_TGENTE gente = new GE_TGENTE();
                
                gente.gent_ccostos = txtIdCcostos["txtIdCcostos"].ToString() != "" ? Convert.ToInt32(txtIdCcostos["txtIdCcostos"].ToString()) : (int?) null;
                gente.gent_descripcion_ccostos = txtDescCcostos["txtDescCcostos"].ToString();
                gente.gent_nombre_cargo = txtCargo["txtCargo"].ToString();
                gente.gent_empresa = txtEmpresa["txtEmpresa"].ToString();
                gente.gent_costo_colaborador = Convert.ToDecimal(txtCostoColaborador.Value.ToString());
                gente.gent_persona = Convert.ToInt32(txtIdPersona["txtIdPersona"].ToString());
                gente.gent_porcentaje_manual_dedicacion = Convert.ToDecimal(txtPorcentaje.Value.ToString());
                gente.gent_usuario_carga = strUsuario[0].ToString();
                gente.gent_fecha_cargue = DateTime.Now;
                gente.gent_estado = Convert.ToInt32(cmbActivo.Value);
                gente.gent_periodo = CPeriodo.GetPeriodoActivo().peri_consecutivo;
                
                if (Convert.ToInt32(txtConsecutivo["txtConsecutivo"].ToString()) == 0)
                {
                    CGente.Add(gente);
                }
                else
                {
                    gente.gent_consecutivo = Convert.ToInt32(txtConsecutivo["txtConsecutivo"].ToString());
                    CGente.Update(gente);
                }

                Session["mensaje2"] = "OK";
                Session["persona"] = null;
                    
                Response.Redirect("frmPersonas.aspx", false);
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarError("Error guardando registro. " + ex.Message);
            }
        }
        #endregion

    }
}