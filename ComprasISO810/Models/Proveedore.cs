using System;
using System.Collections.Generic;

namespace ComprasISO810.Models;

public partial class Proveedore
{
    public int Id { get; set; }

    public string Cedula { get; set; } = null!;

    public string NombreComercial { get; set; } = null!;

    public string Estado { get; set; } = null!;
}
