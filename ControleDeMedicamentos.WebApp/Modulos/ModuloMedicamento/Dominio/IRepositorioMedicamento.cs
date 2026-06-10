using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;

public interface IRepositorioMedicamento : IRepositorio<Medicamento>
{
    Medicamento? SelecionarPorNome(string nome);
}