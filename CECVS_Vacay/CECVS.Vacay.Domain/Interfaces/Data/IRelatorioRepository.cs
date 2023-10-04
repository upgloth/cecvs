using CECVS.Vacay.Domain.Models;

namespace CECVS.Vacay.Domain.Interfaces.Data
{
    public interface IRelatorioRepository
    {
        Task<IEnumerable<FeriasExpandido>> GetFeriasExpandidaAsync(int idDepartamento);
    }
}