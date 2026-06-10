using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;

public interface IRepositorioFornecedor : IRepositorio<Fornecedor>
{
    Fornecedor? SelecionarPorCnpj(string cnpj);
}