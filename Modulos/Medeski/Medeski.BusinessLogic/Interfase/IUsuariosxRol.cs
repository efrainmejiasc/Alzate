using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IUsuariosxRol
    {
        IList<GE_TUSUARIOSXROL> GetUsuariosXRol(GE_TUSUARIOS user);
        void DeleteRolXUsuario(GE_TUSUARIOSXROL usuarioXRol);
        int InsertarUsuarioXrol(List<string> grupos, GE_TUSUARIOS usuario);
        void Add(params GE_TUSUARIOSXROL[] objeto);
    }
}
