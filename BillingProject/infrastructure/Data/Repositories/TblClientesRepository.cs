using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TblClientesRepository : ITblClientesRepository
    {
        private readonly string _connectionString;

        public TblClientesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<TblClientes>> GetAllAsync()
        {
            var clientes = new List<TblClientes>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_ObtenerClientes", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            clientes.Add(new TblClientes
                            {
                                Id = (int)reader["Id"],
                                RazonSocial = reader["RazonSocial"].ToString(),
                                IdTipoCliente = (int)reader["IdTipoCliente"],
                                FechaCreacion = (DateTime)reader["FechaCreacion"],
                                RFC = reader["RFC"].ToString()
                            });
                        }
                    }
                }
            }
            return clientes;
        }

        public async Task<int> CreateAsync(TblClientes cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_InsertarCliente", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RazonSocial", cliente.RazonSocial);
                    command.Parameters.AddWithValue("@IdTipoCliente", cliente.IdTipoCliente);
                    command.Parameters.AddWithValue("@FechaCreacion", cliente.FechaCreacion);
                    command.Parameters.AddWithValue("@RFC", cliente.RFC);

                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0 ? 1 : 0;
                }
            }
        }

        public async Task<bool> UpdateAsync(TblClientes cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_ActualizarCliente", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", cliente.Id);
                    command.Parameters.AddWithValue("@RazonSocial", cliente.RazonSocial);
                    command.Parameters.AddWithValue("@IdTipoCliente", cliente.IdTipoCliente);
                    command.Parameters.AddWithValue("@FechaCreacion", cliente.FechaCreacion);
                    command.Parameters.AddWithValue("@RFC", cliente.RFC);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_EliminarCliente", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}
