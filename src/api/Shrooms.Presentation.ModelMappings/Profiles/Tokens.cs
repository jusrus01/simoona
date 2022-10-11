using AutoMapper;
using Shrooms.Contracts.DataTransferObjects.Models.Tokens;
using Shrooms.Presentation.WebViewModels.Models.Tokens;

namespace Shrooms.Presentation.ModelMappings.Profiles
{
    public class Tokens : Profile
    {
        public Tokens()
        {
            CreateDtoToViewModelMappings();
            CreateViewModelToDtoMappings();
        }

        public void CreateDtoToViewModelMappings()
        {
            CreateMap<TokenResponseDto, TokenResponseViewModel>();
        }

        public void CreateViewModelToDtoMappings()
        {
            CreateMap<TokenRequestViewModel, TokenRequestDto>();
        }
    }
}
