using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medeski.BusinessLogic.Class
{
    /// <summary>
    /// Se crea este DTO generico para realizar cualquier tipo de cargue de algún archivo
    /// que se requiera en el sistema.
    /// JaramirezT
    /// Creado: 13 - Agosto - 2017
    /// </summary>
    public class DTOgenericoCargueArchivos
    {
        public int dto_generic_id_consecutivo { get; set; }
        public int dto_generic_id_consecutivo_persona { get; set; }
        public int dto_generic_id_consecutivo_padre { get; set; }
        public int dto_generic_id_consecutivo_presupuesto { get; set; }
        public int dto_generic_numero_cedula { get; set; }
        public int dto_generic_estado { get; set; }
        public string dto_generic_productos { get; set; }
        public string dto_generic_ccostos { get; set; }
        public string dto_generic_coperaciones { get; set; }
        public string dto_generic_descripcion_ccostos { get; set; }
        public string dto_generic_descripcion_a { get; set; }
        public string dto_generic_descripcion_b { get; set; }
        public decimal? dto_generic_valor { get; set; }
        public decimal? dto_generic_valor_suma { get; set; }
        public decimal? dto_generic_valor_adicional { get; set; }
        public decimal? dto_generic_valor_total { get; set; }
        public decimal? dto_generic_valor_directos { get; set; }
        public decimal? dto_generic_valor_distribuidos { get; set; }
        public decimal? dto_generic_valor_colaborador { get; set; }
        public decimal? dto_generic_porcentaje_colaborador { get; set; }
        public string dto_generic_codigo { get; set; }
        public string dto_generic_usuario_carga { get; set; }
        public string dto_generic_driver { get; set; }
        public string dto_generic_observaciones { get; set; }
        public string dto_generic_empresa { get; set; }
        public string dto_generic_sede { get; set; }
        public string dto_generic_proveedor { get; set; }
        public string dto_generic_cantidad { get; set; }
        public string dto_generic_cantidad_total { get; set; }
        public string dto_generic_nombres { get; set; }
        public string dto_generic_apellidos { get; set; }
        public string dto_generic_cantidad_directos { get; set; }
        public string dto_generic_cantidad_distribuidos { get; set; }
        public string dto_generic_centro_operacion { get; set; }
        public string dto_generic_descripcion_cargo { get; set; }
        public int dto_generic_id_parametro { get; set; }
    }
}
