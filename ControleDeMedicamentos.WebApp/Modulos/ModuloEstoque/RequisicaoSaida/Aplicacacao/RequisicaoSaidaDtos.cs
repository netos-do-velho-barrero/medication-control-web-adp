using System;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesSaida.Aplicacao;

public record CadastrarRequisicaoSaidaDto(
    DateTime Data,
    string MedicamentoId,
    string PacienteId,
    int Quantidade
);

public record ListarRequisicaoSaidaDto(
    string Id,
    DateTime Data,
    string MedicamentoNome,
    string PacienteNome,
    int Quantidade
);


public record EditarRequisicaoSaidaDto(
    string Id,
    DateTime Data,
    string MedicamentoId,
    string PacienteId,
    int Quantidade
);

public record ExcluirRequisicaoSaidaDto(
    string Id
);