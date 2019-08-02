using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedeskiView.Engine
{
    public class SumaDistribucion
    {
        public decimal sumServCdm { get; set; }
        public decimal sumServDesarrollo { get; set; }
        public decimal sumServvGerenciaTecnica { get; set; }
        public decimal sumServInfraestructura { get; set; }
        public decimal sumServJefatura { get; set; }
        public decimal sumServOperaciones { get; set; }

        public decimal sumProdCdm { get; set; }
        public decimal sumProdDesarrollo { get; set; }
        public decimal sumProdvGerenciaTecnica { get; set; }
        public decimal sumProdInfraestructura { get; set; }
        public decimal sumProdJefatura { get; set; }
        public decimal sumProdOperaciones { get; set; }
    }
}