using AutoMapper;
using CECVS.Vacay.Api.Contracts.Models;
using CECVS.Vacay.Business.Exceptions;
using CECVS.Vacay.Domain.Interfaces.Services;
using CECVS.Vacay.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CECVS.Vacay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeriasController : ControllerBase
    {
        private readonly IFeriasService _feriasService;
        private readonly IMapper _mapper;

        public FeriasController(IFeriasService feriasService, IMapper mapper)
        {
            _feriasService = feriasService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeriasDTO>>> GetFerias()
        {
            var ferias = await _feriasService.GetFeriasAsync();

            var _fers = _mapper.Map<IEnumerable<FeriasDTO>>(ferias);

            return Ok(_fers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FeriasDTO>> GetFerias(int id)
        {
            var ferias = await _feriasService.GetFeriasByIdAsync(id);
            if (ferias == null)
            {
                return NotFound();
            }

            var _fer = _mapper.Map<FeriasDTO>(ferias);

            return Ok(_fer);
        }

        [HttpGet("departamento/{idDepartamento}")]
        public async Task<ActionResult<IEnumerable<FeriasDTO>>> GetFeriasByIdDepartamento(int idDepartamento)
        {
            var ferias = await _feriasService.GetFeriasByIdDepartamentoAsync(idDepartamento);
            if (ferias == null || !ferias.Any())
            {
                return NotFound();
            }

            var _fer = _mapper.Map<IEnumerable<FeriasDTO>>(ferias);

            return Ok(_fer);
        }

        [HttpPost]
        public async Task<ActionResult<FeriasDTO>> CreateFerias([FromBody] FeriasDTO feriasDTO)
        {
            try
            {
                // Valida o modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var ferias = await _feriasService.CreateFeriasAsync(feriasDTO.IdFuncionario, feriasDTO.DtInicio, feriasDTO.QtDias);

                if (ferias == null)
                {
                    return BadRequest();
                }

                var _fer = _mapper.Map<FeriasDTO>(ferias);

                return CreatedAtAction(nameof(GetFerias), new { id = _fer.IdFerias }, _fer);
            }
            catch (LimiteExcedidoException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FeriasDTO>> UpdateFerias(int id, [FromBody] UpdateFeriasDTO updateFeriasDTO)
        {
            try
            {
                // Valida o modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != updateFeriasDTO.IdFerias)
                {
                    return BadRequest();
                }

                var success = await _feriasService.UpdateFeriasAsync(id, updateFeriasDTO.DtInicio, updateFeriasDTO.QtDias);

                if (!success)
                {
                    return NotFound();
                }

                var updatedFerias = await _feriasService.GetFeriasByIdAsync(id);

                var _fer = _mapper.Map<FeriasDTO>(updatedFerias);

                return Ok(_fer);
            }
            catch (LimiteExcedidoException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFerias(int id)
        {
            var success = await _feriasService.DeleteFeriasAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}