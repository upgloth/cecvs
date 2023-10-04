using CECVS.Vacay.Api.AutoMapper;
using CECVS.Vacay.Business.Services;
using CECVS.Vacay.Data;
using CECVS.Vacay.Data.Repositories;
using CECVS.Vacay.Domain.Interfaces.Data;
using CECVS.Vacay.Domain.Interfaces.Services;
using CECVS.Vacay.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Configuration;

namespace CECVS.Vacay.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("VacayDB");

            // Add services to the container.
            //builder.Services.AddDbContext<VacayDBContext>();
            builder.Services.AddDbContext<VacayDBContext>((options) =>
            {
                if (!options.IsConfigured)
                    options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<IVacayDBContext, VacayDBContext>();

            builder.Services.AddScoped<IRepository<Funcionario>, Repository<Funcionario>>();
            builder.Services.AddScoped<IRepository<Departamento>, Repository<Departamento>>();
            builder.Services.AddScoped<IRepository<Ferias>, Repository<Ferias>>();
            builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();

            builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
            builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
            builder.Services.AddScoped<IFeriasService, FeriasService>();
            builder.Services.AddScoped<IRelatorioService, RelatorioService>();

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}