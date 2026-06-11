using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Apresentacao;

public class RequisicaoEntradaController : Controller
{
    private readonly ServicoRequisicaoEntrada servicoRequisicaoEntrada;
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioFuncionario repositorioFuncionario;
    private readonly IMapper mapeador;

    public RequisicaoEntradaController(
        ServicoRequisicaoEntrada servicoRequisicaoEntrada,
        IRepositorioMedicamento repositorioMedicamento,
        IRepositorioFuncionario repositorioFuncionario,
        IMapper mapeador)
    {
        this.servicoRequisicaoEntrada = servicoRequisicaoEntrada;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFuncionario = repositorioFuncionario;
        this.mapeador = mapeador;
    }

    public IActionResult Listar()
    {
        ViewBag.Titulo = "Requisições de Entrada";

        List<ListarRequisicaoEntradaDto> requisicoesDto = servicoRequisicaoEntrada.SelecionarTodos();

        List<ListarRequisicaoEntradaViewModel> requisicoesVm = mapeador
            .Map<List<ListarRequisicaoEntradaViewModel>>(requisicoesDto);

        return View(requisicoesVm);
    }

    public IActionResult Cadastrar()
    {
        ViewBag.Titulo = "Cadastrar Requisição de Entrada";

        CadastrarRequisicaoEntradaViewModel viewModel = new CadastrarRequisicaoEntradaViewModel();

        CarregarSelecoes(viewModel);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Cadastrar(CadastrarRequisicaoEntradaViewModel viewModel)
    {
        ViewBag.Titulo = "Cadastrar Requisição de Entrada";

        if (!ModelState.IsValid)
        {
            CarregarSelecoes(viewModel);
            return View(viewModel);
        }

        CadastrarRequisicaoEntradaDto dto = mapeador.Map<CadastrarRequisicaoEntradaDto>(viewModel);

        ResultadoOperacaoRequisicaoEntrada resultado = servicoRequisicaoEntrada.Cadastrar(dto);

        if (!resultado.Conseguiu)
        {
            ModelState.AddModelError(string.Empty, resultado.MensagemErro!);
            CarregarSelecoes(viewModel);
            return View(viewModel);
        }

        TempData["MensagemSucesso"] = "Requisição de entrada cadastrada com sucesso.";

        return RedirectToAction(nameof(Listar));
    }

    private void CarregarSelecoes(CadastrarRequisicaoEntradaViewModel viewModel)
    {
        viewModel.Medicamentos = repositorioMedicamento
            .SelecionarTodos()
            .Select(m => new SelectListItem(m.Nome, m.Id))
            .ToList();

        viewModel.Funcionarios = repositorioFuncionario
            .SelecionarTodos()
            .Select(f => new SelectListItem(f.Nome, f.Id))
            .ToList();
    }
}