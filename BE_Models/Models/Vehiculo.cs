using System;
using System.Collections.Generic;

namespace BE_Models.Models;

public partial class Vehiculo
{
    public int Id { get; set; }

    public string? Modelo { get; set; }

    public string? Color { get; set; }

    public decimal? TarifaBase { get; set; }

    public string? Placa { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
