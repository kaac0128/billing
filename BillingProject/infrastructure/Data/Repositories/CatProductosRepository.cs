using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace infrastructure.Data.Repositories
{
    public class CatProductosRepository : ICatProductosRepository
    {
        private readonly string _connectionString;

        public CatProductosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<CatProductos>> GetAllAsync()
        {
            var productos = new List<CatProductos>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_ObtenerProductos", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            productos.Add(new CatProductos
                            {
                                Id = (int)reader["Id"],
                                NombreProducto = reader["NombreProducto"].ToString(),
                                PrecioUnitario = (decimal)reader["PrecioUnitario"],
                                Ext = reader["Ext"] as string,
                                ImagenProducto = reader["ImagenProducto"] as byte[]
                            });
                        }
                    }
                }
            }
            return productos;
        }

        public async Task<int> CreateAsync(CatProductos producto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_InsertarProducto", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                    command.Parameters.AddWithValue("@PrecioUnitario", producto.PrecioUnitario);
                    command.Parameters.AddWithValue("@Ext", (object)producto.Ext ?? DBNull.Value);

                    // Retornar el ID del producto insertado
                    var result = await command.ExecuteScalarAsync();
                    return (int)result;
                }
            }
        }

        public async Task<bool> UpdateAsync(CatProductos producto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_ActualizarProducto", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", producto.Id);
                    command.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                    command.Parameters.AddWithValue("@PrecioUnitario", producto.PrecioUnitario);
                    command.Parameters.AddWithValue("@Ext", (object)producto.Ext ?? DBNull.Value);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_EliminarProducto", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}
