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
    public partial class frmMonedaItems_form : System.Web.UI.Page
    {
        CtrPeriodoPresupuesto Cperiodo = new CtrPeriodoPresupuesto();
        CtrVarEconomicas CvarEco = new CtrVarEconomicas();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        CtrCParametros Cparametros = new CtrCParametros();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null){
                
                CargarListas();
                if (!IsPostBack)
                {
                    if (Session["objeto"] != null)
                    {
                        GE_TPARAMETROS mPadre = Session["objeto"] as GE_TPARAMETROS;
                        txtMoneda["txtMoneda"] = mPadre.parm_consecutivo;
                    }


                    if (Session["items"] != null)
                    {
                        GE_TVARECONOMICAS mObjeto = Session["items"] as GE_TVARECONOMICAS;
                        
                        txtId["txtId"] = mObjeto.vari_consecutivo;
                        txtAno["txtAno"] = mObjeto.vari_ano;
                        
                        if (mObjeto.GE_TPARAMETROS1.parm_descripcion != null)
                        {
                            ListEditItem liCentroOperaciones = cmbMeses.Items.FindByText(mObjeto.GE_TPARAMETROS1.parm_descripcion.ToString());
                            liCentroOperaciones.Selected = true;
                        }

                        if (mObjeto.vari_activo != null)
                        {
                            ListEditItem liEstado = cmbActivo.Items.FindByValue(mObjeto.vari_activo.ToString());
                            liEstado.Selected = true;
                        }

                        if (mObjeto.vari_valor != null)
                        {
                            txtValor.Value = mObjeto.vari_valor.ToString();
                        }

                    }
                }
                
            }
                
            else
                Response.Redirect(strUrl);
        }

        #region Metodos
        
        public void CargarListas()
        {
            try
            {
                cmbActivo.Items.Clear();
                cmbActivo.Items.Add("Activo?", null);
                cmbActivo.Items.Add("SI", 1);
                cmbActivo.Items.Add("NO", 0);
                
                cmbMeses.Items.Clear();
                cmbMeses.Items.Add("Seleccionar Mes", null);
                IList<GE_TPARAMETROS> lstMeses = Cparametros.GetListbyClase("MESES");

                foreach (GE_TPARAMETROS item in lstMeses)
                {
                    cmbMeses.Items.Add(item.parm_descripcion, item.parm_consecutivo);
                }

            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se pueden cargar los datos. ", ex);
            }
        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarComboObligatorio("Mes", cmbMeses.Value);
                VentanaValidaciones.validarComboObligatorio("Activo", cmbActivo.Value);
                VentanaValidaciones.validarTxtNumericoObligatorio("Valor", txtValor.Value, 20);
                
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void Limpiar()
        {
            txtId["txtId"] = "";
            cmbActivo.Value = null;
            cmbMeses.Value = null;
            
            txtValor.Value = null;

            Session["items"] = null;

        }
        #endregion

        #region Eventos
        
        protected void RegresarClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmMonedaItems.aspx");
        }
        
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
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
                DateTime dtFecha = DateTime.Now;

                GE_TVARECONOMICAS pr = new GE_TVARECONOMICAS();
                pr.vari_activo = Convert.ToInt32(cmbActivo.Value.ToString());
                pr.vari_mes = Convert.ToInt32(cmbMeses.Value.ToString());
                pr.vari_tipo_moneda = Convert.ToInt32(txtMoneda["txtMoneda"].ToString());
                pr.vari_valor = Convert.ToDecimal(txtValor.Value.ToString());
                pr.vari_usuario = strUsuario[0].ToString();
                pr.vari_fecha = DateTime.Now;
                pr.vari_ano = Cperiodo.GetPeriodoActivo().peri_ano;

                GE_TVARECONOMICAS estaAnoMes = CvarEco.GetByAnoMes(pr.vari_mes, pr.vari_tipo_moneda, pr.vari_ano);

                /*if (estaAnoMes != null && estaAnoMes.vari_activo != 0)
                {
                    VentanaValidaciones.mostrarError("Ya existe un valor registrado para este mes");
                    return;
                }*/
                
                // Modificar Valor
                if (txtId.Contains("txtId") && !String.IsNullOrEmpty(txtId["txtId"].ToString())) 
                {
                    pr.vari_consecutivo = Convert.ToInt32(txtId["txtId"].ToString());
                    CvarEco.Update(pr);                                        
                }
                else // Nuevo Valor
                {
                    if (estaAnoMes != null && estaAnoMes.vari_activo != 0)
                    {
                        VentanaValidaciones.mostrarError("Ya existe un valor registrado para este mes");
                        return;
                    }
                    CvarEco.Add(pr);
                }

                Session["mensaje"] = "OK";
                Response.Redirect("frmMonedaItems.aspx", false);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se puede guardar el registro", ex);
            }
        }
        
        #endregion
    }
}