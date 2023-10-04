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
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IRepository<Funcionario> _funcionarioRepository;

        public FuncionarioService(IRepository<Funcionario> repository)
        {
            _funcionarioRepository = repository;
        }

        public async Task<IEnumerable<Funcionario>> GetFuncionariosAsync()
        {
            return await _funcionarioRepository.GetAllAsync();
        }

        public async Task<Funcionario> GetFuncionarioByIdAsync(int id_funcionario)
        {
            return await _funcionarioRepository.GetByIdAsync(id_funcionario);
        }

        public async Task<Funcionario> CreateFuncionarioAsync(string co_matricula, string no_funcionario, DateTime dt_admissao, int id_departamento)
        {
            // TODO: Verificar se departamento existe


            var newFuncionario = new Funcionario()
            {
                CoMatricula = co_matricula,
                NoFuncionario = no_funcionario,
                DtAdmissao = dt_admissao,
                IdDepartamento = id_departamento
            };

            _funcionarioRepository.Add(newFuncionario);
            await _funcionarioRepository.SaveAsync();
            return newFuncionario;
        }

        public async Task<bool> UpdateFuncionarioAsync(int id_funcionario, string co_matricula, string no_funcionario, DateTime dt_admissao, int id_departamento)
        {
            var existingFuncionario = await _funcionarioRepository.GetByIdAsync(id_funcionario);

            if (existingFuncionario == null)
            {
                return false; // Employee not found
            }

            existingFuncionario.CoMatricula = co_matricula;
            existingFuncionario.NoFuncionario = no_funcionario;
            existingFuncionario.DtAdmissao = dt_admissao;
            existingFuncionario.IdDepartamento = id_departamento;

            _funcionarioRepository.Update(existingFuncionario);
            return await _funcionarioRepository.SaveAsync();
        }

        public async Task<bool> DeleteFuncionarioAsync(int id_funcionario)
        {
            var existingFuncionario = await _funcionarioRepository.GetByIdAsync(id_funcionario);

            if (existingFuncionario == null)
            {
                return false; // Employee not found
            }

            _funcionarioRepository.Delete(existingFuncionario);
            return await _funcionarioRepository.SaveAsync();
        }
    }
}
