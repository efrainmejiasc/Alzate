using Medeski.BusinessLogic.Class;
using Medeski.BusinessLogic.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedeskiView.Controllers
{
    public class CtrGenteTecnica
    {
        IvwGenteTecnica IvwGenteTecnica = new CvwGenteTecnica();


        public IList<VW_GENTE_TECNICA> lstgetAllFindName(String p_nombreTecnico)
        {
            try
            {
                return IvwGenteTecnica.getAllFindName(p_nombreTecnico);
            }
            catch 
            {
                throw;
            }
        }

        public IList<String> lstgetTecnicos()
        {
            try
            {
                return IvwGenteTecnica.getAllTecnicos();
            }
            catch
            {
                
                throw;
            }
        }
    }
}