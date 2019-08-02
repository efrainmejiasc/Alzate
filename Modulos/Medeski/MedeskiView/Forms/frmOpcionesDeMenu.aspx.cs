using DevExpress.Web;
using MedeskiView.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedeskiView.Forms
{
    public partial class frmOpcionesDeMenu : System.Web.UI.Page
    {
        CtrRoles cRoles = new CtrRoles();
        CtrOpcionesMenu OpcionesMenu = new CtrOpcionesMenu();
        CtrOpcionesMenuRol OpcionesMenuRol = new CtrOpcionesMenuRol();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        IList<GE_TOPCIONESMENU> opcionesMenu;

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUrl = "../" + ctrParam.GetByClase("URLLOGIN").vhpg_valor;

            if (Session["usuario"] != null)
            {
                opcionesMenu = OpcionesMenu.GetAllOpcionesMenu();

                btnGuardar.Enabled = false;

                if (!IsPostBack)
                {
                    CargarDatos();
                    CargarListas();
                }

            }

            else
                Response.Redirect(strUrl);
        }

        #region Metodos
        public void addChildrens(TreeViewNodeCollection nodos)
        {
            foreach (TreeViewNode nodo in nodos)
            {
                IList<GE_TOPCIONESMENU> lista = opcionesMenu.Where(x => x.opcm_idpadre == Convert.ToInt32(nodo.Name)).OrderBy(x => x.opcm_idonma).ToList();
    
                foreach (var hijo in lista)
                {
                    nodo.Nodes.Add(hijo.opcm_titulo, hijo.opcm_consecutivo.ToString());

                    IList<GE_TOPCIONESMENU> hijos = opcionesMenu.Where(x => x.opcm_idpadre == hijo.opcm_consecutivo).OrderBy(x => x.opcm_idonma).ToList();
                    if (hijos.Count != 0)
                    {                        
                        addChildrens(nodo.Nodes);
                    }
                }
            }
            
        }

        public void CargarDatos()
        {
            try
            {
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
            try
            {
                char delimiter = ';';
                string[] strUsuario = null;
                strUsuario = Session["usuario"].ToString().Split(delimiter);

                IEnumerable<GE_TROLES> lstRoles = cRoles.GetRolesActivos();

                cmbRol.Items.Clear();
                cmbRol.Items.Add("Seleccionar Rol", null);

                foreach (var rol in lstRoles)
                {
                    cmbRol.Items.Add(rol.rolm_nombre, rol.rolm_consecutivo);
                }
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarError("Error cargando listas. " + ex.Message);
            }

        }

        public void limpiar()
        {
            cmbRol.Value = null;
            btnGuardar.Enabled = false;
            foreach (TreeViewNode item in treeView.Nodes)
            {
                item.Checked = false;
            }
        }

        private bool validar()
        {
            try
            {
                VentanaValidaciones.validarTxtNumericoObligatorio("Roles", cmbRol.Value, 6);
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Eventos
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void cmbRol_Change(object sender, EventArgs e)
        {
            btnGuardar.Enabled = true;

            try
            {
                IList<GE_TOPCIONESMENUXROL> miLista = OpcionesMenuRol.GetOpciones(Convert.ToInt32(cmbRol.Value));

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
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarError("Error cargando opciones de Menu. " + ex.Message);
            }
        }
        #endregion

        protected void GuardarClicked(object sender, EventArgs e)
        {
            try
            {
                if (validar())
                {
                    Char delimiter = ';';
                    string[] strUsuario = null;
                    strUsuario = Session["usuario"].ToString().Split(delimiter);

                    OpcionesMenuRol.deleteOpcionesUsuario(Convert.ToInt32(cmbRol.Value));

                    foreach (TreeViewNode item in treeView.Nodes)
                    {
                        bool flag = false;

                        foreach (TreeViewNode child in item.Nodes)
                        {
                            if (child.Checked == true)
                            {
                                GE_TOPCIONESMENUXROL opRol = new GE_TOPCIONESMENUXROL();
                                opRol.opcr_usuario = strUsuario[0].ToString();
                                opRol.opcr_fecha = DateTime.Now;
                                opRol.rolm_consecutivo = Convert.ToInt32(cmbRol.Value);
                                opRol.opcm_consecutivo = Convert.ToInt32(child.Name);

                                OpcionesMenuRol.Add(opRol);
                                flag = true;
                            }
                        }

                        if (flag)
                        {
                            GE_TOPCIONESMENUXROL opRol = new GE_TOPCIONESMENUXROL();
                            opRol.opcr_usuario = strUsuario[0].ToString();
                            opRol.opcr_fecha = DateTime.Now;
                            opRol.rolm_consecutivo = Convert.ToInt32(cmbRol.Value);
                            opRol.opcm_consecutivo = Convert.ToInt32(item.Name);

                            OpcionesMenuRol.Add(opRol);
                        }
                    }
                    limpiar();
                }
            }
            catch (Exception ex)
            {
                VentanaValidaciones.mostrarErrorGuardar();
            }
        }

    }
}