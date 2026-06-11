using System.Text.Json;
using System.Text.Json.Serialization;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;
// using ControleDeMedicamentos.WebApp.ModuloFornecedor.Dominio;
// using ControleDeMedicamentos.WebApp.ModuloPaciente.Dominio;
// using ControleDeMedicamentos.WebApp.ModuloMedicamento.Dominio;
// using ControleDeMedicamentos.WebApp.ModuloFuncionario.Dominio;
// using ControleDeMedicamentos.WebApp.ModuloEstoque.Dominio;

namespace ControleDeMedicamentos.WebApp.Compartilhado.Infra.Arquivos;

public sealed class ContextoJson
{
    public List<Fornecedor> Fornecedores { get; set; } = new List<Fornecedor>();

    public List<Paciente> Paciente { get; set; } = new List<Paciente>();

    public List<Medicamento> Medicamentos { get; set; } = new List<Medicamento>();

    // public List<Funcionários> Funcionários { get; set; } = new List<Funcionários>();

    // public List<RequisicaoEntrada> RequisicoesEntrada { get; set; } = new List<RequisicaoEntrada>();

    // public List<RequisicaoSaida> RequisicoesSaida { get; set; } = new List<RequisicaoSaida>();


    private readonly string caminhoArquivo;

    public ContextoJson()
    {
        string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoDiretorio = Path.Combine(caminhoAppData, "ListaDeComprasWeb");

        Directory.CreateDirectory(caminhoDiretorio);

        caminhoArquivo = Path.Combine(caminhoDiretorio, "dados.json");
    }

    public void Salvar()
    {
        JsonSerializerOptions opcoesJson = new JsonSerializerOptions();

        opcoesJson.WriteIndented = true;
        opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;

        string jsonString = JsonSerializer.Serialize(this, opcoesJson);

        File.WriteAllText(caminhoArquivo, jsonString);
    }

    public void Carregar()
    {
        if (!File.Exists(caminhoArquivo))
            return;

        string jsonString = File.ReadAllText(caminhoArquivo);

        JsonSerializerOptions opcoesJson = new JsonSerializerOptions();

        opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoJson? contextoSalvo = JsonSerializer.Deserialize<ContextoJson>(jsonString, opcoesJson);

        if (contextoSalvo == null)
            return;

        // Fornecedores = contextoSalvo.Fornecedores;
        Paciente = contextoSalvo.Paciente;
        Fornecedores = contextoSalvo.Fornecedores;
        Medicamentos = contextoSalvo.Medicamentos;
        // Funcionários = contextoSalvo.Funcionários;
        // RequisicoesEntrada = contextoSalvo.RequisicoesEntrada;
        // RequisicoesSaida = contextoSalvo.RequisicoesSaida;
    }
}
