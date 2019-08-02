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
    public partial class frmAnoPresupuesto_form : System.Web.UI.Page
    {
        CtrPeriodoPresupuesto Cperiodo = new CtrPeriodoPresupuesto();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        CtrUtilidades Cutilidades = new CtrUtilidades();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null){
                
                CargarListas();
                if (!IsPostBack)
                {
                    if (Session["objeto"] != null)
                    {
                        GE_TPERIODOPRESUPUESTO mObjeto = Session["objeto"] as GE_TPERIODOPRESUPUESTO;
                        
                        txtId["txtId"] = mObjeto.peri_consecutivo;
                        txtPaso["txtPaso"] = mObjeto.peri_paso;
//***************************************************************************************************************************************************************
                        try
                        {
                            if (mObjeto.peri_ano != null)
                            {
                                ListEditItem liCentroOperaciones = cmbAno.Items.FindByText(mObjeto.peri_ano.ToString());
                                liCentroOperaciones.Selected = true;
                            }
                        }catch(Exception ex)
                        {
                            VentanaValidaciones.mostrarMensajePersonalizadoError("Error","Error", ex);
                        }
//****************************************************************************************************************************************************************
                        if (mObjeto.peri_activo != null)
                        {
                            ListEditItem liEstado = cmbActivo.Items.FindByValue(mObjeto.peri_activo.ToString());
                            liEstado.Selected = true;
                        }

                        chkDuplicar.Enabled = false;
                        chkDuplicar.Checked = false;

                        cmbCopiarAno.Enabled = false;
                    }
                }
                if (chkDuplicar.Checked)
                {
                    cmbCopiarAno.Enabled = true;
                }
                else
                {
                    cmbCopiarAno.Enabled = false;
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
                
                int inAno = DateTime.Today.Year;
                cmbAno.Items.Clear();
                cmbAno.Items.Add("Seleccionar Año Presupuesto", null);
                cmbAno.Items.Add(inAno.ToString(), inAno);
                cmbAno.Items.Add((inAno + 1).ToString(), inAno + 1);

                cmbCopiarAno.Items.Clear();
                cmbCopiarAno.Items.Add("Seleccionar Año y Versión", null);
                IList<GE_TPERIODOPRESUPUESTO> lstPeriodos = Cperiodo.GetAll();

                foreach (GE_TPERIODOPRESUPUESTO item in lstPeriodos)
                {
                    cmbCopiarAno.Items.Add(item.peri_ano + "-" + item.peri_paso, item.peri_consecutivo);
                }

                if (!txtId.Contains("txtId")) txtId["txtId"] = "";
                
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
                VentanaValidaciones.validarTxtNumericoObligatorio("Ano", cmbAno.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Activo", cmbActivo.Value, 6);
                
                if (chkDuplicar.Checked)
                {
                    VentanaValidaciones.validarTxtNumericoObligatorio("Ano a copiar", cmbCopiarAno.Value, 6);
                    VentanaValidaciones.validarTxtNumericoObligatorio("Paso a copiar", cmbCopiarAno.Value, 6);

                    if (!String.IsNullOrEmpty(txtId["txtId"].ToString()) && cmbCopiarAno.Value.ToString() == txtId["txtId"].ToString())
                    {
                        VentanaValidaciones.mostrarError("Debe seleccionar un Año y Paso Diferente");
                        return false;
                    }
                }
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
            cmbAno.Value = null;

            chkDuplicar.Checked = false;
            chkDuplicar.Enabled = true;
            cmbCopiarAno.Value = null;
            cmbCopiarAno.Enabled = true;
            Session["objeto"] = null;

        }
        #endregion

        #region Eventos
        
        protected void RegresarClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmAnoPresupuesto.aspx");
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

                GE_TPERIODOPRESUPUESTO pr = new GE_TPERIODOPRESUPUESTO();
                pr.peri_activo = Convert.ToInt32(cmbActivo.Value.ToString());
                pr.peri_ano = Convert.ToInt32(cmbAno.Value.ToString());
                pr.peri_fecha = DateTime.Now;
                pr.peri_usuario = strUsuario[0].ToString();

                IList<GE_TPERIODOPRESUPUESTO> listaPeriodos = Cperiodo.GetAll();

                // Modificar Año
                if (!String.IsNullOrEmpty(txtId["txtId"].ToString())) 
                {
                    GE_TPERIODOPRESUPUESTO anyAtive = listaPeriodos.Where(x => x.peri_activo == 1 && x.peri_consecutivo != Convert.ToInt32(txtId["txtId"].ToString())).FirstOrDefault();
                
                    if (anyAtive != null && pr.peri_activo == 0)
                    {
                        pr.peri_consecutivo = Convert.ToInt32(txtId["txtId"].ToString());
                        pr.peri_paso = Convert.ToInt32(txtPaso["txtPaso"].ToString());

                        Cperiodo.Update(pr);
                        
                        Session["cargue"] = 0;
                        
                        VentanaValidaciones.mostrarRegistroExitoso();                        
                        
                    }
                    else if (anyAtive == null && pr.peri_activo == 0)
                    {
                        VentanaValidaciones.mostrarError("No existen periodos activos");
                    }
                    else if (anyAtive != null && pr.peri_activo == 1)
                    {
                        anyAtive.peri_activo = 0;
                        Cperiodo.Update(anyAtive);

                        pr.peri_consecutivo = Convert.ToInt32(txtId["txtId"].ToString());
                        pr.peri_paso = Convert.ToInt32(txtPaso["txtPaso"].ToString());

                        Cperiodo.Update(pr);

                        Session["cargue"] = 0;
                        
                        VentanaValidaciones.mostrarRegistroExitoso();
                    }
                    else
                    {
                        pr.peri_consecutivo = Convert.ToInt32(txtId["txtId"].ToString());
                        pr.peri_paso = Convert.ToInt32(txtPaso["txtPaso"].ToString());
                        
                        Cperiodo.Update(pr);
                        
                        Session["cargue"] = 0;
                        
                        VentanaValidaciones.mostrarRegistroExitoso();
                                                
                    }                    
                }
                else // Nuevo Año
                {
                    GE_TPERIODOPRESUPUESTO anyAtive = listaPeriodos.Where(x => x.peri_activo == 1).FirstOrDefault();
                
                    if (anyAtive != null && pr.peri_activo == 0)
                    {
                        int paso = listaPeriodos.Where(x => x.peri_ano == pr.peri_ano).Count() + 1;
                        pr.peri_paso = paso; 
                        
                        Cperiodo.Add(pr);
                        
                        Session["cargue"] = 0;
                        
                        VentanaValidaciones.mostrarRegistroExitoso();
                    }
                    else if (anyAtive == null && pr.peri_activo == 0)
                    {
                        VentanaValidaciones.mostrarError("No existen periodos activos");
                    }
                    else if (anyAtive != null && pr.peri_activo == 1)
                    {

                        anyAtive.peri_activo = 0;
                        Cperiodo.Update(anyAtive);

                        int paso = listaPeriodos.Where(x => x.peri_ano == pr.peri_ano).Count() + 1;
                        pr.peri_paso = paso;

                        Cperiodo.Add(pr);
                        Session["cargue"] = 0;                        
                        VentanaValidaciones.mostrarRegistroExitoso();
                    }
                    else
                    {
                        int paso = listaPeriodos.Where(x => x.peri_ano == pr.peri_ano).Count() + 1;
                        pr.peri_paso = paso;

                        Cperiodo.Add(pr);
                        Session["cargue"] = 0;
                        VentanaValidaciones.mostrarRegistroExitoso();                        
                    }

                    if (chkDuplicar.Checked)
                    {
                        // Periodo a Copiar                         
                        int consec_copiar = Convert.ToInt32(cmbCopiarAno.Value.ToString());

                        Cperiodo.LoadTransactions(consec_copiar, pr.peri_consecutivo); // Stored Procedure
                        // Llamar el SP pasando como parametro el periodo a copiar "consec_copiar"
                        // y el periodo creado "pr.peri_consecutivo"
                        // exec dbo.sp_DuplicarValores @buscar = peri_copiar.id, @nuevo = pr.peri_consecutivo

                    }
                }

                Session["mensaje"] = "OK";
                Response.Redirect("frmAnoPresupuesto.aspx", false);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se puede guardar el registro", ex);
            }
        }
        

        protected void chkDuplicar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDuplicar.Checked)
            {
                cmbCopiarAno.Enabled = true;

                if (cmbCopiarAno.Items.Count == 2)
                {
                    cmbCopiarAno.Items[1].Selected = true;
                }

            }
            else
            {
                cmbCopiarAno.Enabled = false;
            }
        }
        #endregion
    }
}