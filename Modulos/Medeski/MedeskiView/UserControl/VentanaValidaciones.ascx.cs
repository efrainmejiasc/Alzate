using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedeskiView.UserControl
{
    public partial class VentanaValidaciones : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Mensajes
        public void mostrarError(String mensaje)
        {
            mostrarMensaje("Error", mensaje, "errado");
        }

        public void mostrarErrorEliminar()
        {
            mostrarError("No se puede eliminar el registro");
        }

        public void mostrarErrorGuardar()
        {
            mostrarError("No se puede guardar el registro");
        }

        public void mostrarRegistroExitoso()
        {
            mostrarMensaje("Registro satisfactorio", "Transacción realizada satisfactoriamente", "exitoso");
        }

        public void mostrarMensajePersonalizado(String titulo, String mensaje)
        {
            mostrarMensaje(titulo, mensaje);
        }

        public void mostrarMensajePersonalizadoError(String strTitulo, String strMensaje, Exception ex)
        {
            mostrarMensaje(strTitulo, strMensaje + "." + ex.Message, "errado");
        }

        public void mostrarConfirmarAccion(String strTitulo, String strMensaje)
        {
            mostrarMensaje(strTitulo, strMensaje, "confirmarAccion");
        }

        private void mostrarMensaje(String titulo, String mensaje, string funcion = "informativo")
        {
            /*
            ventanaValid.HeaderText = titulo;
            lblMensaje.Text = mensaje;
            ventanaValid.ShowOnPageLoad = true;
            */
            ScriptManager.RegisterStartupScript(this, GetType(), "", funcion + "('" + titulo + "','" + mensaje + "');", true);
        }
        #endregion

        #region Validaciones

        public void validarRadioObligatorio(String nombreCampo, Object valorCampo)
        {
            if (valorCampo == null || valorCampo.ToString().ToLower().Equals("false"))
            {
                String error = "El campo " + nombreCampo + " es obligatorio";
                mostrarError(error);
                throw new Exception(error);
            }
        }


        public void validarTxtObligatorio(String nombreCampo, Object valorCampo, int cantidadCaracteres)
        {
            if (valorCampo == null || valorCampo.ToString().Equals(""))
            {
                String error = "El campo " + nombreCampo + " es obligatorio";
                mostrarError(error);
                throw new Exception(error);
            }
           if (valorCampo.ToString().Length >cantidadCaracteres)
            {
                String error = "El campo " + nombreCampo + " puede tener máximo " + cantidadCaracteres + " caracteres";
                mostrarError(error);
                throw new Exception(error);
            }
        }


        public void validarComboObligatorio(String nombreCampo, Object valorCampo)
        {
            if (valorCampo == null || valorCampo.ToString().Equals(""))
            {
                String error = "El campo " + nombreCampo + " es obligatorio";
                mostrarError(error);
                throw new Exception(error);
            }            
        }

        public void validarTxtNumericoObligatorio(String nombreCampo, Object valorCampo, int cantidadCaracteres)
        {
            validarTxtObligatorio(nombreCampo, valorCampo, cantidadCaracteres);
            try
            {
                Double.Parse(valorCampo.ToString());
            }
            catch
            {
                String error = "El campo " + nombreCampo + " debe ser numérico";
                mostrarError(error);
                throw new Exception(error);
            }
        }

        public void validarTxtMonedaObligatorio(String nombreCampo, Object valorCampo, int cantidadCaracteres)
        {
            validarTxtObligatorio(nombreCampo, valorCampo, cantidadCaracteres);
            try
            {
                Double.Parse(valorCampo.ToString());
            }
            catch
            {
                String error = "El campo " + nombreCampo + " debe ser numérico";
                mostrarError(error);
                throw new Exception(error);
            }
        }


        public void validarFechaObligatoria(String nombreCampo, Object valorCampo, int cantidadCaracteres)
        {
            validarTxtObligatorio(nombreCampo, valorCampo, cantidadCaracteres);
            try
            {
                DateTime.Parse(valorCampo.ToString());
            }
            catch
            {
                String error = "El campo " + nombreCampo + " debe ser tipo fecha";
                mostrarError(error);
                throw new Exception(error);
            }
        }
        #endregion
    }
}