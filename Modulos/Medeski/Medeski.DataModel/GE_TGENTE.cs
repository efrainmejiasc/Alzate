//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class GE_TGENTE
{
    public int gent_consecutivo { get; set; }
    public Nullable<int> gent_periodo { get; set; }
    public Nullable<int> gent_ccostos { get; set; }
    public string gent_descripcion_ccostos { get; set; }
    public Nullable<int> gent_ceop { get; set; }
    public string gent_nombre_cargo { get; set; }
    public string gent_empresa { get; set; }
    public Nullable<decimal> gent_costo_colaborador { get; set; }
    public Nullable<int> gent_persona { get; set; }
    public Nullable<decimal> gent_porcentaje_manual_dedicacion { get; set; }
    public string gent_usuario_carga { get; set; }
    public Nullable<System.DateTime> gent_fecha_cargue { get; set; }
    public Nullable<int> gent_estado { get; set; }

    public virtual GE_TCENTROSCOSTOS GE_TCENTROSCOSTOS { get; set; }
    public virtual GE_TCENTROSOPERACION GE_TCENTROSOPERACION { get; set; }
    public virtual GE_TPERIODOPRESUPUESTO GE_TPERIODOPRESUPUESTO { get; set; }
    public virtual GE_TPERSONAS GE_TPERSONAS { get; set; }
}
