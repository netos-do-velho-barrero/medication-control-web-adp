using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloEstoque.RequisicoesEntrada.Apresentacao;

public class RequisicaoEntradaProfile : Profile
{
    public RequisicaoEntradaProfile()
    {
        CreateMap<CadastrarRequisicaoEntradaViewModel, CadastrarRequisicaoEntradaDto>();

        CreateMap<ListarRequisicaoEntradaDto, ListarRequisicaoEntradaViewModel>();
    }
}