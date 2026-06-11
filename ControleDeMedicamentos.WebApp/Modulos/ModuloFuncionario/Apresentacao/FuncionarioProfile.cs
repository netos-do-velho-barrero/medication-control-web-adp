using System;
using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloFuncionario.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloFuncionario.Apresentacao;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<ListarFuncionarioDto, ListarFuncionarioViewModel>();
        CreateMap<CadastrarFuncionarioViewModel, CadastrarFuncionarioDto>();
        CreateMap<EditarFuncionarioViewModel, EditarFuncionarioDto>();

        CreateMap<DetalhesFuncionarioDto, EditarFuncionarioViewModel>();
        CreateMap<DetalhesFuncionarioDto, ExcluirFuncionarioViewModel>();
    }
}