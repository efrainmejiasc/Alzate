using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IVwVlrGenteTecnicaProd
    {
        IList<VW_VLR_GENTE_TECNICA_PROD> getAllByserver(String p_nombreServidor);
        IList<String> getAllServer();
    }
}

