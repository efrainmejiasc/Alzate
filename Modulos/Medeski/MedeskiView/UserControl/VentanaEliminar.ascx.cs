using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedeskiView.UserControl
{
    public partial class VentanaEliminar : System.Web.UI.UserControl
    {
        public event EventHandler Elimina;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Mensaje(String mensaje, String strId, String strDescripcion)
        {
            lblMensaje.Text = mensaje;
            lblId.Text = strId;
            lblDescripcion.Text = strDescripcion;
        }

        protected void okClick(object sender, EventArgs e)
        {
            Elimina(sender, e);
        }

       
    }
}