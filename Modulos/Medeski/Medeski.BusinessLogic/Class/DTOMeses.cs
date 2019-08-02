using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    public class DTOMeses
    {
        public decimal valor_a { get; set; }
        public decimal valor_b { get; set; }
        
        public Int32 consec_a { get; set; }
        public Int32 consec_b { get; set; }

        public String mes_a { get; set; }
        public String mes_b { get; set; }

        public decimal trm_a { get; set; }
        public decimal trm_b { get; set; }

        public decimal equiv_trm_a { get; set; }
        public decimal equiv_trm_b { get; set; }
    }
}
