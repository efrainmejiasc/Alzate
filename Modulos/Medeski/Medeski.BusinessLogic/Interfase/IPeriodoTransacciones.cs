using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IPeriodoTransacciones
    {
        IList<GE_TPERIODOTRANSACCIONES> GetAll();
        IList<GE_TPERIODOTRANSACCIONES> GetById(int inConsecutivo);
        void Add(params GE_TPERIODOTRANSACCIONES[] objeto);
        void Update(params GE_TPERIODOTRANSACCIONES[] objeto);
        IEnumerable<GE_TPERIODOTRANSACCIONES> GetAllGridView(string strUsuario, string strSubCateg);
        IEnumerable<GE_TPERIODOTRANSACCIONES> GetAllGridViewViaje(string strUsuario, string strSubCateg, string strTipo);
        IList<GE_TPERIODOTRANSACCIONES> GetAllActive();
        int LoadTransactions(int inConsecutivo);
        int LoadTransactionsPersons(int inConsecutivo);
    }
}
