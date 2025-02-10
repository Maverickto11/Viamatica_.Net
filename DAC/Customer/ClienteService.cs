using BE_Models.DTO;
using BE_Models.Models;
using BE_Models.Response;
using BL.Customer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAC.Customer
{
    public class ClienteService : IClienteService
    {
        private readonly ClienteRepository _clienteRepositroy;

        public ClienteService(ClienteRepository clienteRepositroy)
        {
            _clienteRepositroy = clienteRepositroy;
        }

        public List<Cliente> GetCliente()
        {
            return _clienteRepositroy.GetCliente();
        }

        public Response GetClienteCedula(string cedula)
        {
            return _clienteRepositroy.GetClienteCedula(cedula);
        }

        public Task<Response> CrearFactura(int idCliente, int idVehiculo)
        {
            return _clienteRepositroy.CrearFactura(idCliente, idVehiculo);
        }

        public async Task<Response> UploadFileAsync(IFormFile file)
        {
            return await _clienteRepositroy.UploadFileAsync(file);
        }
    }
}
