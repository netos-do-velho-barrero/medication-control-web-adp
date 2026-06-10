namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Aplicacao;

public record CadastrarRequisicaoEntradaDto(
    DateTime Data,
    string MedicamentoId,
    string FuncionarioId,
    int Quantidade
);

public record ListarRequisicaoEntradaDto(
    string Id,
    DateTime Data,
    string MedicamentoNome,
    string FuncionarioNome,
    int Quantidade
);