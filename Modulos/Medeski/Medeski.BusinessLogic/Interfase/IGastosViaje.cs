using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IGastosViaje
    {
        IList<GE_TCALCULOGASTOSVIAJE> GetAll();
        IList<GE_TCALCULOGASTOSVIAJE> GetxGrupo(int inIdGrupo);
        IList<GE_TCALCULOGASTOSVIAJE> GetxDestino(int inIdDestino);
        GE_TCALCULOGASTOSVIAJE GetGrupoDestino(int inIdGrupo, int inIdDestino);
    }
}
