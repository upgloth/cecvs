using AutoMapper;
using CECVS.Vacay.Api.Contracts.Models;
using CECVS.Vacay.Domain.Interfaces.Services;
using CECVS.Vacay.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CECVS.Vacay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;
        private readonly IMapper _mapper;

        public FuncionariosController(IFuncionarioService funcionarioService, IMapper mapper)
        {
            _funcionarioService = funcionarioService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuncionarioDTO>>> GetFuncionarios()
        {
            var funcionarios = await _funcionarioService.GetFuncionariosAsync();

            var _funs = _mapper.Map<IEnumerable<FuncionarioDTO>>(funcionarios);

            return Ok(_funs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FuncionarioDTO>> GetFuncionario(int id)
        {
            var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            var _fun = _mapper.Map<FuncionarioDTO>(funcionario);

            return Ok(_fun);
        }

        [HttpPost]
        public async Task<ActionResult<FuncionarioDTO>> CreateFuncionario([FromBody] FuncionarioDTO funcionarioDTO)
        {
            // Valida o modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var funcionario = await _funcionarioService.CreateFuncionarioAsync(funcionarioDTO.CoMatricula,
                                                                               funcionarioDTO.NoFuncionario,
                                                                               funcionarioDTO.DtAdmissao,
                                                                               funcionarioDTO.IdDepartamento);
            if (funcionario == null)
            {
                return BadRequest();
            }

            var _fun = _mapper.Map<FuncionarioDTO>(funcionario);

            return CreatedAtAction(nameof(GetFuncionario), new { id = _fun.IdFuncionario }, _fun);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FuncionarioDTO>> UpdateFuncionario(int id, [FromBody] FuncionarioDTO funcionarioDTO)
        {
            // Valida o modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != funcionarioDTO.IdFuncionario)
            {
                return BadRequest();
            }

            var success = await _funcionarioService.UpdateFuncionarioAsync(id, funcionarioDTO.CoMatricula,
                                                                               funcionarioDTO.NoFuncionario,
                                                                               funcionarioDTO.DtAdmissao,
                                                                               funcionarioDTO.IdDepartamento);
            if (!success)
            {
                return NotFound();
            }

            var updatedFuncionario = await _funcionarioService.GetFuncionarioByIdAsync(id);

            var _fun = _mapper.Map<FuncionarioDTO>(updatedFuncionario);

            return Ok(_fun);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFuncionario(int id)
        {
            var success = await _funcionarioService.DeleteFuncionarioAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
