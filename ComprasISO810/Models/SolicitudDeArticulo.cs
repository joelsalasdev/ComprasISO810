using System;
using System.Collections.Generic;

namespace ComprasISO810.Models;

public partial class SolicitudDeArticulo
{
    public int Id { get; set; }

    public int? EmpleadoSolicitante { get; set; }

    public DateOnly FechaSolicitud { get; set; }

    public int? Articulo { get; set; }

    public int Cantidad { get; set; }

    public int? UnidadesDeMedida { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Articulo? ArticuloNavigation { get; set; }

    public virtual Empleado? EmpleadoSolicitanteNavigation { get; set; }

    public virtual ICollection<OrdenDeCompra> OrdenDeCompras { get; set; } = new List<OrdenDeCompra>();

    public virtual UnidadesDeMedidum? UnidadesDeMedidaNavigation { get; set; }
}
