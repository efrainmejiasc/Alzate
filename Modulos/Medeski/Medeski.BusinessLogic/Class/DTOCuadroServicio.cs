using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class DTOCuadroServicio
    {
        public int idProducto { get; set; }
        public string prod_codigo { get; set; }
        public decimal vlr_directo { get; set; }
        public decimal vlr_infraest { get; set; }
        public decimal vlr_interm { get; set; }
        public decimal vlr_ga { get; set; }
    }
}
