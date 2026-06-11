namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;

public record CadastrarMedicamentoDto(
    string Nome,
    string Descricao,
    int QuantidadeEmEstoque,
    string FornecedorId
);

public record EditarMedicamentoDto(
    string Id,
    string Nome,
    string Descricao,
    int QuantidadeEmEstoque,
    string FornecedorId
);

public record ListarMedicamentoDto(
    string Id,
    string Nome,
    string Descricao,
    int QuantidadeEmEstoque,
    string FornecedorNome,
    bool EmFalta
);

public record ExcluirMedicamentoDto(
    string Id,
    string Nome,
    string FornecedorNome
);