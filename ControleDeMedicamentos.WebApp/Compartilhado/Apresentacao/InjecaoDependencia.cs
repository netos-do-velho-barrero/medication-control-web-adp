using ControleDeMedicamentos.WebApp.ModuloFornecedor.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Apresentacao;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Apresentacao;
// using ControleDeMedicamentos.WebApp.ModuloMedicamentos.Apresentacao;
// using ControleDeMedicamentos.WebApp.ModuloFuncionários.Apresentacao;
// using ControleDeMedicamentos.WebApp.ModuloEstoque.Apresentacao;



namespace ControleDeMedicamentos.WebApp.Compartilhado.Apresentacao;

public static class InjecaoDependencia
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddControllersWithViews().AddRazorOptions(options =>
        {
            options.ViewLocationFormats.Clear();

            options.ViewLocationFormats.Add("/Modulos/Modulo{1}/Apresentacao/Views/{0}.cshtml");

            options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
        });

        services.AddAutoMapper(config =>
        {
            config.AddProfile<FornecedorProfile>();
            config.AddProfile<MedicamentoProfile>();
            config.AddProfile<PacienteProfile>();

            config.AddProfile<PacienteProfile>();
            //     config.AddProfile<MedicamentosProfile>();
            //     config.AddProfile<FuncionáriosProfile>();
            //     config.AddProfile<RequisicaoEntradaProfile>();
            //     config.AddProfile<RequisicaoSaidaProfile>();

        });
    
    }
}