using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IDominios
    {
        IList<GE_TDOMINIOS> GruposDirectorioActivo();
        int InsertAll();
        IList<GE_TDOMINIOS> GetGroups(string path);
        void Add(params GE_TDOMINIOS[] objeto);
        IList<GE_TDOMINIOS> GetAll();
        void Remove(params GE_TDOMINIOS[] objeto);
    }
}
