using AutoMapper;
using CECVS.Vacay.Api.Contracts.Models;
using CECVS.Vacay.Business.Services;
using CECVS.Vacay.Domain.Interfaces.Services;
using CECVS.Vacay.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CECVS.Vacay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;

        public RelatoriosController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }


        [HttpGet("ferias")]
        public async Task<ActionResult<IEnumerable<FeriasExpandido>>> GetFerias()
        {
            var ferias = await _relatorioService.GetFeriasExpandidaAsync(0);

            return Ok(ferias);
        }

        [HttpGet("ferias/departamento/{idDepartamento}")]
        public async Task<ActionResult<IEnumerable<FeriasExpandido>>> GetFeriasByIdDepartamento(int idDepartamento)
        {
            var ferias = await _relatorioService.GetFeriasExpandidaAsync(idDepartamento);

            return Ok(ferias);
        }
    }
}
