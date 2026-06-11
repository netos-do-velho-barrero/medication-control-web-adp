using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao;

public class CadastrarMedicamentoViewModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo Nome deve conter entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    [StringLength(255, MinimumLength = 5, ErrorMessage = "O campo Descrição deve conter entre 5 e 255 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Quantidade em Estoque é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O campo Quantidade em Estoque deve ser um número positivo.")]
    public int QuantidadeEmEstoque { get; set; }

    [Required(ErrorMessage = "O campo Fornecedor é obrigatório.")]
    public string FornecedorId { get; set; } = string.Empty;

    public List<SelectListItem> Fornecedores { get; set; } = new List<SelectListItem>();
}

public class EditarMedicamentoViewModel : CadastrarMedicamentoViewModel
{
    public string Id { get; set; } = string.Empty;
}

public class ListarMedicamentoViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int QuantidadeEmEstoque { get; set; }
    public string FornecedorNome { get; set; } = string.Empty;
    public bool EmFalta { get; set; }
}

public class ExcluirMedicamentoViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string FornecedorNome { get; set; } = string.Empty;
}