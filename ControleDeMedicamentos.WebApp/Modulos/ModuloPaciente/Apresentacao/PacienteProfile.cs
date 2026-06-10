using System;
using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloPaciente.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloPaciente.Apresentacao;

public class PacienteProfile : Profile
{
    public PacienteProfile()
    {
        CreateMap<ListarPacienteDto, ListarPacienteViewModel>();
        CreateMap<CadastrarPacienteViewModel, CadastrarPacienteDto>();
        CreateMap<EditarPacienteViewModel, EditarPacienteDto>();

        CreateMap<DetalhesPacienteDto, EditarPacienteViewModel>();
        CreateMap<DetalhesPacienteDto, ExcluirPacienteViewModel>();
    }
}
