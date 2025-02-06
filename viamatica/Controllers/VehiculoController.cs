using BE_Models.DTO;
using BE_Models.Models;
using DAC.Vehicle;
using Microsoft.AspNetCore.Mvc;

namespace viamatica.Controllers
{
    [Route("/api/{controller}")]
    [ApiController]
    public class VehiculoController : Controller
    {
        private readonly VehiculoService _vehiculoService;

        public VehiculoController(VehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        [HttpGet("/getAllVehicles")]
        public async Task<IActionResult> GetAllVehicles()
        {
            return Ok(await _vehiculoService.GetVehiculo());
        }

        [HttpGet("/getPlaca/{placa}")]
        public async Task<IActionResult> GetPlacaVehiculo(string placa)
        {
            return Ok(await _vehiculoService.GetVehiculobyPlaca(placa));
        }

        [HttpPost("/AddVehiculo")]
        public async Task<IActionResult> PostVehiculo([FromBody] VehiculoDTO vehiculo)
        {
            return Ok(_vehiculoService.AddVehiculo(vehiculo));        
        }
        [HttpPut("/Actulizar_Vehiculo")]
        public async Task<IActionResult> PutVehiculo([FromBody] VehiculoDTO vehiculo)
        {
            return Ok(await _vehiculoService.UpdateVehiculo(vehiculo));
        }
        [HttpPut("/Eliminar_Vehiculo/{id}")]
        public async Task<IActionResult> DeleteVehiculo(int id)
        {
            return Ok(await _vehiculoService.DeleteVehiculo(id));
        }

    }
}
