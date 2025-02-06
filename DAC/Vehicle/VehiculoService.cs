using BE_Models.DTO;
using BE_Models.Models;
using BE_Models.Response;
using BL.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAC.Vehicle
{
    public class VehiculoService : IVehiculoService
    {
        private readonly VehiculoRepository _vehiculoRepository;

        public VehiculoService(VehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }

        public Response AddVehiculo(VehiculoDTO vehiculo)
        {
            return _vehiculoRepository.AddVehiculo(vehiculo);

        }

        public async Task<Response> GetVehiculo()
        {
            return await _vehiculoRepository.GetVehiculo();
        }

        public async Task<Response> GetVehiculobyPlaca(string placa)
        {
            return await _vehiculoRepository.GetVehiculobyPlaca(placa);
        }

        public async Task<Response> UpdateVehiculo(VehiculoDTO vehiculo)
        {
            return await _vehiculoRepository.UpdateVehiculo(vehiculo);
        }

        public async Task<Response> DeleteVehiculo(int id)
        {
            return await _vehiculoRepository.DeleteVehiculo(id);
        }
    }
}
