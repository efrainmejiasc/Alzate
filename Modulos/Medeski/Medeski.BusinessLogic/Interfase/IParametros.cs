using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Interfase
{
    public interface IParametros
    {
        IList<GE_TPARAMETROS> GetAll();
        IList<GE_TPARAMETROS> GetList(int Clase);
        GE_TPARAMETROS GetById(string idParametro);
        IList<GE_TPARAMETROS> GetListbyClase(string strClase);
        IEnumerable<GE_TPARAMETROS> GetListbyClaseOrdenada(string strClase);
        IList<GE_TPARAMETROS> GetAllGridview();
        void Add(params GE_TPARAMETROS[] objeto);
        void Update(params GE_TPARAMETROS[] objeto);
        void Remove(params GE_TPARAMETROS[] objeto);
        int GetNextSequenceValue(string nombreSequencia);
        GE_TPARAMETROS GetByConsecutivo(int inConsecutivo);
        IEnumerable<GE_TPARAMETROS> GetListbyClaseOrdenadaParametro(string strClase, int inConsecutivo);
        IEnumerable<GE_TPARAMETROS> GetByClaseCodigo(string strClase, string strCodigo);
    }
}
