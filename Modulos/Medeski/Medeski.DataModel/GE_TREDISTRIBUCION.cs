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

public partial class GE_TREDISTRIBUCION
{
    public int redi_consecutivo { get; set; }
    public int redi_periodo { get; set; }
    public int redi_producto_orig { get; set; }
    public int redi_producto_dist { get; set; }
    public decimal redi_valor { get; set; }
    public string redi_usuario { get; set; }
    public System.DateTime redi_fecha { get; set; }
    public Nullable<decimal> redi_valor_asignado { get; set; }
    public Nullable<decimal> redi_valor_producto { get; set; }

    public virtual GE_TPERIODOPRESUPUESTO GE_TPERIODOPRESUPUESTO { get; set; }
    public virtual GE_TPRODUCTOS GE_TPRODUCTOS { get; set; }
    public virtual GE_TPRODUCTOS GE_TPRODUCTOS1 { get; set; }
}
