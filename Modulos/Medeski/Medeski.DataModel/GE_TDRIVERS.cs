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

public partial class GE_TDRIVERS
{
    public GE_TDRIVERS()
    {
        this.GE_TCARGUEDRIVERS = new HashSet<GE_TCARGUEDRIVERS>();
        this.GE_TPRODUCTOS = new HashSet<GE_TPRODUCTOS>();
        this.GE_TPRODUCTOS1 = new HashSet<GE_TPRODUCTOS>();
    }

    public int driv_consecutivo { get; set; }
    public string driv_nombre { get; set; }
    public string driv_descripcion { get; set; }
    public string driv_tipo_cobro { get; set; }
    public string driv_aplica_sede { get; set; }
    public string driv_aplica_valor { get; set; }
    public string driv_aplica_proveedor { get; set; }
    public string driv_usuario { get; set; }
    public Nullable<System.DateTime> driv_fecha { get; set; }
    public string driv_activo { get; set; }
    public string driv_estadoStr { get; set; }

    public virtual ICollection<GE_TCARGUEDRIVERS> GE_TCARGUEDRIVERS { get; set; }
    public virtual ICollection<GE_TPRODUCTOS> GE_TPRODUCTOS { get; set; }
    public virtual ICollection<GE_TPRODUCTOS> GE_TPRODUCTOS1 { get; set; }
}
