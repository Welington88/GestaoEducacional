using GestaoEducacional.Application.Interfaces;
using GestaoEducacional.Application.ServiceManagement;
using GestaoEducacional.Application.Services;
using GestaoEducacional.Data.Contexts;
using GestaoEducacional.Data.Repositories;
using GestaoEducacional.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoEducacional.CC.Ioc;
#nullable disable
public class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services)
    {
        // ASPNET
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddScoped<IUrlHelper>(factory =>
        {
            var actionContext = factory.GetService<IActionContextAccessor>()
                                       .ActionContext;
            return new UrlHelper(actionContext);
        });

        //Injeção dos serviços
        services.AddScoped<IAlunoService, AlunoService>();
        services.AddScoped<ICursoService, CursoService>();
        services.AddScoped<IDisciplinaService, DisciplinaService>();
        services.AddScoped<INotaService, NotaService>();
        services.AddScoped<IProfessorService, ProfessorService>();

        //Injeção dos repositórios
        services.AddScoped<IAlunoRepository, AlunoRepository>();
        services.AddScoped<ICursoRepository, CursoRepository>();
        services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();
        services.AddScoped<INotaRepository, NotaRepository>();
        services.AddScoped<IProfessorRepository, ProfessorRepository>();

        //Injeção do contexto
        services.AddScoped<Context>();
        services.AddDbContext<Context>();

        //Injeção do controle de Serviços
        services.AddSingleton<AsyncService>();
    }
}