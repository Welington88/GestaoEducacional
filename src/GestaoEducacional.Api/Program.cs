using System.Text.Json.Serialization;
using GestaoEducacional.Api.Configurations;
using GestaoEducacional.Application.Services;
using GestaoEducacional.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//cors
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => {
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

//builder.Logging.AddConfiguration();
// Add services to the container.
builder.Services.AddDbContext<Context>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("ApiConnection"));
});

builder.Services.AddControllers();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDIConfiguration();

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>{
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gestao Educacional", Version = "v1" });
        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<AlunoService>("/servicealuno");
app.MapHub<CursoService>("/servicecurso");
app.MapHub<DisciplinaService>("/servicedisciplina");
app.MapHub<NotaService>("/servicenota");
app.MapHub<ProfessorService>("/serviceprofessor");

app.UseSwagger();

app.UseSwaggerUI();

app.UseRouting();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();