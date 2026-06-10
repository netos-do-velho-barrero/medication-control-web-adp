
using ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentos.WebApp.ModuloCategoria.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;


namespace ControleDeMedicamentos.WebApp.ModuloCategoria.Infra;

public class RepositorioPacienteEmArquivo : RepositorioBaseEmArquivo<Paciente>, IRepositorioPaciente
{
  public RepositorioPacienteEmArquivo(ContextoJson contexto) : base(contexto) { }

    protected override List<Paciente> CarregarRegistros()
    {
        return contexto.Paciente;
    }
}
