using CECVS.Vacay.Business.Exceptions;
using CECVS.Vacay.Domain.Interfaces.Data;
using CECVS.Vacay.Domain.Interfaces.Services;
using CECVS.Vacay.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CECVS.Vacay.Business.Services
{
    public class FeriasService : IFeriasService
    {
        private readonly IRepository<Ferias> _feriasRepository;
        private readonly IRepository<Funcionario> _funcionarioRepository;

        public FeriasService(IRepository<Ferias> feriasRepository, IRepository<Funcionario> funcionarioRepository)
        {
            _feriasRepository = feriasRepository;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<IEnumerable<Ferias>> GetFeriasAsync()
        {
            return await _feriasRepository.GetAllAsync();
        }

        public async Task<Ferias> GetFeriasByIdAsync(int id_ferias)
        {
            return await _feriasRepository.GetByIdAsync(id_ferias);
        }

        public async Task<Ferias> CreateFeriasAsync(int id_funcionario, DateTime dt_inicio, int qt_dias)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id_funcionario);

            if (funcionario == null)
            {
                return null;
            }

            var respeita30Porcento = await this.Respeita30PorcentoDepartamento(funcionario.IdDepartamento, dt_inicio, qt_dias)
                && await this.Respeita30PorcentoUnidade(dt_inicio, qt_dias);

            if (!respeita30Porcento)
            {
                //return null;
                throw new LimiteExcedidoException("Percentual máximo de funcionários em férias ao mesmo tempo excedido.");
            }

            var newFerias = new Ferias
            {
                IdFuncionario = id_funcionario,
                DtInicio = dt_inicio,
                QtDias = qt_dias,
            };

            _feriasRepository.Add(newFerias);
            await _feriasRepository.SaveAsync();
            return newFerias;
        }

        public async Task<bool> UpdateFeriasAsync(int id_ferias, DateTime dt_inicio, int qt_dias)
        {
            var existingFerias = await _feriasRepository.GetByIdAsync(id_ferias);

            if (existingFerias == null)
            {
                return false;
            }

            var funcionario = await _funcionarioRepository.GetByIdAsync(existingFerias.IdFuncionario);

            var respeita30Porcento = await this.Respeita30PorcentoDepartamentoUpdate(funcionario.IdDepartamento, dt_inicio, qt_dias, id_ferias)
                && await this.Respeita30PorcentoUnidadeUpdate(dt_inicio, qt_dias, id_ferias);

            if (!respeita30Porcento)
            {
                //return false;
                throw new LimiteExcedidoException("Percentual máximo de funcionários em férias ao mesmo tempo excedido.");
            }

            existingFerias.DtInicio = dt_inicio;
            existingFerias.QtDias = qt_dias;

            _feriasRepository.Update(existingFerias);
            return await _feriasRepository.SaveAsync();
        }

        public async Task<bool> DeleteFeriasAsync(int id_ferias)
        {
            var existingFerias = await _feriasRepository.GetByIdAsync(id_ferias);

            if (existingFerias == null)
            {
                return false;
            }

            _feriasRepository.Delete(existingFerias);
            return await _feriasRepository.SaveAsync();
        }

        //---------------------------------------------------------------------------------------------------------

        public async Task<IEnumerable<Ferias>> GetFeriasByIdFuncionarioAsync(int id_funcionario)
        {
            return await _feriasRepository.GetAllAsync(v => v.IdFuncionario == id_funcionario);
        }

        public async Task<IEnumerable<Ferias>> GetFeriasByIdDepartamentoAsync(int id_departamento)
        {
            return await _feriasRepository.GetAllAsync(v => v.IdFuncionarioNavigation.IdDepartamento == id_departamento);
        }

        //---------------------------------------------------------------------------------------------------------
        private async Task<bool> Respeita30Porcento(DateTime dt_nicio, int qt_dias, int id_departamento = 0, int id_ferias = 0)
        {
            // 30%
            // Criar hash com datas de 30 dias para tras e 30 para frente
            // Pegar todas as ferias que caiam no periodo de -30 e +30 (outros inicio+qt >= meu inicio || outro inicio <= meu inicio+qt
            // Marcar todos os dias com gente de ferias
            // Pegar o maior valor do hashset
            // Se for maior do que 30% do numero de funcionarios é proibido            

            var dtRangeInicio = dt_nicio.AddDays(-30);
            var dtRangeFim = dt_nicio.AddDays(30);

            // Pega todas as ferias do departamento nos ultimos 30 e proximos 30 dias
            var _feriasList = await this._feriasRepository.GetAllAsync(x =>
                        (id_departamento == 0 || x.IdFuncionarioNavigation.IdDepartamento == id_departamento)
                        && (id_ferias == 0 || x.IdFerias != id_ferias)
                        && x.DtInicio >= dtRangeInicio
                        && x.DtInicio <= dtRangeFim);

            var numeroFuncionarios = await this._funcionarioRepository.CountAsync(x => (id_departamento == 0 || x.IdDepartamento == id_departamento));

            var feriasList = _feriasList.ToList();

            // Departamento com 1-3 funcionarios vai gerar valor acima de 30% já na primeira, então não considera na conta a nova ferias
            if (numeroFuncionarios > 3)
            {
                feriasList.Add(new Ferias()
                {
                    DtInicio = dt_nicio,
                    QtDias = qt_dias
                });
            }

            // Dias em ferias
            var ocorrenciasTotais = GetOcorrencias(feriasList);

            // Considera apenas os dias em conflito
            var ocorrencias = ocorrenciasTotais.Where(x => x.Key >= dt_nicio && x.Key <= dt_nicio.AddDays(qt_dias - 1));

            // Cada unidade representa um funcionario em ferias no dia
            var maiorValor = ocorrencias.Any() ? ocorrencias.MaxBy(x => x.Value).Value : 0;

            return ((maiorValor * 100) / numeroFuncionarios) < 30;
        }

        private async Task<bool> Respeita30PorcentoDepartamento(int id_departamento, DateTime dt_nicio, int qt_dias)
        {
            return await Respeita30Porcento(dt_nicio, qt_dias, id_departamento);
        }

        private async Task<bool> Respeita30PorcentoUnidade(DateTime dt_nicio, int qt_dias)
        {
            return await Respeita30Porcento(dt_nicio, qt_dias);
        }

        private async Task<bool> Respeita30PorcentoDepartamentoUpdate(int id_departamento, DateTime dt_nicio, int qt_dias, int id_ferias)
        {
            return await Respeita30Porcento(dt_nicio, qt_dias, id_departamento, id_ferias);
        }

        private async Task<bool> Respeita30PorcentoUnidadeUpdate(DateTime dt_nicio, int qt_dias, int id_ferias)
        {
            return await Respeita30Porcento(dt_nicio, qt_dias, 0, id_ferias);
        }

        private static Dictionary<DateTime, int> GetOcorrencias(List<Ferias> feriasList)
        {
            Dictionary<DateTime, int> ocorrencias = new Dictionary<DateTime, int>();

            if (feriasList?.Any() == true)
            {
                foreach (var ferias in feriasList)
                {
                    var dataFim = ferias.DtInicio.AddDays(ferias.QtDias - 1);
                    for (DateTime date = ferias.DtInicio; date <= dataFim; date = date.AddDays(1))
                    {
                        if (ocorrencias.ContainsKey(date))
                        {
                            ocorrencias[date]++;
                        }
                        else
                        {
                            ocorrencias[date] = 1;
                        }
                    }
                }
            }

            return ocorrencias;
        }
    }
}
