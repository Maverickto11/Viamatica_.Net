using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Models.DTO
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string? Cedula { get; set; }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public string? Direccion { get; set; }

        public string? NumCelular { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public bool? Estado { get; set; }
    }
}
