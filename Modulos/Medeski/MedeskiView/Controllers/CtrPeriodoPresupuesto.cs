using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Medeski.BusinessLogic.Interfase;
using Medeski.BusinessLogic.Class;


namespace MedeskiView.Controllers
{
    public class CtrPeriodoPresupuesto : ApiController
    {
        IPeriodoPresupuesto perp = new CPeriodoPresupuesto();

        public IList<GE_TPERIODOPRESUPUESTO> GetAll()
        {
            try
            {
                return perp.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPERIODOPRESUPUESTO> GetAllActive()
        {
            try
            {
                return perp.GetAllActive();
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TPERIODOPRESUPUESTO pr)
        {
            try
            {
                perp.Add(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TPERIODOPRESUPUESTO pr)
        {
            try
            {
                perp.Update(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public GE_TPERIODOPRESUPUESTO GetPeriodoActivo()
        {
            try
            {
                return perp.GetPeriodoActivo();
            }
            catch
            {
                throw;
            }
        }



        public GE_TPERIODOPRESUPUESTO GetByAnioPaso(int anio, int paso)
        {
            try
            {
                return perp.GetByAnioPaso(anio, paso);
            }
            catch
            {
                throw;
            }
        }


        public IList<GE_TPERIODOPRESUPUESTO> FindByAnoEtapa(int ano, string etapa)
        {
            try
            {
                return perp.FindByAnoEtapa(ano, etapa);
            }
            catch
            {
                throw;
            }
        }


        public int LoadTransactions(int buscar, int nuevo)
        {
            try
            {
                return perp.LoadTransactions(buscar, nuevo);
            }
            catch
            {
                throw;
            }
        }

    }
}
