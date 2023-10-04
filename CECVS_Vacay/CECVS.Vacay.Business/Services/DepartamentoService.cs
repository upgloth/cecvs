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
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IRepository<Departamento> _repository;

        public DepartamentoService(IRepository<Departamento> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Departamento>> GetDepartamentosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Departamento> GetDepartamentoByIdAsync(int id_departamento)
        {
            return await _repository.GetByIdAsync(id_departamento);
        }

        public async Task<Departamento> CreateDepartamentoAsync(string no_departamento)
        {
            var newDepartamento = new Departamento
            {
                NoDepartamento = no_departamento
            };

            _repository.Add(newDepartamento);
            await _repository.SaveAsync();
            return newDepartamento;
        }

        public async Task<bool> UpdateDepartamentoAsync(int id_departamento, string no_departamento)
        {
            var existingDepartamento = await _repository.GetByIdAsync(id_departamento);

            if (existingDepartamento == null)
            {
                return false;
            }

            existingDepartamento.NoDepartamento = no_departamento;

            _repository.Update(existingDepartamento);
            return await _repository.SaveAsync();
        }

        public async Task<bool> DeleteDepartamentoAsync(int id_departamento)
        {
            var existingDepartamento = await _repository.GetByIdAsync(id_departamento);

            if (existingDepartamento == null)
            {
                return false;
            }

            _repository.Delete(existingDepartamento);
            return await _repository.SaveAsync();
        }
    }
}
