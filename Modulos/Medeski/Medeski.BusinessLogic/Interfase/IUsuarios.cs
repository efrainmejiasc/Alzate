using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IUsuarios
    {
        IList<GE_TUSUARIOS> GetUsuarios();
        GE_TUSUARIOS LoginUsuario(GE_TUSUARIOS usuario);
        GE_TUSUARIOS LoginUsuarioBD(GE_TUSUARIOS usuario);
        GE_TUSUARIOS GetUsuarioID(GE_TUSUARIOS usuario);
        GE_TUSUARIOS GetUsuario(GE_TUSUARIOS usuario);
        GE_TUSUARIOS ObtenerUsuario(GE_TUSUARIOS usuario);
        GE_TUSUARIOS ObtenerUsuarioConContrasena(GE_TUSUARIOS usuario);
        int GetNextSequenceValue(string strSecuencia);
        GE_TUSUARIOS ObtenerUsuarioXID(int codigoUsuario);
        void Add(params GE_TUSUARIOS[] objeto);
        void Update(params GE_TUSUARIOS[] objeto);
        void Remove(params GE_TUSUARIOS[] objeto);

    }
}
