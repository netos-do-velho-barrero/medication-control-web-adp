using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesSaida.Dominio;

public class RequisicaoSaida : EntidadeBase<RequisicaoSaida>
{
    public DateTime Data { get; set; }
    public string MedicamentoId { get; set; } = string.Empty;
    public string MedicamentoNome { get; set; } = string.Empty;
    public string PacienteNome { get; set; } = string.Empty;
    public string PacienteId { get; set; } = string.Empty;

    public int Quantidade { get; set; }



    public RequisicaoSaida()
    {
    }

    public RequisicaoSaida(
        DateTime data,
        string medicamentoId,
        string medicamentoNome,
        string pacienteId,
        string pacienteNome,
        int quantidade
        )
    {
        Data = data;
        MedicamentoId = medicamentoId;
        MedicamentoNome = medicamentoNome;
        PacienteId = pacienteId;
        PacienteNome = pacienteNome;
        Quantidade = quantidade;
    }


    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Data == default)
            erros.Add("O campo Data é obrigatório.");

        if (string.IsNullOrWhiteSpace(MedicamentoId))
            erros.Add("O campo Medicamento é obrigatório.");

        if (string.IsNullOrWhiteSpace(PacienteId))
            erros.Add("O campo Funcionário é obrigatório.");

        if (Quantidade <= 0)
            erros.Add("O campo Quantidade deve ser um número positivo.");

        return erros;
    }

    public override void Atualizar(RequisicaoSaida requisicaoAtualizada)
    {
        Data = requisicaoAtualizada.Data;
        MedicamentoId = requisicaoAtualizada.MedicamentoId;
        MedicamentoNome = requisicaoAtualizada.MedicamentoNome;
        PacienteId = requisicaoAtualizada.PacienteId;
        PacienteNome = requisicaoAtualizada.PacienteNome;
        Quantidade = requisicaoAtualizada.Quantidade;
    }

}