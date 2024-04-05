using System;
using System.Collections.Generic;

namespace ComprasISO810.Models;

public partial class Articulo
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int? Marca { get; set; }

    public int? UnidadDeMedida { get; set; }

    public int Existencia { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Marca? MarcaNavigation { get; set; }

    public virtual ICollection<OrdenDeCompra> OrdenDeCompras { get; set; } = new List<OrdenDeCompra>();

    public virtual ICollection<SolicitudDeArticulo> SolicitudDeArticulos { get; set; } = new List<SolicitudDeArticulo>();

    public virtual UnidadesDeMedidum? UnidadDeMedidaNavigation { get; set; }
}
