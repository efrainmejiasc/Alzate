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
    public class CtrPeriodoTransaccion : ApiController
    {
        IPeriodoTransacciones Iperiodo = new CPeriodoTransacciones();

        public IList<GE_TPERIODOTRANSACCIONES> GetAll()
        {
            try
            {
                return Iperiodo.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TPERIODOTRANSACCIONES> GetAllActive()
        {
            try
            {
                return Iperiodo.GetAllActive();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPERIODOTRANSACCIONES> GetAllGridView(string strUsuario, string strSubCateg)
        {
            try
            {
                return Iperiodo.GetAllGridView(strUsuario, strSubCateg);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<GE_TPERIODOTRANSACCIONES> GetAllGridViewViaje(string strUsuario, string strSubCateg, string strTipo)
        {
            try
            {
                return Iperiodo.GetAllGridViewViaje(strUsuario, strSubCateg, strTipo);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Add(GE_TPERIODOTRANSACCIONES pr)
        {
            try
            {
                Iperiodo.Add(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public IHttpActionResult Update(GE_TPERIODOTRANSACCIONES pr)
        {
            try
            {
                Iperiodo.Update(pr);
                return Ok(true);
            }
            catch
            {
                throw;
            }
        }

        public int LoadTransactions(int inConsecutivo)
        {
            try
            {
                return Iperiodo.LoadTransactions(inConsecutivo);
            }
            catch
            {
                throw;
            }
        }

        public int LoadTransactionsPersons(int inConsecutivo)
        {
            try
            {
                return Iperiodo.LoadTransactionsPersons(inConsecutivo);
            }
            catch
            {
                throw;
            }
        }
    }
}
