using BE_Models.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Shared;
using BE_Models.DTO;
using BE_Models.Response;
using BL.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace BL.Customer
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;
        public ClienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Connection") ?? string.Empty;
        }
        public List<Cliente> GetCliente()
        {
            List<Cliente> listaCliente = new();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("Obtener_clientes", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Cedula = reader["cedula"].ToString(),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            NumCelular = reader["num_celular"].ToString(),
                            FechaNacimiento = Parse.Date(reader["fecha_nacimiento"].ToString() ?? ""),
                            Estado = Convert.ToBoolean(reader["estado"])
                        };
                        listaCliente.Add(cliente);

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return listaCliente;
        }

        public Response GetClienteCedula(string cedula)
        {
            Response response = new Response();
            ClienteDTO cliente = new();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(SPName.GetClienteCedula, conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Parameters.cedula, cedula);
                    SqlDataReader reader = command.ExecuteReader();


                    if (reader.Read())
                    {
                        cliente = new ClienteDTO
                        {
                            Cedula = reader[tablesName.columCedula].ToString(),
                            Nombre = reader[tablesName.columNombre].ToString(),
                            Apellido = reader[tablesName.columApellido].ToString(),
                        };
                    }
                    if (cliente == null)
                    {
                        response.data = cliente!;
                        response.message = Message.foundNotData;
                        return response;
                    }

                    response.data = cliente;
                    response.message = Message.foundData;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return response;
        }


        public async Task<Response> UploadFileAsync(IFormFile file)
        {
            Response response = new();
            string fullPath = Path.GetFullPath(file.FileName);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string fileContent;
            List<Cliente> list = new();
            Cliente cliente = new();

            using (StreamReader reader = new StreamReader(fullPath))
            {
                fileContent = await reader.ReadLineAsync();

                cliente.Cedula = fileContent.Substring(0, 10).ToString();
                cliente.Nombre = fileContent.Substring(32, 25).ToString();
                cliente.Apellido = fileContent.Substring(64, 25).ToString();
                cliente.Direccion = fileContent.Substring(104, 40).ToString();
                cliente.NumCelular = fileContent.Substring(156, 10).ToString();
                cliente.FechaNacimiento = Convert.ToDateTime(fileContent.Substring(176, 8).ToString());

                list.Add(cliente);
            }


            ingresarUsuario(list);

            if (list != null)
            {
                response.data = list;
                response.message = Message.list;
            }
            else
            {
                response.message = Message.errorList;

            }

            return response;
        }
    

        private /*bool*/ void ingresarUsuario(List<Cliente> clientes)           
        {
            foreach( Cliente cliente in clientes)
            {

            }
        }
    }
}
