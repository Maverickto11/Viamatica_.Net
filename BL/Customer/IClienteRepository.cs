using BE_Models.DTO;
using BE_Models.Models;
using BE_Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Customer
{
    public interface IClienteRepository
    {
        List<Cliente> GetCliente();
        Response GetClienteCedula(string cedula);
    }
}
