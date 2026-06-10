using ControleDeMedicamentos.WebApp.Compartilhado.Aplicacao;
using ControleDeMedicamentos.WebApp.Compartilhado.Apresentacao;
using ControleDeMedicamentos.WebApp.Compartilhado.Infra;


var builder = WebApplication.CreateBuilder(args);

// feito para facilitar a implementação dos demais módulos com uma base de injeções e compartilhamento já criada.

// registra os repositórios da camada de Infra.
// conforme os módulos forem criados, descomentar os registros dentro de:
// compartilhado/Infra/InjecaoDependencia.cs
builder.Services.AddInfraRepositories();

// registra os serviços da camada de Aplicação.
// conforme os módulos forem criados, descomente os registros dentro de:
// compartilhado/Aplicacao/InjecaoDependencia.cs
builder.Services.AddApplicationServices();

// registra Controllers, Razor Views e AutoMapper.
// conforme os módulos forem criados, descomente os Profiles dentro de:
// compartilhado/Apresentacao/InjecaoDependencia.cs
builder.Services.AddPresentation();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.MapDefaultControllerRoute();

app.Run();