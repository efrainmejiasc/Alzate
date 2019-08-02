using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IPorcentajesPYG
    {
        IList<GE_TPORCENTAJESPYG> GetAll();
        IList<GE_TPORCENTAJESPYG> GetAllActive();
        void Add(params GE_TPORCENTAJESPYG[] objeto);
        void Update(params GE_TPORCENTAJESPYG[] objeto);
    }
}
