using Medeski.DataAcces.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class CvwGenteTecnica : Interfase.IvwGenteTecnica
    {
        //Interfaz de la Vista de Gente Tecnica
        private readonly IvwGenteTecnica CRUD;

        public CvwGenteTecnica()
        {
            CRUD = new VwGenteTecnica();
        }

        public IList<VW_GENTE_TECNICA> getAllFindName(String p_nombreTecnico)
        {

            try
            {
                return CRUD.GetAll().Where(a => a.NOMBRE == p_nombreTecnico).ToList();
            }
            catch
            {
                
                throw;
            }

        }

        public IList<String> getAllTecnicos()
        {
            try
            {
                return CRUD.GetAll().GroupBy(a => a.NOMBRE).Select(b => b.Key).ToList();
            }
            catch 
            {
                
                throw;
            }
        }

    }
}
