using System;
using System.Collections.Generic;

namespace BE_Models.Models;

public partial class Factura
{
    public int Id { get; set; }

    public string? Detalle { get; set; }

    public decimal? Total { get; set; }

    public int? TotalHoras { get; set; }

    public DateTime? FechaEmision { get; set; }

    public DateTime? FechaRetorno { get; set; }

    public int? IdVehiculo { get; set; }

    public int? IdCliente { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Vehiculo? IdVehiculoNavigation { get; set; }
}
