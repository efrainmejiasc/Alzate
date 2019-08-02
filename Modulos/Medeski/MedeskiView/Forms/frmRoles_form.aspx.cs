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
    public partial class frmRoles_form : System.Web.UI.Page
    {
        IList<GE_TPARAMETROS> lstParams;

        CtrParametros CParametros = new CtrParametros(); 
        CtrRoles ctrRoles = new CtrRoles();
        CtrOpcionesMenu OpcionesMenu = new CtrOpcionesMenu();
        CtrOpcionesMenuRol OpcionesMenuRol = new CtrOpcionesMenuRol();
        CtrVlrsParamGrales parametros = new CtrVlrsParamGrales();
        CtrUtilidades Cutilidades = new CtrUtilidades();
        IList<GE_TOPCIONESMENU> opcionesMenu;
        IDictionary<int, GE_TOPCIONESMENUXROL> mLista = new Dictionary<int, GE_TOPCIONESMENUXROL>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + parametros.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                opcionesMenu = OpcionesMenu.GetAllOpcionesMenu();
                
                if (!IsPostBack)
                {
                    CargarDatos();
                    CargarListas();

                    if (Session["objeto"] != null)
                    {
                        GE_TROLES mObjeto = Session["objeto"] as GE_TROLES;

                        txtRol["txtRol"] = mObjeto.rolm_consecutivo.ToString();
                        txtNombre.Value = mObjeto.rolm_nombre.ToString();
                        txtDescripcion.Value = mObjeto.rolm_descripcion.ToString();
                        lbxEstado.Value = mObjeto.rolm_estado.ToString();

                        if (mObjeto.GE_TPARAMETROS.parm_descripcion != "")
                        {
                            ListEditItem liRoles = cmbGrupos.Items.FindByText(mObjeto.GE_TPARAMETROS.parm_descripcion.ToString());
                            liRoles.Selected = true;
                        }

                        IList<GE_TOPCIONESMENUXROL> miLista = OpcionesMenuRol.GetOpciones(Convert.ToInt32(mObjeto.rolm_consecutivo.ToString()));

                        if (miLista != null)
                        {
                            foreach (TreeViewNode item in treeView.Nodes)
                            {
                                item.Checked = false;
                                foreach (TreeViewNode child in item.Nodes)
                                {
                                    bool esta = miLista.Any(x => x.opcm_consecutivo == Convert.ToInt32(child.Name));

                                    if (esta)
                                    {
                                        child.Checked = true;
                                    }
                                }
                            }
                        }
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
            txtRol["txtRol"] = string.Empty;

            foreach (TreeViewNode item in treeView.Nodes)
            {
                item.Checked = false;
            }
        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarTxtObligatorio("Nombre", txtNombre.Text, 100);
                VentanaValidaciones.validarTxtObligatorio("Descripción", txtDescripcion.Text, 200);
                VentanaValidaciones.validarTxtNumericoObligatorio("Estado", lbxEstado.Value, 1);

                VentanaValidaciones.validarComboObligatorio("Grupo", cmbGrupos.Value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void addNietos(TreeViewNode nodo, IList<GE_TOPCIONESMENU> nietos)
        {
            foreach (var hijo in nietos)
            {
                IList<GE_TOPCIONESMENU> hijos = opcionesMenu.Where(x => x.opcm_idpadre == hijo.opcm_consecutivo).OrderBy(x => x.opcm_idonma).ToList();

                if (hijos.Count != 0)
                {
                    TreeViewNode myNode = new TreeViewNode(hijo.opcm_titulo, hijo.opcm_consecutivo.ToString());
                    addNietos(myNode, hijos);
                    nodo.Nodes.Add(myNode);
                }
                else
                {
                    nodo.Nodes.Add(hijo.opcm_titulo, hijo.opcm_consecutivo.ToString());

                }
            }
        }

        public void addChildrens(TreeViewNodeCollection nodos)
        {
            foreach (TreeViewNode nodo in nodos)
            {
                IList<GE_TOPCIONESMENU> lista = opcionesMenu.Where(x => x.opcm_idpadre == Convert.ToInt32(nodo.Name)).OrderBy(x => x.opcm_idonma).ToList();

                foreach (var hijo in lista)
                {
                    IList<GE_TOPCIONESMENU> hijos = opcionesMenu.Where(x => x.opcm_idpadre == hijo.opcm_consecutivo).OrderBy(x => x.opcm_idonma).ToList();
                    
                    if (hijos.Count != 0)
                    {
                        TreeViewNode myNode = new TreeViewNode(hijo.opcm_titulo, hijo.opcm_consecutivo.ToString());
                        addNietos(myNode, hijos);
                        nodo.Nodes.Add(myNode);                        
                    }
                    else
                    {
                        nodo.Nodes.Add(hijo.opcm_titulo, hijo.opcm_consecutivo.ToString());

                    }
                    
                }
            }

        }

        public void CargarDatos()
        {
            try
            {
                treeView.Nodes.Clear();

                foreach (var opcion in opcionesMenu)
                {
                    if (opcion.opcm_idpadre == 0)
                    {
                        treeView.Nodes.Add(opcion.opcm_titulo, opcion.opcm_consecutivo.ToString());
                    }
                }
                addChildrens(treeView.Nodes);
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarError("Error cargando Datos. " + ex.Message);
            }

        }

        public void CargarListas()
        {

            lstParams = CParametros.GetListbyClase("GRUPOSLDAP");
            cmbGrupos.Items.Clear();
            cmbGrupos.Items.Add("Seleccionar Grupo", null);
            foreach (GE_TPARAMETROS param in lstParams)
            {
                cmbGrupos.Items.Add(param.parm_descripcion, param.parm_consecutivo);
            }

            if (cmbGrupos.Items.Count == 2)
            {
                cmbGrupos.Items[1].Selected = true;
            }

        }

        private void addPermiso(TreeViewNode myNodo, int myRol, String myUser)
        {
            foreach (TreeViewNode child in myNodo.Nodes)
            {
                if (child.Nodes.Count() > 0)
                {
                    addPermiso(child, myRol, myUser);
                }
                
                if (child.Checked)
                {
                    // Child
                    if (!mLista.ContainsKey(Convert.ToInt32(child.Name)))
                    {
                        GE_TOPCIONESMENUXROL opRol = new GE_TOPCIONESMENUXROL();
                        opRol.opcr_usuario = myUser.ToString();
                        opRol.opcr_fecha = DateTime.Now;
                        opRol.rolm_consecutivo = Convert.ToInt32(myRol);
                        opRol.opcm_consecutivo = Convert.ToInt32(child.Name);
                    
                        mLista.Add(Convert.ToInt32(child.Name), opRol);
                    }

                    // Parent
                    if (!mLista.ContainsKey(Convert.ToInt32(child.Parent.Name)))
                    {
                        GE_TOPCIONESMENUXROL opRolPadre = new GE_TOPCIONESMENUXROL();
                        opRolPadre.opcr_usuario = myUser.ToString();
                        opRolPadre.opcr_fecha = DateTime.Now;
                        opRolPadre.rolm_consecutivo = Convert.ToInt32(myRol);
                        opRolPadre.opcm_consecutivo = Convert.ToInt32(child.Parent.Name);

                        mLista.Add(Convert.ToInt32(child.Parent.Name), opRolPadre);
                    }

                }
                else
                {
                    // Parent When Item No Checked but some Child is checked
                    bool Esta = mLista.Any(x => x.Key == Convert.ToInt32(child.Name));
                    if (Esta)
                    {
                        if (!mLista.ContainsKey(Convert.ToInt32(child.Parent.Name)))
                        {
                            GE_TOPCIONESMENUXROL opRolPadre = new GE_TOPCIONESMENUXROL();
                            opRolPadre.opcr_usuario = myUser.ToString();
                            opRolPadre.opcr_fecha = DateTime.Now;
                            opRolPadre.rolm_consecutivo = Convert.ToInt32(myRol);
                            opRolPadre.opcm_consecutivo = Convert.ToInt32(child.Parent.Name);

                            mLista.Add(Convert.ToInt32(child.Parent.Name), opRolPadre);
                        }
                    }
                    
                }
            }                        
        }


        #region Eventos 
        protected void RegresarClicked(object sender, EventArgs e)
        {
            Response.Redirect("frmRoles.aspx");
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

                GE_TROLES cRol = new GE_TROLES();
                cRol.rolm_nombre = txtNombre.Text;
                cRol.rolm_descripcion = txtDescripcion.Text;
                cRol.rolm_estado = Convert.ToInt32(lbxEstado.Value.ToString());
                cRol.rolm_fecha_act = DateTime.Now;
                cRol.rolm_usuario_act = strUsuario[0].ToString();

                cRol.rolm_grupo = Convert.ToInt32(cmbGrupos.Value);
                int myConsec = 0;

                if (txtRol.Contains("txtRol") && !string.IsNullOrEmpty(txtRol["txtRol"].ToString()))
                {
                    cRol.rolm_consecutivo = Convert.ToInt32(txtRol["txtRol"].ToString());
                    ctrRoles.Update(cRol);
                    myConsec = cRol.rolm_consecutivo;
                }
                else
                {
                    cRol.rolm_fecha = DateTime.Now;
                    cRol.rolm_usuario = strUsuario[0].ToString();

                    ctrRoles.Add(cRol);
                    myConsec = cRol.rolm_consecutivo;
                }


                OpcionesMenuRol.deleteOpcionesUsuario(Convert.ToInt32(myConsec));

                foreach (TreeViewNode item in treeView.Nodes)
                {
                    this.addPermiso(item, myConsec, strUsuario[0].ToString());                   
                }

                foreach(var item in mLista)
                {
                    OpcionesMenuRol.Add(item.Value);
                }

                Session["mensaje"] = "OK";
                Response.Redirect("frmRoles.aspx", false);

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