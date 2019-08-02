using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using MedeskiView.Controllers;

namespace MedeskiView.Forms
{
    public partial class frmProductos_form : System.Web.UI.Page
    {
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        CtrPersonas Cpersonas = new CtrPersonas();
        CtrParametros Cparametros = new CtrParametros();
        CtrProductos Cproductos = new CtrProductos();
        CtrDrivers Cdrivers = new CtrDrivers();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        Hashtable productoSeleccionado = null;
        string[] camposProducto = new string[] { "prod_consecutivo", "prod_intermedio", "prod_contrato",
            "prod_tipo_licencia", "prod_criterio", "prod_componente", "prod_activo", "prod_codigo", 
            "prod_descripcion", "prod_responsable", "prod_serv_venta", "prod_driver1", "prod_driver2" , "prod_redistribuir",
            "GE_TPARAMETROS2.parm_descripcion" };


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
                        GE_TPRODUCTOS mObjeto = Session["objeto"] as GE_TPRODUCTOS;

                        txtId["txtId"] = mObjeto.prod_consecutivo.ToString();
                        cmbIntermedio.Value = mObjeto.prod_intermedio.ToString();
                        cmbContrato.Value = mObjeto.prod_contrato.ToString();
                        cmbActivo.Value = mObjeto.prod_activo.ToString();
                        cmbComponente.Value = mObjeto.prod_componente.ToString();
                        cmbCriterio.Value = mObjeto.prod_criterio.ToString();
                        cmbResponsable.Value = mObjeto.prod_responsable.ToString();
                        cmbTipoLicencia.Value = mObjeto.prod_tipo_licencia.ToString();
                        cmbServicio.Value = mObjeto.prod_serv_venta.ToString();
                        cmbDriver1.Value = mObjeto.prod_driver1.ToString();
                        cmbDriver2.Value = mObjeto.prod_driver2.ToString();
                        txtCodigo.Text = mObjeto.prod_codigo.ToString();
                        txtDescripcion.Text = mObjeto.prod_descripcion.ToString();
                        cmbReDistribucion.Value = mObjeto.prod_redistribuir.ToString();

