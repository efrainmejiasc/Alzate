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
    public partial class frmItems_form : System.Web.UI.Page
    {
        CtrProductosItems CproductosItems = new CtrProductosItems();
        CtrProductos Cproductos = new CtrProductos();
        CtrCuentas Ccuentas = new CtrCuentas();
        CtrUtilidades Cutilidades = new CtrUtilidades();
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
                        GE_TPRODUCTOSITEMS mObjeto = Session["objeto"] as GE_TPRODUCTOSITEMS;

                        if (mObjeto.prit_tipo != null)
                        {
                            ListEditItem liEstado = cmbTipo.Items.FindByValue(mObjeto.prit_tipo.ToString());
                            liEstado.Selected = true;
                        }

                        txtId["txtId"] = mObjeto.prit_consecutivo.ToString();
                        cmbProducto.Value = mObjeto.GE_TPRODUCTOS.prod_consecutivo.ToString();
                        cmbActivo.Value = mObjeto.prit_activo.ToString();
                        cmbCuentaAux.Value = mObjeto.GE_TCUENTAS.cuen_consecutivo.ToString();
                        txtItem.Text = mObjeto.prit_item.ToString();
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
                /*Cargar Producto*/
                cmbProducto.Items.Clear();
                cmbProducto.Items.Add("Seleccionar Producto", null);
                GE_TPRODUCTOS producto = Session["producto"] as GE_TPRODUCTOS;
                IList<GE_TPRODUCTOS> pr = Cproductos.GetAll().Where(x => x.prod_consecutivo == producto.prod_consecutivo ).ToList();
                foreach (GE_TPRODUCTOS p in pr)
                {
                    cmbProducto.Items.Add(p.prod_descripcion, p.prod_consecutivo);
                }

                if (cmbProducto.Items.Count == 2)
                {
                    cmbProducto.Items[1].Selected = true;
                }

                /*Cargar Cuentas*/
                cmbCuentaAux.Items.Clear();
                cmbCuentaAux.Items.Add("Seleccionar Cuenta", null);
                IList<GE_TCUENTAS> cuenta = Ccuentas.GetAllActive();
                foreach (GE_TCUENTAS c in cuenta)
                {
                    cmbCuentaAux.Items.Add(c.cuen_auxiliar + "-" + c.cuen_descripcion, c.cuen_consecutivo);
                }

                if (cmbCuentaAux.Items.Count == 2)
                {
                    cmbCuentaAux.Items[1].Selected = true;
                }
         
                cmbActivo.Items.Clear();
                cmbActivo.Items.Add("Activo?", null);
                cmbActivo.Items.Add("SI", 1);
                cmbActivo.Items.Add("NO", 0);

                cmbTipo.Items.Clear();
                cmbTipo.Items.Add("Seleccionar Tipo de Item", null);
                cmbTipo.Items.Add("Cuentas Especiales", "CE");
                cmbTipo.Items.Add("Gastos de Área", "GA");
                cmbTipo.Items.Add("Gastos de Viaje", "VI");
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se pueden cargar los combos. ", ex);
            }
        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarTxtNumericoObligatorio("Producto", cmbProducto.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Cuenta", cmbCuentaAux.Value, 6);
                VentanaValidaciones.validarTxtNumericoObligatorio("Activo", cmbActivo.Value, 6);
                VentanaValidaciones.validarTxtObligatorio("Item", txtItem.Text, 200);

                VentanaValidaciones.validarComboObligatorio("Tipo de Item", cmbTipo.Value);                
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
            cmbCuentaAux.Value = null;
            cmbProducto.Value = null;
            cmbTipo.Value = null;
            Session["opModificar"] = "0";
            txtItem.Text = "";
        }

        #endregion

        #region Eventos

        protected void RegresarClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmItems.aspx");
        }

        protected void NuevoClicked(object sender, EventArgs e)
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

                GE_TPRODUCTOSITEMS pr = new GE_TPRODUCTOSITEMS();
                pr.prit_cuenta = Convert.ToInt32(cmbCuentaAux.Value.ToString());
                pr.prit_activo = Convert.ToInt32(cmbActivo.Value.ToString());
                pr.prit_item = txtItem.Text;
                pr.prit_producto = Convert.ToInt32(cmbProducto.Value.ToString());
                pr.prit_usuario = strUsuario[0].ToString();
                pr.prit_usuario_act = strUsuario[0].ToString();
                pr.prit_tipo = cmbTipo.Value.ToString();
                pr.prit_fecha = dtFecha;
                pr.prit_fecha_act = dtFecha;

                if (txtId.Contains("txtId") && !String.IsNullOrEmpty(txtId["txtId"].ToString()))
                {
                    pr.prit_consecutivo = Convert.ToInt32(txtId["txtId"].ToString());
                    CproductosItems.Update(pr);

                }
                else
                {
                    CproductosItems.Add(pr);
                }

                Session["mensaje"] = "OK";
                Response.Redirect("frmItems.aspx", false);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizadoError("Error", "No se puede guardar el registro", ex);
            }
        }

        #endregion
    }
}