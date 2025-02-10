using BE_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Models.DTO
{
    public class FacturaDTO
    {
        public int Id { get; set; }

        public string? Detalle { get; set; }

        public decimal? Total { get; set; }

        public int? TotalHoras { get; set; }

        public DateTime? FechaEmision { get; set; }

        public DateTime? FechaRetorno { get; set; }

        public int? IdVehiculo { get; set; }

        public int? IdCliente { get; set; }

        public virtual ClienteDTO? IdClienteNavigation { get; set; }

        public virtual VehiculoDTO? IdVehiculoNavigation { get; set; }

    }
}
