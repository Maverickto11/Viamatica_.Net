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
                    throw new Exception (ex.Message); 
                }
            }
            return response;   
        }
    }
}
