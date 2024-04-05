using System;
using System.Collections.Generic;

namespace ComprasISO810.Models;

public partial class UnidadesDeMedidum
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();

    public virtual ICollection<OrdenDeCompra> OrdenDeCompras { get; set; } = new List<OrdenDeCompra>();

    public virtual ICollection<SolicitudDeArticulo> SolicitudDeArticulos { get; set; } = new List<SolicitudDeArticulo>();
}
