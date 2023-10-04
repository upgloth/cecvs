using CECVS.Vacay.Domain.Interfaces.Data;
using CECVS.Vacay.Domain.Interfaces.Services;
using CECVS.Vacay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Business.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IRelatorioRepository _relatorioRepository;

        public RelatorioService(IRelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
        }

        public async Task<IEnumerable<FeriasExpandido>> GetFeriasExpandidaAsync(int idDepartamento)
        {
            var expandedVacations = await _relatorioRepository.GetFeriasExpandidaAsync(idDepartamento);

            return expandedVacations;
        }
    }
}
