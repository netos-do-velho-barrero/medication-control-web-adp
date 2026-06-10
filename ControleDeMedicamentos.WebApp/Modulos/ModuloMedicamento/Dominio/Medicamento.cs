using ControleDeMedicamentos.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;

public class Medicamento : EntidadeBase<Medicamento>
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int QuantidadeEmEstoque { get; set; }
    public string FornecedorId { get; set; } = string.Empty;
    public string FornecedorNome { get; set; } = string.Empty;

    public Medicamento()
    {
    }

    public Medicamento(string nome, string descricao, int quantidadeEmEstoque, string fornecedorId, string fornecedorNome)
    {
        Nome = nome.Trim();
        Descricao = descricao.Trim();
        QuantidadeEmEstoque = quantidadeEmEstoque;
        FornecedorId = fornecedorId;
        FornecedorNome = fornecedorNome;
    }

    public bool EstaEmFalta()
    {
        return QuantidadeEmEstoque < 20;
    }

    public void AdicionarQuantidade(int quantidade)
    {
        QuantidadeEmEstoque += quantidade;
    }

    public override void Atualizar(Medicamento medicamentoAtualizado)
    {
        Nome = medicamentoAtualizado.Nome;
        Descricao = medicamentoAtualizado.Descricao;
        QuantidadeEmEstoque = medicamentoAtualizado.QuantidadeEmEstoque;
        FornecedorId = medicamentoAtualizado.FornecedorId;
        FornecedorNome = medicamentoAtualizado.FornecedorNome;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo Nome é obrigatório.");
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo Nome deve conter entre 3 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Descricao))
            erros.Add("O campo Descrição é obrigatório.");
        else if (Descricao.Length < 5 || Descricao.Length > 255)
            erros.Add("O campo Descrição deve conter entre 5 e 255 caracteres.");

        if (QuantidadeEmEstoque <= 0)
            erros.Add("O campo Quantidade em Estoque deve ser um número positivo.");

        if (string.IsNullOrWhiteSpace(FornecedorId))
            erros.Add("O campo Fornecedor é obrigatório.");

        return erros;
    }
}