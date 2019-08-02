using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IDistribucionInfraest
    {
        IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> GetAllProductoItem(int periodo, int producto, int item); 
        IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> GetAllProductosDistribuidos(int inPeriodo, int inProductoInfraest, int inItem, string strTipo);
        IList<GE_TDISTRIBUCIONINFRAESTRUCTURA> GetAllProductosDistribuidosServ(int inPeriodo, int inServidor, string strTipo);
        void Add(params GE_TDISTRIBUCIONINFRAESTRUCTURA[] objeto);
        void Update(params GE_TDISTRIBUCIONINFRAESTRUCTURA[] objeto);
    }
}
