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

public partial class GE_TPRODUCTOSITEMS
{
    public GE_TPRODUCTOSITEMS()
    {
        this.GE_TDISTRIBUCIONINFRAESTRUCTURA = new HashSet<GE_TDISTRIBUCIONINFRAESTRUCTURA>();
        this.GE_TDISTRIBUCIONINTERMEDIOS = new HashSet<GE_TDISTRIBUCIONINTERMEDIOS>();
        this.GE_TPERIODOTRANSACCIONES = new HashSet<GE_TPERIODOTRANSACCIONES>();
        this.GE_TRELITEMSDATACENTERPROD = new HashSet<GE_TRELITEMSDATACENTERPROD>();
        this.GE_TSALIDAPRESUPUESTO = new HashSet<GE_TSALIDAPRESUPUESTO>();
    }

    public int prit_consecutivo { get; set; }
    public int prit_producto { get; set; }
    public string prit_item { get; set; }
    public int prit_cuenta { get; set; }
    public int prit_activo { get; set; }
    public string prit_usuario { get; set; }
    public System.DateTime prit_fecha { get; set; }
    public string prit_usuario_act { get; set; }
    public System.DateTime prit_fecha_act { get; set; }
    public string prit_tipo { get; set; }

    public virtual GE_TCUENTAS GE_TCUENTAS { get; set; }
    public virtual ICollection<GE_TDISTRIBUCIONINFRAESTRUCTURA> GE_TDISTRIBUCIONINFRAESTRUCTURA { get; set; }
    public virtual ICollection<GE_TDISTRIBUCIONINTERMEDIOS> GE_TDISTRIBUCIONINTERMEDIOS { get; set; }
    public virtual ICollection<GE_TPERIODOTRANSACCIONES> GE_TPERIODOTRANSACCIONES { get; set; }
    public virtual GE_TPRODUCTOS GE_TPRODUCTOS { get; set; }
    public virtual ICollection<GE_TRELITEMSDATACENTERPROD> GE_TRELITEMSDATACENTERPROD { get; set; }
    public virtual ICollection<GE_TSALIDAPRESUPUESTO> GE_TSALIDAPRESUPUESTO { get; set; }
}
