using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicaoEntrada.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicaoEntrada.Apresentacao;

public class RequisicaoEntradaProfile : Profile
{
    public RequisicaoEntradaProfile()
    {
        CreateMap<CadastrarRequisicaoEntradaViewModel, CadastrarRequisicaoEntradaDto>();

        CreateMap<ListarRequisicaoEntradaDto, ListarRequisicaoEntradaViewModel>();
    }
}