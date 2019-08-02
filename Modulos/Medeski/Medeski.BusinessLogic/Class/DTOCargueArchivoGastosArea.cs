using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    /// <summary>
    /// Se crea este DTO para crear objetos y listas del tipo de archivo de Gastos de Área.
    /// JaramirezT
    /// </summary>
    public class DTOCargueArchivoGastosArea
    {
        public string ga_productos { get; set; }
        public string ga_ccostos{ get; set; }
        public decimal ga_valor{ get; set; }
        public string ga_usuario_carga { get; set; }
        public string ga_observaciones { get; set; }
    }
}
