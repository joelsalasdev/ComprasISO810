using System;
using System.Collections.Generic;

namespace ComprasISO810.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int? Departamento { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Departamento? DepartamentoNavigation { get; set; }

    public virtual ICollection<SolicitudDeArticulo> SolicitudDeArticulos { get; set; } = new List<SolicitudDeArticulo>();
}
