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
    public class FacturasRepository : IFacturasRepository
    {
        private readonly string _connectionString;

        public FacturasRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<TblFacturas>> GetFacturasAsync(string tipoBusqueda, string valorBusqueda)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_BuscarFactura", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Parámetros de la consulta
                    command.Parameters.AddWithValue("@TipoBusqueda", tipoBusqueda);
                    command.Parameters.AddWithValue("@ValorBusqueda", valorBusqueda);

                    // Ejecutar el comando y obtener resultados
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var facturas = new List<TblFacturas>();

                        while (await reader.ReadAsync())
                        {
                            var factura = new TblFacturas
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FechaEmisionFactura = reader.GetDateTime(reader.GetOrdinal("FechaEmisionFactura")),
                                NumeroFactura = reader.GetInt32(reader.GetOrdinal("NumeroFactura")),
                                SubTotalFacturas = reader.GetDecimal(reader.GetOrdinal("SubTotalFacturas")),
                                TotalImpuestos = reader.GetDecimal(reader.GetOrdinal("TotalImpuestos")),
                                TotalFactura = reader.GetDecimal(reader.GetOrdinal("TotalFactura"))
                            };

                            facturas.Add(factura);
                        }

                        return facturas;
                    }
                }
            }
        }

        public async Task<int> CreateAsync(TblFacturas factura, List<TblDetallesFactura> detallesFactura, TblClientes cliente, List<CatProductos> productos)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("sp_CreateFactura", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var productosJson = new System.Text.StringBuilder();
                    foreach (var producto in productos)
                    {
                        productosJson.AppendFormat("[{{\"ProductoId\":{0},\"PrecioUnitario\":{1},\"Cantidad\":{2}}}]",
                            producto.Id, producto.PrecioUnitario, detallesFactura.Find(d => d.IdProducto == producto.Id).CantidadDeProducto);
                    }

                    command.Parameters.AddWithValue("@NumeroFactura", factura.NumeroFactura);
                    command.Parameters.AddWithValue("@IdCliente", cliente.Id);
                    command.Parameters.AddWithValue("@Productos", productosJson.ToString());
                    command.Parameters.AddWithValue("@FechaEmision", factura.FechaEmisionFactura);

                    var result = await command.ExecuteScalarAsync();
                    return (int)result;
                }
            }
        }
    }
}
