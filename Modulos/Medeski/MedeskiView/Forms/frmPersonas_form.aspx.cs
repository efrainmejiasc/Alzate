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
    public partial class frmPersonas_form : System.Web.UI.Page
    {
        IList<GE_TPARAMETROS> lstParams;
        IList<GE_TCENTROSCOSTOS> lstCCostos;
        IList<GE_TPERSONAS> lstPersonas;

        CtrParametros CParametros = new CtrParametros();
        CtrCentroCosto CCostos = new CtrCentroCosto();
        CtrPersonas CPersonas = new CtrPersonas();
        CtrCompanias CCompania = new CtrCompanias();
        CtrGente CGente = new CtrGente();
        CtrPeriodoPresupuesto CPeriodo = new CtrPeriodoPresupuesto();
        CtrUtilidades CUtilidades = new CtrUtilidades();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();

        Hashtable camposSeleccionado = null;
        string[] camposClaseparametro = new string[] { "pers_consecutivo", "pers_tipodoc", "pers_identificacion", 
                                                        "pers_nombre","pers_apellido","pers_nombres", "GE_TPERSONAS2.pers_nombres", 
                                                        "GE_TPARAMETROS.parm_descripcion", "GE_TPARAMETROS1.parm_descripcion",
                                                        "GE_TPARAMETROS2.parm_descripcion", "GE_TPARAMETROS3.parm_descripcion",
                                                        "GE_TPARAMETROS4.parm_descripcion", "GE_TCENTROSCOSTOS2.cost_codigo", 
                                                        "pers_usudom", "pers_nombre_area", "pers_nombre_busq", "pers_activo"
                                                        };

        #region Metodos
        

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
                        GE_TPERSONAS mObjeto = Session["objeto"] as GE_TPERSONAS;

                        txtConsecutivo["txtConsecutivo"] = mObjeto.pers_consecutivo.ToString();
                        txtTipoDoc.Value = mObjeto.pers_tipodoc.ToString();
                        txtDocumento.Value = mObjeto.pers_identificacion.ToString();
                        txtNombres.Value = mObjeto.pers_nombre.ToString();
                        txtApellidos.Value = mObjeto.pers_apellido.ToString();

                        txtUsuDom.Value = mObjeto.pers_usudom.ToString();

                        if (mObjeto.GE_TPARAMETROS5.parm_descripcion != "")
                        {
                            ListEditItem li = cmbNombreArea.Items.FindByText(mObjeto.GE_TPARAMETROS5.parm_descripcion.ToString());
                            li.Selected = true;
                        }

                        txtNombreBusqueda.Value = mObjeto.pers_nombre_busq.ToString();

                        if (mObjeto.GE_TPARAMETROS2.parm_descripcion != "")
                        {
                            ListEditItem li = cmbCargo.Items.FindByText(mObjeto.GE_TPARAMETROS2.parm_descripcion.ToString());
                            li.Selected = true;
                        }

                        if (mObjeto.GE_TCENTROSCOSTOS2.cost_codigo != "")
                        {
                            ListEditItem li = cmbCentroCostos.Items.FindByText(mObjeto.GE_TCENTROSCOSTOS2.cost_codigo.ToString());
                            li.Selected = true;
                        }

                        if (mObjeto.GE_TPARAMETROS4.parm_descripcion != "")
                        {
                            ListEditItem li = cmbEmpresa.Items.FindByText(mObjeto.GE_TPARAMETROS4.parm_descripcion.ToString());
                            li.Selected = true;
                        }

                        if (mObjeto.GE_TPARAMETROS3.parm_descripcion != "")
                        {
                            ListEditItem li = cmbGrupo.Items.FindByText(mObjeto.GE_TPARAMETROS3.parm_descripcion.ToString());
                            li.Selected = true;
                        }

                        if (mObjeto.GE_TPARAMETROS.parm_descripcion != "")
                        {
                            ListEditItem li = cmbTipoContrato.Items.FindByText(mObjeto.GE_TPARAMETROS.parm_descripcion.ToString());
                            li.Selected = true;
                        }

                        if (mObjeto.GE_TPARAMETROS1.parm_descripcion != "")
                        {
                            ListEditItem li = cmbTipoDistribucion.Items.FindByText(mObjeto.GE_TPARAMETROS1.parm_descripcion.ToString());
                            li.Selected = true;
                        }

                        if (mObjeto.GE_TPERSONAS2.pers_nombres != "")
                        {
                            ListEditItem li = cmbJefe.Items.FindByText(mObjeto.GE_TPERSONAS2.pers_nombres.ToString());
                            li.Selected = true;
                        }

                        ListEditItem liEstado = cmbEstado.Items.FindByValue(mObjeto.pers_activo.ToString());
                        liEstado.Selected = true;
                    }
                }
            }
            else
                Response.Redirect(strUrl);
        }

        protected void CargarListas()
        {
            try
            {
                lstParams = CParametros.GetListbyClase("TIPO_CONTRATO");
                cmbTipoContrato.Items.Clear();
                cmbTipoContrato.Items.Add("Seleccionar Tipo de Contrato", null);
                foreach (GE_TPARAMETROS param in lstParams)
                {
                    cmbTipoContrato.Items.Add(param.parm_descripcion, param.parm_consecutivo);
                }
                
                if (cmbTipoContrato.Items.Count == 2)
                {
                    cmbTipoContrato.Items[1].Selected = true;
                }


                lstParams = CParametros.GetListbyClase("METODO_DISTRIB");
                cmbTipoDistribucion.Items.Clear();
                cmbTipoDistribucion.Items.Add("Seleccionar Tipo de Distribucion", null);
                foreach (GE_TPARAMETROS param in lstParams)
                {
                    cmbTipoDistribucion.Items.Add(param.parm_descripcion, param.parm_consecutivo);
                }

                if (cmbTipoDistribucion.Items.Count == 2)
                {
                    cmbTipoDistribucion.Items[1].Selected = true;
                } 
                

                lstParams = CParametros.GetListbyClase("CARGO");
                cmbCargo.Items.Clear();
                cmbCargo.Items.Add("Seleccionar Cargo", null);
                foreach (GE_TPARAMETROS param in lstParams)
                {
                    cmbCargo.Items.Add(param.parm_descripcion, param.parm_consecutivo);
                }

                if (cmbCargo.Items.Count == 2)
                {
                    cmbCargo.Items[1].Selected = true;
                } 



                lstParams = CParametros.GetListbyClase("GRUPO");
                cmbGrupo.Items.Clear();
                cmbGrupo.Items.Add("Seleccionar Grupo", null);
                foreach (GE_TPARAMETROS param in lstParams)
                {
                    cmbGrupo.Items.Add(param.parm_descripcion, param.parm_consecutivo);
                }

                if (cmbGrupo.Items.Count == 2)
                {
                    cmbGrupo.Items[1].Selected = true;
                } 


                lstParams = CParametros.GetListbyClase("EMPRESA");
                cmbEmpresa.Items.Clear();
                cmbEmpresa.Items.Add("Seleccionar Empresa", null);
                foreach (GE_TPARAMETROS param in lstParams)
                {
                    cmbEmpresa.Items.Add(param.parm_descripcion, param.parm_consecutivo);
                }

                if (cmbEmpresa.Items.Count == 2)
                {
                    cmbEmpresa.Items[1].Selected = true;
                } 


                lstParams = CParametros.GetListbyClase("AREAS_PERSONAS");
                cmbNombreArea.Items.Clear();
                cmbNombreArea.Items.Add("Seleccionar Area", null);
                foreach (GE_TPARAMETROS param in lstParams)
                {
                    cmbNombreArea.Items.Add(param.parm_descripcion, param.parm_consecutivo);
                }

                if (cmbNombreArea.Items.Count == 2)
                {
                    cmbNombreArea.Items[1].Selected = true;
                } 


                lstCCostos = CCostos.GetAllActive();
                cmbCentroCostos.Items.Clear();
                cmbCentroCostos.Items.Add("Seleccionar Centro de Costos", null);
                foreach (GE_TCENTROSCOSTOS ccosto in lstCCostos)
                {
                    cmbCentroCostos.Items.Add(ccosto.cost_codigo, ccosto.cost_consecutivo);
                }

                if (cmbCentroCostos.Items.Count == 2)
                {
                    cmbCentroCostos.Items[1].Selected = true;
                }


                lstPersonas = CPersonas.GetAllActive();
                cmbJefe.Items.Clear();
                cmbJefe.Items.Add("Seleccionar Jefe", null);
                foreach (GE_TPERSONAS jefe in lstPersonas)
                {
                    cmbJefe.Items.Add(jefe.pers_nombres, jefe.pers_consecutivo);
                }

                if (cmbJefe.Items.Count == 2)
                {
                    cmbJefe.Items[1].Selected = true;
                }

            }
            catch(Exception ex)
            {
                VentanaValidaciones.mostrarError("No se pudieron cargar las listas. " + ex.Message);
            }            
        }

        protected void limpiar()
        {
            txtConsecutivo["txtConsecutivo"] = string.Empty;
            txtTipoDoc.Value = ""; 
            txtDocumento.Value = "";
            txtNombres.Value = "";
            txtApellidos.Value = "";
            txtUsuDom.Value = "";
            cmbNombreArea.Value = "";
            txtNombreBusqueda.Value = "";

            cmbCargo.Value = "";
            cmbCentroCostos.Value = "";
            cmbEmpresa.Value = "";
            cmbEstado.Value = "";
            cmbGrupo.Value = "";
            cmbTipoContrato.Value = "";
            cmbTipoDistribucion.Value = "";
            cmbJefe.Value = "";
            
        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarTxtObligatorio("Tipo de Documento", txtTipoDoc.Text, 10);
                VentanaValidaciones.validarTxtNumericoObligatorio("Documento", txtDocumento.Text, 50);

                VentanaValidaciones.validarTxtObligatorio("Nombres", txtNombres.Text, 100);
                VentanaValidaciones.validarTxtObligatorio("Apellidos", txtApellidos.Text, 100);
                VentanaValidaciones.validarTxtObligatorio("Usuario de Dominio", txtUsuDom.Text, 30);                
                VentanaValidaciones.validarTxtObligatorio("Nombre de Busqueda", txtNombreBusqueda.Text, 200);

                VentanaValidaciones.validarComboObligatorio("Nombre de Area", cmbNombreArea.Value);
                VentanaValidaciones.validarComboObligatorio("Cargo", cmbCargo.Value);
                VentanaValidaciones.validarComboObligatorio("Centro de Costos", cmbCentroCostos.Value);
                VentanaValidaciones.validarComboObligatorio("Empresa", cmbEmpresa.Value);
                VentanaValidaciones.validarComboObligatorio("Grupo", cmbGrupo.Value);
                VentanaValidaciones.validarComboObligatorio("Tipo de Contrato", cmbTipoContrato.Value);
                VentanaValidaciones.validarComboObligatorio("Tipo de Distribucion", cmbTipoDistribucion.Value);
                VentanaValidaciones.validarComboObligatorio("Jefe", cmbJefe.Value);
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
            Response.Redirect("frmPersonas.aspx");
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
                
                Char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                GE_TPERSONAS persona = new GE_TPERSONAS();
                persona.pers_tipodoc = txtTipoDoc.Text.ToString();
                persona.pers_identificacion = txtDocumento.Text.ToString();

                persona.pers_nombre = txtNombres.Text.ToString();
                persona.pers_apellido = txtApellidos.Text.ToString();
                persona.pers_nombres = txtApellidos.Text.ToString() + " " + txtNombres.Text.ToString();
                
                persona.pers_consec_jefe = Convert.ToInt32(cmbJefe.Value);
                persona.pers_tipo_contrato = Convert.ToInt32(cmbTipoContrato.Value);
                persona.pers_metodo_distrib = Convert.ToInt32(cmbTipoDistribucion.Value);
                persona.pers_cargo = Convert.ToInt32(cmbCargo.Value);
                persona.pers_grupo = Convert.ToInt32(cmbGrupo.Value); 
                persona.pers_activo = Convert.ToInt32(cmbEstado.Value);
                persona.pers_empresa = Convert.ToInt32(cmbEmpresa.Value); 
                persona.pers_ccosto = Convert.ToInt32(cmbCentroCostos.Value);
                persona.pers_usudom = txtUsuDom.Text.ToString();
                persona.pers_nombre_area = Convert.ToInt32(cmbNombreArea.Value);
                persona.pers_nombre_busq = txtNombreBusqueda.Text.ToString();

                persona.pers_usuario_act = strUsuario[0].ToString();
                persona.pers_fecha_act = DateTime.Now;

                persona.pers_usuario = strUsuario[0].ToString();
                persona.pers_fecha = DateTime.Now;


                if (txtConsecutivo.Contains("txtConsecutivo") && !string.IsNullOrEmpty(txtConsecutivo["txtConsecutivo"].ToString()))
                {
                    persona.pers_consecutivo = Convert.ToInt32(txtConsecutivo["txtConsecutivo"].ToString());
                    CPersonas.Update(persona);

                    GE_TGENTE gente = CGente.getByPersonaId(persona.pers_consecutivo);

                    GE_TPARAMETROS compania = CParametros.GetByConsecutivo(Convert.ToInt32(persona.pers_empresa));
                    GE_TPERIODOPRESUPUESTO periodo = CPeriodo.GetPeriodoActivo();
                    GE_TCENTROSCOSTOS ccostos = CCostos.GetSingle(Convert.ToInt32(persona.pers_ccosto));

                    if( 
                        gente != null
                        &&
                        (
                            gente.gent_ccostos != persona.pers_ccosto ||
                            gente.gent_empresa != compania.parm_descripcion
                        )
                        &&
                            gente.gent_periodo == periodo.peri_consecutivo
                    )
                    {
                        gente.gent_ccostos = persona.pers_ccosto;
                        
                        gente.gent_periodo = periodo.peri_consecutivo;

                        gente.gent_usuario_carga = strUsuario[0].ToString();
                        gente.gent_fecha_cargue = DateTime.Now;

                        gente.gent_descripcion_ccostos = ccostos.cost_descripcion;

                        gente.gent_empresa = compania.parm_descripcion;

                        CGente.Add(gente);    
                    }
                }
                else
                {
                    CPersonas.Add(persona);
                }

                Session["mensaje"] = "OK";
                Session["objeto"] = persona;
                Session["persona"] = persona;

                Response.Redirect("frmPersonas.aspx", false);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarErrorGuardar();
            }
        }
        #endregion
    }
}