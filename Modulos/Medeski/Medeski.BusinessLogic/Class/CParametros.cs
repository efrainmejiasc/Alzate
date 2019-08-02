using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces;
using Medeski.DataAcces.Class;

namespace Medeski.BusinessLogic.Class
{
    public class CParametros : Interfase.IParametros
    {
        private readonly IParametros CRUD;

        public CParametros()
        {
            CRUD = new Parametros();
        }

        public IList<GE_TPARAMETROS> GetAll()
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

        public IList<GE_TPARAMETROS> GetAllGridview()
        {
            try
            {
                using (var context = new Entities())
                {
                    var consulta = from param in context.GE_TPARAMETROS
                                   join clapclase in context.GE_TCLASESPARAMETROS on param.clap_clase equals clapclase.clap_clase
                                   select new
                                   {
                                       parm_consecutivo = param.parm_consecutivo,
                                       clap_clase = clapclase.clap_clase,
                                       clap_nombre = clapclase.clap_nombre,
                                       clap_descripcion = clapclase.clap_descripcion,
                                       parm_descripcion = param.parm_descripcion,
                                       parm_fechadesde = param.parm_fechadesde,
                                       parm_fechahasta = param.parm_fechahasta,
                                       parm_estado = param.parm_estado,
                                       parm_infoadicional = param.parm_infoadicional,
                                       parm_codigo = param.parm_codigo
                                   };

                    IList<GE_TPARAMETROS> lstParametros = new List<GE_TPARAMETROS>();
                    foreach (var item in consulta)
                    {
                        GE_TPARAMETROS param = new GE_TPARAMETROS();
                        GE_TCLASESPARAMETROS clap = new GE_TCLASESPARAMETROS();

                        param.parm_codigo = item.parm_codigo;
                        param.parm_consecutivo = item.parm_consecutivo;
                        param.parm_descripcion = item.parm_descripcion;
                        param.parm_estado = item.parm_estado;
                        param.parm_fechadesde = item.parm_fechadesde;
                        param.parm_fechahasta = item.parm_fechahasta;
                        param.parm_infoadicional = item.parm_infoadicional;
                        param.clap_clase = item.clap_clase;
                        clap.clap_clase = item.clap_clase;
                        clap.clap_nombre = item.clap_nombre;
                        clap.clap_descripcion = item.clap_descripcion;
                        param.GE_TCLASESPARAMETROS = clap;

                        lstParametros.Add(param);
                    }
                    return lstParametros;
                }
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPARAMETROS> GetList(int Clase)
        {
            try
            {
                return CRUD.GetList(d => d.clap_clase == Clase);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPARAMETROS> GetListbyClase(string strClase)
        {
            try
            {
                using (var contEnt = new Entities())
                {
                    var query = from par in contEnt.GE_TPARAMETROS
                                 join cla in contEnt.GE_TCLASESPARAMETROS on par.clap_clase equals cla.clap_clase
                                 where par.parm_estado == 1 && cla.clap_nombre == strClase
                                 select new
                                 {
                                    par.clap_clase,
                                    par.parm_codigo,
                                    par.parm_consecutivo,
                                    par.parm_descripcion,
                                    par.parm_estado,
                                    par.parm_fechadesde,
                                    par.parm_fechahasta,
                                    par.parm_infoadicional,
                                    cla.clap_nombre,
                                    cla.clap_descripcion
                                 };
                    IList<GE_TPARAMETROS> myList = new List<GE_TPARAMETROS>();

                    foreach(var item in query)
                    {
                        GE_TPARAMETROS param = new GE_TPARAMETROS();
                        param.parm_codigo = item.parm_codigo;
                        param.clap_clase = item.clap_clase; 
                        param.parm_consecutivo = item.parm_consecutivo;
                        param.parm_descripcion = item.parm_descripcion;
                        param.parm_estado = item.parm_estado;
                        param.parm_fechadesde = item.parm_fechadesde;
                        param.parm_fechahasta = item.parm_fechahasta;
                        param.parm_infoadicional = item.parm_infoadicional;

                        GE_TCLASESPARAMETROS clase = new GE_TCLASESPARAMETROS();
                        clase.clap_nombre = item.clap_nombre;
                        clase.clap_descripcion = item.clap_descripcion;

                        param.GE_TCLASESPARAMETROS = clase;

                        myList.Add(param);
                    }

                    return myList;
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPARAMETROS> GetListbyClaseOrdenada(string strClase)
        {
            try
            {
                using (var contEnt = new Entities())
                {
                    var query = (from par in contEnt.GE_TPARAMETROS
                                 join cla in contEnt.GE_TCLASESPARAMETROS on par.clap_clase equals cla.clap_clase
                                 where par.parm_estado == 1 && cla.clap_nombre == strClase
                                 select par).ToList().OrderBy(x =>x.parm_consecutivo);

                    return query;
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPARAMETROS> GetListbyClaseOrdenadaParametro(string strClase, int inConsecutivo)
        {
            try
            {
                using (var contEnt = new Entities())
                {
                    var query = (from par in contEnt.GE_TPARAMETROS
                                 join cla in contEnt.GE_TCLASESPARAMETROS on par.clap_clase equals cla.clap_clase
                                 where par.parm_estado == 1 && cla.clap_nombre == strClase && par.parm_consecutivo >= inConsecutivo
                                 select par).ToList().OrderBy(x => x.parm_consecutivo);

                    return query;
                }
            }
            catch
            {
                throw;
            }
        }


        public GE_TPARAMETROS GetById(string idParametro)
        {
            try
            {
                return CRUD.GetSingle(d => d.parm_consecutivo.Equals(idParametro));
            }
            catch
            {
                throw;
            }
        }

        public GE_TPARAMETROS GetByConsecutivo(int inConsecutivo)
        {
            try
            {
                return CRUD.GetSingle(d => d.parm_consecutivo == inConsecutivo);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPARAMETROS> GetByClaseCodigo(string strClase, string strCodigo)
        {
            try
            {
                using (var contEnt = new Entities())
                {
                    var query = (from par in contEnt.GE_TPARAMETROS
                                 join cla in contEnt.GE_TCLASESPARAMETROS on par.clap_clase equals cla.clap_clase
                                 where par.parm_estado == 1 && cla.clap_nombre == strClase && par.parm_codigo == strCodigo
                                 select par).ToList().OrderBy(x => x.parm_consecutivo);

                    return query;
                }
            }
            catch
            {
                throw;
            }
        }

        public void Add(params GE_TPARAMETROS[] objeto)
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

        public void Update(params GE_TPARAMETROS[] objeto)
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

        public void Remove(params GE_TPARAMETROS[] objeto)
        {
            try
            {
                CRUD.Remove(objeto);
            }
            catch
            {
                throw;
            }
        }

        public int GetNextSequenceValue(string nombreSequencia)
        {
            try
            {
                return CRUD.GetNextSequenceValue(nombreSequencia);
            }
            catch
            {
                throw;
            }
        }
    }
}
