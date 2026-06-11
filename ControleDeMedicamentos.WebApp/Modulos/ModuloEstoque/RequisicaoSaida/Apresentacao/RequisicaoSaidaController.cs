using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesSaida.Aplicacao;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesSaida.Apresentacao;

public class RequisicaoSaidaController : Controller
{
    private readonly ServicoRequisicaoSaida servicoRequisicaoSaida;
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioPaciente repositorioPaciente;
    private readonly IMapper mapeador;

    public RequisicaoSaidaController(
        ServicoRequisicaoSaida servicoRequisicaoSaida,
        IRepositorioMedicamento repositorioMedicamento,
        IRepositorioPaciente repositorioPaciente,
        IMapper mapeador)
    {
        this.servicoRequisicaoSaida = servicoRequisicaoSaida;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioPaciente = repositorioPaciente;
        this.mapeador = mapeador;
    }

    public IActionResult Listar()
    {
        ViewBag.Titulo = "Requisições de Saída";

        List<ListarRequisicaoSaidaDto> requisicoesDto = servicoRequisicaoSaida.SelecionarTodos();

        List<ListarRequisicaoSaidaViewModel> requisicoesVm = mapeador
            .Map<List<ListarRequisicaoSaidaViewModel>>(requisicoesDto);

        return View(requisicoesVm);
    }

    public IActionResult Cadastrar()
    {
        ViewBag.Titulo = "Cadastrar Requisição de Saída";

        CadastrarRequisicaoSaidaViewModel viewModel = new CadastrarRequisicaoSaidaViewModel();

        CarregarSelecoes(viewModel);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Cadastrar(CadastrarRequisicaoSaidaViewModel viewModel)
    {
        ViewBag.Titulo = "Cadastrar Requisição de Saída";

        if (!ModelState.IsValid)
        {
            CarregarSelecoes(viewModel);
            return View(viewModel);
        }

        CadastrarRequisicaoSaidaDto dto = mapeador.Map<CadastrarRequisicaoSaidaDto>(viewModel);

        ResultadoOperacaoRequisicaoSaida resultado = servicoRequisicaoSaida.Cadastrar(dto);

        if (!resultado.Conseguiu)
        {
            ModelState.AddModelError(string.Empty, resultado.MensagemErro!);
            CarregarSelecoes(viewModel);
            return View(viewModel);
        }

        TempData["MensagemSucesso"] = "Requisição de saída cadastrada com sucesso.";

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Editar(string id)
    {
        ViewBag.Titulo = "Editar Requisição de Saída";

        EditarRequisicaoSaidaDto? dto = servicoRequisicaoSaida.SelecionarPorId(id);

        if (dto == null)
            return NotFound();

        EditarRequisicaoSaidaViewModel viewModel = mapeador.Map<EditarRequisicaoSaidaViewModel>(dto);

        CarregarSelecoes(viewModel);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Editar(EditarRequisicaoSaidaViewModel viewModel)
    {
        ViewBag.Titulo = "Editar Requisição de Saída";

        if (!ModelState.IsValid)
        {
            CarregarSelecoes(viewModel);
            return View(viewModel);
        }

        EditarRequisicaoSaidaDto dto = mapeador.Map<EditarRequisicaoSaidaDto>(viewModel);

        ResultadoOperacaoRequisicaoSaida resultado = servicoRequisicaoSaida.Editar(dto);

        if (!resultado.Conseguiu)
        {
            ModelState.AddModelError(string.Empty, resultado.MensagemErro!);
            CarregarSelecoes(viewModel);
            return View(viewModel);
        }

        TempData["MensagemSucesso"] = "Requisição de saída editada com sucesso.";

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Excluir(string id)
    {
        ViewBag.Titulo = "Excluir Requisição de Saída";

        EditarRequisicaoSaidaDto? dto = servicoRequisicaoSaida.SelecionarPorId(id);

        if (dto == null)
            return NotFound();

        ListarRequisicaoSaidaViewModel viewModel = mapeador.Map<ListarRequisicaoSaidaViewModel>(dto);

        return View(viewModel);
    }

    [HttpPost, ActionName("Excluir")]
    public IActionResult ExcluirConfirmado(string id)
    {
        ExcluirRequisicaoSaidaDto dto = new ExcluirRequisicaoSaidaDto(id);

        ResultadoOperacaoRequisicaoSaida resultado = servicoRequisicaoSaida.Excluir(dto);

        if (!resultado.Conseguiu)
        {
            TempData["MensagemErro"] = resultado.MensagemErro;
            return RedirectToAction(nameof(Listar));
        }

        TempData["MensagemSucesso"] = "Requisição de saída excluída com sucesso (estoque devolvido).";

        return RedirectToAction(nameof(Listar));
    }

   
    private void CarregarSelecoes(dynamic viewModel)
    {
        viewModel.Medicamentos = repositorioMedicamento
            .SelecionarTodos()
            .Select(m => new SelectListItem(m.Nome, m.Id))
            .ToList();

        viewModel.Pacientes = repositorioPaciente
            .SelecionarTodos()
            .Select(p => new SelectListItem(p.Nome, p.Id))
            .ToList();
    }
}