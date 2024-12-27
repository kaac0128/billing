using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Data.Repositories
{
    public class CatTipoClienteRepository : ICatTipoClienteRepository
    {
        private readonly string _connectionString;

        public CatTipoClienteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<CatTipoCliente>> GetAllAsync()
        {
            var tiposCliente = new List<CatTipoCliente>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_ObtenerTipoCliente", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tiposCliente.Add(new CatTipoCliente
                            {
                                Id = (int)reader["Id"],
                                TipoCliente = reader["TipoCliente"].ToString()
                            });
                        }
                    }
                }
            }
            return tiposCliente;
        }

        public async Task<int> CreateAsync(CatTipoCliente tipoCliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_InsertarTipoCliente", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoCliente", tipoCliente.TipoCliente);

                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0 ? 1 : 0;
                }
            }
        }

        public async Task<bool> UpdateAsync(CatTipoCliente tipoCliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_ActualizarTipoCliente", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", tipoCliente.Id);
                    command.Parameters.AddWithValue("@TipoCliente", tipoCliente.TipoCliente);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_EliminarTipoCliente", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}
