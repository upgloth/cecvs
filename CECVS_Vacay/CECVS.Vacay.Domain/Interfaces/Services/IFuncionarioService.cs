using CECVS.Vacay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Domain.Interfaces.Services
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<Funcionario>> GetFuncionariosAsync();
        Task<Funcionario> GetFuncionarioByIdAsync(int id_funcionario);
        Task<Funcionario> CreateFuncionarioAsync(string co_matricula, string no_funcionario, DateTime dt_admissao, int id_departamento);
        Task<bool> UpdateFuncionarioAsync(int id_funcionario, string co_matricula, string no_funcionario, DateTime dt_admissao, int id_departamento);
        Task<bool> DeleteFuncionarioAsync(int id_funcionario);
    }
}
