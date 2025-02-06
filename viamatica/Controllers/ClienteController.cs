using DAC.Customer;
using Microsoft.AspNetCore.Mvc;

namespace viamatica.Controllers
{
    [Route("/api")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("/ListaCliente")]
        public async Task<IActionResult> GetallCustomer()
        {
            return Ok(_clienteService.GetCliente());
        }

        [HttpGet("/ClienteCedula/{cedula}")]
        public async Task<IActionResult> GetallCustomerUser(string cedula)
        {
            if (_clienteService.GetClienteCedula(cedula) == null)
            {
                return BadRequest();
            }
            return Ok(_clienteService.GetClienteCedula(cedula));
        }

        [HttpPost("/Carga_Archivo")]
        public async Task<IActionResult> UploadFile(IFormFile File)
        {
            return Ok(await _clienteService.UploadFileAsync(File));
        }
    }
}
