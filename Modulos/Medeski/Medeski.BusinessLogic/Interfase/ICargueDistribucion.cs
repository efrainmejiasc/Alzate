using Medeski.BusinessLogic.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface ICargueDistribucion
    {
        IList<DTOgenericoCargueArchivos> informacionExcelPoi(String p_hoja, String p_archivo);
        IList<DTOgenericoCargueArchivos> GetAllActive();
        void Guardar(IList<GE_TCARGUEDISTRIBUCION> p_lstDrivers);
        IList<GE_TCARGUEDISTRIBUCION> GetAll();
        // void Update(params GE_TCARGUEDISTRIBUCION[] objeto);
    }
}
