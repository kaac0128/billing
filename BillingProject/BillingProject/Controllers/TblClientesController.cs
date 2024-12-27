using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BillingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblClientesController : ControllerBase
    {
        private readonly ITblClientesService _tblClientesService;

        public TblClientesController(ITblClientesService tblClientesService)
        {
            _tblClientesService = tblClientesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblClientesDTO>>> GetAllAsync()
        {
            var clientes = await _tblClientesService.GetAllAsync();
            return Ok(clientes);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync([FromBody] TblClientesDTO cliente)
        {
            var clienteId = await _tblClientesService.CreateAsync(cliente);
            return CreatedAtAction(nameof(GetAllAsync), new { id = clienteId }, clienteId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TblClientesDTO cliente)
        {
            var success = await _tblClientesService.UpdateAsync(cliente);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var success = await _tblClientesService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}