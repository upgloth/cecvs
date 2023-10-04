using CECVS.Vacay.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CECVS.Vacay.Data
{
    public interface IVacayDBContext
    {
        DbSet<Departamento> Departamentos { get; set; }
        DbSet<Ferias> Ferias { get; set; }
        DbSet<Funcionario> Funcionarios { get; set; }

        Task<int> SaveChangesAsync();

        DbSet<T> Set<T>() where T : class;
    }
}