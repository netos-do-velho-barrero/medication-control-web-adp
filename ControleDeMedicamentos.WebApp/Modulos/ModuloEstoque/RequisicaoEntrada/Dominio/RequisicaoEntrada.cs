using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicaoEntrada.Dominio;

public class RequisicaoEntrada : EntidadeBase<RequisicaoEntrada>
{
    public DateTime Data { get; set; }

    public string MedicamentoId { get; set; } = string.Empty;

    public string MedicamentoNome { get; set; } = string.Empty;

    public string FuncionarioId { get; set; } = string.Empty;

    public string FuncionarioNome { get; set; } = string.Empty;

    public int Quantidade { get; set; }

    public RequisicaoEntrada()
    {
    }

    public RequisicaoEntrada(
        DateTime data,
        string medicamentoId,
        string medicamentoNome,
        string funcionarioId,
        string funcionarioNome,
        int quantidade)
    {
        Data = data;
        MedicamentoId = medicamentoId;
        MedicamentoNome = medicamentoNome;
        FuncionarioId = funcionarioId;
        FuncionarioNome = funcionarioNome;
        Quantidade = quantidade;
    }

    public override void Atualizar(RequisicaoEntrada requisicaoAtualizada)
    {
        Data = requisicaoAtualizada.Data;
        MedicamentoId = requisicaoAtualizada.MedicamentoId;
        MedicamentoNome = requisicaoAtualizada.MedicamentoNome;
        FuncionarioId = requisicaoAtualizada.FuncionarioId;
        FuncionarioNome = requisicaoAtualizada.FuncionarioNome;
        Quantidade = requisicaoAtualizada.Quantidade;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Data == default)
            erros.Add("O campo Data é obrigatório.");

        if (string.IsNullOrWhiteSpace(MedicamentoId))
            erros.Add("O campo Medicamento é obrigatório.");

        if (string.IsNullOrWhiteSpace(FuncionarioId))
            erros.Add("O campo Funcionário é obrigatório.");

        if (Quantidade <= 0)
            erros.Add("O campo Quantidade deve ser um número positivo.");

        return erros;
    }
}