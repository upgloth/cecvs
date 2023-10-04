using CECVS.Vacay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Domain.Interfaces.Services
{
    public interface IDepartamentoService
    {
        Task<IEnumerable<Departamento>> GetDepartamentosAsync();
        Task<Departamento> GetDepartamentoByIdAsync(int id_departamento);
        Task<Departamento> CreateDepartamentoAsync(string no_departamento);
        Task<bool> UpdateDepartamentoAsync(int id_departamento, string no_departamento);
        Task<bool> DeleteDepartamentoAsync(int id_departamento);
    }
}
