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

public partial class GE_TCOMPANIAS
{
    public GE_TCOMPANIAS()
    {
        this.GE_TCARGUEDRIVERS = new HashSet<GE_TCARGUEDRIVERS>();
        this.GE_TCENTROSCOSTOS = new HashSet<GE_TCENTROSCOSTOS>();
    }

    public int comp_consecutivo { get; set; }
    public string comp_nombre { get; set; } 
    public string comp_descripcion { get; set; }
    public Nullable<int> comp_activo { get; set; }
    public string comp_estadoStr { get; set; }
    public Nullable<int> comp_usa_co { get; set; }
    public string comp_usuario { get; set; }
    public Nullable<System.DateTime> comp_fecha { get; set; }

    public virtual ICollection<GE_TCARGUEDRIVERS> GE_TCARGUEDRIVERS { get; set; }
    public virtual ICollection<GE_TCENTROSCOSTOS> GE_TCENTROSCOSTOS { get; set; }
}
