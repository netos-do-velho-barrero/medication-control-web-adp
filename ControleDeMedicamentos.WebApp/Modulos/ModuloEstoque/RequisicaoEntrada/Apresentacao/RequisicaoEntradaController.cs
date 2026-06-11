using AutoMapper;
using System.Collections.Generic;
using System.Linq;
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

    public IActionResult Editar(string id)
    {
        ViewBag.Titulo = "Editar Requisição de Entrada";

        EditarRequisicaoEntradaDto? dto = servicoRequisicaoEntrada.SelecionarPorId(id);

        if (dto == null)
            return NotFound();

        EditarRequisicaoEntradaViewModel viewModel = mapeador.Map<EditarRequisicaoEntradaViewModel>(dto);

        CarregarSelecoes(viewModel);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Editar(EditarRequisicaoEntradaViewModel viewModel)
    {
        ViewBag.Titulo = "Editar Requisição de Entrada";

        if (!ModelState.IsValid)
        {
            CarregarSelecoes(viewModel);
            return View(viewModel);
        }

        EditarRequisicaoEntradaDto dto = mapeador.Map<EditarRequisicaoEntradaDto>(viewModel);

        ResultadoOperacaoRequisicaoEntrada resultado = servicoRequisicaoEntrada.Editar(dto);

        if (!resultado.Conseguiu)
        {
            ModelState.AddModelError(string.Empty, resultado.MensagemErro!);
            CarregarSelecoes(viewModel);
            return View(viewModel);
        }

        TempData["MensagemSucesso"] = "Requisição de entrada editada com sucesso.";

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Excluir(string id)
    {
        ViewBag.Titulo = "Excluir Requisição de Entrada";

        EditarRequisicaoEntradaDto? dto = servicoRequisicaoEntrada.SelecionarPorId(id);

        if (dto == null)
            return NotFound();

        ListarRequisicaoEntradaViewModel viewModel = mapeador.Map<ListarRequisicaoEntradaViewModel>(dto);

        return View(viewModel);
    }

    [HttpPost, ActionName("Excluir")]
    public IActionResult ExcluirConfirmado(string id)
    {
        ExcluirRequisicaoEntradaDto dto = new ExcluirRequisicaoEntradaDto(id);

        ResultadoOperacaoRequisicaoEntrada resultado = servicoRequisicaoEntrada.Excluir(dto);

        if (!resultado.Conseguiu)
        {
            TempData["MensagemErro"] = resultado.MensagemErro;
            return RedirectToAction(nameof(Listar));
        }

        TempData["MensagemSucesso"] = "Requisição de entrada excluída com sucesso (estoque ajustado).";

        return RedirectToAction(nameof(Listar));
    }

    // Método unificado usando dynamic adaptado para Medicamentos e Funcionários
    private void CarregarSelecoes(dynamic viewModel)
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