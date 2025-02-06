using BE_Models.DTO;
using BE_Models.Models;
using BE_Models.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAC.Customer
{
    public interface IClienteService
    {
        List<Cliente> GetCliente();
        Response GetClienteCedula(string cedula);
        Task<Response> UploadFileAsync(IFormFile file);


    }
}
