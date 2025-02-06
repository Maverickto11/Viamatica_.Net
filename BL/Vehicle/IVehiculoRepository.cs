using BE_Models.DTO;
using BE_Models.Models;
using BE_Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Vehicle
{
    public interface IVehiculoRepository
    {
        Task<Response> GetVehiculo();
        Task<Response> GetVehiculobyPlaca(string placa);
        Response AddVehiculo(VehiculoDTO vehiculo);

        Task<Response> UpdateVehiculo(VehiculoDTO vehiculo);
        Task<Response> DeleteVehiculo(int id);
    }
}
