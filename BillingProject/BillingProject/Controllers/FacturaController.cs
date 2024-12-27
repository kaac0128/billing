using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BillingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturasService _facturasService;

        public FacturaController(IFacturasService facturasService)
        {
            _facturasService = facturasService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblFacturas>>> GetFacturasAsync([FromQuery] string tipoBusqueda, [FromQuery] string valorBusqueda)
        {
            if (string.IsNullOrEmpty(tipoBusqueda) || string.IsNullOrEmpty(valorBusqueda))
            {
                return BadRequest("Tipo de búsqueda y valor de búsqueda son requeridos.");
            }

            var facturas = await _facturasService.GetFacturasAsync(tipoBusqueda, valorBusqueda);

            if (facturas == null)
            {
                return NotFound("No se encontraron facturas.");
            }

            return Ok(facturas);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync([FromBody] FacturaRequest factura)
        {
            var facturaId = await _facturasService.CreateAsync(factura.Factura, factura.DetallesFactura, factura.Cliente, factura.Productos);
            return facturaId;
        }
    }
}
