using CECVS.Vacay.Domain.Models;

namespace CECVS.Vacay.Domain.Interfaces.Services
{
    public interface IRelatorioService
    {
        Task<IEnumerable<FeriasExpandido>> GetFeriasExpandidaAsync(int idDepartamento);
    }
}