                        if (mObjeto.GE_TPARAMETROS2.parm_descripcion != "")
                        {                            
                            ListEditItem itemTipoProd = cmbTipoProd.Items.FindByText(mObjeto.GE_TPARAMETROS2.parm_descripcion.ToString());
                            itemTipoProd.Selected = true;
                        }

                    }
                }
                
            }
            else
            {
                Response.Redirect(strUrl);
            }
        }

        #region Metodos

        public void CargarListas()
        {
            try
            {
                /*Cargar Personas*/
                cmbResponsable.Items.Clear();
                cmbResponsable.Items.Add("Seleccionar Responsable", null);
                IList<GE_TPERSONAS> persona = Cpersonas.GetAllJefe();
                foreach (GE_TPERSONAS p in persona)
                {
                    cmbResponsable.Items.Add(p.pers_nombres, p.pers_consecutivo);
                }

                if (cmbResponsable.Items.Count == 2)
                {
                    cmbResponsable.Items[1].Selected = true;
                }

                /*Cargar Parametros*/
                cmbComponente.Items.Clear();
                cmbComponente.Items.Add("Seleccionar Componente", null);
                IList<GE_TPARAMETROS> param = Cparametros.GetListbyClase("COMPONENTE");
                foreach (GE_TPARAMETROS p in param)
                {
                    cmbComponente.Items.Add(p.parm_descripcion, p.parm_consecutivo);
                }

                cmbCriterio.Items.Clear();
                cmbCriterio.Items.Add("Seleccionar Criterio", null);
                IList<GE_TPARAMETROS> par = Cparametros.GetListbyClase("CRITERIO_PROD");
                foreach (GE_TPARAMETROS pr in par)
                {
                    cmbCriterio.Items.Add(pr.parm_descripcion, pr.parm_consecutivo);
                }

                if (cmbCriterio.Items.Count == 2)
                {
                    cmbCriterio.Items[1].Selected = true;
                }


                cmbTipoLicencia.Items.Clear();
                cmbTipoLicencia.Items.Add("Seleccionar Tipo Licencia", null);
                IList<GE_TPARAMETROS> para = Cparametros.GetListbyClase("TIPO_LICENCIA");
                foreach (GE_TPARAMETROS pr in para)
                {
                    cmbTipoLicencia.Items.Add(pr.parm_descripcion, pr.parm_consecutivo);
                }

                if (cmbTipoLicencia.Items.Count == 2)
                {
                    cmbTipoLicencia.Items[1].Selected = true;
                }


                cmbServicio.Items.Clear();
                cmbServicio.Items.Add("Seleccionar Servicio de venta", null);
                IList<GE_TPARAMETROS> pars = Cparametros.GetListbyClase("SERV_VENTA");
                foreach (GE_TPARAMETROS pr in pars)
                {
                    cmbServicio.Items.Add(pr.parm_descripcion, pr.parm_consecutivo);
                }

                if (cmbServicio.Items.Count == 2)
                {
                    cmbServicio.Items[1].Selected = true;
                }


                cmbDriver1.Items.Clear();
                cmbDriver1.Items.Add("Seleccionar Driver de distribución principal", null);
                cmbDriver2.Items.Clear();
                cmbDriver2.Items.Add("Seleccionar Driver de distribución secundario", null);

                IList<GE_TDRIVERS> pard = Cdrivers.GetAllActive();
                foreach (GE_TDRIVERS pr in pard)
                {
                    cmbDriver1.Items.Add(pr.driv_nombre, pr.driv_consecutivo);
                    cmbDriver2.Items.Add(pr.driv_nombre, pr.driv_consecutivo);
                }

                if (cmbDriver1.Items.Count == 2)
                {
                    cmbDriver1.Items[1].Selected = true;
                }

                if (cmbDriver2.Items.Count == 2)
                {
                    cmbDriver2.Items[1].Selected = true;
                }


                cmbTipoProd.Items.Clear();
                cmbTipoProd.Items.Add("Seleccionar Tipo de Producto", null);
                IList<GE_TPARAMETROS> parTipoProd = Cparametros.GetListbyClase("TIPO_PRODUCTO");
                foreach (GE_TPARAMETROS paramTP in parTipoProd)
                {
                    cmbTipoProd.Items.Add(paramTP.parm_descripcion, paramTP.parm_consecutivo);
                }

                if (cmbTipoProd.Items.Count == 2)
                {
                    cmbTipoProd.Items[1].Selected = true;
                }

                //Contrato - Intermedio
                cmbContrato.Items.Clear();
                cmbContrato.Items.Add("Tiene Contrato?", null);
                cmbContrato.Items.Add("SI", 1);
                cmbContrato.Items.Add("NO", 0);

                cmbIntermedio.Items.Clear();
                cmbIntermedio.Items.Add("Es intermedio?", null);
                cmbIntermedio.Items.Add("SI", 1);
                cmbIntermedio.Items.Add("NO", 0);

                cmbActivo.Items.Clear();
                cmbActivo.Items.Add("Activo?", null);
                cmbActivo.Items.Add("SI", 1);
                cmbActivo.Items.Add("NO", 0);

                cmbReDistribucion.Items.Clear();
                cmbReDistribucion.Items.Add("Se Redistribuye?", null);
                cmbReDistribucion.Items.Add("SI", 1);
                cmbReDistribucion.Items.Add("NO", 0);
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se pueden cargar las listas. " + ex.Message);
            }
        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarTxtObligatorio("Codigo", txtCodigo.Text, 100);
                VentanaValidaciones.validarTxtObligatorio("Descripción", txtDescripcion.Text, 200);
                VentanaValidaciones.validarTxtNumericoObligatorio("Responsable", cmbResponsable.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Componente", cmbComponente.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Criterio", cmbCriterio.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Tipo Licencia", cmbTipoLicencia.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Contrato", cmbContrato.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Intermedio", cmbIntermedio.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Activo", cmbActivo.Value, 6);

                VentanaValidaciones.validarTxtNumericoObligatorio("Servicio", cmbServicio.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Driver 1", cmbDriver1.Value, 6);
                // VentanaValidaciones.validarTxtNumericoObligatorio("Driver 2", cmbDriver2.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Se Redistribuye", cmbReDistribucion.Value, 6);

                VentanaValidaciones.validarComboObligatorio("Tipo de Producto", cmbTipoProd.Value);

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
            cmbComponente.Value = null;
            cmbContrato.Value = null;
            cmbCriterio.Value = null;
            cmbIntermedio.Value = null;
            cmbResponsable.Value = null;
            cmbTipoLicencia.Value = null;
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
            Session["opModificar"] = "0";
            cmbActivo.Value = null;
            cmbServicio.Value = null;
            cmbDriver1.Value = null;
            cmbDriver2.Value = null;
            cmbReDistribucion.Value = null;
            cmbTipoProd.Value = null;
        }

        public bool ValidarDatos ()
        {
            bool blEstado = true;
            
            try
            {
                if ((cmbComponente.Text.ToString().ToUpper() == "INFRAESTRUCTURA") && (cmbIntermedio.Value.ToString() == "1"))
                {
                    VentanaValidaciones.mostrarMensajePersonalizado("Error", "El componente infraestructura no puede ser intermedio");
                    blEstado = false;
                }

                if ((cmbComponente.Text.ToString().ToUpper() == "DIRECTO") && (cmbIntermedio.Value.ToString() == "0"))
                {
                    if (cmbServicio.Value == null)
                    {
                        VentanaValidaciones.mostrarMensajePersonalizado("Error", "El componente directo debe seleccionar un servicio de venta");
                        blEstado = false;
                    }
                    else
                    {
                        if ((cmbDriver1.Value == null) && (cmbDriver2.Value == null))
                        {
                            VentanaValidaciones.mostrarMensajePersonalizado("Error", "El componente directo debe seleccionar driver de distribución");
                            blEstado = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede validar el registro" + ex.Message);
            }

            return blEstado;
        }
        
        
        #endregion

        #region Eventos
        protected void RegresarClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmProductos.aspx");
        }

        
        protected void NuevoClicked(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void ConsultarClicked()
        {
            Session["opModificar"] = "1";
        }

        protected void GuardarClicked(object sender, EventArgs e)
        {
            try
            {
                if (!validar() || !ValidarDatos())
                {
                    return;
                }

                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);
                DateTime dtFecha = DateTime.Now;

                GE_TPRODUCTOS pr = new GE_TPRODUCTOS();
                pr.prod_codigo = txtCodigo.Text;
                pr.prod_activo = Convert.ToInt32(cmbActivo.Value.ToString());
                pr.prod_componente = Convert.ToInt32(cmbComponente.Value.ToString());
                pr.prod_contrato = Convert.ToInt32(cmbContrato.Value.ToString());
                pr.prod_criterio = Convert.ToInt32(cmbCriterio.Value.ToString());
                pr.prod_descripcion = txtDescripcion.Text;
                pr.prod_intermedio = Convert.ToInt32(cmbIntermedio.Value.ToString());
                pr.prod_responsable = Convert.ToInt32(cmbResponsable.Value.ToString());
                pr.prod_tipo_licencia = Convert.ToInt32(cmbTipoLicencia.Value.ToString());
                pr.prod_serv_venta = (cmbServicio.Value == null) ? (int?)null : Convert.ToInt32(cmbServicio.Value.ToString());
                pr.prod_driver1 = (cmbDriver1.Value == null) ? (int?)null : Convert.ToInt32(cmbDriver1.Value.ToString());
                pr.prod_driver2 = (cmbDriver2.Value == null) ? (int?)null : Convert.ToInt32(cmbDriver2.Value.ToString());
                pr.prod_usuario_act = strUsuario[0].ToString();
                pr.prod_fecha_act = dtFecha;
                pr.prod_redistribuir = Convert.ToInt32(cmbReDistribucion.Value.ToString());
                pr.prod_distrib_serv = chkDistribucionDTC.Checked == true ? 1 : 0;
                pr.prod_interm_no_distrib = chkIntermedioNoDistribuible.Checked == true ? 1 : 0;
                
                pr.prod_usuario = strUsuario[0].ToString();
                pr.prod_fecha = dtFecha;
                
                    
                pr.prod_tipo_producto = Convert.ToInt32(cmbTipoProd.Value.ToString());

                if ( txtId.Contains("txtId") && !String.IsNullOrEmpty(txtId["txtId"].ToString()))
                {
                    pr.prod_consecutivo = Convert.ToInt32(txtId["txtId"].ToString());
                    Cproductos.Update(pr);
                    
                }
                else
                {
                    Cproductos.Add(pr);
                }

                Session["mensaje"] = "OK";
                Session["producto"] = pr;

                Response.Redirect("frmProductos.aspx", false);

            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede guardar el registro" + ex.Message);
            }
        }

        protected void Eliminar(Object sender, EventArgs e)
        {
            
        }

        protected void EliminarClicked()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede guardar el registro" + ex.Message);
            }
            
        }

        protected void cmbComponente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbComponente.Value != null)
                {
                    if (cmbComponente.Text.ToUpper() == "INFRAESTRUCTURA")
                    {
                        cmbIntermedio.Value = "0";
                        cmbIntermedio.Enabled = false;
                        chkIntermedioNoDistribuible.Enabled = false;
                    }
                    else
                    {
                        cmbIntermedio.Enabled = true;
                        cmbIntermedio.Value = null;
                    }

                    if (cmbComponente.Text.ToUpper() == "DIRECTO")
                    {
                        chkDistribucionDTC.Enabled = true;
                        cmbIntermedio.Value = null;
                    }

                }
                else
                {
                    cmbIntermedio.Enabled = true;
                    cmbIntermedio.Value = null;
                }

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede consultar el registro. " + ex.Message);
            }

        }

        #endregion

        protected void cmbIntermedio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIntermedio.Text.ToUpper() == "SI")
            {
                chkIntermedioNoDistribuible.Enabled = true;
                chkDistribucionDTC.Enabled = true;
            }
            else
            {
                chkIntermedioNoDistribuible.Enabled = false;
                chkDistribucionDTC.Enabled = true;
            }
        }
       
    }
}