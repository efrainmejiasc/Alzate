using Medeski.DataAcces;
using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CDistribucionDedicacionPersonas : Interfase.IDistribucionDedicacionPersonas
    {
        private readonly IDistribucionDedicacionPersonas CRUD;

        public CDistribucionDedicacionPersonas()
        {
            CRUD = new DistribucionDedicacionPersonas();
        }


        public IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> getAllServidoresFindPeriodo(Int32 p_periodo_activo, Int32 p_consecutivoPersona)
        {
            try
            {
                IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> lstDedicacionPersona = new List<GE_TDISTRIBUCIONDEDICACIONPERSONA>();
                return lstDedicacionPersona = CRUD.GetAll().Where(a => a.dper_tipo != "Producto")
                                                           .Where(a => a.dper_periodo == p_periodo_activo)
                                                           .Where(a => a.dper_persona == p_consecutivoPersona).ToList();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> getAllDedicacionPersona(Int32 p_consecutivoPersona, Int32 p_periodo_activo)
        {
            try
            {
                return CRUD.GetAll().Where(a => a.dper_persona == p_consecutivoPersona && a.dper_periodo == p_periodo_activo && a.dper_estado == 1).ToList();

            }
            catch
            {
                throw;
            }
        }

        public void guardar(IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> p_lstDistribucionDedicacionPersonas)
        {
            try
            {
                CRUD.Add(p_lstDistribucionDedicacionPersonas.ToArray());
            }
            catch
            {
                throw;
            }
        }

        public void actualizar(IList<GE_TDISTRIBUCIONDEDICACIONPERSONA> p_lstDistribucionDedicacionPersonas)
        {
            try
            {
                CRUD.Update(p_lstDistribucionDedicacionPersonas.ToArray());
            }
            catch
            {
                throw;
            }
        }

        public bool validaDistribucion(Int32 p_consecutivoProducto)
        {
            try
            {
                bool contieneDistribucion = false;
                contieneDistribucion = CRUD.GetAll().Any(a => a.dper_producto == p_consecutivoProducto);
                return contieneDistribucion;
            }
            catch
            {
                throw;
            }
        }

    }
}
