using AutoMapper;
using ControleDeMedicamentos.WebApp.ModuloMedicamento.Aplicacao;

namespace ControleDeMedicamentos.WebApp.ModuloMedicamento.Apresentacao;

public class MedicamentoProfile : Profile
{
    public MedicamentoProfile()
    {
        CreateMap<CadastrarMedicamentoViewModel, CadastrarMedicamentoDto>();

        CreateMap<EditarMedicamentoViewModel, EditarMedicamentoDto>();
        CreateMap<EditarMedicamentoDto, EditarMedicamentoViewModel>();

        CreateMap<ListarMedicamentoDto, ListarMedicamentoViewModel>();

        CreateMap<ExcluirMedicamentoDto, ExcluirMedicamentoViewModel>();
    }
}