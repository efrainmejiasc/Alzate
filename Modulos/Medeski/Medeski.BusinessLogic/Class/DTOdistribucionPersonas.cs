using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class DTOdistribucionPersonas
    {
        public Int32 consecutivo { get; set; }
        public Int32 consecutivoItem { get; set; }
        public String descripcion { get; set; }
        public decimal vlrDistribucion { get; set; }
        public Int32 consecutivoPersona { get; set; }
        public String tipo_distribucion { get; set; }
        public String usuarioSesion { get; set; }
    }
}
