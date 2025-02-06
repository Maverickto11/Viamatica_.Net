using System;
using System.Collections.Generic;

namespace BE_Models.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string? Cedula { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Direccion { get; set; }

    public string? NumCelular { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
