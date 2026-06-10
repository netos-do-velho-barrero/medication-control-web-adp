using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Apresentacao;

public class FornecedorController : Controller
{
    private readonly ServicoFornecedor servicoFornecedor;
    private readonly IMapper mapeador;

    public FornecedorController(ServicoFornecedor servicoFornecedor, IMapper mapeador)
    {
        this.servicoFornecedor = servicoFornecedor;
        this.mapeador = mapeador;
    }

    public IActionResult Listar()
    {
        ViewBag.Titulo = "Fornecedores";

        List<ListarFornecedorDto> fornecedoresDto = servicoFornecedor.SelecionarTodos();

        List<ListarFornecedorViewModel> fornecedoresVm = mapeador
            .Map<List<ListarFornecedorViewModel>>(fornecedoresDto);

        return View(fornecedoresVm);
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

        CadastrarFornecedorDto dto = mapeador.Map<CadastrarFornecedorDto>(viewModel);

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

        EditarFornecedorDto? fornecedorDto = servicoFornecedor.SelecionarPorId(id);

        if (fornecedorDto == null)
            return RedirectToAction(nameof(Listar));

        EditarFornecedorViewModel viewModel = mapeador.Map<EditarFornecedorViewModel>(fornecedorDto);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Editar(EditarFornecedorViewModel viewModel)
    {
        ViewBag.Titulo = "Editar Fornecedor";

        if (!ModelState.IsValid)
            return View(viewModel);

        EditarFornecedorDto dto = mapeador.Map<EditarFornecedorDto>(viewModel);

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

        ExcluirFornecedorDto? fornecedorDto = servicoFornecedor.SelecionarParaExclusao(id);

        if (fornecedorDto == null)
            return RedirectToAction(nameof(Listar));

        ExcluirFornecedorViewModel viewModel = mapeador.Map<ExcluirFornecedorViewModel>(fornecedorDto);

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