using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MedeskiView.UserControl
{
    public partial class BarraHerramientas : System.Web.UI.UserControl
    {
        public event EventHandler nuevoClick;
        public event EventHandler editarClick;
        public event EventHandler guardarClick;
        public event EventHandler eliminarClick;
        public event EventHandler exportarClick;

        public bool Nuevo { get; set; }
        public bool Editar { get; set; }
        public bool Guardar { get; set; }
        public bool Eliminar { get; set; }
        public bool Exportar { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // se implementan las propiedades de la barra de herramientas
                // para implementar los estados de las opciones se deben 
                // de colocar como variables de session y instanciarlar
                // en cada una de las propiedades y asi tomara el valor para cada una
                // de las opciones.
                this.Nuevo = Session["accionNuevo"] == null || Session["accionNuevo"].ToString().Equals("") || Session["accionNuevo"].ToString().Equals("0") ? false : true;
                this.Editar = Session["accionEditar"] == null || Session["accionEditar"].ToString().Equals("") || Session["accionEditar"].ToString().Equals("0") ? false : true;
                this.Guardar = Session["accionGuardar"] == null || Session["accionGuardar"].ToString().Equals("") || Session["accionGuardar"].ToString().Equals("0") ? false : true;
                this.Eliminar = Session["accionEliminar"] == null || Session["accionEliminar"].ToString().Equals("") || Session["accionEliminar"].ToString().Equals("0") ? false : true;
                this.Exportar = Session["accionExportar"] == null || Session["accionExportar"].ToString().Equals("") || Session["accionExportar"].ToString().Equals("0") ? false : true;
                foreach (DevExpress.Web.MenuItem item in toolbar.Items)
                {
                    switch (item.Name)
                    {
                        case "Nuevo":
                            item.Enabled = this.Nuevo;
                            break;
                        case "Editar":
                            item.Enabled = this.Editar;
                            break;
                        case "Guardar":
                            item.Enabled = this.Guardar;
                            break;
                        case "Eliminar":
                            item.Enabled = this.Eliminar;
                            break;
                        case "Exportar":
                            item.Enabled = this.Exportar;
                            break;
                    }
                }
            }
        }


        private Boolean ConsultarSesionActiva()
        {
            //pendiente
            return true;
        }

        public void LimpiarSesion()
        {
            Session["accionNuevo"] = "0";
            Session["accionEditar"] = "0";
            Session["accionGuardar"] = "0";
            Session["accionEliminar"] = "0";
            Session["accionExportar"] = "0";
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            var opcion = toolbar.SelectedItem.Name;
            switch (opcion)
            {
                case "Nuevo":
                    if (ConsultarSesionActiva())
                    {
                        if (nuevoClick != null)
                        {
                            nuevoClick(source, e);
                        }
                    }
                    else
                    {
                        LimpiarSesion();
                    }
                    break;
                case "Editar":
                    if (ConsultarSesionActiva())
                    {
                        if (editarClick != null)
                        {
                            editarClick(source, e);
                        }
                    }
                    else
                    {
                        LimpiarSesion();
                    }
                    break;
                case "Guardar":
                    if (ConsultarSesionActiva())
                    {
                        if (guardarClick != null)
                        {
                            guardarClick(source, e);
                        }
                    }
                    else
                    {
                        LimpiarSesion();
                    }
                    break;
                case "Eliminar":
                    if (ConsultarSesionActiva())
                    {
                        if (eliminarClick != null)
                        {
                            eliminarClick(source, e);
                        }
                    }
                    else
                    {
                        LimpiarSesion();
                    }
                    break;
                case "Exportar":
                    if (ConsultarSesionActiva())
                    {
                        if (exportarClick != null)
                        {
                            exportarClick(source, e);
                        }
                    }
                    else
                    {
                        LimpiarSesion();
                    }
                    break;
            }
        }
    }
}