using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Apresentacao;

public class FornecedorController : Controller
{
    private readonly ServicoFornecedor servicoFornecedor;

    public FornecedorController(ServicoFornecedor servicoFornecedor)
    {
        this.servicoFornecedor = servicoFornecedor;
    }

    public IActionResult Listar()
    {
        ViewBag.Titulo = "Fornecedores";

        List<ListarFornecedorViewModel> fornecedores = servicoFornecedor
            .SelecionarTodos()
            .Select(f => new ListarFornecedorViewModel
            {
                Id = f.Id,
                Nome = f.Nome,
                Telefone = f.Telefone,
                Cnpj = f.Cnpj
            })
            .ToList();

        return View(fornecedores);
    }

    public IActionResult Cadastrar()
    {
        ViewBag.Titulo = "Cadastrar Fornecedor";

        return View(new CadastrarFornecedorViewModel());
    }

    [HttpPost]
    public IActionResult Cadastrar(CadastrarFornecedorViewModel viewModel)
    {
        ViewBag.Titulo = "Cadastrar Fornecedor";

        if (!ModelState.IsValid)
            return View(viewModel);

        CadastrarFornecedorDto dto = new CadastrarFornecedorDto(
            viewModel.Nome,
            viewModel.Telefone,
            viewModel.Cnpj
        );

        ResultadoOperacao resultado = servicoFornecedor.Cadastrar(dto);

        if (!resultado.Conseguiu)
        {
            ModelState.AddModelError(string.Empty, resultado.MensagemErro!);
            return View(viewModel);
        }

        TempData["MensagemSucesso"] = "Fornecedor cadastrado com sucesso.";

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Editar(string id)
    {
        ViewBag.Titulo = "Editar Fornecedor";

        EditarFornecedorDto? fornecedor = servicoFornecedor.SelecionarPorId(id);

        if (fornecedor == null)
            return RedirectToAction(nameof(Listar));

        EditarFornecedorViewModel viewModel = new EditarFornecedorViewModel
        {
            Id = fornecedor.Id,
            Nome = fornecedor.Nome,
            Telefone = fornecedor.Telefone,
            Cnpj = fornecedor.Cnpj
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Editar(EditarFornecedorViewModel viewModel)
    {
        ViewBag.Titulo = "Editar Fornecedor";

        if (!ModelState.IsValid)
            return View(viewModel);

        EditarFornecedorDto dto = new EditarFornecedorDto(
            viewModel.Id,
            viewModel.Nome,
            viewModel.Telefone,
            viewModel.Cnpj
        );

        ResultadoOperacao resultado = servicoFornecedor.Editar(dto);

        if (!resultado.Conseguiu)
        {
            ModelState.AddModelError(string.Empty, resultado.MensagemErro!);
            return View(viewModel);
        }

        TempData["MensagemSucesso"] = "Fornecedor editado com sucesso.";

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Excluir(string id)
    {
        ViewBag.Titulo = "Excluir Fornecedor";

        ExcluirFornecedorDto? fornecedor = servicoFornecedor.SelecionarParaExclusao(id);

        if (fornecedor == null)
            return RedirectToAction(nameof(Listar));

        ExcluirFornecedorViewModel viewModel = new ExcluirFornecedorViewModel
        {
            Id = fornecedor.Id,
            Nome = fornecedor.Nome,
            Cnpj = fornecedor.Cnpj
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult ExcluirConfirmado(string id)
    {
        ResultadoOperacao resultado = servicoFornecedor.Excluir(id);

        if (!resultado.Conseguiu)
        {
            TempData["MensagemErro"] = resultado.MensagemErro;
            return RedirectToAction(nameof(Listar));
        }

        TempData["MensagemSucesso"] = "Fornecedor excluído com sucesso.";

        return RedirectToAction(nameof(Listar));
    }
}