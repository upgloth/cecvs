using AutoMapper;
using CECVS.Vacay.Api.Contracts.Models;
using CECVS.Vacay.Domain.Models;

namespace CECVS.Vacay.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Departamento, DepartamentoDTO>();
            CreateMap<Funcionario, FuncionarioDTO>();
            CreateMap<Ferias, FeriasDTO>();
        }
    }
}
