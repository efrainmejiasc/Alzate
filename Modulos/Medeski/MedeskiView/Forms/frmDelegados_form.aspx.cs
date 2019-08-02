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
    public partial class frmDelegados_form : System.Web.UI.Page
    {
        CtrPersonas Ctrpersonas = new CtrPersonas();
        CtrParametros Cparametros = new CtrParametros();
        CtrDelegados Cdelegados = new CtrDelegados();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "dele_consecutivo"};
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                CargarListas();
            }
            else
                Response.Redirect(strUrl);
        }

        #region Metodos
        private void CargarListas()
        {
            try
            {
                char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);
                
                cbLPersonas.DataSource = null;
                cbLPersonas.DataSource = Ctrpersonas.GetAllActiveJefeOrderBy(strUsuario[0]);
                cbLPersonas.DataBind();

                cmbParametros.Items.Clear();
                cmbParametros.Items.Add("Seleccionar Fase", null);
                IList<GE_TPARAMETROS> lstParametros = Cparametros.GetListbyClase("CLASES_PPTO");
                foreach (GE_TPARAMETROS parm in lstParametros)
                {
                    cmbParametros.Items.Add(parm.parm_descripcion, parm.parm_consecutivo);
                }

                if (cmbParametros.Items.Count == 2)
                {
                    cmbParametros.Items[1].Selected = true;
                    parametrosChanged();
                }

            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarError("Error cargando listas. " + ex.Message);
            }
        }

        private void Limpiar()
        {
            cbLPersonas.UnselectAll();
            CargarListas();
            txtId["txtId"] = "";

            cmbParametros.Value = "";
        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarComboObligatorio("Fase", cmbParametros.Value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void IngresarPersonas()
        {
            try
            {
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);
                GE_TPERSONAS p = Ctrpersonas.GetbyUsuario(strUsuario[0].ToString());

                foreach (ListEditItem item in cbLPersonas.Items)
                {

                    GE_TDELEGADOS delegado = Cdelegados.GetByDelegadoFase(Convert.ToInt32(item.Value.ToString()), Convert.ToInt32(cmbParametros.Value));

                    if(delegado == null)
                    {
                        if (item.Selected)
                        {
                            GE_TPERSONAS pe = Ctrpersonas.GetbyConsecutivo(Convert.ToInt32(item.Value.ToString()));
                            GE_TDELEGADOS del = new GE_TDELEGADOS();

                            del.dele_fase_parm = Convert.ToInt32(cmbParametros.Value);

                            del.dele_jefe = p.pers_consecutivo;
                            del.dele_delegado = Convert.ToInt32(item.Value.ToString());

                            del.dele_usuario_act = strUsuario[0].ToString();
                            del.dele_fecha_act = DateTime.Now;

                            del.dele_usuario_crea = strUsuario[0].ToString();
                            del.dele_fecha = DateTime.Now;

                            del.dele_activo = 1;

                            Cdelegados.Add(del);
                        }
                    }
                    else
                    {
                        delegado.dele_usuario_act = strUsuario[0].ToString().ToUpper();
                        delegado.dele_fecha_act = DateTime.Now;

                        if(item.Selected)
                            delegado.dele_activo = 1;
                        else
                            delegado.dele_activo = 0;

                        Cdelegados.Update(delegado);
                    }
                    
                    
                }
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarMensajePersonalizado("Error", "No se puede realizar la acción." + ex.Message);
            }

        }
        #endregion

        #region Eventos

        protected void RegresarClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmDelegados.aspx");
        }
        
        protected void GuardarClicked(object sender, EventArgs e)
        {
            try
            {
                if (!validar())
                {
                    return;
                }
                if (cbLPersonas.SelectedItems.Count == 0)
                {
                    VentanaValidaciones.mostrarError("Se debe seleccionar al menos un delegado");
                    return;
                }
                else
                {
                    IngresarPersonas();

                    Session["mensaje"] = "OK";
                    Response.Redirect("frmDelegados.aspx", false);
                }
            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarError("Error guardando delegados. " + ex.Message);
            }
        }

        private void parametrosChanged()
        {
            try
            {
                char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);
                GE_TPERSONAS p = Ctrpersonas.GetbyUsuario(strUsuario[0].ToString().ToUpper());

                if (p != null)
                {
                    Ctrpersonas = new CtrPersonas();
                    IList<GE_TDELEGADOS> delegados = Cdelegados.GetByJefeFase(p.pers_consecutivo, Convert.ToInt32(cmbParametros.Value));

                    foreach (ListEditItem Item in cbLPersonas.Items)
                    {
                        if (delegados.Any(x => x.dele_delegado == Convert.ToInt32(Item.Value.ToString())))
                        {
                            Item.Selected = true;
                        }
                        else
                        {
                            Item.Selected = false;
                        }
                    }
                }
                else
                {
                    VentanaValidaciones.mostrarError("No se encuentran delegados a su cargo. ");
                }

            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarError("Error mostrando lista de delegados. " + ex.Message);
            }

        }

        #endregion

        protected void cmbParametros_SelectedIndexChanged(object sender, EventArgs e)
        {
            parametrosChanged();
        }
            
    }
}