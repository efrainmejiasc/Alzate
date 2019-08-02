using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IOpcionesMenuRol
    {
        IList<GE_TOPCIONESMENUXROL> GetOpciones(int idRol);
        void deleteOpcionesUsuario(int rol);
        void Add(params GE_TOPCIONESMENUXROL[] objeto);
    }
}
