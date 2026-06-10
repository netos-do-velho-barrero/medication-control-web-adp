using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFornecedor.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloFornecedor.Apresentacao;

public class FornecedorProfile : Profile
{
    public FornecedorProfile()
    {
        CreateMap<CadastrarFornecedorViewModel, CadastrarFornecedorDto>();

        CreateMap<EditarFornecedorViewModel, EditarFornecedorDto>();
        CreateMap<EditarFornecedorDto, EditarFornecedorViewModel>();

        CreateMap<ListarFornecedorDto, ListarFornecedorViewModel>();

        CreateMap<ExcluirFornecedorDto, ExcluirFornecedorViewModel>();
    }
}