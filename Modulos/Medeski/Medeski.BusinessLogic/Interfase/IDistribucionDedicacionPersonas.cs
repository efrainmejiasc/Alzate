using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IDistribucionDedicacionPersonas
    {
        IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> getAllServidoresFindPeriodo(Int32 p_periodo_activo, Int32 p_consecutivoPersona);
        IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> getAllDedicacionPersona(Int32 p_usuario, Int32 p_periodo_activo);
        void guardar(IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> p_lstDistribucionDedicacionPersonas);
        void actualizar(IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> p_lstDistribucionDedicacionPersonas);
        bool validaDistribucion(Int32 p_codProducto);

    }
}
