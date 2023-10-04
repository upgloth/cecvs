using CECVS.Vacay.Domain.Interfaces.Data;
using CECVS.Vacay.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Data.Repositories
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly IVacayDBContext _context;

        public RelatorioRepository(IVacayDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeriasExpandido>> GetFeriasExpandidaAsync(int idDepartamento)
        {
            var feriasExpandida = await _context.Set<Ferias>()
                .Where(v => (idDepartamento == 0 || v.IdFuncionarioNavigation.IdDepartamento == idDepartamento))
                .Select(v => new FeriasExpandido
                {
                    IdFerias = v.IdFerias,
                    IdFuncionario = v.IdFuncionario,
                    CoMatricula = v.IdFuncionarioNavigation.CoMatricula,
                    NoFuncionario = v.IdFuncionarioNavigation.NoFuncionario,
                    DtInicio = v.DtInicio,
                    QtDias = v.QtDias
                }).ToListAsync<FeriasExpandido>();

            return feriasExpandida;
        }
    }
}
