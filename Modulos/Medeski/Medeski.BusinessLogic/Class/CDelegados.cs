using Medeski.DataAcces;
using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    internal class DelegadosComparer : IEqualityComparer<GE_TDELEGADOS>
    {
        public bool Equals(GE_TDELEGADOS x, GE_TDELEGADOS y)
        {
            if (string.Equals(x.dele_consecutivo.ToString(), y.dele_consecutivo.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(GE_TDELEGADOS obj)
        {
            return obj.dele_consecutivo.GetHashCode();
        }
    }

    public class CDelegados : Interfase.IDelegados
    {
        private readonly IDelegados CRUD;

        public CDelegados()
        {
            CRUD = new Delegados();
        }

        public IList<GE_TDELEGADOS> GetAll() 
        {
            try
            {
                return CRUD.GetAll();
            }
            catch
            {
                throw;
            }
        }


        public IList<GE_TDELEGADOS> GetAllActive()
        {
            try
            {
                return CRUD.GetList(x => x.dele_activo == 1);
            }
            catch
            {
                throw;
            }
        }

        public GE_TDELEGADOS GetSingle(int consecutivo)
        {
            try
            {
                return CRUD.GetSingle(x => x.dele_consecutivo == consecutivo);
            }
            catch
            {
                throw;
            }
        }

        public GE_TDELEGADOS GetByDelegado(int delegado)
        {
            try
            {
                return CRUD.GetSingle(x => x.dele_delegado == delegado);
            }
            catch
            {
                throw;
            }
        }


        public GE_TDELEGADOS GetByDelegadoFase(int delegado, int fase)
        {
            try
            {
                return CRUD.GetSingle(x => x.dele_delegado == delegado && x.dele_fase_parm == fase);
            }
            catch
            {
                throw;
            }
        }


        public IList<GE_TDELEGADOS> GetByJefeFase(int jefe, int fase)
        {
            try
            {
                return CRUD.GetList(x => x.dele_jefe == jefe && x.dele_fase_parm == fase && x.dele_activo == 1);
            }
            catch
            {
                throw;
            }
        }


        public void Add(params GE_TDELEGADOS[] objeto)
        {
            try
            {
                CRUD.Add(objeto);
            }
            catch
            {
                throw;
            }
        }

        public void Update(params GE_TDELEGADOS[] objeto)
        {
            try
            {
                CRUD.Update(objeto);
            }
            catch
            {
                throw;
            }
        }


        public IEnumerable<GE_TDELEGADOS> GetAllDelegados(string jefe) 
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from dele in context.GE_TDELEGADOS
                                   join boss in context.GE_TPERSONAS on dele.dele_jefe equals boss.pers_consecutivo
                                   join delegado in context.GE_TPERSONAS on dele.dele_delegado equals delegado.pers_consecutivo
                                   join param in context.GE_TPARAMETROS on dele.dele_fase_parm equals param.parm_consecutivo
                                   where dele.dele_activo == 1 && delegado.pers_activo == 1 && boss.pers_activo == 1 && boss.pers_usudom.Equals(jefe)
                                   select new
                                   {
                                       parametro = param.parm_descripcion,
                                       consecutivo = dele.dele_consecutivo,
                                       jefe = boss.pers_nombres,
                                       delegado = delegado.pers_nombres
                                   };
                    IList<GE_TDELEGADOS> lstDelegados = new List<GE_TDELEGADOS>();
                    foreach(var item in consulta){
                        GE_TPERSONAS boss = new GE_TPERSONAS();
                        boss.pers_nombres = item.jefe;

                        GE_TPERSONAS delegado = new GE_TPERSONAS();
                        delegado.pers_nombres = item.delegado;

                        GE_TPARAMETROS param = new GE_TPARAMETROS();
                        param.parm_descripcion = item.parametro;
                        
                        GE_TDELEGADOS del = new GE_TDELEGADOS();
                        del.dele_consecutivo = item.consecutivo;
                        del.GE_TPARAMETROS = param;
                        del.GE_TPERSONAS = boss;
                        del.GE_TPERSONAS1 = delegado;

                        lstDelegados.Add(del);

                    }
                    return lstDelegados.Distinct(new DelegadosComparer());
                }
                
            }
            catch
            {
                throw;
            }
        }
    }
}
