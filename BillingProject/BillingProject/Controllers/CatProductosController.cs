using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BillingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatProductosController : ControllerBase
    {
        private readonly ICatProductosService _catProductosService;

        public CatProductosController(ICatProductosService catProductosService)
        {
            _catProductosService = catProductosService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatProductosDTO>>> GetAllAsync()
        {
            var productos = await _catProductosService.GetAllAsync();
            return Ok(productos);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync([FromBody] CatProductosDTO producto)
        {
            var productoId = await _catProductosService.CreateAsync(producto);
            return CreatedAtAction(nameof(GetAllAsync), new { id = productoId }, productoId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CatProductosDTO producto)
        {
            var success = await _catProductosService.UpdateAsync(producto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var success = await _catProductosService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
