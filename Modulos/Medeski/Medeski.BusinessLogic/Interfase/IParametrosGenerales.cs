using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IParametrosGenerales
    {
        IList<PARAMETROSGRALES> GetAll();
        PARAMETROSGRALES GetByClase(string strClase);
        void Add(params PARAMETROSGRALES[] objeto);
        void Update(params PARAMETROSGRALES[] objeto);
        void Remove(params PARAMETROSGRALES[] objeto);
        int GetNextSequenceValue(string nombreSequencia);
    }
}
