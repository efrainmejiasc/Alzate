using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MedeskiView.Controllers;
using Presentacion;

namespace MedeskiView.Forms
{
    public partial class inicio : System.Web.UI.Page
    {
        public string urlPaginaInicial = "";
        CtrUsuarios ctrUsuarios = new CtrUsuarios();
        CtrPersonas ctrPersonas = new CtrPersonas();
        Utilidades utilidades = new Utilidades();
        CtrVlrsParamGrales ctrParam = new CtrVlrsParamGrales();
        CtrParametros cParam = new CtrParametros();
        CtrUtilidades ctrUtilidades = new CtrUtilidades();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string resul = ctrUsuarios.LoginUsuarioDominio2();

                if (resul == "true")
                {
                    dominio.Visible = false;

                }
                else
                {
                    lbTexto.Text = resul;
                    dominio.Visible = false;

                }

            }
            urlPaginaInicial = ctrParam.GetByClase("URLPAGINICIAL").vhpg_valor;
            dominio.Text = ctrParam.GetByClase("DOMINIO").vhpg_valor;
            username.Focus();
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            try
            {
                string validacionDirectorio = "";
                /*
                if (!ctrUtilidades.IngresoUsuarioNoValidar(username.Text.ToUpper(), password.Text))
                {
                */
                    //se valida si el usuario se encuentra en la base de datos
                    if (ctrUsuarios.AutenticarUsuarioBd(username.Text.ToUpper(), dominio.Text.ToUpper()))
                    {
                        //se obtiene el usuario de la base de datos
                        // modificar **
                        GE_TUSUARIOS usuario = ctrUsuarios.ObtenerUsuario(username.Text.ToUpper(), dominio.Text.ToUpper());

                        //se valida si el usuario debe ser consultado en el directorio activo
                        if (usuario.USUA_VALIDALDAP == "S")
                        {
                            validacionDirectorio = ctrUsuarios.ValidarUsuarioDA(dominio.Text, username.Text.ToUpper(), password.Text);

                            string s = Request.Url.ToString();
                            if (validacionDirectorio == "sesionOk")
                            {
                                IList<GE_TUSUARIOSXROL> ObtenerRolXUsuariosBD = ctrUsuarios.ObtenerRolXUsuariosBD(usuario);
                                if (ObtenerRolXUsuariosBD.Count() > 0)
                                {
                                    SesionIniciada();
                                }
                                else
                                {
                                    SesionErrada("El usuario no cuenta con un rol asignado.");
                                }                                
                            }
                            else
                            {
                                SesionErrada(validacionDirectorio);
                            }

                        }
                        else
                        {
                            if (ctrUsuarios.AutenticarUsuarioBd(username.Text.ToUpper(), password.Text, dominio.Text.ToUpper()))
                            {
                                IList<GE_TUSUARIOSXROL> ObtenerRolXUsuariosBD = ctrUsuarios.ObtenerRolXUsuariosBD(usuario);
                                if (ObtenerRolXUsuariosBD.Count() > 0)
                                {
                                    SesionIniciada();
                                }
                                else
                                {
                                    SesionErrada("El usuario no cuenta con un rol asignado.");
                                }
                            }
                            else 
							{
                                SesionErrada("Contraseña Invalida");
                            }
                        }

                    }
                    else
                    {
                        validacionDirectorio = ctrUsuarios.ValidarUsuarioDA(dominio.Text.ToUpper(), username.Text.ToUpper(), password.Text);

                        if (validacionDirectorio == "sesionOk")
                        {
                            SesionIniciada();
                        }
                        else
                        {
                            SesionErrada(validacionDirectorio);
                        }

                    }
                /*}
                else
                {
                    SesionIniciada();
                }
                 * */
            }
            catch (Exception ex)
            {
                this.Show("Error Iniciado Sesión", ex.Message, "errado");
            }
        }

        public void SesionIniciada()
        {
            Session["Usuario"] = username.Text + ";" + dominio.Text.ToUpper() + ";" + ctrUsuarios.Encrypt(password.Text);
            // GE_TPERSONAS persona = ctrPersonas.GetbyUsuario(username.Text.ToString().ToUpper());
            // Session["Persona"] = persona.pers_nombres + '|' + persona.pers_apellido + '|' + username.Text.ToString().ToLower() + "@fanalca.com";
            string urlActual = Request.Url.ToString();
            Session["Url"] = urlActual;
            Session["GridTamano"] = ctrParam.GetByClase("GRID_TAMANO").vhpg_valor;
            Session["GridFilterRow"] = ctrParam.GetByClase("GRID_FILTERROW").vhpg_valor;
            Session["GridHdBackColor"] = ctrParam.GetByClase("GRID_HEADERBACKCOLOR").vhpg_valor;
            Session["GridHdForeColor"] = ctrParam.GetByClase("GRID_HEADERFORECOLOR").vhpg_valor;
            Session["GridHdFontBold"] = ctrParam.GetByClase("GRID_HEADERFONTBOLD").vhpg_valor;
            Session["GridHdHorAlign"] = ctrParam.GetByClase("GRID_HEADERHORIZALIGN").vhpg_valor;
            Session["GridFrBackColor"] = ctrParam.GetByClase("GRID_FROWBACKCOLOR").vhpg_valor;
            Session["GridFrForeColor"] = ctrParam.GetByClase("GRID_FROWFORECOLOR").vhpg_valor;
            Session["GridAlternativeRowColor"] = ctrParam.GetByClase("GRID_ALTERNATIVEROW").vhpg_valor;
            Session["GridAllowFocuseRow"] = ctrParam.GetByClase("GRID_ALLOWFOCUSEROW").vhpg_valor;
            paginaInicial();

        }

        public void SesionErrada()
        {
            Session["Usuario"] = null;
            string strMensaje = "Credenciales invalidas";

            // utilidades.Show("Se presento el siguiente error iniciado sesión: ");
            this.Show("Información", strMensaje, "informativo");

            LimpiarCampos();
        }

        public void SesionErrada(string respuesta)
        {
            Session["Usuario"] = null;
            
            // utilidades.Show("Se presento el siguiente error iniciado sesión: " + respuesta);
            this.Show("Información", respuesta, "informativo");
            LimpiarCampos();
        }

        protected void paginaInicial()
        {
            Response.Redirect(urlPaginaInicial);
        }

        public void LimpiarCampos()
        {
            dominio.Text = "";
            username.Text = "";
            password.Text = "";

        }

        public void Show(string titulo, string mensaje, string funcion)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "carga", funcion + "('" +
                                            titulo + " :','" +
                                            mensaje + "'); ", true);
        }
    }
}