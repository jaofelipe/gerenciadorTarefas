using AutoMapper;
using GerenciadorTarefas.Models;
using GerenciadorTarefas.ViewModels;

namespace GerenciadorTarefas.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Tarefa, RetornoTarefaViewModel>().ReverseMap();


           

        }
    }
}
