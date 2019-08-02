using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IRoles
    {
        IList<GE_TROLES> GetRoles();
        IList<GE_TROLES> GetRolesActivos();
        void Add(params GE_TROLES[] objeto);
        void Update(params GE_TROLES[] objeto);

    }
}
