using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IvwGenteTecnica
    {
        IList<VW_GENTE_TECNICA> getAllFindName(String p_nombreTecnico);
        IList<String> getAllTecnicos();
    }
}
