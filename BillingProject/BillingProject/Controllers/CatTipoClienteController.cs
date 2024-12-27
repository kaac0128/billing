using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BillingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatTipoClienteController : ControllerBase
    {
        private readonly ICatTipoClienteService _catTipoClienteService;

        public CatTipoClienteController(ICatTipoClienteService catTipoClienteService)
        {
            _catTipoClienteService = catTipoClienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatTipoClienteDTO>>> GetAllAsync()
        {
            var catTipoClientes = await _catTipoClienteService.GetAllAsync();
            return Ok(catTipoClientes);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync([FromBody] CatTipoClienteDTO catTipoCliente)
        {
            var catTipoClienteId = await _catTipoClienteService.CreateAsync(catTipoCliente);
            return CreatedAtAction(nameof(GetAllAsync), new { id = catTipoClienteId }, catTipoClienteId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CatTipoClienteDTO catTipoCliente)
        {
            var success = await _catTipoClienteService.UpdateAsync(catTipoCliente);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var success = await _catTipoClienteService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
