using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using Medeski.DataAcces;

namespace Medeski.BusinessLogic.Class
{
    public class CVarEconomicas : Interfase.IVarEconomicas
    {
        private readonly IVareConomicas CRUD;

        public CVarEconomicas()
        {
            CRUD = new VareConomicas();
        }

        public void Add(params GE_TVARECONOMICAS[] objeto)
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

        public void Update(params GE_TVARECONOMICAS[] objeto)
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

        public IList<GE_TVARECONOMICAS> GetAll(int inAno)
        {
            try
            {
                return CRUD.GetList(x => x.vari_ano == inAno).ToList();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TVARECONOMICAS> GetByMonedaAno(int moneda, int inAno)
        {
            try
            {
                using(var context = new Entities())
                {
                    var query = from varec in context.GE_TVARECONOMICAS
                                join param in context.GE_TPARAMETROS on varec.vari_tipo_moneda equals param.parm_consecutivo
                                join param2 in context.GE_TPARAMETROS on varec.vari_mes equals param2.parm_consecutivo
                                where param.parm_consecutivo == moneda && varec.vari_ano == inAno
                                select new
                                {
                                    moneda = param.parm_descripcion,
                                    mes = param2.parm_descripcion,
                                    consec = varec.vari_consecutivo,
                                    valor = varec.vari_valor,
                                    activo = varec.vari_activo,
                                    tipoMoneda = varec.vari_tipo_moneda,
                                    ano = varec.vari_ano,
                                };

                    IList<GE_TVARECONOMICAS> lista = new List<GE_TVARECONOMICAS>();
                    foreach (var item in query)
                    {
                        GE_TPARAMETROS param = new GE_TPARAMETROS();
                        param.parm_descripcion = item.moneda;

                        GE_TPARAMETROS param2 = new GE_TPARAMETROS();
                        param2.parm_descripcion = item.mes;

                        GE_TVARECONOMICAS varec = new GE_TVARECONOMICAS();
                        varec.vari_consecutivo = item.consec;
                        varec.vari_valor = item.valor;
                        varec.vari_activo = item.activo;
                        varec.vari_tipo_moneda = item.tipoMoneda;
                        varec.vari_ano = item.ano;
                        
                        varec.GE_TPARAMETROS = param;
                        varec.GE_TPARAMETROS1 = param2;

                        lista.Add(varec);

                    }

                    return lista;
                }
                
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TVARECONOMICAS> GetAllActive(int inAno)
        {
            try
            {
                return CRUD.GetList(x => x.vari_ano == inAno && x.vari_activo == 1).ToList();
            }
            catch
            {
                throw;
            }
        }

        public GE_TVARECONOMICAS GetById(int inConsecutivo)
        {
            try
            {
                return CRUD.GetSingle(x => x.vari_consecutivo == inConsecutivo);
            }
            catch
            {
                throw;
            }
        }

        public GE_TVARECONOMICAS GetByAnoMes(int inMes, int inMoneda, int inAno)
        {
            try
            {
                return CRUD.GetSingle(x => x.vari_ano == inAno && x.vari_mes == inMes && x.vari_tipo_moneda == inMoneda && x.vari_activo == 1);
            }
            catch
            {
                throw;
            }
        }
    }
}
