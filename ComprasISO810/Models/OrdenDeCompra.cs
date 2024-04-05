using System;
using System.Collections.Generic;

namespace ComprasISO810.Models;

public partial class OrdenDeCompra
{
    public int Id { get; set; }

    public int? IdSolicitud { get; set; }

    public DateOnly FechaOrden { get; set; }

    public string? Estado { get; set; }

    public int? Articulo { get; set; }

    public int Cantidad { get; set; }

    public int? UnidadDeMedida { get; set; }

    public int? Marca { get; set; }

    public virtual Articulo? ArticuloNavigation { get; set; }

    public virtual SolicitudDeArticulo? IdSolicitudNavigation { get; set; }

    public virtual Marca? MarcaNavigation { get; set; }

    public virtual UnidadesDeMedidum? UnidadDeMedidaNavigation { get; set; }
}
