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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Transactions;
using System.Globalization;

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
                    SqlCommand command = new SqlCommand("Obtener_Todos_clientes", conn);
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

        public async Task<Response> CrearFactura(int idCliente, int idVehiculo)
        {
            Response response = new();
            Cliente cliente = new();
            Vehiculo vehiculo = new();
            Factura factura = new();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    await conn.OpenAsync();

                    // Obtener datos del cliente
                    SqlCommand cmdCliente = new SqlCommand("Obtener_clientes", conn);
                    cmdCliente.CommandType = CommandType.StoredProcedure;
                    cmdCliente.Parameters.AddWithValue("@id", idCliente);
                    SqlDataReader readerCliente = await cmdCliente.ExecuteReaderAsync();

                    if (await readerCliente.ReadAsync())
                    {
                        cliente = new Cliente
                        {
                            Id = Convert.ToInt32(readerCliente["id"]),
                            Cedula = readerCliente["cedula"].ToString(),
                            Nombre = readerCliente["nombre"].ToString(),
                            Apellido = readerCliente["apellido"].ToString(),
                            Direccion = readerCliente["direccion"].ToString(),
                            NumCelular = readerCliente["num_celular"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(readerCliente["fecha_nacimiento"])
                        };
                    }
                    readerCliente.Close();

                    // Obtener datos del vehículo
                    SqlCommand cmdVehiculo = new SqlCommand("Obtener_Vehiculo", conn);
                    cmdVehiculo.CommandType = CommandType.StoredProcedure;
                    cmdVehiculo.Parameters.AddWithValue("@id", idVehiculo);
                    SqlDataReader readerVehiculo = await cmdVehiculo.ExecuteReaderAsync();

                    if (await readerVehiculo.ReadAsync())
                    {
                        vehiculo = new Vehiculo
                        {
                            Id = Convert.ToInt32(readerVehiculo["id"]),
                            Modelo = readerVehiculo["modelo"].ToString(),
                            Color = readerVehiculo["color"].ToString(),
                            TarifaBase = Convert.ToDecimal(readerVehiculo["tarifaBase"]),
                            Placa = readerVehiculo["placa"].ToString(),
                        };
                    }
                    readerVehiculo.Close();

                    // Crear la factura
                    factura = new Factura
                    {
                        IdCliente = cliente.Id,
                        IdVehiculo = vehiculo.Id,
                        FechaEmision = DateTime.Now,
                        Total = vehiculo.TarifaBase ?? 0,
                        IdClienteNavigation = cliente,
                        IdVehiculoNavigation = vehiculo
                    };

                    // Insertar la factura en la base de datos
                    SqlCommand cmdFactura = new SqlCommand("InsertarFactura", conn);
                    cmdFactura.CommandType = CommandType.StoredProcedure;
                    cmdFactura.Parameters.AddWithValue("@idCliente", factura.IdCliente);
                    cmdFactura.Parameters.AddWithValue("@idVehiculo", factura.IdVehiculo);
                    cmdFactura.Parameters.AddWithValue("@FechaEmision", factura.FechaEmision);
                    cmdFactura.Parameters.AddWithValue("@Total", factura.Total);

                    int rowsAffected = await cmdFactura.ExecuteNonQueryAsync();
                  
                    
                        response.data = factura;
                        response.message = "Factura creada exitosamente";
                   
                   
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    await conn.CloseAsync();
                }
            }

            return response;
        }


        public async Task<Response> UploadFileAsync(IFormFile file)
        {
            Response response = new();
            List<ClienteDTO> list = new();

            try
            {
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    string line;
                    while ((line = await stream.ReadLineAsync()) != null)
                    {
                        // Asumiendo que cada línea del archivo contiene los datos del cliente separados por comas
                        var data = line.Split(',');

                        if (data.Length < 6)
                        {
                            // Handle the case where the line does not have enough data
                            continue;
                        }

                        ClienteDTO cliente = new ClienteDTO
                        {
                            Cedula = data[0],
                            Nombre = data[1],
                            Apellido = data[2],
                            Direccion = data[3],
                            NumCelular = data[4],
                            // FechaNacimiento = DateTime.Parse(data[5])
                            //FechaNacimiento = Convert.ToDateTime(data[5])
                            FechaNacimiento = DateTime.TryParseExact(data[5].Trim(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha) ? fecha : DateTime.MinValue
                        };

                        list.Add(cliente);
                    }
                }

                bool boolIngresar = ingresarUsuario(list);

                if (boolIngresar)
                {
                    response.data = list;
                    response.message = Message.list;
                }
                else
                {
                    response.message = Message.errorList;
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return response;
        }

        private bool ingresarUsuario(List<ClienteDTO> clientes)
        {
            bool verificar = false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    foreach (ClienteDTO cliente in clientes)
                    {
                        SqlCommand command = new SqlCommand(SPName.ingresarUsuario, conn, transaction);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue(Parameters.cedula, cliente.Cedula);
                        command.Parameters.AddWithValue(Parameters.nombre, cliente.Nombre);
                        command.Parameters.AddWithValue(Parameters.apellido, cliente.Apellido);
                        command.Parameters.AddWithValue(Parameters.direccion, cliente.Direccion);
                        command.Parameters.AddWithValue(Parameters.numCelular, cliente.NumCelular);
                        command.Parameters.AddWithValue(Parameters.fechaNacimiento, cliente.FechaNacimiento);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    verificar = true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return verificar;
        }
    }
}

