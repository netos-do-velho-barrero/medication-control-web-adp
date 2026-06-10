namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;

public record CadastrarFornecedorDto(string Nome, string Telefone, string Cnpj);

public record EditarFornecedorDto(string Id, string Nome, string Telefone, string Cnpj);

public record ListarFornecedorDto(string Id, string Nome, string Telefone, string Cnpj);

public record ExcluirFornecedorDto(string Id, string Nome, string Cnpj);