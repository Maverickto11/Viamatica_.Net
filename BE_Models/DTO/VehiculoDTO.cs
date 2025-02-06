using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Models.DTO
{
    public class VehiculoDTO
    {
        public int Id { get; set; }

        public string? Modelo { get; set; }

        public string? Color { get; set; }

        public decimal? TarifaBase { get; set; }

        public string? Placa { get; set; }

        public bool? Estado { get; set; }
    }
}
