using CECVS.Vacay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Domain.Interfaces.Services
{
    public interface IFeriasService
    {
        Task<IEnumerable<Ferias>> GetFeriasAsync();
        Task<Ferias> GetFeriasByIdAsync(int id_ferias);
        Task<Ferias> CreateFeriasAsync(int id_funcionario, DateTime dt_inicio, int qt_dias);
        Task<bool> UpdateFeriasAsync(int id_ferias, DateTime dt_inicio, int qt_dias);
        Task<bool> DeleteFeriasAsync(int id_ferias);

        Task<IEnumerable<Ferias>> GetFeriasByIdFuncionarioAsync(int id_funcionario);

        Task<IEnumerable<Ferias>> GetFeriasByIdDepartamentoAsync(int id_departamento);
    }
}
