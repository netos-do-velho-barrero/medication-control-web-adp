using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao;

public class MedicamentoController : Controller
{
    private readonly ServicoMedicamento servicoMedicamento;
    private readonly IRepositorioFornecedor repositorioFornecedor;
    private readonly IMapper mapeador;

    public MedicamentoController(
        ServicoMedicamento servicoMedicamento,
        IRepositorioFornecedor repositorioFornecedor,
        IMapper mapeador)
    {
        this.servicoMedicamento = servicoMedicamento;
        this.repositorioFornecedor = repositorioFornecedor;
        this.mapeador = mapeador;
    }

    public IActionResult Listar()
    {
        ViewBag.Titulo = "Medicamentos";

        List<ListarMedicamentoDto> medicamentosDto = servicoMedicamento.SelecionarTodos();

        List<ListarMedicamentoViewModel> medicamentosVm = mapeador
            .Map<List<ListarMedicamentoViewModel>>(medicamentosDto);

        return View(medicamentosVm);
    }

    public IActionResult Cadastrar()
    {
        ViewBag.Titulo = "Cadastrar Medicamento";

        CadastrarMedicamentoViewModel viewModel = new CadastrarMedicamentoViewModel();

        CarregarFornecedores(viewModel);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Cadastrar(CadastrarMedicamentoViewModel viewModel)
    {
        ViewBag.Titulo = "Cadastrar Medicamento";

        if (!ModelState.IsValid)
        {
            CarregarFornecedores(viewModel);
            return View(viewModel);
        }

        CadastrarMedicamentoDto dto = mapeador.Map<CadastrarMedicamentoDto>(viewModel);

        ResultadoOperacaoMedicamento resultado = servicoMedicamento.Cadastrar(dto);

        if (!resultado.Conseguiu)
        {
            ModelState.AddModelError(string.Empty, resultado.MensagemErro!);
            CarregarFornecedores(viewModel);
            return View(viewModel);
        }

        TempData["MensagemSucesso"] = "Medicamento cadastrado com sucesso.";

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Editar(string id)
    {
        ViewBag.Titulo = "Editar Medicamento";

        EditarMedicamentoDto? medicamentoDto = servicoMedicamento.SelecionarPorId(id);

        if (medicamentoDto == null)
            return RedirectToAction(nameof(Listar));

        EditarMedicamentoViewModel viewModel = mapeador.Map<EditarMedicamentoViewModel>(medicamentoDto);

        CarregarFornecedores(viewModel);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Editar(EditarMedicamentoViewModel viewModel)
    {
        ViewBag.Titulo = "Editar Medicamento";

        if (!ModelState.IsValid)
        {
            CarregarFornecedores(viewModel);
            return View(viewModel);
        }

        EditarMedicamentoDto dto = mapeador.Map<EditarMedicamentoDto>(viewModel);

        ResultadoOperacaoMedicamento resultado = servicoMedicamento.Editar(dto);

        if (!resultado.Conseguiu)
        {
            ModelState.AddModelError(string.Empty, resultado.MensagemErro!);
            CarregarFornecedores(viewModel);
            return View(viewModel);
        }

        TempData["MensagemSucesso"] = "Medicamento editado com sucesso.";

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Excluir(string id)
    {
        ViewBag.Titulo = "Excluir Medicamento";

        ExcluirMedicamentoDto? medicamentoDto = servicoMedicamento.SelecionarParaExclusao(id);

        if (medicamentoDto == null)
            return RedirectToAction(nameof(Listar));

        ExcluirMedicamentoViewModel viewModel = mapeador.Map<ExcluirMedicamentoViewModel>(medicamentoDto);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult ExcluirConfirmado(string id)
    {
        ResultadoOperacaoMedicamento resultado = servicoMedicamento.Excluir(id);

        if (!resultado.Conseguiu)
        {
            TempData["MensagemErro"] = resultado.MensagemErro;
            return RedirectToAction(nameof(Listar));
        }

        TempData["MensagemSucesso"] = "Medicamento excluído com sucesso.";

        return RedirectToAction(nameof(Listar));
    }

    private void CarregarFornecedores(CadastrarMedicamentoViewModel viewModel)
    {
        viewModel.Fornecedores = repositorioFornecedor
            .SelecionarTodos()
            .Select(f => new SelectListItem(f.Nome, f.Id))
            .ToList();
    }
}