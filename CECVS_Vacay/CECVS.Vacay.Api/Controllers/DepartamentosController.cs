using AutoMapper;
using CECVS.Vacay.Api.Contracts.Models;
using CECVS.Vacay.Domain.Interfaces.Services;
using CECVS.Vacay.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CECVS.Vacay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentosController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;

        private readonly IMapper _mapper;

        public DepartamentosController(IDepartamentoService departmentService, IMapper mapper)
        {
            _departamentoService = departmentService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartamentoDTO>>> GetDepartamentos()
        {
            var departamentos = await _departamentoService.GetDepartamentosAsync();

            var _deps = _mapper.Map<IEnumerable<DepartamentoDTO>>(departamentos);

            return Ok(_deps);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartamentoDTO>> GetDepartamento(int id)
        {
            var departamento = await _departamentoService.GetDepartamentoByIdAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }

            var _dep = _mapper.Map<DepartamentoDTO>(departamento);

            return Ok(_dep);
        }

        [HttpPost]
        public async Task<ActionResult<DepartamentoDTO>> CreateDepartamento([FromBody] DepartamentoDTO departamentoDTO)
        {
            // Valida o modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departamento = await _departamentoService.CreateDepartamentoAsync(departamentoDTO.NoDepartamento);

            if (departamento == null)
            {
                return BadRequest();
            }

            var _dep = _mapper.Map<DepartamentoDTO>(departamento);

            return CreatedAtAction(nameof(GetDepartamento), new { id = _dep.IdDepartamento }, _dep);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DepartamentoDTO>> UpdateDepartamento(int id, [FromBody] DepartamentoDTO departamentoDTO)
        {
            // Valida o modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departamentoDTO.IdDepartamento)
            {
                return BadRequest();
            }

            var success = await _departamentoService.UpdateDepartamentoAsync(id, departamentoDTO.NoDepartamento);

            if (!success)
            {
                return NotFound();
            }

            var updatedDepartamento = await _departamentoService.GetDepartamentoByIdAsync(id);

            var _dep = _mapper.Map<DepartamentoDTO>(updatedDepartamento);

            return Ok(_dep);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            var success = await _departamentoService.DeleteDepartamentoAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
