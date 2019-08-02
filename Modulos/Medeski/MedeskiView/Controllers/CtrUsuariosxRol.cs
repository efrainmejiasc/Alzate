using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Medeski.BusinessLogic.Interfase;
using Medeski.BusinessLogic.Class;

namespace MedeskiView.Controllers
{
    public class CtrUsuariosxRol : ApiController
    {
        IUsuariosxRol IUsuariosxRol = new CUsuariosxRol();

        public IList<GE_TUSUARIOSXROL> GetUsuariosXRol(GE_TUSUARIOS user)
        {
            return IUsuariosxRol.GetUsuariosXRol(user);
        }

        public void DeleteRolXUsuario(GE_TUSUARIOSXROL usuarioXRol)
        {
            IUsuariosxRol.DeleteRolXUsuario(usuarioXRol);
        }

        public int insertarUsuarioXrol (List<String> grupos,GE_TUSUARIOS usuario)
        {
            return IUsuariosxRol.InsertarUsuarioXrol(grupos, usuario);
        }
    }
}
