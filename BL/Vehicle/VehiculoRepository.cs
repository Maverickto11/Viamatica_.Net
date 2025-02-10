using BE_Models.DTO;
using BE_Models.Models;
using BE_Models.Response;
using BL.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BL.Vehicle
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly string _pruebaContext;
        public VehiculoRepository(IConfiguration configuration)
        {
            _pruebaContext = configuration.GetConnectionString("Connection");

        }

        public Response AddVehiculo(VehiculoDTO vehiculo)
        {
            Response response = new Response();

            using (SqlConnection conn = new SqlConnection(_pruebaContext))
            {
                try
                {
                    if (validateEmptyField(vehiculo))
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(SPName.Add_Vehiculo, conn);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                        command.Parameters.AddWithValue("@color", vehiculo.Color);
                        command.Parameters.AddWithValue("@tarifaBase", vehiculo.TarifaBase);
                        command.Parameters.AddWithValue("@placa", vehiculo.Placa);
                        command.Parameters.AddWithValue("@estado", vehiculo.Estado);

                        int rowAffect = command.ExecuteNonQuery();

                        if (rowAffect > 0)
                        {
                            response.data = vehiculo;
                            response.message = "Registro exitoso";
                            return response;
                        }
                        else
                        {
                            response.message = "Registro fallido";
                            return response;
                        }
                    }
                    else
                    {
                        response.message = "Hay campos vacios";
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return response;
        }

        private bool validateEmptyField(VehiculoDTO vehiculo)
        {
            bool verificar = false;

            if (!string.IsNullOrWhiteSpace(vehiculo.Modelo) &&
                !string.IsNullOrWhiteSpace(vehiculo.Color) &&
                vehiculo.TarifaBase > 0 &&
                !string.IsNullOrWhiteSpace(vehiculo.Placa)
                )
            {

                verificar = true;

            }
            return verificar;
        }

        public async Task<Response> GetVehiculo()
        {
            Response response = new();
            List<VehiculoDTO> list = new();

            using (SqlConnection conn = new(_pruebaContext))
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand cmd = new("SELECT * FROM Vehiculo", conn);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        VehiculoDTO vehiculoDTO = new VehiculoDTO
                        {
                            //Id = Convert.ToInt32(reader["Id"])
                            Id = reader.GetInt32(0),
                            Modelo = reader.GetString(1),
                            Color = reader.GetString(2),
                            TarifaBase = reader.GetDecimal(3),
                            Placa = reader.GetString(4),
                            Estado = reader.GetBoolean(5)
                        };
                        list.Add(vehiculoDTO);
                    }
                    response.data = list;
                    response.message = "Datos obtenidos";
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

        public async Task<Response> GetVehiculobyPlaca(string placa)
        {
            Response response = new();
            VehiculoDTO vehiculoDTO = new();
            using (SqlConnection conn = new(_pruebaContext))
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = new("GetVehiculobyPlaca", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Parameters.placa, placa);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        vehiculoDTO = new VehiculoDTO
                        {
                            Id = Convert.ToInt32(reader[tablesName.columId]),
                            Modelo = reader[tablesName.columModelo].ToString(),
                            Color = reader[tablesName.columColor].ToString(),
                            TarifaBase = Convert.ToDecimal(reader[tablesName.columTarifaBase]),
                            Placa = reader[tablesName.columPlaca].ToString(),
                            Estado = Convert.ToBoolean(reader[tablesName.columEstado])

                        };
                    }
                    if (vehiculoDTO == null)
                    {
                        response.data = vehiculoDTO;
                        response.message = Message.foundNotData;
                        return response;
                    }
                    response.data = vehiculoDTO;
                    response.message = Message.foundData;
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }

        public async Task<Response> UpdateVehiculo(VehiculoDTO vehiculo)
        {
            Response response = new();
       
            using (SqlConnection conn = new(_pruebaContext))
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = new("UpdateVehiculo", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", vehiculo.Id);
                    command.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                    command.Parameters.AddWithValue("@color", vehiculo.Color);
                    command.Parameters.AddWithValue("@tarifaBase", vehiculo.TarifaBase);
                    command.Parameters.AddWithValue("@placa", vehiculo.Placa);
                    command.Parameters.AddWithValue("@estado", vehiculo.Estado);

                    int row = await command.ExecuteNonQueryAsync();

                    if (row > 0)
                    {
                        response.data = vehiculo;
                        response.message = Message.UpdateVehiculo;
                    }
                    else
                    {
                        response.message = Message.UpdateNotVehiculo;
                    }
                }
                catch
                {

                }

            }return response;
        }

        public async Task<Response> DeleteVehiculo(int id)
        {
            Response response = new();
            
            
            using(SqlConnection conn = new(_pruebaContext))
            {
                try
                {

                    await conn.OpenAsync();
                    SqlCommand command = new("DeleteVehiculo", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);


                    int num = await command.ExecuteNonQueryAsync();
                    if (num > 0)
                    {
                        response.message = Message.DeleteVehiculo;
                    }
                    else
                    {
                        response.message = Message.UpdateNotVehiculo;
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }

               
            }return response;
            
        }

      
    }
}
